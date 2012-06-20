using System;
using System.Threading.Tasks;
using SharpMod.Helper;
using SharpMod.Database;

namespace SharpMod.Commands
{
	[CommandInfo(CommandString = "smod_bans", CommandType = CommandType.Both,
		MinimumArguments = 2,
		HelpString = " - lists all bans")]
	public class ListBans : Command
	{
		public ListBans(string[] arguments)
			: base(arguments)
		{
		}

		public ListBans()
		: this(new string[] { "smod_bans" })
		{
		}

		private BanInfo[] biList = null;

		public override void Execute(Player player)
		{
			int userid = Player.GetUserID(player);

			if (player != null && (!player.Privileges.HasPrivilege("ban") | !player.Privileges.HasPrivilege("unban"))) {
				WriteLine(player, "You have no ban privileges");
				return;
			}

			SharpMod.Database.GetAllBans((Exception exception, BanInfo[] list) => {
				if (exception != null) {
					OnFailure(userid, exception);
				} else {
					biList = list;
					OnSuccess(userid);
				}
			});
		}

		protected override void OnSuccess(Player player)
		{
			WriteLine(player, "Listing bans from {0} to {1}", 1, biList.Length);
			foreach (BanInfo bi in biList) {
				WriteLine(player, "{0}", bi.Player.AuthId);
			}
		}
	}
}

