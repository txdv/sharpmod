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

// TODO: implement all methods from amxmodx/dlls/cstrike/cstrike/cstrike.cpp

using System;
using System.Reflection;
using SharpMod.Messages;
using SharpMod.GeneratedMessages;

namespace SharpMod.CounterStrike
{

  #region Enumerations

  public enum Weapons : int
  {
    weapon_p228      = 1,
    weapon_scout     = 3,
    weapon_hegrenade,
    weapon_xm1014,
    weapon_c4,
    weapon_mac10,
    weapon_aug,
    weapon_smokegrenade,
    weapon_elite,
    weapon_fiveseven,
    weapon_ump45,
    weapon_sg550,
    weapon_galil,
    weapon_famas,
    weapon_usp,
    weapon_glock18,
    weapon_awp,
    weapon_mp5navy,
    weapon_m249,
    weapon_m3,
    weapon_m4a1,
    weapon_tmp,
    weapon_g3sg1,
    weapon_flashbang,
    weapon_deagle,
    weapon_sg552,
    weapon_ak47,
    weapon_knife,
    weapon_p90,
    weapon_vest,
    weapon_vesthelm,
  }

  internal enum WeaponsSilenced : int
  {
    weapon_usp  = (1 << 0),
    weapon_m4a1 = (1 << 2),
  }

  public enum WeaponAnimations : int
  {
    noanimation = 0,
    weapon_m4a1_silencer_attach =  6,
    weapon_m4a1_silencer_detach = 13,
    weapon_usp_silencer_attach  =  7,
    weapon_usp_silencer_detach  = 15,
  }

  public enum WeaponMode : int
  {
    weapon_glock_semiautomatic = 0,
    weapon_glock_burstmode     = 2,
    weapon_famas_automatic     = 0,
    weapon_famas_burstmode     = 16,
  }

  public enum WeaponAmmo : int
  {
    ammo_338magnum = 0,
    ammo_762nato,
    ammo_556natobox,
    ammo_556nato,
    ammo_buckshot,
    ammo_45acp,
    ammo_57mm,
    ammo_50ae,
    ammo_357sig,
    ammo_9mm
  }

  public enum ItemFlag : int
  {
    SelectOnEmpty = 1,
    NoAutoreaload = 2,
    NoAutoswitchEmpty = 4,
    LimitInWorld = 8,
    Exhaustible = 16,
  }

  public enum Team : int
  {
    Unassigned = 0,
    Terrorist = 1,
    CounterTerrorist = 2,
    Spectator = 3,
  }

  /// <summary>
  /// Enumerator for colors that can be used in CounterStrike
  /// </summary>
  public enum Color : int
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
  // The offsets can be found in the amxmodx cstrike plugin code
  // amxmodx-1.8.1/dlls/cstrike/cstrike/cstrike.h

  public enum ArmorType : int
  {
    NoArmor = 0,
    Vest = 1,
    VestHelmet = 2,
  }

  #endregion

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

    #region Money

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
      player.SendMoneyMessage(money, (byte)(flash ? 1 : 0));
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

    #endregion

    #region Deaths

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
    public static void SetDeaths(this Player player, int val)
    {
      player.SetPrivateData(CounterStrikeOffset.csdeaths, val);
    }

    #endregion

    #region Team functions

    public static int GetTeamID(this Player player)
    {
      return player.GetPrivateData(CounterStrikeOffset.team);
    }

    public static Team GetTeamEnum(this Player player)
    {
      return (Team)player.GetTeamID();
    }

    public static string GetTeamString(this Player player)
    {
      return CounterStrike.GetTeamString(player.GetTeamID());
    }

    public static SpecialColor GetTeamColor(this Player player)
    {
      return CounterStrike.GetTeamColor(player.GetTeamID());
    }

    #endregion

