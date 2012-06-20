using System;

namespace SharpMod.Menu
{
	public class PlayerMenuInfo
	{
		public Player Player   { get; protected set; }
		public SimpleMenu Menu { get; protected set; }
		public int CurrentPage { get; protected set; }

		public PlayerMenuInfo(Player player, SimpleMenu menu)
		{
			Player = player;
			Menu = menu;
		}
	}
}

