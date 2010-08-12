// 
//     This file is part of sharpmod.
//     sharpmod is a metamod plugin which enables you to write plugins
//     for Valve GoldSrc using .NET programms.
// 
//     Copyright (C) 2010  Andrius Bentkus
// 
//     csharpmod is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     csharpmod is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with csharpmod.  If not, see <http://www.gnu.org/licenses/>.
// 

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

    public static void ShowMenu (this Player player, short keys, byte displaytime, string text)
    {
      ShowMenu(player, keys, (char)displaytime, text);
    }

    public static void ShowMenu (this Player player, short keys, char displaytime, string text)
    {
      int count = text.Length / defaultShowMenuTextLength;
      if (count == 0) ShowMenu(player, keys, displaytime, 0, text);
      else
      {
        for (int i = 0; i <  count; i++)
        {
          ShowMenu(player, keys, displaytime, (byte)(i == count-1 ? 0 : 1), text.Substring(i * defaultShowMenuTextLength, defaultShowMenuTextLength));
        }
      }
    }

    public static void ShowMenu (this Player player, short keys, byte displaytime, byte multipart, string text)
    {
      ShowMenu(player, keys, (char)displaytime, multipart, text);
    }

    public static void ShowMenu (this Player player, short keys, char displaytime, byte multipart, string text)
    {
      Message.Begin(MessageDestination.OneReliable, Message.Types.GetValue("ShowMenu"), IntPtr.Zero, player.Pointer);
      Message.Write(keys);
      Message.Write(displaytime);
      Message.Write(multipart);
      Message.Write(text);
      Message.End();
    }

    #endregion

    private static Dictionary<Player, MenuTime> dict;

    static PlayerMenuExtender ()
    {
      dict = new Dictionary<Player, MenuTime>();
    }

    public static void SelectMenu (this Player player, int index)
    {

      if (dict.ContainsKey(player))
      {
        MenuTime mt = dict[player];
        if (DateTime.Now <= mt.start.Add(mt.timeSpan))
        {
          mt.menu.DoSelect(player, index);
        }
        else
        {
          // TODO: random menuselect, make a log entry for abusive behaviour?
        }
        dict.Remove(player);
      }

    }

    public static void ShowMenu (this Player player, IMenu menu)
    {
      MenuInfo menuInfo = menu.GetMenuInfo(player);
      dict[player] = new MenuTime(menu, DateTime.Now, TimeSpan.FromSeconds(menuInfo.displayTime));
      player.ShowMenu(menuInfo.keys, menuInfo.displayTime, menuInfo.text);
    }

    public static void ShowMenu (this Player player, IMenu menu, byte displayTime)
    {
      MenuInfo menuInfo = menu.GetMenuInfo(player);
      menuInfo.displayTime = displayTime;
      dict[player] = new MenuTime(menu, DateTime.Now, TimeSpan.FromSeconds(menuInfo.displayTime));
      player.ShowMenu(menuInfo.keys, menuInfo.displayTime, menuInfo.text);
    }
  }
}

namespace SharpMod.Menues
{

  public static class SimpleMenuExtender
  {

    public static void Show (this SimpleMenu menu, Player player)
    {
      player.ShowMenu(menu);
    }

    public static void Show (this SimpleMenu menu, Player player, byte displayTime)
    {
      player.ShowMenu(menu, displayTime);
    }

    public static void Show (this SimpleMenu menu, Player player, int displayTime)
    {
      Show(menu, player, (byte)displayTime);
    }

  }

  public static class MenuColor
  {
    public const string Red    = @"\r";
    public const string Yellow = @"\y";
    public const string Green  = @"\g";
    public const string White  = @"\w";
    public const string Smoke  = @"\d";
  }

  public class Item
  {
    public virtual string Text { get; protected set; }
    public virtual bool Enabled { get; protected set; }
    public virtual bool Selectable { get; protected set; }

    public Item(string text)
      : this(text, true, true) { }

    public Item(string text, bool selectable)
      : this(text, selectable, true) { }

    public Item(string text, bool selectable, bool enabled)
    {
      Text = text;
      Enabled = enabled;
      Selectable = selectable;
    }

    public virtual void MenuIterator(Player player, IList<Item> itemlist, int start, ref int current, int end)
    {
      if ((start <= current) && (current < end)) itemlist.Add(this);
      if (current < end) current++;
    }

