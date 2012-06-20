using System;
using System.Threading.Tasks;
using SharpMod.Helper;
using SharpMod.Database;

namespace SharpMod.Commands
{
	[CommandInfo(CommandString = "smod_ban", CommandType = CommandType.Both,
		MinimumArguments = 3, MaximumArguments = -1,
		HelpString = "<target> <duration> [reason] - bans a target by partial steamid, nick or ip with optional reason for duration")]
	public class Ban : Command
	{
		public Ban(string[] arguments)
			: base(arguments)
		{
		}

		public Ban(string player)
			: this(new string[] { "smod_ban", player })
		{
		}

		public Ban(string player, string banlength)
			: this(new string[] { "smod_ban", player, banlength })
		{
		}

		public Ban(string player, string banlength, string reason)
			: this(new string[] { "smod_ban", banlength, reason })
		{
		}

		public Ban(Player player, string banlength, string reason)
			: this(new string[] { "smod_ban", player.AuthID, banlength, reason })
		{
		}

		public Ban(Player player, int banlength, string reason)
			: this(new string[] { "smod_ban", player.AuthID, String.Format("{0}m", banlength), reason })
		{
		}

		protected string Target {
			get {
				return Arguments[1];
			}
		}

		public string Duration {
			get {
				return Arguments[2];
			}
		}

		public bool TryParseDuration(out TimeSpan timespan)
		{
			int hours;
			if (int.TryParse(Arguments[2], out hours)) {
				timespan = TimeSpan.FromMinutes(hours);
				return true;
			} else {
				timespan = TimeSpan.Zero;
				return false;
			}
		}

		public string Reason {
			get {
				return Arguments.Join(3, ' ');
			}
		}

		public override void Execute(Player player)
		{
			if (player != null && !player.Privileges.HasPrivilege("ban")) {
				WriteLine(player, "You have no ban privileges");
				return;
			}

			Player target = Player.Find(Target);

			if (target == null) {
				WriteLine(player, "Couldn't find target player");
				return;
			}

			if (target.Privileges.HasPrivilege("immunity")) {
				WriteLine(player, "Target has general immunity");
				return;
			}

			if (target.Privileges.HasPrivilege("noban")) {
				WriteLine(player, "Target has ban immunity");
				return;
			}

			TimeSpan duration;
			if (!TryParseDuration(out duration)) {
				WriteLine(player, "Duration was misformed");
			}

			BanInfo bi = new BanInfo(player, target, duration, Reason);

			int userid = Player.GetUserID(player);

			Task.Factory.StartNew(() => {
				SharpMod.Database.AddBan(bi, (Exception exception, bool success) => {
					if (success) {
						OnSuccess(userid);
					} else {
						OnFailure(userid, exception);
					}
				});
			});
		}

		protected override void OnSuccess(Player player)
		{
		Player target = Player.Find(Target);

		if (target == null) {
		return;
		}

		target.Kick(Reason);
		}
	}
}

