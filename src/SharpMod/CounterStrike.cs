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

namespace SharpMod.CounterStrike
{
  public enum Weapons : byte
  {
    P228         = 1,
    SCOUT        = 3,
    HEGRENADE    = 4,
    XM1014       = 5,
    C4           = 6,
    MAC10        = 7,
    AUG          = 8,
    SMOKEGRENADE = 9,
    ELITE        = 10,
    FIVESEVEN    = 11,
    UMP45        = 12,
    SG550        = 13,
    GALIL        = 14,
    FAMAS        = 15,
    USP          = 16,
    GLOCK18      = 17,
    AWP          = 18,
    MP5NAVY      = 19,
    M249         = 20,
    M3           = 21,
    M4A1         = 22,
    TMP          = 23,
    G3SG1        = 24,
    FLASHBANG    = 25,
    DEAGLE       = 26,
    SG552        = 27,
    AK47         = 28,
    KNIFE        = 29,
    P90          = 30,
    VEST         = 31,
    VESTHELM     = 32
  }

  // The offsets can be found in the amxmodx cstrike plugin code
  // amxmodx-1.8.1/dlls/cstrike/cstrike/cstrike.h

  internal static class CounterStrikeOffset
  {
    public static int armortype;
    public static int team;
    public static int csmoney;
    public static int primaryweapon;
    public static int lastactivity;
    public static int internalmodel;
    public static int nvgoggles;
    public static int defuseplant;
    public static int vip;
    public static int tk;
    public static int hostagekills;
    public static int mapzone;
    public static int isdriving;
    public static int stationary;
    public static int zoomtype;
    public static int activeitem;

    internal static class Ammo
    {
      public static int awm;
      public static int scout;
      public static int para;
      public static int famas;
      public static int m3;
      public static int usp;
      public static int fiveseven;
      public static int deagle;
      public static int p228;
      public static int glock;
      public static int flash;
      public static int he;
      public static int smoke;
      public static int c4;
    }

    public static int csdeaths;
    public static int shield;

    // + Extra weapons offset
    public static int weapontype;
    public static int clipammo;
    public static int silencerfiremode;


    public static int hostagefollow;
    public static int hostagenextuse;
    public static int hostagelastuse;
    public static int hostageid;

    public static int armourytype;

    public static class C4
    {
      public static int explodetime;
      public static int defusing;
    }

    // TODO: add some detection in here
    static CounterStrikeOffset()
    {
      int extraOffset = 0;
      int extraOffsetWeapons = 0;
      int actualExtraOffset = 0;

      switch (Environment.OSVersion.Platform)
      {
      case PlatformID.Unix:
        extraOffset = 5;
        extraOffsetWeapons = 4;
        actualExtraOffset = 20;
        break;
      case PlatformID.Win32NT:
      case PlatformID.Win32S:
      case PlatformID.Win32Windows:
        // No Change on Windows platforms
        break;
      }

      if (Server.Is64Bit)
      {

        // TODO: enter values from amxmodx-1.8.1/dlls/cstrike/cstrike/cstrike.h

      } else { // its a 32bit system

        armortype     = 112 + extraOffset;
        team          = 114 + extraOffset;
        csmoney       = 115 + extraOffset;
        primaryweapon = 116 + extraOffset;
        lastactivity  = 124 + extraOffset;
        internalmodel = 126 + extraOffset;
        nvgoggles     = 129 + extraOffset;
        defuseplant   = 193 + extraOffset;
        vip           = 209 + extraOffset;
        tk            = 216 + extraOffset;
        hostagekills  = 217 + extraOffset;
        mapzone       = 235 + extraOffset;
        isdriving     = 350 + extraOffset;
        stationary    = 362 + extraOffset;
        zoomtype      = 363 + extraOffset;

        activeitem    = 373 + extraOffset;

        Ammo.awm        = 377 + extraOffset;
        Ammo.scout      = 378 + extraOffset;
        Ammo.para       = 379 + extraOffset;
        Ammo.famas      = 380 + extraOffset;
        Ammo.m3         = 381 + extraOffset;
        Ammo.usp        = 382 + extraOffset;
        Ammo.fiveseven  = 383 + extraOffset;
        Ammo.deagle     = 384 + extraOffset;
        Ammo.p228       = 385 + extraOffset;
        Ammo.glock      = 386 + extraOffset;
        Ammo.flash      = 387 + extraOffset;
        Ammo.he         = 388 + extraOffset;
        Ammo.smoke      = 389 + extraOffset;
        Ammo.c4         = 390 + extraOffset;

        csdeaths  = 444 + extraOffset;
        shield    = 510 + extraOffset;

        weapontype        = 43 + extraOffsetWeapons;
        clipammo          = 51 + extraOffsetWeapons;
        silencerfiremode  = 74 + extraOffsetWeapons;

        hostagefollow   = 86 + extraOffset;
        hostagenextuse  = 100 + extraOffset;
        hostagenextuse  = 483 + extraOffset;
        hostageid       = 487 + extraOffset;

        armourytype = 34 + extraOffsetWeapons;

        C4.explodetime  = 100 + extraOffset;
        C4.defusing     = 0x181 + actualExtraOffset;

      }
    }
  }

