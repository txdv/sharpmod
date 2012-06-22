using System;
using SharpMod;
using SwearFilter;

namespace SharpMod.SwearFilter
{
	public class Plugin : BasicPlugin
	{
		public override void Load()
		{
			Player.RegisterCommand("say", SwearCheck);
			Player.RegisterCommand("say_team", SwearCheck);
		}

		public void SwearCheck(Player player, Command cmd)
		{
		}
	}
}
