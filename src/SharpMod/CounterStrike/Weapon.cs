using System;
using System.Reflection;
using SharpMod.Messages;

namespace SharpMod.CounterStrike
{
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

		public enum Class {
			Undefined,
			Pistol,
			Primary,
			Grenade,
			Armor,
		};

		unsafe internal Weapon(void *ptr)
			: base(ptr)
		{
		}

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

		public int ClipSize {
			get {
				return maxclipsize[Type];
			}
		}

		#region Silencer

		public bool HasSilencer {
			get {
				switch (TypeEnum) {
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
				switch (TypeEnum) {
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
						GetPrivateData(CounterStrikeOffset.silencerfiremode) | (int)Weapon.GetWeaponSilence(TypeEnum));

						if (Owner != null && Owner is Player) {
							(Owner as Player).SetWeaponAnimation(AttachAnimation);
						}
					} else if (!value && Silencer) {
						SetPrivateData(CounterStrikeOffset.silencerfiremode,
						GetPrivateData(CounterStrikeOffset.silencerfiremode) & (~(int)Weapon.GetWeaponSilence(TypeEnum)));

					if (Owner != null && Owner is Player) {
							(Owner as Player).SetWeaponAnimation(DetachAnimation);
						}
					}
				}
			}
		}

		internal static WeaponsSilenced GetWeaponSilence(Weapons weapon)
		{
			switch (weapon) {
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
			switch (weapon) {
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
			switch (weapon) {
			case Weapons.weapon_m4a1:
				return WeaponAnimations.weapon_m4a1_silencer_detach;
			case Weapons.weapon_usp:
				return WeaponAnimations.weapon_usp_silencer_detach;
			default:
				return WeaponAnimations.noanimation;
			}
		}

		public WeaponAnimations AttachAnimation {
			get {
				return GetSilencerAttachAnimation(TypeEnum);
			}
		}

		public WeaponAnimations DetachAnimation {
			get {
				return GetSilencerDetachAnimation(TypeEnum);
			}
		}

		#endregion

		#endregion

		#region Burstmode

		public bool HasBurstMode {
			get {
				switch (TypeEnum) {
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
				switch (TypeEnum) {
				case Weapons.weapon_glock18:
					return (silencer & (int)WeaponMode.weapon_glock18_burstmode) > 0;
				case Weapons.weapon_famas:
					return (silencer & (int)WeaponMode.weapon_famas_burstmode) > 0;
				}
				// all other weapons have no silencer ..
				return false;
			}

			set {
				if (HasBurstMode) {
					if (value && !BurstMode) {
						SetPrivateData(CounterStrikeOffset.silencerfiremode, (int)GetWeaponBurstMode(TypeEnum));
							if (Owner != null && Owner is Player) {
							(Owner as Player).SendTextMsgMessage(TextMsgPosition.Center, "#Switch_To_BurstFire");
						}
				} else if (!value && BurstMode) {
					SetPrivateData(CounterStrikeOffset.silencerfiremode, 0);
					if (Owner != null && Owner is Player) {
						(Owner as Player).SendTextMsgMessage(TextMsgPosition.Center, GetAutomaticModeString(TypeEnum));
						}
					}
				}
			}
		}

		internal static WeaponMode GetWeaponBurstMode(Weapons weapon)
		{
			switch (weapon) {
			case Weapons.weapon_glock18:
				return WeaponMode.weapon_glock18_burstmode;
			case Weapons.weapon_famas:
				return WeaponMode.weapon_famas_burstmode;
			default:
				throw new Exception();
			}
		}

		internal static WeaponMode GetWeaponAutomaticMode(Weapons weapon)
		{
			switch (weapon) {
			case Weapons.weapon_glock18:
				return WeaponMode.weapon_glock18_semiautomatic;
			case Weapons.weapon_famas:
				return WeaponMode.weapon_famas_automatic;
			default:
				throw new Exception();
			}
		}

		internal static string GetAutomaticModeString(Weapons weapon)
		{
			switch (weapon) {
			case Weapons.weapon_glock18:
				return "#Switch_To_SemiAuto";
			case Weapons.weapon_famas:
				return "#Switch_To_FullAuto";
			default:
				throw new Exception();
			}
		}

		#endregion

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
			foreach (FieldInfo fi in fields) {
				weaponStrings[i] = fi.Name;
				weaponEnum[i] = (Weapons)fi.GetValue(null);
				i++;
				if (i == 2) {
					i++;
				}
			}
		}

		public static int GetType(string weaponname)
		{
			for (int i = 0; i < weaponStrings.Length; i++) {
				if (weaponStrings[i] == weaponname) {
					return i;
				}
			}
			return -1;
		}
		public static int GetType(Weapons weapon)
		{
			for (int i = 0; i < weaponEnum.Length; i++) {
				if (weaponEnum[i] == weapon) {
					return i;
				}
			}
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
	}
}
