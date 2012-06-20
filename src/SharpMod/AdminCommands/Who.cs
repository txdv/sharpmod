using System;
using System.Linq;
using System.Threading.Tasks;
using SharpMod.Helper;
using SharpMod.Database;

namespace SharpMod.Commands
{
	[CommandInfo(CommandString = "smod_who", CommandType = CommandType.Both,
		MinimumArguments = 1,
		HelpString = "- shows the active player list with the according privileges")]
	public class Who : Command
	{
		private static Player playerInstance = null;
		private static TextTools.TextTable table = new TextTools.TextTable(new string[] { "# ", "nick", "authid", "userid", "privileges" });

		static Who()
		{
			table.Header[0].Alignment = TextTools.Align.Right;
			table.Header[3].Alignment = TextTools.Align.Right;
		}

		private static void write(string text)
		{
			Write(playerInstance, text);
		}

		public Who(string[] arguments)
			: base(arguments)
		{
		}

		public Who()
			: this(new string[] { "smod_who" })
		{
		}

		public override void Execute(Player player)
		{
			if (player != null && !player.Privileges.HasPrivilege("status")) {
				WriteLine(player, "You have no status privileges");
				return;
			}

			var data = from p in Player.Players
			select new string [] {
				p.Index.ToString(),
				p.Name,
				p.AuthID,
				string.Format("#{0}", p.UserID),
				p.Privileges.PrivilegesString
			};

			// TODO: do something about this hack
			playerInstance = player;
			table.Render(data.ToArray(), write, Console.WindowWidth);
		}
	}
}

