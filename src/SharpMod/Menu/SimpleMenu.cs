using System;
using System.Text;
using System.Collections.Generic;

namespace SharpMod.Menu
{
	public class SimpleMenu : Item, IList<Item>, IMenu
	{
		private List<Item> list;

		public byte DisplayTime { get; set; }
		public bool NumberedItems { get; set; }
		public bool ColoredMenu { get; set; }
		public string NumberColor { get; set; }
		public string DefaultColor { get; set; }

		public SimpleMenu(string text)
			: this(text, true)
		{
		}

		public SimpleMenu(string text, bool enabled)
			: base(text, enabled)
		{
			list = new List<Item>();
			DisplayTime = 255;
		}

		public override void MenuIterator (Player player, IList<Item> itemlist, int start, ref int current, int end)
		{
			foreach (Item item in list) {
				item.MenuIterator(player, itemlist, start, ref current, end);
			}
		}

		protected void AddItemText(StringBuilder sb, int index, Item item)
		{
			if (NumberedItems) {
				if (ColoredMenu) {
					sb.Append(String.Format("{0}{1}. ", NumberColor, index+1));
				} else {
					sb.Append(String.Format("{0}. ", index+1));
				}
			}
			// The text is for sure there!
			sb.Append(String.Format("{0}{1}\n", (Selectable ? DefaultColor : MenuColor.Smoke), item.Text));
		}

		#region IMenu implementation
		public virtual MenuInfo GetMenuInfo (Player player)
		{
			StringBuilder sb = new StringBuilder();
			List<Item> acc = new List<Item>();

			int i = 0, j = 0;
			short keys = 0;

			MenuIterator(player, acc, 0, ref i, 10);

			player.menu = this;
			player.menu_items = acc.ToArray();

			sb.Append(Text);
			sb.Append("\n\n");

			foreach (Item item in acc) {
				AddItemText(sb, j+1, item);
				if (item.Enabled) {
					keys |= (short)(1 << j);
				}
				if (item.Selectable) {
					j++;
				}
			}

			return new MenuInfo(keys, DisplayTime, sb.ToString());
		}

		public override bool DoSelect(Player player, int index)
		{
			return player.menu_items[index].DoSelect(player, index);
		}
		#endregion

		#region IList<Item> implementation
		public int IndexOf(Item item)
		{
			return list.IndexOf(item);
		}
		public void Insert(int index, Item item)
		{
			list.Insert(index, item);
		}
		public void RemoveAt(int index)
		{
			list.RemoveAt(index);
		}
		public Item this[int index] {
			get {
				return list[index];
			}
			set {
				list[index] = value;
			}
		}
		#endregion

		#region IEnumerable<Item> implementation
		public IEnumerator<Item> GetEnumerator()
		{
			return list.GetEnumerator();
		}
		#endregion

		#region IEnumerable implementation
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}
		#endregion

		#region ICollection<Item> implementation

		public void Add(Item item)
		{
			list.Add(item);
		}

		public void Clear()
		{
			list.Clear();
		}

		public bool Contains(Item item)
		{
			return list.Contains(item);
		}

		public void CopyTo(Item[] array, int arrayIndex)
		{
			list.CopyTo(array, arrayIndex);
		}

		public bool Remove(Item item)
		{
			return list.Remove(item);
		}

		public int Count {
			get {
				return list.Count;
			}
		}

		public bool IsReadOnly {
			get {
				throw new System.NotImplementedException();
			}
		}

		#endregion

		#region IEnumerable<Item> implementation
		IEnumerator<Item> IEnumerable<Item>.GetEnumerator ()
		{
			return list.GetEnumerator();
		}
		#endregion
	}
}

