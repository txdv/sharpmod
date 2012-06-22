using System;
using System.Text;
using System.Collections.Generic;

namespace SharpMod
{
	public struct MenuInfo
	{
		public MenuInfo(short keys, byte displayTime, string text)
		{
			this.keys = keys;
			this.displayTime = displayTime;
			this.text = text;
		}
		public short keys;
		public byte displayTime;
		public string text;
	}

	public interface IMenu
	{
		MenuInfo GetMenuInfo(Player player);
		bool DoSelect(Player player, int index);
		void Add(Menu.Item item, Action action);
	}

	public struct MenuTime
	{
		public MenuTime(IMenu menu, DateTime start, TimeSpan timeSpan)
		{
			this.menu = menu;
			this.start = start;
			this.timeSpan = timeSpan;
		}
		public IMenu menu;
		public DateTime start;
		public TimeSpan timeSpan;
	}

	public static class PlayerMenuExtender
	{

		#region Menu Messages

		private static int defaultShowMenuTextLength = 175;

		public static void ShowMenu(this Player player, short keys, byte displaytime, string text)
		{
			ShowMenu(player, keys, (char)displaytime, text);
		}

		public static void ShowMenu(this Player player, short keys, char displaytime, string text)
		{
			int count = text.Length / defaultShowMenuTextLength;
			if (count == 0) {
				ShowMenu(player, keys, displaytime, 0, text);
			} else {
				for (int i = 0; i <  count; i++) {
					ShowMenu(player, keys, displaytime, (byte)(i == count-1 ? 0 : 1), text.Substring(i * defaultShowMenuTextLength, defaultShowMenuTextLength));
				}
			}
		}

		public static void ShowMenu(this Player player, short keys, byte displaytime, byte multipart, string text)
		{
			ShowMenu(player, keys, (char)displaytime, multipart, text);
		}

		public static void ShowMenu(this Player player, short keys, char displaytime, byte multipart, string text)
		{
			Message.Begin(MessageDestination.OneReliable, Message.GetUserMessageID("ShowMenu"), IntPtr.Zero, player.Pointer);
			Message.Write(keys);
			Message.Write(displaytime);
			Message.Write(multipart);
			Message.Write(text);
			Message.End();
		}

		#endregion

		private static Dictionary<Player, MenuTime> dict;

		static PlayerMenuExtender()
		{
			dict = new Dictionary<Player, MenuTime>();
		}

		public static void SelectMenu(this Player player, int index)
		{
			#if DEBUG
			Console.WriteLine("SelectMenu({0}, {1})", player.Name, index);
			#endif
			MenuTime mt;
			if (dict.TryGetValue(player, out mt)) {
				dict.Remove(player);

				if (DateTime.Now <= mt.start.Add(mt.timeSpan)) {
					mt.menu.DoSelect(player, index);
				} else {
					// TODO: random menuselect, make a log entry for abusive behaviour?
					#if DEBUG
					Console.WriteLine("SelectMenu({0}, {1})- bad time", player.Name, index);
					#endif
				}
			}
		}

		public static void ShowMenu(this Player player, IMenu menu)
		{
			MenuInfo menuInfo = menu.GetMenuInfo(player);
			dict[player] = new MenuTime(menu, DateTime.Now, TimeSpan.FromSeconds(menuInfo.displayTime));
			player.ShowMenu(menuInfo.keys, menuInfo.displayTime, menuInfo.text);
		}

		public static void ShowMenu(this Player player, IMenu menu, byte displayTime)
		{
			MenuInfo menuInfo = menu.GetMenuInfo(player);
			menuInfo.displayTime = displayTime;
			dict[player] = new MenuTime(menu, DateTime.Now, TimeSpan.FromSeconds(menuInfo.displayTime));
			player.ShowMenu(menuInfo.keys, menuInfo.displayTime, menuInfo.text);
		}
	}
}

