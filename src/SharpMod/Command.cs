using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using SharpMod.Commands;
using SharpMod.MetaMod;
using SharpMod.Helper;

namespace SharpMod
{
	public enum CommandType
	{
		Player = (1 << 0),
		Server = (1 << 1),
		Both   = (Player | Server)
	}

	public class CommandInfo : System.Attribute
	{
		public int MinimumArguments { get; set; }

		public int MaximumArguments { get; set; }

		public string CommandString { get; set; }

		public Type Type { get; protected set; }

		public string HelpString { get; set; }

		public CommandType CommandType { get; set; }
	}

	/// <summary>
	/// A class for handling commands send by the players
	/// </summary>
	public class Command
	{
		internal static bool overrideArguments = false;
		internal static Command instance = null;

		/// <summary>
		/// Calls the GoldSrc engine (Cmd_Argc, Cmd_Argv) and returns the values as a C# array
		/// </summary>
		public static string[] EngineArguments {
			get {
				string[] args = new string[MetaModEngine.engineFunctions.Cmd_Argc()];
				for (int i = 0; i < args.Length; i++) {
					args[i] = Mono.Unix.UnixMarshal.PtrToString(MetaModEngine.engineFunctions.Cmd_Argv(i));
				}
				return args;
			}
		}

		public virtual bool Success { get; protected set; }

		public virtual bool Override { get; protected set; }

		public virtual bool Valid { get; protected set; }

		/// <summary>
		/// The arguments of the specific Command instance
		/// </summary>
		public string[] Arguments { set; get; }

		private string Escape(string text)
		{
			if (text.Contains(" ")) {
				return String.Format("\"{0}\"", text);
			} else {
				return text;
			}
		}

		/// <summary>
		/// Constructor of the class
		/// </summary>
		/// <param name="arguments">
		/// A managed array with the arguments <see cref="System.String[]"/>
		/// </param>
		public Command(string[] arguments)
		{
			Arguments = arguments;
			Success = false;
			Override = false;
			Valid = false;
		}

		/// <summary>
		/// Emulates a player calling a command.
		/// </summary>
		/// <param name="p">
		/// Instance of the player <see cref="Player"/>
		/// </param>
		public void FakeCall(Player p)
		{
			overrideArguments = true;
			instance = this;
			MetaModEngine.dllapiFunctions.ClientCommand(p.Pointer);
			instance = null;
			overrideArguments = false;
		}

		/// <summary>
		/// A string representation of the class.
		/// Returns a online command string, which could be written into the console.
		/// </summary>
		/// <returns>
		/// <see cref="System.String"/>
		/// </returns>
		public override string ToString()
		{
			if (Arguments == null) {
				return string.Empty;
			}

			StringBuilder sb = new StringBuilder(Arguments[0]);
			for (int i = 1; i < Arguments.Length; i++) {
				sb.Append(String.Format(" \"{0}\"", Arguments[i]));
			}
			return sb.ToString();
		}

		public virtual void Execute(Player player)
		{
			Server.LogDeveloper("Command executed: {0}", Arguments.Join(' '));
			OnFailure(player.UserID);
		}

		protected void OnSuccess(int userid)
		{
			OnSuccess(Player.FindByUserId(userid));
		}
		protected virtual void OnSuccess(Player player)
		{
			Server.LogDeveloper("Command successful: {0}", Arguments.Join(' '));
		}

		protected virtual void OnFailure(int userid)
		{
			OnFailure(Player.FindByUserId(userid));
		}
		protected virtual void OnFailure(int userid, string errorMessage)
		{
			Player player = Player.FindByUserId(userid);
			WriteLine(player, errorMessage);
			OnFailure(player);
		}
		protected virtual void OnFailure(int userid, Exception exception)
		{
			Player player = Player.FindByUserId(userid);
			WriteLine(player, exception.Message);
			WriteLine(player, exception.StackTrace);
			OnFailure(player);
		}
		protected virtual void OnFailure(Player player)
		{
			Server.LogDeveloper("Command failed: {0}", Arguments.Join(' '));
		}

		protected static void Write(Player player, string text)
		{
			if (player == null) {
				Server.Print(text);
			} else {
				player.PrintConsole(text);
			}
		}

		protected static void Write(Player player, string format, params object[] param)
		{
			Write(player, string.Format(format, param));
		}

		protected static void WriteLine(Player player, string text)
		{
			Write(player, text + '\n');
		}

		protected static void WriteLine(Player player, string format, params object[] param)
		{
			WriteLine(player, string.Format(format, param));
		}
	}

	public delegate void ClientCommandDelegate(Player player, Command cmd);

	/// <summary>
	/// A class for managing all the command associations
	/// </summary>
	public class CommandManager
	{
		private static Dictionary<string, ClientCommandDelegate> events = new Dictionary<string, ClientCommandDelegate>();
		private static Dictionary<string, Tuple<CommandInfo, Type>> cmdInfo = new Dictionary<string, Tuple<CommandInfo, Type>>();

