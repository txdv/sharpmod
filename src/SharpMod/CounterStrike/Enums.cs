using System;
using System.Reflection;
using SharpMod.Messages;

namespace SharpMod.CounterStrike
{
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

	internal enum WeaponMode : int
	{
		weapon_glock18_semiautomatic =  0,
		weapon_glock18_burstmode     =  2,
		weapon_famas_automatic       =  0,
		weapon_famas_burstmode       = 16,
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
		SelectOnEmpty     =  1,
		NoAutoreaload     =  2,
		NoAutoswitchEmpty =  4,
		LimitInWorld      =  8,
		Exhaustible       = 16,
	}

	public enum Team : int
	{
		Unassigned       = 0,
		Terrorist        = 1,
		CounterTerrorist = 2,
		Spectator        = 3,
	}

	/// <summary>
	/// Enumerator for colors that can be used in CounterStrike
	/// </summary>
	public enum Color : int
	{
		Yellow  = 0x01,
		Special = 0x03,
		Green   = 0x04
	}

	/// <summary>
	/// Enumerator for CounterStrike SpecialColors (Spectator, Terrorist, Blue)
	/// </summary>
	public enum SpecialColor {
		White = 0,
		Red = 1,
		Blue = 3
	}
	// The offsets can be found in the amxmodx cstrike plugin code
	// amxmodx-1.8.1/dlls/cstrike/cstrike/cstrike.h

	public enum ArmorType : int
	{
		NoArmor    = 0,
		Vest       = 1,
		VestHelmet = 2,
	}

	public enum InternalModels : int
	{
		CS_DONTCHANGE  =  0,
		CS_CT_URBAN    =  1,
		CS_T_TERROR    =  2,
		CS_T_LEET      =  3,
		CS_T_ARCTIC    =  4,
		CS_CT_GSG9     =  5,
		CS_CT_GIGN     =  6,
		CS_CT_SAS      =  7,
		CS_T_GUERILLA  =  8,
		CS_CT_VIP      =  9,
		CZ_T_MILITIA   = 10,
		CZ_CT_SPETSNAZ = 11
	};

	public enum MapZones : int
	{
		Buyzone = (1 << 0),
	}
}