    public delegate bool SelectDelegate(Player player, int index);
    public event SelectDelegate Select;
    public virtual bool DoSelect(Player player, int index)
    {
      if (Select == null) return false;
      else return Select(player, index);
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

  public class CVarItem : Item
  {
    private CVar cvar = null;
    public string[] Values { get; protected set; }
    public string CVarName { get { return cvar.Name; } protected set { cvar = CVar.Get(value); } }

    public CVarItem(string varname, string[] values)
      : base(varname)
    {
      Values = values;
      cvar = CVar.Get(varname);
    }

    public override string Text {
      get {
        int index = -1;
        StringBuilder sb = new StringBuilder();
        if (Values != null)
        for (int i = 0; i < Values.Length; i++)
        {
          if (Values[i].ToLower() == cvar.String.ToLower())
          {
            sb.Append(MenuColor.Red);
            sb.Append(Values[i]);
            sb.Append(MenuColor.White);
            index = i;
          }
          else
          {
            sb.Append(Values[i]);
          }
          if (i != Values.Length-1) sb.Append(", ");
        }

        if (index == -1)
          return string.Format("{0} ({1}): {2}", base.Text, cvar.String, sb.ToString());
        else
          return string.Format("{0}: {1}", base.Text, sb.ToString());
      }
      protected set { base.Text = value; }
    }

    public int Index
    {
      get
      {
        if (Values != null)
        {
          for (int i = 0; i < Values.Length; i++)
            if (Values[i].ToLower() == cvar.String.ToLower()) return i;
        }
        return -1;
      }
    }

    public int NextItem
    {
      get
      {
        if (Index == -1) return 0;
        else             return (Index%Values.Length);
      }
    }

    public override bool DoSelect (Player player, int index)
    {
      base.DoSelect (player, index);
      cvar.String = Values[NextItem];
      return true;
    }

  }

  public class SimpleMenu : Item, IList<Item>, IMenu
  {
    private List<Item> list;

    public byte DisplayTime { get; set; }
    public bool NumberedItems { get; set; }
    public bool ColoredMenu { get; set; }
    public string NumberColor { get; set; }
    public string DefaultColor { get; set; }

    public SimpleMenu(string text)
      : this(text, true) { }

    public SimpleMenu(string text, bool enabled)
      : base(text, enabled)
    {
      list = new List<Item>();
      DisplayTime = 255;
    }

    public override void MenuIterator (Player player, IList<Item> itemlist, int start, ref int current, int end)
    {
      foreach (Item item in list)
      {
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

      foreach (Item item in acc)
      {
        AddItemText(sb, j+1, item);
        if (item.Enabled) keys |= (short)(1 << j);
        if (item.Selectable) j++;
      }

      return new MenuInfo(keys, DisplayTime, sb.ToString());
    }

    public override bool DoSelect(Player player, int index)
    {
      return player.menu_items[index].DoSelect(player, index);
    }
    #endregion

    #region IList<Item> implementation
    public int IndexOf (Item item)
    {
      return list.IndexOf(item);
    }
    public void Insert (int index, Item item)
    {
      list.Insert(index, item);
    }
    public void RemoveAt (int index)
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
    public IEnumerator<Item> GetEnumerator ()
    {
      return list.GetEnumerator();
    }
    #endregion

    #region IEnumerable implementation
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
    {
      return list.GetEnumerator();
    }
    #endregion

    #region ICollection<Item> implementation

    public void Add (Item item)
    {
      list.Add(item);
    }

    public void Clear ()
    {
      list.Clear();
    }

    public bool Contains (Item item)
    {
      return list.Contains(item);
    }

    public void CopyTo (Item[] array, int arrayIndex)
    {
      list.CopyTo(array, arrayIndex);
    }

    public bool Remove (Item item)
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

  public class Menu : SimpleMenu
  {
    public const int maximumItemsPerPage = 8;
    private int itemsPerPage = maximumItemsPerPage;
    public int ItemsPerPage {
      get { return itemsPerPage; }
      set { itemsPerPage = (value < 1 ? 1 : (value > maximumItemsPerPage ? maximumItemsPerPage : value )); }
    }

    public Menu(string text)
      : this(text, true) { }

    public Menu(string text, bool enabled)
      : base(text, enabled) { }


    private int GetItemIndex(int page)
    {
      return itemsPerPage * page;
    }

    private int GetPageCount(int items)
    {
      return (items / itemsPerPage) + (items % itemsPerPage > 0 ? 1 : 0);
    }


    public override MenuInfo GetMenuInfo (Player player)
    {
      StringBuilder sb = new StringBuilder();
      List<Item> acc = new List<Item>();


      int i = 0, j = 0;;
      short keys = 0;
      int page = 0;

      if (player.menu == this) {
        page = player.menu_page;
      } else {
        player.menu      = this;
        player.menu_page = 0;
      }

      MenuIterator(player, acc, GetItemIndex(page), ref i, GetItemIndex(page) + itemsPerPage);

      player.menu_items = acc.ToArray();

      sb.Append(Text);
      sb.Append(String.Format(" ({0}/{1})\n\n", page+1, GetPageCount(i)+1));

      foreach (Item item in acc)
      {
        AddItemText(sb, j, item);
        if (item.Enabled) keys |= (short)(1 << j);
        if (item.Selectable) j++;
      }
      sb.Append("\n\n");

      if (page > 0) {
        keys |= (short)(1 << 8);  // back
        sb.Append("9. Back\n");
      }
      if (page < GetPageCount(i)) {
        // if there are some more pages, hit the next
        // page counter
        keys |= (short)(1 << 9); // next
        sb.Append("0. Next\n");
      }

      return new MenuInfo(keys, DisplayTime, sb.ToString());
    }
    public override bool DoSelect (Player player, int index)
    {
      switch (index)
      {
      case 9:
        if (player.menu_page > 0) player.menu_page--;
        this.Show(player);
        return true;
      case 10:
        player.menu_page++;
        this.Show(player);
        return true;
      default:
        return base.DoSelect(player, index);
      }
    }

  }
}