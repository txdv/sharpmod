using System;
using System.Threading.Tasks;
using SharpMod.Helper;
using SharpMod.Database;

namespace SharpMod
{
	[CommandInfo(CommandString = "smod_map", CommandType = CommandType.Both,
		MinimumArguments = 2,
		HelpString = "<map> - changes the active map to")]
	public class ChangeMap : Command
	{
		public ChangeMap(string[] arguments)
			: base(arguments)
		{
		}

		public ChangeMap(string map)
			: this(new string[] { "smod_map", map })
		{
		}

		public string Map {
			get {
				string map = Arguments[1].ToLower();
				if (map.EndsWith(".bsp")) {
					return map.Substring(0, map.Length - 4);
				}
				return map;
			}
		}

		public override void Execute(Player player)
		{
			int userid = Player.GetUserID(player);

			if (player != null && !player.Privileges.HasPrivilege("map")) {
				OnFailure(userid, "You have no map privileges");
				return;
			}


			if (!Server.IsMapValid(Map)) {
				OnFailure(userid, "Invalid map provided");
				return;
			}

			MapChangeInfo mc = new MapChangeInfo(player, Map);

			//int comp = int.Parse(CVar.Get("smod_map_completness").String);
			int comp = 1;

			// TODO: make this work
			/*
			Task.Factory.StartNew(() => {
				try {
					var res = SharpMod.Verifier.VerifyMap(Map + ".bsp");
					if (comp >= 1 && !res.ServerCapable) {
						TaskManager.Join(OnFailure, userid, "Server is not capable of running this map");
					}

					if (comp >= 2 && !res.ClientCapable) {
						TaskManager.Join(OnFailure, userid, "Server is not capable of serving all client files");
					}
					SharpMod.Database.AddMapChange(mc);
					TaskManager.Join(OnSuccess, userid);
				} catch (Exception e) {
					TaskManager.Join(OnFailure, userid, e);
				}
			});
			*/
		}

		protected override void OnSuccess(Player player)
		{
			Server.ExecuteCommand("changelevel {0}", Map);

			// TODO: make this beautiful, 30 == SVC_INTERMISSSION
			Message.Begin(MessageDestination.AllReliable, 30, IntPtr.Zero, IntPtr.Zero);
			Message.End();
		}
	}
}