  /// <summary>
  /// Extensions for the Player class for the CounterStrike game mod
  /// </summary>
  public static class PlayerExtensions
  {

    /// <summary>
    /// Sets the amount of money a player posesses.
    /// Only server side is updated.
    /// </summary>
    /// <param name="player">
    /// The player <see cref="Player"/>
    /// </param>
    /// <param name="money">
    /// The amount of money <see cref="System.Int32"/>
    /// </param>
    public static void SetMoney(this Player player, int money)
    {
      player.SetPrivateData(CounterStrikeOffset.csmoney, money);
    }

    /// <summary>
    /// Gets the amount of money a player posesses
    /// The actual server side value is returned.
    /// </summary>
    /// <param name="player">
    /// Player <see cref="Player"/>
    /// </param>
    /// <returns>
    /// The amount of money the player posesses <see cref="System.Int32"/>
    /// </returns>
    public static int GetMoney(this Player player)
    {
      return player.GetPrivateData(CounterStrikeOffset.csmoney);
    }

    /// <summary>
    /// Sends a message to the player in order to update the HUD for the money.
    /// Only client side is udpated.
    /// </summary>
    /// <param name="player">
    /// The player <see cref="Player"/>
    /// </param>
    /// <param name="money">
    /// The new amount of money the player shoud posess <see cref="System.Int32"/>
    /// </param>
    /// <param name="flash">
    /// Enables/disables a flash of the money icon in the HUD <see cref="System.Boolean"/>
    /// </param>
    public static void SendMoneyMessage(this Player player, int money, bool flash)
    {
      Message.Begin(MessageDestination.OneReliable, Message.Types.GetValue("Money"), IntPtr.Zero, player.Pointer);
      Message.Write((long)money);
      if (flash) Message.Write((byte)1);
      else Message.Write((byte)0);
      Message.End();
    }

    /// <summary>
    /// Sets the player money and sends a message to the player to update
    /// Both client and server side.
    /// </summary>
    /// <param name="player">
    /// The player <see cref="Player"/>
    /// </param>
    /// <param name="money">
    /// The amount of money the player will posess afterwards <see cref="System.Int32"/>
    /// </param>
    /// <param name="flash">
    /// Wether to make the HUD flash(True) or not(False) <see cref="System.Boolean"/>
    /// </param>
    public static void SetMoney(this Player player, int money, bool flash)
    {
      SetMoney(player, money);
      SendMoneyMessage(player, money, flash);
    }

    /// <summary>
    /// Gets the deathcount of a player
    /// </summary>
    /// <param name="player">
    /// A player <see cref="Player"/>
    /// </param>
    /// <returns>
    /// Times the player died <see cref="System.Int32"/>
    /// </returns>
    public static int GetDeaths(this Player player)
    {
      return player.GetPrivateData(CounterStrikeOffset.csdeaths);
    }

