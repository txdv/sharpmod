using System;
using System.Threading.Tasks;
using SharpMod.Helper;
using SharpMod.Database;

namespace SharpMod
{
	[CommandInfo(CommandString = "smod_maps", CommandType = CommandType.Both,
	MinimumArguments = 1, MaximumArguments = 1,
	HelpString = "- lists all available maps")]
	public class ListMaps : Command
	{
		public ListMaps(string[] arguments)
			: base(arguments)
		{
		}

		public ListMaps()
			: this(new string[] { "smod_maps" })
		{
		}

		public override void Execute(Player player)
		{
			int userid = Player.GetUserID(player);

			if (player != null && !player.Privileges.HasPrivilege("map")) {
				WriteLine(player, "You have no map privileges");
				OnFailure(userid);
				return;
			}

			Task.Factory.StartNew(() => {
				try {
					TaskManager.Join(List, userid, Server.LoadMapListFromDirectory());
				} catch (Exception e) {
					TaskManager.Join(OnFailure, userid, e);
				}
			});
		}

		private void List(int userid, string[] maps)
		{
			Player player = null;

			if (userid != 0) {
				player = Player.FindByUserId(userid);
				if (player == null) {
					return;
					}
			}

			foreach (string map in maps) {
				WriteLine(player, map);
			}

			OnSuccess(player);
		}
	}
}

