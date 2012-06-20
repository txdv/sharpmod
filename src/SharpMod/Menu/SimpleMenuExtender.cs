using System;

namespace SharpMod.Menu
{
	public static class SimpleMenuExtender
	{
		public static void Show(this SimpleMenu menu, Player player)
		{
			player.ShowMenu(menu);
		}

		public static void Show(this SimpleMenu menu, Player player, byte displayTime)
		{
			player.ShowMenu(menu, displayTime);
		}

		public static void Show(this SimpleMenu menu, Player player, int displayTime)
		{
			Show(menu, player, (byte)displayTime);
		}
	}
}