    /// <summary>
    /// Sets the deathcount of a player
    /// </summary>
    /// <param name="player">
    /// A player <see cref="Player"/>
    /// </param>
    /// <param name="val">
    /// The deathcount the player shall have afterwards <see cref="System.Int32"/>
    /// </param>
    public static unsafe void SetDeaths(this Player player, int val)
    {
      player.SetPrivateData(CounterStrikeOffset.csdeaths, val);
    }

    /// <summary>
    /// Enumerator for colors that can be used in CounterStrike
    /// </summary>
    public enum Color
    {
      Yellow = 0x01,
      Special = 0x03,
      Green = 0x04
    }


    /// <summary>
    /// Enumerator for CounterStrike SpecialColors (Spectator, Terrorist, Blue)
    /// </summary>
    public enum SpecialColor
    {
      White = 0,
      Red = 1,
      Blue = 3
    }

    /// <summary>
    /// Sends a TeamInfo message to the player to inform of a teamchange
    /// The SpecialColor is set according to the Team the player is in.
    /// This is needed in order to use All 3 Counter Strike colors in chat.
    /// </summary>
    /// <param name="player">
    /// A player <see cref="Player"/>
    /// </param>
    /// <param name="team">
    /// The Team strings ("CT","TERRORIST", "SPECTATOR") <see cref="System.String"/>
    /// </param>
    public static void TeamInfo(this Player player, string team)
    {
      Message.Begin(MessageDestination.OneReliable, Message.Types.GetValue("TeamInfo"), IntPtr.Zero, player.Pointer);
      //Message.Write(player.ID);
      Message.Write(team);
      Message.End();
    }

    /// <summary>
    /// Prints some colored text in the chat (yellow, green, special).
    /// You can choose the special color in this Version (A fake teammessage is send to the client and afterwards
    /// the information is restored).
    /// </summary>
    /// <param name="player">
    /// A player <see cref="Player"/>
    /// </param>
    /// <param name="specialColor">
    /// The SpecialColor you want to send <see cref="SpecialColor"/>
    /// </param>
    /// <param name="text">
    /// The text you want to send, you can use C#-style arguments like {0} <see cref="System.String"/>
    /// </param>
    /// <param name="paramlist">
    /// The additional parameterlist. <see cref="System.Object[]"/>
    /// </param>
    public static void ClientColorPrint(this Player player, SpecialColor specialColor, string text, params object[] paramlist)
    {
      // backup teaminfo
      // normal color
      // teaminfo undo

      switch (specialColor)
      {
      case SpecialColor.Blue:
        player.TeamInfo("CT");
        break;
      case SpecialColor.Red:
        player.TeamInfo("TERRORIST");
        break;
      case SpecialColor.White:
        player.TeamInfo("Spectator");
        break;
      }
      player.ClientColorPrint(text, paramlist);

    }

    /// <summary>
    /// Prints some colored text in the chat (yellow, green, special).
    /// Special is the teamcolor (White for Spectator, Red for Terrorist, Blue for CounterTerrorist)
    /// </summary>
    /// <param name="player">
    /// A player <see cref="Player"/>
    /// </param>
    /// <param name="text">
    /// The text to be printed, C#-style arguments like {0} can be used <see cref="System.String"/>
    /// </param>
    /// <param name="paramlist">
    /// The object list <see cref="System.Object[]"/>
    /// </param>
    public static void ClientColorPrint(this Player player, string text, params object[] paramlist)
    {

      Message.Begin(MessageDestination.OneReliable, Message.Types.GetValue("SayText"), IntPtr.Zero, player.Pointer);
      Message.Write((byte)1); // printchat
      Message.Write(String.Format(text, paramlist));
      Message.End();
    }


    unsafe public static Weapon GetActiveWeapon(this Player player)
    {
      int *ptr = (int *)player.GetPrivateData(CounterStrikeOffset.activeitem);
      if (ptr == (int *)0) return null;
      Entvars *pev = *(Entvars **)ptr;
      if (pev == (Entvars *)0) return null;
      return new Weapon(pev->pContainingEntity);
    }

