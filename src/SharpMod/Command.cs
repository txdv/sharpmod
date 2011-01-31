//
//     This file is part of sharpmod.
//     sharpmod is a metamod plugin which enables you to write plugins
//     for Valve GoldSrc using .NET programms.
// 
//     Copyright (C) 2010  Andrius Bentkus
// 
//     csharpmod is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     csharpmod is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with csharpmod.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Text;
using System.Collections.Generic;
using SharpMod.Commands;
using SharpMod.MetaMod;
using SharpMod.Helper;

namespace SharpMod
{
  public class CommandInformation
  {
    public int MinimumArguments { get; set; }

    public int MaximumArguments { get; set; }

    public string CommandString { get; set; }

    public Type Type { get; protected set; }

    public string HelpString { get; set; }

    public bool ValidArgumentCount(string[] args)
    {
      return (MinimumArguments <= args.Length) && (args.Length <= MaximumArguments);
    }

    public Command GetInstance(string[] args)
    {
      return (Command)Activator.CreateInstance(Type, new object[] { args });
    }

    public CommandInformation(Type type)
    {
      if (!type.IsSubclassOf(typeof(Command))) {
        throw new ArgumentException("Argument is not of subclass of Command");
      }
      Type = type;
      HelpString = string.Empty;
    }
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
      if (Arguments == null) return string.Empty;

      StringBuilder sb = new StringBuilder(Arguments[0]);
      for (int i = 1; i < Arguments.Length; i++)
        sb.Append(String.Format(" \"{0}\"", Arguments[i]));
      return sb.ToString();
    }

    public virtual void Execute(Player player)
    {
      Server.LogDeveloper("Command executed: {0}", Arguments.Join(' '));
      OnFailure();
    }

    protected virtual void OnSuccess()
    {
      Server.LogDeveloper("Command successful: {0}", Arguments.Join(' '));
    }

    protected virtual void OnFailure()
    {
      Server.LogDeveloper("Command failed: {0}", Arguments.Join(' '));
    }

    protected void Write(Player player, string format, params object[] param)
    {
      if (player == null) {
        Server.Print(format, param);
      } else {
        player.PrintConsole(format, param);
      }
    }

    protected void WriteLine(Player player, string format, params object[] param)
    {
      if (player == null) {
        Server.Print(format, param);
      } else {
        player.PrintConsole(format + "\n", param);
      }
    }
  }

  public delegate void ClientCommandDelegate(Player player, Command cmd);

  /// <summary>
  /// A class for managing all the command associations
  /// </summary>
  public class CommandManager
  {
    private static Dictionary<string, ClientCommandDelegate> events = new Dictionary<string, ClientCommandDelegate>();
    private static List<CommandInformation> commandInformationList = new List<CommandInformation>();

    static CommandManager()
    {
      RegisterCommand(new CommandInformation(typeof(SayCommand)) {
        CommandString = "say",
        MinimumArguments = 2,
        MaximumArguments = 2,
        HelpString = "<text> - will enter a message in the global chat"
      });

      RegisterCommand(new CommandInformation(typeof(SayCommand)) {
        CommandString = "say_team",
        MinimumArguments = 2,
        MaximumArguments = 2,
        HelpString = "<text> - will enter a message in the team chat"
      });

      RegisterCommand(new CommandInformation(typeof(Kick)) {
        CommandString = "smod_kick",
        MinimumArguments = 2,
        MaximumArguments = 3,
        HelpString = "<target> [reason] - kicks a target by partial steamid, nick or ip with the reason"
      });

      RegisterCommand(new CommandInformation(typeof(Ban)) {
        CommandString = "smod_ban",
        MinimumArguments = 3,
        MaximumArguments = 4,
        HelpString = "<target> <duration> [reason] - bans a target by partial steamid, nick or ip with optional reason for duration"
      });

      RegisterCommand(new CommandInformation(typeof(Who)) {
        CommandString = "smod_who",
        MinimumArguments = 1,
        MaximumArguments = 1,
        HelpString = "- shows the active player list with the according privileges"
      });

      RegisterCommand(new CommandInformation(typeof(AdminReload)) {
        CommandString = "smod_reloadadmins",
        MinimumArguments = 1,
        MaximumArguments = 1,
        HelpString = "- reloads the admins"
      });
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
        if (cmddelegate != null) cmddelegate += handler;
        else cmddelegate = handler;
      }
      else events.Add(str, handler);
    }

    public static void RegisterCommand(CommandInformation commandInformation) {
      commandInformationList.Add(commandInformation);
    }

    public static Command CreateCommand(string[] arguments) {
      // special case, if the arguments string is empty
      if (arguments.Length < 1) {
        return new Command(arguments);
      }

      foreach (CommandInformation ci in commandInformationList) {
        if (arguments[0] == ci.CommandString) {
          return (Command)Activator.CreateInstance(ci.Type, new object[] { arguments });
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
    internal static Command CreateCommandFromGameEngine()
    {
      return CreateCommand(Command.EngineArguments);
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
          if (keyval.Value != null) keyval.Value(player, cmd);
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

  public class KickCommand : Command
  {
    public string Target {
      get {
        return Arguments[1];
      }
    }

    public string Reason {
      get {
        if (Arguments.Length < 2) return string.Empty;
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
