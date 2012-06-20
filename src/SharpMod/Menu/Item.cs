using System;
using System.Collections.Generic;

namespace SharpMod.Menu
{
	public class Item
	{
		public virtual string Text { get; protected set; }
		public virtual bool Enabled { get; protected set; }
		public virtual bool Selectable { get; protected set; }

		public Item(string text)
		: this(text, true, true)
		{
		}

		public Item(string text, bool selectable)
		: this(text, selectable, true)
		{
		}

		public Item(string text, bool selectable, bool enabled)
		{
			Text = text;
			Enabled = enabled;
			Selectable = selectable;
		}

		public virtual void MenuIterator(Player player, IList<Item> itemlist, int start, ref int current, int end)
		{
			if ((start <= current) && (current < end)) {
				itemlist.Add(this);
			}
			if (current < end) {
				current++;
			}
		}

		public delegate bool SelectDelegate(Player player, int index);
		public event SelectDelegate Select;
		public virtual bool DoSelect(Player player, int index)
		{
			if (Select == null) {
				return false;
			} else {
				return Select(player, index);
			}
		}

		public virtual bool IsSelectable(Player player)
		{
			return Selectable;
		}

		public virtual bool IsEnabled(Player player)
		{
			return Enabled;
		}
	}
}