    public static void SendWeaponPickupMessage(this Player player, Weapons weapon)
    {
      player.SendWeaponPickupMessage((byte)weapon);
    }

  }


  public class Weapon : Entity
  {

    #region data
    private static int[] maxclipsize = new int[] {
      0,  // first is empty
      13, // P228
      0,  // second is empty too
      10, // Scout
      1,  // hegrenade
      5,  // XM1014, (automatic shotgun)
      1,  // C4
      30, // MAC10 (Terrorist UZI)
      30, // AUG
      1,  // SMOKEGRENADE
      30, // ELITE (beretas)
      20, // FIVESEVEN (pistol)
      25, // UMP45
      30, // SG550
      35, // GALIL
      25, // FAMAS
      12, // USP
      20, // GLOCK18
      10, // AWP
      30, // MP5NAVY
      100,// M249 (machine gun)
      8,  // M3 (shotgun)
      30, // M4A1
      30, // TMP (Counter-Terrorist UZI)
      30, // G3SG1 (Terrorist double zoom in weapon)
      2,  // FLASHBANG
      7,  // DEAGLE
      30, // SG552 (Terrorist zoom in weapon)
      30, // AK47
      -1, // KNIFE
      50, // P90
      1,  // VEST
      1   // VESTHELM
    };
    #endregion

    public enum Class
    {
      Undefined,
      Pistol,
      Primary,
      Grenade,
      Armor,
    };

    public static Weapon.Class GetWeaponType(Weapons weapon)
    {
      switch (weapon)
      {

      case Weapons.P228:
      case Weapons.ELITE:
      case Weapons.FIVESEVEN:
      case Weapons.USP:
      case Weapons.GLOCK18:
      case Weapons.DEAGLE:
        return Weapon.Class.Pistol;

      case Weapons.SCOUT:
      case Weapons.XM1014:
      case Weapons.AUG:
      case Weapons.SG550:
      case Weapons.GALIL:
      case Weapons.FAMAS:
      case Weapons.AWP:
      case Weapons.MP5NAVY:
      case Weapons.M249:
      case Weapons.M3:
      case Weapons.M4A1:
      case Weapons.TMP:
      case Weapons.G3SG1:
      case Weapons.AK47:
      case Weapons.P90:
        return Weapon.Class.Primary;

      case Weapons.SMOKEGRENADE:
      case Weapons.HEGRENADE:
      case Weapons.FLASHBANG:
        return Weapon.Class.Grenade;

      case Weapons.VEST:
      case Weapons.VESTHELM:
        return Weapon.Class.Armor;

      default:
        return Weapon.Class.Undefined;
      }
    }


    unsafe internal Weapon(void *ptr)
      : base(ptr)
    {
    }

    public int Ammo {
      get {
        return GetPrivateData(CounterStrikeOffset.clipammo);
      }
      set {
        SetPrivateData(CounterStrikeOffset.clipammo, value);
      }
    }

    public int Type {
     get {
        return GetPrivateData(CounterStrikeOffset.weapontype);
      }
    }

    public Weapon.Class TypeClass {
      get {
        return GetWeaponType((Weapons)Type);
      }
    }

    public int Silencer {
      get {
        return GetPrivateData(CounterStrikeOffset.silencerfiremode);
      }
    }

    public int ClipSize {
      get {
        return maxclipsize[Type];
      }
    }
  }

  public class CounterStrike
  {

    public delegate void BuyzoneDelegate(bool inzone);
    public static event BuyzoneDelegate Buyzone;

    public static void Init()
    {
      BinaryTree.Node node = Message.Types.GetNode("StatusIcon");
      if (node != null)
      {
        node.invoker = (Action<Player, byte, string>)StatusIcon;
      }
    }

    internal static void StatusIcon(Player player, byte status, string spriteName)
    {
      if (spriteName == "buyzone")
      {
        if (Buyzone != null) Buyzone(status == 1);
      }
    }
  }
}
