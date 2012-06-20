using System;
using System.Threading.Tasks;
using SharpMod.Helper;
using SharpMod.Database;

namespace SharpMod.Commands
{
	[CommandInfo(CommandString = "smod_kick", CommandType = CommandType.Both,
		MinimumArguments = 2, MaximumArguments = -1,
		HelpString = "<target> [reason] - kicks a target by partial steamid, nick or ip with the reason")]
	public class Kick : Command
	{
		public string Target {
			get {
				return Arguments[1];
			}
		}

		public string Reason {
			get {
				return Arguments.Join(2, ' ');
			}
		}

		public Kick(string[] arguments)
			: base(arguments)
		{
		}

		public Kick(string player)
			: this(new string[] { "smod_kick",  player })
		{
		}

		public Kick(Player player)
			: this(player.Name)
		{
		}

		public Kick(string player, string reason)
			: this(new string[] { "smod_kick", player, reason })
		{
		}

		public Kick(Player player, string reason)
			: this(new string[] { "smod_kick", string.Format("#{0}", player.UserID), reason })
		{
		}

		public override void Execute(Player player)
		{
			if (player != null && !player.Privileges.HasPrivilege("kick")) {
				WriteLine(player, "You have no kick privileges");
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

			if (target.Privileges.HasPrivilege("nokick")) {
				WriteLine(player, "Target has kick immunity");
				return;
			}

			KickInfo ki = new KickInfo(player, target, Reason);

			target.Kick(Reason);

			SharpMod.Database.AddKick(ki, (kicked) => { });
		}
	}
}

