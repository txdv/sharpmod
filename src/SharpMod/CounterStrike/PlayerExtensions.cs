// 
//     This file is part of sharpmod.
//     sharpmod is a metamod plugin which enables you to write plugins
//     for Valve GoldSrc using .NET programms.
// 
//     Copyright (C) 2011 Andrius Bentkus
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
using System.Reflection;
using SharpMod.Messages;

namespace SharpMod.CounterStrike
{
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

      switch (Environment.OSVersion.Platform) {
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

      if (Server.Is64Bit) {
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
        hostagelastuse  = 483 + extraOffset;
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

    #region InternalModel

    public static void SetInternalMode(this Player player, InternalModels model)
    {
      player.SetPrivateData(CounterStrikeOffset.internalmodel, (int)model);
    }

    public static InternalModels GetInternalMode(this Player player)
    {
      return (InternalModels)player.GetPrivateData(CounterStrikeOffset.internalmodel);
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

    #region Weapon Functions

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

    #endregion

    #region Weapon Ammo Functions

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

    public static int GetArmorTypeID(this Player player)
    {
      return player.GetPrivateData(CounterStrikeOffset.armortype);
    }
    public static ArmorType GetArmorType(this Player player)
    {
      return (ArmorType)player.GetArmorTypeID();
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

    public static void MakeVIP(this Player player, bool updateModel, bool updateScoreboard)
    {
      player.SetVIP(true);
      if (updateModel) {
        player.SetInternalMode(InternalModels.CS_CT_VIP);
        player.UpdateUserInfo(player.InfoKeyBuffer);
      }
      if (updateScoreboard) {
        player.SendScoreAttribMessage(ScoreAttribute.VIP);
      }
    }

    public static void UnmakeVIP(this Player player, bool updateModel, bool updateScoreboard, InternalModels newModel)
    {
      player.SetVIP(false);
      if (updateModel) {
        player.SetInternalMode(newModel);
        player.UpdateUserInfo(player.InfoKeyBuffer);
      }
      if (updateScoreboard) {
        player.SendScoreAttribMessage(player.GetDefaultScoreBoardAttribute());
      }
    }

    public static void SetVIP(this Player player, bool value, bool updateModel, bool updateScoreBoard)
    {
      if (value) player.MakeVIP(updateModel, updateScoreBoard);
      else       player.UnmakeVIP(updateModel, updateScoreBoard, (InternalModels)(new Random().Next(4)));
    }

    #endregion

    #region Animation

    public static void SetWeaponAnimation(this Player player, WeaponAnimations animation)
    {
      player.WeaponAnimation = (int)animation;
    }

    #endregion

    #region Mapzone Functions

    public static int GetMapzoneRaw(this Player player)
    {
      return player.GetPrivateData(CounterStrikeOffset.mapzone);
    }

    public static void SetMapzoneRaw(this Player player, int mapzone)
    {
      player.SetPrivateData(CounterStrikeOffset.mapzone, mapzone);
    }

    public static bool IsInBuyzone(this Player player)
    {
      return player.GetMapzoneRaw() == (int)MapZones.Buyzone;
    }

    #endregion

    public static ScoreAttribute GetDefaultScoreBoardAttribute(this Player player)
    {
      if (player.IsDead) return ScoreAttribute.Dead;
      // TODO: maybe check if model is equal VIP too?
      if (player.GetVIP()) return ScoreAttribute.VIP;
      // TODO: add bomb check
      return ScoreAttribute.Nothing;
    }

    // TODO: implement this
    //public static bool CanPlantBomb(this Player player)
    //{
    //  return player.GetPrivateData(CounterStrikeOffset.defuseplant
    //}
  }
}