    #region ColorPrint

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
      // TODO: for some reason this doesn't work, thought the messages come in a proper way
      string team = player.GetTeamString();
      player.SendTeamInfoMessage(CounterStrike.GetTeamString(specialColor));
      player.ClientColorPrint(text, paramlist);
      player.SendTeamInfoMessage(team);
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
      player.SendSayTextMessage(String.Format(text, paramlist));
    }

    #endregion

    /// <summary>
    /// Get's the Entity of the Weapon weared right now by they player
    /// </summary>
    /// <param name="player">
    /// The player <see cref="Player"/>
    /// </param>
    /// <returns>
    /// A Weapon Entity <see cref="Weapon"/>
    /// </returns>
    unsafe public static Weapon GetActiveWeapon(this Player player)
    {
      // TODO: rewrite this ugly code
      int *ptr = (int *)player.GetPrivateData(CounterStrikeOffset.activeitem);
      if (ptr == (int *)0) return null;
      Entvars *pev = *(Entvars **)ptr;
      if (pev == (Entvars *)0) return null;
      return Entity.CreateEntity(new IntPtr(pev->pContainingEntity)) as Weapon;
    }

    public static void SendWeaponPickupMessage(this Player player, Weapons weapon)
    {
      player.SendWeapPickupMessage((byte)weapon);
    }

    #region Ammo

    /// <summary>
    /// Sets the "backpack" ammo ammount, the ammo the players carries around.
    /// </summary>
    /// <param name="player">
    /// the player <see cref="Player"/>
    /// </param>
    /// <param name="ammoType">
    /// The weaponammo type <see cref="WeaponAmmo"/>
    /// </param>
    /// <param name="amount">
    /// amount of ammo <see cref="System.Int32"/>
    /// </param>
    public static void SetAmmo(this Player player, WeaponAmmo ammoType, int ammount)
    {
      player.SetPrivateData(CounterStrikeOffset.Ammo.awm + (int)ammoType, ammount);
    }

    /// <summary>
    /// Gets the ammount of ammo the player carries around.
    /// </summary>
    /// <param name="player">
    /// The player <see cref="Player"/>
    /// </param>
    /// <param name="ammoType">
    /// The ammo type <see cref="WeaponAmmo"/>
    /// </param>
    /// <returns>
    /// The amount of ammo <see cref="System.Int32"/>
    /// </returns>
    public static int GetAmmo(this Player player, WeaponAmmo ammoType)
    {
      return player.GetPrivateData(CounterStrikeOffset.Ammo.awm + (int)ammoType);
    }


    public static void SendAmmo(this Player player, WeaponAmmo ammoType, byte ammount)
    {
      player.SetAmmo(ammoType, ammount);
      player.SendAmmoXMessage((byte)ammoType, ammount);
    }

    public static void SendAmmo(this Player player, WeaponAmmo ammoType, int ammount)
    {
      player.SendAmmo(ammoType, (byte)ammount);
    }

    #endregion

    #region Armor

    public static ArmorType GetArmorType(this Player player)
    {
      return (ArmorType)player.GetPrivateData(CounterStrikeOffset.armortype);
    }

    public static void SetArmorType(this Player player, ArmorType armorType)
    {
      player.SetArmorType((int)armorType);
    }

    public static void SetArmorType(this Player player, int armorType)
    {
      player.SetPrivateData(CounterStrikeOffset.armortype, armorType);
    }

    #endregion

    #region SendTeamInfoMessage

    public static void SendTeamInfoMessage(this Player player, Team team)
    {
      player.SendTeamInfoMessage(CounterStrike.GetTeamString(team));
    }

    public static void SendTeamInfoMessage(this Player player, Player playerTeamChange, Team team)
    {
      player.SendTeamInfoMessage(playerTeamChange, CounterStrike.GetTeamString(team));
    }
    #endregion

    #region VIP

    public static bool GetVIP(this Player player)
    {
      return (player.GetPrivateData(CounterStrikeOffset.vip) == 1);
    }

    public static void SetVIP(this Player player, bool value)
    {
      player.SetPrivateData(CounterStrikeOffset.vip, (value ? 1 : 0));
    }

    // TODO: implement this one
    // public static void SetVIP(this Player player, bool value, bool updateModel, bool updateScoreBoard) { }

    #endregion

    #region Animation

    public static void SetWeaponAnimation(this Player player, WeaponAnimations animation)
    {
      player.WeaponAnimation = (int)animation;
    }

    #endregion

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

    unsafe internal Weapon(void *ptr)
      : base(ptr) { }

    public int ClipAmmo {
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

    public Weapons TypeEnum {
      get {
        return Weapon.GetTypeEnum(Type);
      }
    }

    public string TypeString {
      get {
        return Weapon.GetTypeString(Type);
      }
    }

    public bool HasSilencer {
      get {
        switch (TypeEnum)
        {
        case Weapons.weapon_m4a1:
        case Weapons.weapon_usp:
          return true;
        default:
          return false;
        }
      }
    }

    unsafe public bool Silencer {
      get {
        int silencer = GetPrivateData(CounterStrikeOffset.silencerfiremode);
        switch (TypeEnum)
        {
        case Weapons.weapon_m4a1:
          return (silencer & (int)WeaponsSilenced.weapon_m4a1) > 0;
        case Weapons.weapon_usp:
          return (silencer & (int)WeaponsSilenced.weapon_usp) > 0;
        }
        // all other weapons have no silencer ..
        return false;
      }
      set {
        // TODO: Make an extra function for this?
        if (HasSilencer) {
          if (value && !Silencer) {
            SetPrivateData(CounterStrikeOffset.silencerfiremode,
                           GetPrivateData(CounterStrikeOffset.silencerfiremode) | (int)Weapon.GetWeaponSilence(this));

            if (Owner != null && Owner is Player) (Owner as Player).SetWeaponAnimation(AttachAnimation);
          } else if (!value && Silencer) {
            SetPrivateData(CounterStrikeOffset.silencerfiremode,
                           GetPrivateData(CounterStrikeOffset.silencerfiremode) & (~(int)Weapon.GetWeaponSilence(this)));

            if (Owner != null && Owner is Player) (Owner as Player).SetWeaponAnimation(DetachAnimation);
          }
        }
      }
    }

    internal static WeaponsSilenced GetWeaponSilence(Weapon weapon)
    {
      switch (weapon.TypeEnum)
      {
        case Weapons.weapon_m4a1:
          return WeaponsSilenced.weapon_m4a1;
        case Weapons.weapon_usp:
          return WeaponsSilenced.weapon_usp;
      default:
        throw new Exception();
      }
    }

    #region Attach && Detach

    public static WeaponAnimations GetSilencerAttachAnimation(Weapons weapon)
    {
      switch (weapon)
      {
      case Weapons.weapon_m4a1:
        return WeaponAnimations.weapon_m4a1_silencer_attach;
      case Weapons.weapon_usp:
        return WeaponAnimations.weapon_usp_silencer_attach;
      default:
        return WeaponAnimations.noanimation;
      }
    }
    public static WeaponAnimations GetSilencerDetachAnimation(Weapons weapon)
    {
      switch (weapon)
      {
      case Weapons.weapon_m4a1:
        return WeaponAnimations.weapon_m4a1_silencer_detach;
      case Weapons.weapon_usp:
        return WeaponAnimations.weapon_usp_silencer_detach;
      default:
        return WeaponAnimations.noanimation;
      }
    }

    public WeaponAnimations AttachAnimation
    {
      get {
        return GetSilencerAttachAnimation(TypeEnum);
      }
    }
    public WeaponAnimations DetachAnimation
    {
      get {
        return GetSilencerDetachAnimation(TypeEnum);
      }
    }

    #endregion

    #region Burstmode

    public bool HasBurstMode {
      get {
        switch (TypeEnum)
        {
        case Weapons.weapon_glock18:
        case Weapons.weapon_famas:
          return true;
        default:
          return false;
        }
      }
    }

    public bool BurstMode {
      get {
        int silencer = GetPrivateData(CounterStrikeOffset.silencerfiremode);
        switch (TypeEnum)
        {
        case Weapons.weapon_glock18:
          return (silencer & (int)WeaponMode.weapon_glock_burstmode) > 0;
        case Weapons.weapon_famas:
          return (silencer & (int)WeaponMode.weapon_famas_burstmode) > 0;
        }
        // all other weapons have no silencer ..
        return false;
      }
      // TODO: add BurstMode
      set { }
    }

    #endregion

    public int ClipSize {
      get {
        return maxclipsize[Type];
      }
    }

    #region Weapon representations

    private static string[] weaponStrings;
    private static Weapons[] weaponEnum;
    static Weapon()
    {
      FieldInfo[] fields = typeof(Weapons).GetFields(BindingFlags.Public | BindingFlags.Static);
      // since we ommit 0 and 2, we have to add 2 more to accomodate all
      // the fields ;)
      weaponStrings = new string[fields.Length+2];
      weaponEnum = new Weapons[fields.Length+2];

      int i = 1;
      foreach (FieldInfo fi in fields)
      {
        weaponStrings[i] = fi.Name;
        weaponEnum[i] = (Weapons)fi.GetValue(null);
        i++;
        if (i == 2) i++;
      }
    }

    public static int GetType(string weaponname)
    {
      for (int i = 0; i < weaponStrings.Length; i++) if (weaponStrings[i] == weaponname) return i;
      return -1;
    }
    public static int GetType(Weapons weapon)
    {
      for (int i = 0; i < weaponEnum.Length; i++) if (weaponEnum[i] == weapon) return i;
      return -1;
    }

    public static string GetTypeString(int i)
    {
      return weaponStrings[i];
    }
    public static string GetTypeString(Weapons weapon)
    {
      return GetTypeString((int)weapon);
    }

    public static Weapons GetTypeEnum(int i)
    {
      return weaponEnum[i];
    }
    public static Weapons GetTypeEnum(string weaponname)
    {
      return weaponEnum[GetType(weaponname)];
    }

    #endregion

    // TODO: implement this one
    // public bool WeaponSilenced { get { } set { } }
    // public bool BurstMode { get { } set { } }
  }

  public class Hostage : Entity
  {
    unsafe internal Hostage(void *ptr)
      : base(ptr) { }

    public int HostageIndex {
      get {
        return GetPrivateData(CounterStrikeOffset.hostageid);
      }
    }
  }

  public class CounterStrike
  {
    #region team represantations
    public static string GetTeamString(Team team)
    {
      switch (team)
      {
      case Team.Unassigned:
      return "UNASSIGNED";
      case Team.Terrorist:
        return "TERRORIST";
      case Team.CounterTerrorist:
        return "CT";
      case Team.Spectator:
      default:
        return "SPECTATOR";
      }
    }
    public static string GetTeamString(int id)
    {
      return GetTeamString((Team)id);
    }
    public static string GetTeamString(SpecialColor color)
    {
      return GetTeamString(GetTeamID(color));
    }

    public static Team GetTeamEnum(string team)
    {
      return (Team)GetTeamID(team);
    }
    public static Team GetTeamEnum(int id)
    {
      return (Team)id;
    }
    public static Team GetTeamEnum(SpecialColor color)
    {
      return (Team)GetTeamID(color);
    }

    public static int GetTeamID(string team)
    {
      switch (team)
      {
      case "UNASSIGNED":
        return 0;
      case "TERRORIST":
        return 1;
      case "CT":
        return 2;
      case "SPECTATOR":
      default:
        return 3;
      }
    }
    public static int GetTeamID(Team team)
    {
      return (int)team;
    }
    public static int GetTeamID(SpecialColor color)
    {
      switch (color)
      {
      case SpecialColor.Red:
        return 1;
      case SpecialColor.Blue:
        return 2;
      case SpecialColor.White:
      default:
        return 3;
      }
    }

    public static SpecialColor GetTeamColor(string team)
    {
      return GetTeamColor(GetTeamID(team));
    }
    public static SpecialColor GetTeamColor(Team team)
    {
      return GetTeamColor((int)team);
    }
    public static SpecialColor GetTeamColor(int team)
    {
      switch (team)
      {
      case 1:
        return SpecialColor.Red;
      case 2:
        return SpecialColor.Blue;
      case 3:
      default:
        return SpecialColor.White;
      }
    }
    #endregion

    public delegate void BuyzoneDelegate(bool inzone);
    public static event BuyzoneDelegate Buyzone;

    public static void Init()
    {
      BinaryTree.Node node = Message.Types.GetNode("StatusIcon");
      if (node != null) node.invoker = (Action<Player, byte, string>)StatusIcon;
      node = Message.Types.GetNode("WeaponList");
      //if (node != null) node.invoker = (Action<string, byte, byte, byte, byte, byte, byte>)WeaponList;
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
