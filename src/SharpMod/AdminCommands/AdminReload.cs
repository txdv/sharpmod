using System;
using System.Threading.Tasks;
using SharpMod.Helper;
using SharpMod.Database;

namespace SharpMod
{
	[CommandInfo(CommandString = "smod_reloadadmins", CommandType = CommandType.Both,
		MinimumArguments = 1,
		HelpString = "- reloads the admins")]
	public class AdminReload : Command
	{
		public AdminReload(string[] arguments)
			: base(arguments)
		{
		}

		public AdminReload()
			: this(new string[] { "smod_reload" })
		{
		}

		public override void Execute(Player player)
		{
			if (player != null && !player.Privileges.HasPrivileges) {
				WriteLine(player, "You have to have at least on privilege to use this command");
				return;
			}

			WriteLine(player, "Reloading all admin privileges");
			Player.ReloadAllPrivileges();
		}
	}
}