		static CommandManager()
		{
			// build in server commands
			RegisterCommand(typeof(SayCommand));
			RegisterCommand(typeof(SayTeamCommand));
			RegisterCommand(typeof(KickCommand));

			// admin commands
			RegisterCommand(typeof(Kick));
			RegisterCommand(typeof(Ban));
			RegisterCommand(typeof(ListBans));
			RegisterCommand(typeof(Who));
			RegisterCommand(typeof(AdminReload));
			RegisterCommand(typeof(ChangeMap));
			RegisterCommand(typeof(ListMaps));
		}

		/// <summary>
		/// Registers a ClientCommand handler to a specific command
		/// </summary>
		/// <param name="str">
		/// The beginning of the string to witch it has to match (for example "say /death") <see cref="System.String"/>
		/// </param>
		/// <param name="handler">
		/// A handler which to invoke if command occures <see cref="ClientCommandDelegate"/>
		/// </param>
		public static void RegisterCommandHandler(string str, ClientCommandDelegate handler)
		{
			if (events.ContainsKey(str)) {
				ClientCommandDelegate cmddelegate = events[str];
				if (cmddelegate != null) {
					cmddelegate += handler;
				} else {
					cmddelegate = handler;
				}
			} else {
				events.Add(str, handler);
			}
		}

		public static void RegisterCommand(Type type)
		{
			object[] objs = type.GetCustomAttributes(typeof(CommandInfo), false);
			if (objs.Length == 0) {
				throw new ArgumentException("Argument has to have the attribute CommandInfo set");
			}

			CommandInfo info = objs[0] as CommandInfo;

			cmdInfo[info.CommandString] = Tuple.Create(info, type);

			Server.RegisterCommand(info.CommandString, ServerCommandHandler);
		}

		private static void ServerCommandHandler(string[] arguments)
		{
			Command cmd = CreateCommand(CommandType.Server, arguments);
			cmd.Execute(null);
		}

		public static Command CreateCommand(CommandType type, string[] arguments)
		{
			// special case, if the arguments string is empty
			if (arguments.Length < 1) {
				return new Command(arguments);
			}

			if (cmdInfo.ContainsKey(arguments[0])) {
				var combinedInfo = cmdInfo[arguments[0]];
				CommandInfo info = combinedInfo.Item1;
				if ((info.CommandType & type) > 0) {
					return (Command)Activator.CreateInstance(combinedInfo.Item2, new object[] { arguments });
				}
			}

			return new Command(arguments);
		}

		/// <summary>
		/// Creates a command class instance using the gameengine command calls
		/// </summary>
		/// <returns>
		/// A command class instance with the original gameengine commadn values <see cref="Command"/>
		/// </returns>
		internal static Command CreateCommandFromGameEngine(CommandType type)
		{
			return CreateCommand(type, Command.EngineArguments);
		}

		/// <summary>
		/// This function is used internally.
		/// It executes all needed handlers when a specific command occures.
		/// </summary>
		/// <param name="str">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="cmd">
		/// A <see cref="Command"/>
		/// </param>
		internal static void Execute(Player player, Command cmd)
		{
			foreach (KeyValuePair<string, ClientCommandDelegate> keyval in events) {
				if (cmd.ToString().StartsWith(keyval.Key)) {
					if (keyval.Value != null) {
						keyval.Value(player, cmd);
					}
				}
			}

			cmd.Execute(player);
		}
	}

	/// <summary>
	/// A special class for handling SayCommand (say, say_team)
	/// </summary>
	public abstract class TextCommand : Command
	{
		public TextCommand(string[] arguments)
			: base(arguments)
		{
		}

	/// <summary>
	/// A special Field for getting and setting the text
	/// </summary>
		public string Text
		{
			get { return Arguments[1]; }
			set { Arguments[1] = value; }
		}
	}

	/// <summary>
	/// The say command
	/// </summary>
	[CommandInfo(CommandString = "say",
	    MinimumArguments = 2, MaximumArguments = 2,
	    HelpString = "<text> - will enter a message in the global chat")]
	public class SayCommand : TextCommand
	{
		public SayCommand(string[] arguments)
			: base(arguments)
		{
		}

		public SayCommand(string message)
			: base(new string[] { "say", message })
		{
		}
	}

	[CommandInfo(CommandString = "say",
		MinimumArguments = 2, MaximumArguments = 2,
	    HelpString = "<text> - will enter a message in the team chat")]
	public class SayTeamCommand : TextCommand
	{
		public SayTeamCommand(string[] arguments)
			: base(arguments)
		{
		}

		public SayTeamCommand(string message)
			: base(new string[] { "say_team", message })
		{
		}
	}

	[CommandInfo(CommandString = "kick", CommandType = CommandType.Both,
		MinimumArguments = 2, MaximumArguments = -1,
		HelpString = "<text> - will enter a message in the global chat")]
	public class KickCommand : Command
	{
		public string Target {
			get {
				return Arguments[1];
			}
		}

		public string Reason {
			get {
				if (Arguments.Length < 2) {
					return string.Empty;
				}
			return Arguments[2];
			}
		}

		public KickCommand(string[] arguments)
			: base(arguments)
		{
		}

		public KickCommand(string target)
			: this(new string[] { "kick", target })
		{
		}

		public KickCommand(string target, string reason)
			: this(new string[] { "kick", target, reason })
		{
		}

		public KickCommand(Player player)
			: this(player.Name)
		{
		}

		public KickCommand(Player player, string reason)
			: this(player.Name, reason)
		{
		}
	}
}
