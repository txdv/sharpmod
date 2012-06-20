using System;
using System.Reflection;

namespace SharpMod.Messages
{
	#region MessageStructs

	public struct ADStopMessage
	{
	}

	public struct AllowSpecMessage
	{
		public byte Allowed;
	}

	public struct AmmoPickupMessage
	{
		public byte AmmoID;
		public byte Ammount;
	}

	public struct AmmoXMessage
	{
		public byte AmmoID;
		public byte Ammount;
	}

	public struct ArmorTypeMessage
	{
		public byte Flag;
	}

	public struct BarTimeMessage
	{
		public short Duration;
	}

	public struct BarTime2Message
	{
		public short Duratino;
		public short startPercents;
	}

	public struct BatteryMessage
	{
		public short Armor;
	}

	public struct BlinkAcctMessage
	{
		public byte BlinkAmt;
	}

	public struct BombDropMessage
	{
		public int CoordX;
		public int CoordY;
		public int CoordZ;
		public byte Flag;
	}

	public struct BombPickupMessage
	{
	}

	public struct BotProgressMessage
	{
		public byte Flag;
		public byte Progress;
		public string Header;
	}

	public struct BotVoiceMessage
	{
		public byte Status;
		public byte PlayerIndex;
	}

	public struct BrassMessage
	{
		public byte Unknown;
		public int StartX;
		public int StartY;
		public int StartZ;
		public int VelocityX;
		public int VelocityY;
		public int VelocityZ;
		public int UnknownX;
		public int UnknownY;
		public int UnknownZ;
		public int Life;
		public short Model;
		public byte Unknownb1;
		public byte Unknownb2;
		public byte Unknownb3;
	}

	public struct BuyCloseMessage
	{
	}

	public struct ClCorpseMessage
	{
		public string ModelName;
		public long CoordX;
		public long CoordY;
		public long CoordZ;
		public int Angle0;
		public int Angle1;
		public int Angle2;
		public long Delay;
		public byte Sequence;
		public byte ClassID;
		public byte TeamID;
		public byte PlayerID;
	}

	public struct CrosshairMessage
	{
		public byte Flag;
	}

	public struct CurWeaponMessage
	{
		public byte IsActive;
		public byte WeaponID;
		public byte ClipAmmo;
	}

	public struct CZCareerMessage
	{
		public string Type;
		public int Parameters;
	}

	public struct CZCareerHUDMessage
	{
		public string Type;
		public int Parameters;
	}

	public struct DamageMessage
	{
		public byte DamageSave;
		public byte DamageTake;
		public long DamageType;
		public int CoordX;
		public int CoordY;
		public int CoordZ;
	}

	public struct DeathMsgMessage
	{
		public byte KillerID;
		public byte VictimID;
		public byte IsHeadshot;
		public string TruncatedWeaponName;
	}

	public struct FlashBatMessage
	{
		public byte ChargePercents;
	}

	public struct FlashlightMessage
	{
		public byte Flag;
		public byte ChargePercents;
	}

	public struct FogMessage
	{
		public byte FogValue1;
		public byte FogValue2;
		public byte Unknown;
	}

	public struct ForceCamMessage
	{
		public byte ForececamValue;
		public byte ForcechasecamValue;
		public byte Unknown;
	}

	public struct GameModeMessage
	{
		public byte Unknown;
	}

	public struct GameTitleMessage
	{
	}

	public struct GeigerMessage
	{
		public byte Distance;
	}

	public struct HealthMessage
	{
		public byte Health;
	}

	public struct HideWeaponMessage
	{
		public byte Flags;
	}

	public struct HLTVMessage
	{
		public byte ClientID;
		public byte Flags;
	}

	public struct HostageKMessage
	{
		public byte HostageID;
	}

	public struct HostagePosMessage
	{
		public byte Flag;
		public byte HostageID;
		public int CoordX;
		public int CoordY;
		public int CoordZ;
	}

	public struct HudTextMessage
	{
	}

	public struct HudTextArgsMessage
	{
		public string TextCode;
		public byte Unknown1;
		public byte Unknown2;
	}

	public struct HudTextProMessage
	{
	}

	public struct InitHUDMessage
	{
	}

	public struct ItemPickupMessage
	{
		public string ItemName;
	}

	public struct ItemStatusMessage
	{
		public byte ItemBitSum;
	}

	public struct LocationMessage
	{
		public byte Money;
	}

	public struct MoneyMessage
	{
		public long Amount;
		public byte Flag;
	}

	public struct MOTDMessage
	{
		public byte Flag;
		public string Text;
	}

	public struct NVGToggleMessage
	{
		public byte Flag;
	}

	public struct RadarMessage
	{
		public byte PlayerID;
		public int CoordX;
		public int CoordY;
		public int CoordZ;
	}

	public struct ReceiveWMessage
	{
	}

	public struct ReloadSoundMessage
	{
		public byte Unknown1;
		public byte Unknown2;
	}

	public struct ReqStateMessage
	{
	}

	public struct ResetHUDMessage
	{
	}

	public struct RoundTimeMessage
	{
		public short Time;
	}

	public struct SayTextMessage
	{
		public byte SenderID;
		public string String1;
		public string String2;
		public string String3;
	}

	public struct ScenarioMessage
	{
		public byte Active;
		public string Sprite;
		public byte Alpha;
		public short FlashRate;
		public short Unknown;
	}

	public struct ScoreAttribMessage
	{
		public byte PlayerID;
		public byte Flags;
	}

	public struct ScoreInfoMessage
	{
		public byte PlayerID;
		public short Frags;
		public short Deaths;
		public short ClassID;
		public short TeamID;
	}

	public struct ScreenFadeMessage
	{
		public short Duration;
		public short HoldTime;
		public short Flags;
		public byte ColorR;
		public byte ColorG;
		public byte ColorB;
		public byte Alpha;
	}

	public struct ScreenShakeMessage
	{
		public short Amplitude;
		public short Duration;
		public short Frequency;
	}

	public struct SendAudioMessage
	{
		public byte SenderID;
		public string AduioCode;
		public short Pitch;
	}

	public struct ServerNameMessage
	{
		public string ServerName;
	}

	public struct SetFOVMessage
	{
		public byte Degrees;
	}

	public struct ShadowIdxMessage
	{
		public long Unknown;
	}

	public struct ShowMenuMessage
	{
		public short KeyBitSum;
		public char Time;
		public byte MultiPart;
		public string Text;
	}

	public struct ShowTimerMessage
	{
	}

	public struct SpecHealthMessage
	{
		public byte Health;
	}

	public struct SpecHealth2Message
	{
		public byte Health;
		public byte PlayerID;
	}

	public struct SpectatorMessage
	{
		public byte ClientID;
		public byte Unknown;
	}

	public struct StatusIconMessage
	{
		public byte Status;
		public string SpriteName;
		public byte ColorR;
		public byte ColorG;
		public byte ColorB;
	}

	public struct StatusValueMessage
	{
		public byte Flag;
		public short Value;
	}

	public struct StatusTextMessage
	{
		public byte Unknown;
		public string Text;
	}

	public struct TaskTimeMessage
	{
		public short Time;
		public byte Active;
		public byte Fade;
	}

	public struct TeamInfoMessage
	{
		public byte PlayerID;
		public string TeamName;
	}

	public struct TeamScoreMessage
	{
		public string Score;
	}

	public struct TextMsgMessage
	{
		public byte DestinationType;
		public string MessageContent;
		public string Submsg1;
		public string Submsg2;
		public string Submsg3;
		public string Submsg4;
	}

	public struct TrainMessage
	{
		public byte Speed;
	}

	public struct TutorCloseMessage
	{
	}

	public struct TutorLineMessage
	{
	}

	public struct TutorStateMessage
	{
	}

	public struct TutorTextMessage
	{
		public string Unknown1;
		public byte Unknown2;
		public short Unknown3;
		public short Unknown4;
		public short Unknown5;
	}

	public struct ViewModeMessage
	{
	}

	public struct VGUIMenuMessage
	{
		public byte MenuID;
		public short KeyBitSum;
		public char Time;
		public byte MultiPart;
		public string Name;
	}

	public struct VoiceMaskMessage
	{
		public long AudiblePlayersIndexbitSum;
		public long ServerBannedPlayersIndexBitSum;
	}

	public struct WeaponListMessage
	{
		public string WeaponName;
		public byte PrimaryAmmoID;
		public byte PrimaryAmmoMaxAmount;
		public byte SecondaryAmmoID;
		public byte SecondaryAmmoMaxAmount;
		public byte SlotID;
		public byte NumberInSlot;
		public byte WeaponID;
		public byte Flags;
	}

	public struct WeapPickupMessage
	{
		public byte WeaponID;
	}


	#endregion

	#region All message functions

	public static partial class MessageFunctions
	{
		// Let's make message sending fun!
		// Let's create predefined methods for every message, so you dont have to lookup all the time everything!
		
		#region ADStop
		public static void SendADStopMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity)
		{
			Message.Begin(destination, Message.GetUserMessageID("ADStop"), floatValue, playerEntity);
			
			Message.End();
		}

		public static void SendADStopMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ADStopMessage val)
		{
			SendADStopMessage(destination, floatValue,playerEntity );
		}

		public static void SendADStopMessage(MessageDestination destination, IntPtr playerEntity, ADStopMessage val)
		{
			SendADStopMessage(destination, IntPtr.Zero, playerEntity );
		}

		public static void SendADStopMessage(MessageDestination destination, ADStopMessage val)
		{
			SendADStopMessage(destination, IntPtr.Zero, IntPtr.Zero );
		}

		public static void SendADStopMessage(this Player player, IntPtr floatValue, ADStopMessage val)
		{
			SendADStopMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendADStopMessage(this Player player, ADStopMessage val)
		{
			SendADStopMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendADStopMessage(this Player player)
		{
			SendADStopMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer);
		}

		#endregion
		
		#region AllowSpec
		public static void SendAllowSpecMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Allowed)
		{
			Message.Begin(destination, Message.GetUserMessageID("AllowSpec"), floatValue, playerEntity);
			
			Message.WriteByte(Allowed);
			Message.End();
		}

		public static void SendAllowSpecMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, AllowSpecMessage val)
		{
			SendAllowSpecMessage(destination, floatValue,playerEntity , val.Allowed);
		}

		public static void SendAllowSpecMessage(MessageDestination destination, IntPtr playerEntity, AllowSpecMessage val)
		{
			SendAllowSpecMessage(destination, IntPtr.Zero, playerEntity , val.Allowed);
		}

		public static void SendAllowSpecMessage(MessageDestination destination, AllowSpecMessage val)
		{
			SendAllowSpecMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Allowed);
		}

		public static void SendAllowSpecMessage(this Player player, IntPtr floatValue, AllowSpecMessage val)
		{
			SendAllowSpecMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendAllowSpecMessage(this Player player, AllowSpecMessage val)
		{
			SendAllowSpecMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendAllowSpecMessage(this Player player, byte Allowed)
		{
			SendAllowSpecMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Allowed);
		}

		#endregion
		
		#region AmmoPickup
		public static void SendAmmoPickupMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte AmmoID, byte Ammount)
		{
			Message.Begin(destination, Message.GetUserMessageID("AmmoPickup"), floatValue, playerEntity);
			
			Message.WriteByte(AmmoID);
			Message.WriteByte(Ammount);
			Message.End();
		}

		public static void SendAmmoPickupMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, AmmoPickupMessage val)
		{
			SendAmmoPickupMessage(destination, floatValue,playerEntity , val.AmmoID, val.Ammount);
		}

		public static void SendAmmoPickupMessage(MessageDestination destination, IntPtr playerEntity, AmmoPickupMessage val)
		{
			SendAmmoPickupMessage(destination, IntPtr.Zero, playerEntity , val.AmmoID, val.Ammount);
		}

		public static void SendAmmoPickupMessage(MessageDestination destination, AmmoPickupMessage val)
		{
			SendAmmoPickupMessage(destination, IntPtr.Zero, IntPtr.Zero , val.AmmoID, val.Ammount);
		}

		public static void SendAmmoPickupMessage(this Player player, IntPtr floatValue, AmmoPickupMessage val)
		{
			SendAmmoPickupMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendAmmoPickupMessage(this Player player, AmmoPickupMessage val)
		{
			SendAmmoPickupMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendAmmoPickupMessage(this Player player, byte AmmoID, byte Ammount)
		{
			SendAmmoPickupMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, AmmoID, Ammount);
		}

		#endregion
		
		#region AmmoX
		public static void SendAmmoXMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte AmmoID, byte Ammount)
		{
			Message.Begin(destination, Message.GetUserMessageID("AmmoX"), floatValue, playerEntity);
			
			Message.WriteByte(AmmoID);
			Message.WriteByte(Ammount);
			Message.End();
		}

		public static void SendAmmoXMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, AmmoXMessage val)
		{
			SendAmmoXMessage(destination, floatValue,playerEntity , val.AmmoID, val.Ammount);
		}

		public static void SendAmmoXMessage(MessageDestination destination, IntPtr playerEntity, AmmoXMessage val)
		{
			SendAmmoXMessage(destination, IntPtr.Zero, playerEntity , val.AmmoID, val.Ammount);
		}

		public static void SendAmmoXMessage(MessageDestination destination, AmmoXMessage val)
		{
			SendAmmoXMessage(destination, IntPtr.Zero, IntPtr.Zero , val.AmmoID, val.Ammount);
		}

		public static void SendAmmoXMessage(this Player player, IntPtr floatValue, AmmoXMessage val)
		{
			SendAmmoXMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendAmmoXMessage(this Player player, AmmoXMessage val)
		{
			SendAmmoXMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendAmmoXMessage(this Player player, byte AmmoID, byte Ammount)
		{
			SendAmmoXMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, AmmoID, Ammount);
		}

		#endregion
		
		#region ArmorType
		public static void SendArmorTypeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Flag)
		{
			Message.Begin(destination, Message.GetUserMessageID("ArmorType"), floatValue, playerEntity);
			
			Message.WriteByte(Flag);
			Message.End();
		}

		public static void SendArmorTypeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ArmorTypeMessage val)
		{
			SendArmorTypeMessage(destination, floatValue,playerEntity , val.Flag);
		}

		public static void SendArmorTypeMessage(MessageDestination destination, IntPtr playerEntity, ArmorTypeMessage val)
		{
			SendArmorTypeMessage(destination, IntPtr.Zero, playerEntity , val.Flag);
		}

		public static void SendArmorTypeMessage(MessageDestination destination, ArmorTypeMessage val)
		{
			SendArmorTypeMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Flag);
		}

		public static void SendArmorTypeMessage(this Player player, IntPtr floatValue, ArmorTypeMessage val)
		{
			SendArmorTypeMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendArmorTypeMessage(this Player player, ArmorTypeMessage val)
		{
			SendArmorTypeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendArmorTypeMessage(this Player player, byte Flag)
		{
			SendArmorTypeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Flag);
		}

		#endregion
		
		#region BarTime
		public static void SendBarTimeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, short Duration)
		{
			Message.Begin(destination, Message.GetUserMessageID("BarTime"), floatValue, playerEntity);
			
			Message.WriteShort(Duration);
			Message.End();
		}

		public static void SendBarTimeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, BarTimeMessage val)
		{
			SendBarTimeMessage(destination, floatValue,playerEntity , val.Duration);
		}

		public static void SendBarTimeMessage(MessageDestination destination, IntPtr playerEntity, BarTimeMessage val)
		{
			SendBarTimeMessage(destination, IntPtr.Zero, playerEntity , val.Duration);
		}

		public static void SendBarTimeMessage(MessageDestination destination, BarTimeMessage val)
		{
			SendBarTimeMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Duration);
		}

		public static void SendBarTimeMessage(this Player player, IntPtr floatValue, BarTimeMessage val)
		{
			SendBarTimeMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendBarTimeMessage(this Player player, BarTimeMessage val)
		{
			SendBarTimeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendBarTimeMessage(this Player player, short Duration)
		{
			SendBarTimeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Duration);
		}

		#endregion
		
		#region BarTime2
		public static void SendBarTime2Message(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, short Duratino, short startPercents)
		{
			Message.Begin(destination, Message.GetUserMessageID("BarTime2"), floatValue, playerEntity);
			
			Message.WriteShort(Duratino);
			Message.WriteShort(startPercents);
			Message.End();
		}

		public static void SendBarTime2Message(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, BarTime2Message val)
		{
			SendBarTime2Message(destination, floatValue,playerEntity , val.Duratino, val.startPercents);
		}

		public static void SendBarTime2Message(MessageDestination destination, IntPtr playerEntity, BarTime2Message val)
		{
			SendBarTime2Message(destination, IntPtr.Zero, playerEntity , val.Duratino, val.startPercents);
		}

		public static void SendBarTime2Message(MessageDestination destination, BarTime2Message val)
		{
			SendBarTime2Message(destination, IntPtr.Zero, IntPtr.Zero , val.Duratino, val.startPercents);
		}

		public static void SendBarTime2Message(this Player player, IntPtr floatValue, BarTime2Message val)
		{
			SendBarTime2Message(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendBarTime2Message(this Player player, BarTime2Message val)
		{
			SendBarTime2Message(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendBarTime2Message(this Player player, short Duratino, short startPercents)
		{
			SendBarTime2Message(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Duratino, startPercents);
		}

		#endregion
		
		#region Battery
		public static void SendBatteryMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, short Armor)
		{
			Message.Begin(destination, Message.GetUserMessageID("Battery"), floatValue, playerEntity);
			
			Message.WriteShort(Armor);
			Message.End();
		}

		public static void SendBatteryMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, BatteryMessage val)
		{
			SendBatteryMessage(destination, floatValue,playerEntity , val.Armor);
		}

		public static void SendBatteryMessage(MessageDestination destination, IntPtr playerEntity, BatteryMessage val)
		{
			SendBatteryMessage(destination, IntPtr.Zero, playerEntity , val.Armor);
		}

		public static void SendBatteryMessage(MessageDestination destination, BatteryMessage val)
		{
			SendBatteryMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Armor);
		}

		public static void SendBatteryMessage(this Player player, IntPtr floatValue, BatteryMessage val)
		{
			SendBatteryMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendBatteryMessage(this Player player, BatteryMessage val)
		{
			SendBatteryMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendBatteryMessage(this Player player, short Armor)
		{
			SendBatteryMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Armor);
		}

		#endregion
		
		#region BlinkAcct
		public static void SendBlinkAcctMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte BlinkAmt)
		{
			Message.Begin(destination, Message.GetUserMessageID("BlinkAcct"), floatValue, playerEntity);
			
			Message.WriteByte(BlinkAmt);
			Message.End();
		}

		public static void SendBlinkAcctMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, BlinkAcctMessage val)
		{
			SendBlinkAcctMessage(destination, floatValue,playerEntity , val.BlinkAmt);
		}

		public static void SendBlinkAcctMessage(MessageDestination destination, IntPtr playerEntity, BlinkAcctMessage val)
		{
			SendBlinkAcctMessage(destination, IntPtr.Zero, playerEntity , val.BlinkAmt);
		}

		public static void SendBlinkAcctMessage(MessageDestination destination, BlinkAcctMessage val)
		{
			SendBlinkAcctMessage(destination, IntPtr.Zero, IntPtr.Zero , val.BlinkAmt);
		}

		public static void SendBlinkAcctMessage(this Player player, IntPtr floatValue, BlinkAcctMessage val)
		{
			SendBlinkAcctMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendBlinkAcctMessage(this Player player, BlinkAcctMessage val)
		{
			SendBlinkAcctMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendBlinkAcctMessage(this Player player, byte BlinkAmt)
		{
			SendBlinkAcctMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, BlinkAmt);
		}

		#endregion
		
		#region BombDrop
		public static void SendBombDropMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, int CoordX, int CoordY, int CoordZ, byte Flag)
		{
			Message.Begin(destination, Message.GetUserMessageID("BombDrop"), floatValue, playerEntity);
			
			Message.WriteCoord(CoordX);
			Message.WriteCoord(CoordY);
			Message.WriteCoord(CoordZ);
			Message.WriteByte(Flag);
			Message.End();
		}

		public static void SendBombDropMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, BombDropMessage val)
		{
			SendBombDropMessage(destination, floatValue,playerEntity , val.CoordX, val.CoordY, val.CoordZ, val.Flag);
		}

		public static void SendBombDropMessage(MessageDestination destination, IntPtr playerEntity, BombDropMessage val)
		{
			SendBombDropMessage(destination, IntPtr.Zero, playerEntity , val.CoordX, val.CoordY, val.CoordZ, val.Flag);
		}

		public static void SendBombDropMessage(MessageDestination destination, BombDropMessage val)
		{
			SendBombDropMessage(destination, IntPtr.Zero, IntPtr.Zero , val.CoordX, val.CoordY, val.CoordZ, val.Flag);
		}

		public static void SendBombDropMessage(this Player player, IntPtr floatValue, BombDropMessage val)
		{
			SendBombDropMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendBombDropMessage(this Player player, BombDropMessage val)
		{
			SendBombDropMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendBombDropMessage(this Player player, int CoordX, int CoordY, int CoordZ, byte Flag)
		{
			SendBombDropMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, CoordX, CoordY, CoordZ, Flag);
		}

		#endregion
		
		#region BombPickup
		public static void SendBombPickupMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity)
		{
			Message.Begin(destination, Message.GetUserMessageID("BombPickup"), floatValue, playerEntity);
			
			Message.End();
		}

		public static void SendBombPickupMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, BombPickupMessage val)
		{
			SendBombPickupMessage(destination, floatValue,playerEntity );
		}

		public static void SendBombPickupMessage(MessageDestination destination, IntPtr playerEntity, BombPickupMessage val)
		{
			SendBombPickupMessage(destination, IntPtr.Zero, playerEntity );
		}

		public static void SendBombPickupMessage(MessageDestination destination, BombPickupMessage val)
		{
			SendBombPickupMessage(destination, IntPtr.Zero, IntPtr.Zero );
		}

		public static void SendBombPickupMessage(this Player player, IntPtr floatValue, BombPickupMessage val)
		{
			SendBombPickupMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendBombPickupMessage(this Player player, BombPickupMessage val)
		{
			SendBombPickupMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendBombPickupMessage(this Player player)
		{
			SendBombPickupMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer);
		}

		#endregion
		
		#region BotProgress
		public static void SendBotProgressMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Flag, byte Progress, string Header)
		{
			Message.Begin(destination, Message.GetUserMessageID("BotProgress"), floatValue, playerEntity);
			
			Message.WriteByte(Flag);
			Message.WriteByte(Progress);
			Message.WriteString(Header);
			Message.End();
		}

		public static void SendBotProgressMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, BotProgressMessage val)
		{
			SendBotProgressMessage(destination, floatValue,playerEntity , val.Flag, val.Progress, val.Header);
		}

		public static void SendBotProgressMessage(MessageDestination destination, IntPtr playerEntity, BotProgressMessage val)
		{
			SendBotProgressMessage(destination, IntPtr.Zero, playerEntity , val.Flag, val.Progress, val.Header);
		}

		public static void SendBotProgressMessage(MessageDestination destination, BotProgressMessage val)
		{
			SendBotProgressMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Flag, val.Progress, val.Header);
		}

		public static void SendBotProgressMessage(this Player player, IntPtr floatValue, BotProgressMessage val)
		{
			SendBotProgressMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendBotProgressMessage(this Player player, BotProgressMessage val)
		{
			SendBotProgressMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendBotProgressMessage(this Player player, byte Flag, byte Progress, string Header)
		{
			SendBotProgressMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Flag, Progress, Header);
		}

		#endregion
		
		#region BotVoice
		public static void SendBotVoiceMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Status, byte PlayerIndex)
		{
			Message.Begin(destination, Message.GetUserMessageID("BotVoice"), floatValue, playerEntity);
			
			Message.WriteByte(Status);
			Message.WriteByte(PlayerIndex);
			Message.End();
		}

		public static void SendBotVoiceMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, BotVoiceMessage val)
		{
			SendBotVoiceMessage(destination, floatValue,playerEntity , val.Status, val.PlayerIndex);
		}

		public static void SendBotVoiceMessage(MessageDestination destination, IntPtr playerEntity, BotVoiceMessage val)
		{
			SendBotVoiceMessage(destination, IntPtr.Zero, playerEntity , val.Status, val.PlayerIndex);
		}

		public static void SendBotVoiceMessage(MessageDestination destination, BotVoiceMessage val)
		{
			SendBotVoiceMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Status, val.PlayerIndex);
		}

		public static void SendBotVoiceMessage(this Player player, IntPtr floatValue, BotVoiceMessage val)
		{
			SendBotVoiceMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendBotVoiceMessage(this Player player, BotVoiceMessage val)
		{
			SendBotVoiceMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendBotVoiceMessage(this Player player, byte Status, byte PlayerIndex)
		{
			SendBotVoiceMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Status, PlayerIndex);
		}

		#endregion
		
		#region Brass
		public static void SendBrassMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Unknown, int StartX, int StartY, int StartZ, int VelocityX, int VelocityY, int VelocityZ, int UnknownX, int UnknownY, int UnknownZ, int Life, short Model, byte Unknownb1, byte Unknownb2, byte Unknownb3)
		{
			Message.Begin(destination, Message.GetUserMessageID("Brass"), floatValue, playerEntity);
			
			Message.WriteByte(Unknown);
			Message.WriteCoord(StartX);
			Message.WriteCoord(StartY);
			Message.WriteCoord(StartZ);
			Message.WriteCoord(VelocityX);
			Message.WriteCoord(VelocityY);
			Message.WriteCoord(VelocityZ);
			Message.WriteCoord(UnknownX);
			Message.WriteCoord(UnknownY);
			Message.WriteCoord(UnknownZ);
			Message.WriteAngle(Life);
			Message.WriteShort(Model);
			Message.WriteByte(Unknownb1);
			Message.WriteByte(Unknownb2);
			Message.WriteByte(Unknownb3);
			Message.End();
		}

		public static void SendBrassMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, BrassMessage val)
		{
			SendBrassMessage(destination, floatValue,playerEntity , val.Unknown, val.StartX, val.StartY, val.StartZ, val.VelocityX, val.VelocityY, val.VelocityZ, val.UnknownX, val.UnknownY, val.UnknownZ, val.Life, val.Model, val.Unknownb1, val.Unknownb2, val.Unknownb3);
		}

		public static void SendBrassMessage(MessageDestination destination, IntPtr playerEntity, BrassMessage val)
		{
			SendBrassMessage(destination, IntPtr.Zero, playerEntity , val.Unknown, val.StartX, val.StartY, val.StartZ, val.VelocityX, val.VelocityY, val.VelocityZ, val.UnknownX, val.UnknownY, val.UnknownZ, val.Life, val.Model, val.Unknownb1, val.Unknownb2, val.Unknownb3);
		}

		public static void SendBrassMessage(MessageDestination destination, BrassMessage val)
		{
			SendBrassMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Unknown, val.StartX, val.StartY, val.StartZ, val.VelocityX, val.VelocityY, val.VelocityZ, val.UnknownX, val.UnknownY, val.UnknownZ, val.Life, val.Model, val.Unknownb1, val.Unknownb2, val.Unknownb3);
		}

		public static void SendBrassMessage(this Player player, IntPtr floatValue, BrassMessage val)
		{
			SendBrassMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendBrassMessage(this Player player, BrassMessage val)
		{
			SendBrassMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendBrassMessage(this Player player, byte Unknown, int StartX, int StartY, int StartZ, int VelocityX, int VelocityY, int VelocityZ, int UnknownX, int UnknownY, int UnknownZ, int Life, short Model, byte Unknownb1, byte Unknownb2, byte Unknownb3)
		{
			SendBrassMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Unknown, StartX, StartY, StartZ, VelocityX, VelocityY, VelocityZ, UnknownX, UnknownY, UnknownZ, Life, Model, Unknownb1, Unknownb2, Unknownb3);
		}

		#endregion
		
		#region BuyClose
		public static void SendBuyCloseMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity)
		{
			Message.Begin(destination, Message.GetUserMessageID("BuyClose"), floatValue, playerEntity);
			
			Message.End();
		}

		public static void SendBuyCloseMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, BuyCloseMessage val)
		{
			SendBuyCloseMessage(destination, floatValue,playerEntity );
		}

		public static void SendBuyCloseMessage(MessageDestination destination, IntPtr playerEntity, BuyCloseMessage val)
		{
			SendBuyCloseMessage(destination, IntPtr.Zero, playerEntity );
		}

		public static void SendBuyCloseMessage(MessageDestination destination, BuyCloseMessage val)
		{
			SendBuyCloseMessage(destination, IntPtr.Zero, IntPtr.Zero );
		}

		public static void SendBuyCloseMessage(this Player player, IntPtr floatValue, BuyCloseMessage val)
		{
			SendBuyCloseMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendBuyCloseMessage(this Player player, BuyCloseMessage val)
		{
			SendBuyCloseMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendBuyCloseMessage(this Player player)
		{
			SendBuyCloseMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer);
		}

		#endregion
		
		#region ClCorpse
		public static void SendClCorpseMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, string ModelName, long CoordX, long CoordY, long CoordZ, int Angle0, int Angle1, int Angle2, long Delay, byte Sequence, byte ClassID, byte TeamID, byte PlayerID)
		{
			Message.Begin(destination, Message.GetUserMessageID("ClCorpse"), floatValue, playerEntity);
			
			Message.WriteString(ModelName);
			Message.WriteLong(CoordX);
			Message.WriteLong(CoordY);
			Message.WriteLong(CoordZ);
			Message.WriteCoord(Angle0);
			Message.WriteCoord(Angle1);
			Message.WriteCoord(Angle2);
			Message.WriteLong(Delay);
			Message.WriteByte(Sequence);
			Message.WriteByte(ClassID);
			Message.WriteByte(TeamID);
			Message.WriteByte(PlayerID);
			Message.End();
		}

		public static void SendClCorpseMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ClCorpseMessage val)
		{
			SendClCorpseMessage(destination, floatValue,playerEntity , val.ModelName, val.CoordX, val.CoordY, val.CoordZ, val.Angle0, val.Angle1, val.Angle2, val.Delay, val.Sequence, val.ClassID, val.TeamID, val.PlayerID);
		}

		public static void SendClCorpseMessage(MessageDestination destination, IntPtr playerEntity, ClCorpseMessage val)
		{
			SendClCorpseMessage(destination, IntPtr.Zero, playerEntity , val.ModelName, val.CoordX, val.CoordY, val.CoordZ, val.Angle0, val.Angle1, val.Angle2, val.Delay, val.Sequence, val.ClassID, val.TeamID, val.PlayerID);
		}

		public static void SendClCorpseMessage(MessageDestination destination, ClCorpseMessage val)
		{
			SendClCorpseMessage(destination, IntPtr.Zero, IntPtr.Zero , val.ModelName, val.CoordX, val.CoordY, val.CoordZ, val.Angle0, val.Angle1, val.Angle2, val.Delay, val.Sequence, val.ClassID, val.TeamID, val.PlayerID);
		}

		public static void SendClCorpseMessage(this Player player, IntPtr floatValue, ClCorpseMessage val)
		{
			SendClCorpseMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendClCorpseMessage(this Player player, ClCorpseMessage val)
		{
			SendClCorpseMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendClCorpseMessage(this Player player, string ModelName, long CoordX, long CoordY, long CoordZ, int Angle0, int Angle1, int Angle2, long Delay, byte Sequence, byte ClassID, byte TeamID, byte PlayerID)
		{
			SendClCorpseMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, ModelName, CoordX, CoordY, CoordZ, Angle0, Angle1, Angle2, Delay, Sequence, ClassID, TeamID, PlayerID);
		}

		#endregion
		
		#region Crosshair
		public static void SendCrosshairMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Flag)
		{
			Message.Begin(destination, Message.GetUserMessageID("Crosshair"), floatValue, playerEntity);
			
			Message.WriteByte(Flag);
			Message.End();
		}

		public static void SendCrosshairMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, CrosshairMessage val)
		{
			SendCrosshairMessage(destination, floatValue,playerEntity , val.Flag);
		}

		public static void SendCrosshairMessage(MessageDestination destination, IntPtr playerEntity, CrosshairMessage val)
		{
			SendCrosshairMessage(destination, IntPtr.Zero, playerEntity , val.Flag);
		}

		public static void SendCrosshairMessage(MessageDestination destination, CrosshairMessage val)
		{
			SendCrosshairMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Flag);
		}

		public static void SendCrosshairMessage(this Player player, IntPtr floatValue, CrosshairMessage val)
		{
			SendCrosshairMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendCrosshairMessage(this Player player, CrosshairMessage val)
		{
			SendCrosshairMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendCrosshairMessage(this Player player, byte Flag)
		{
			SendCrosshairMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Flag);
		}

		#endregion
		
		#region CurWeapon
		public static void SendCurWeaponMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte IsActive, byte WeaponID, byte ClipAmmo)
		{
			Message.Begin(destination, Message.GetUserMessageID("CurWeapon"), floatValue, playerEntity);
			
			Message.WriteByte(IsActive);
			Message.WriteByte(WeaponID);
			Message.WriteByte(ClipAmmo);
			Message.End();
		}

		public static void SendCurWeaponMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, CurWeaponMessage val)
		{
			SendCurWeaponMessage(destination, floatValue,playerEntity , val.IsActive, val.WeaponID, val.ClipAmmo);
		}

		public static void SendCurWeaponMessage(MessageDestination destination, IntPtr playerEntity, CurWeaponMessage val)
		{
			SendCurWeaponMessage(destination, IntPtr.Zero, playerEntity , val.IsActive, val.WeaponID, val.ClipAmmo);
		}

		public static void SendCurWeaponMessage(MessageDestination destination, CurWeaponMessage val)
		{
			SendCurWeaponMessage(destination, IntPtr.Zero, IntPtr.Zero , val.IsActive, val.WeaponID, val.ClipAmmo);
		}

		public static void SendCurWeaponMessage(this Player player, IntPtr floatValue, CurWeaponMessage val)
		{
			SendCurWeaponMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendCurWeaponMessage(this Player player, CurWeaponMessage val)
		{
			SendCurWeaponMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendCurWeaponMessage(this Player player, byte IsActive, byte WeaponID, byte ClipAmmo)
		{
			SendCurWeaponMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, IsActive, WeaponID, ClipAmmo);
		}

		#endregion
		
		#region CZCareer
		public static void SendCZCareerMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, string Type, int Parameters)
		{
			Message.Begin(destination, Message.GetUserMessageID("CZCareer"), floatValue, playerEntity);
			
			Message.WriteString(Type);
			Message.WriteLong(Parameters);
			Message.End();
		}

		public static void SendCZCareerMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, CZCareerMessage val)
		{
			SendCZCareerMessage(destination, floatValue,playerEntity , val.Type, val.Parameters);
		}

		public static void SendCZCareerMessage(MessageDestination destination, IntPtr playerEntity, CZCareerMessage val)
		{
			SendCZCareerMessage(destination, IntPtr.Zero, playerEntity , val.Type, val.Parameters);
		}

		public static void SendCZCareerMessage(MessageDestination destination, CZCareerMessage val)
		{
			SendCZCareerMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Type, val.Parameters);
		}

		public static void SendCZCareerMessage(this Player player, IntPtr floatValue, CZCareerMessage val)
		{
			SendCZCareerMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendCZCareerMessage(this Player player, CZCareerMessage val)
		{
			SendCZCareerMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendCZCareerMessage(this Player player, string Type, int Parameters)
		{
			SendCZCareerMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Type, Parameters);
		}

		#endregion
		
		#region CZCareerHUD
		public static void SendCZCareerHUDMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, string Type, int Parameters)
		{
			Message.Begin(destination, Message.GetUserMessageID("CZCareerHUD"), floatValue, playerEntity);
			
			Message.WriteString(Type);
			Message.WriteLong(Parameters);
			Message.End();
		}

		public static void SendCZCareerHUDMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, CZCareerHUDMessage val)
		{
			SendCZCareerHUDMessage(destination, floatValue,playerEntity , val.Type, val.Parameters);
		}

		public static void SendCZCareerHUDMessage(MessageDestination destination, IntPtr playerEntity, CZCareerHUDMessage val)
		{
			SendCZCareerHUDMessage(destination, IntPtr.Zero, playerEntity , val.Type, val.Parameters);
		}

		public static void SendCZCareerHUDMessage(MessageDestination destination, CZCareerHUDMessage val)
		{
			SendCZCareerHUDMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Type, val.Parameters);
		}

		public static void SendCZCareerHUDMessage(this Player player, IntPtr floatValue, CZCareerHUDMessage val)
		{
			SendCZCareerHUDMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendCZCareerHUDMessage(this Player player, CZCareerHUDMessage val)
		{
			SendCZCareerHUDMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendCZCareerHUDMessage(this Player player, string Type, int Parameters)
		{
			SendCZCareerHUDMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Type, Parameters);
		}

		#endregion
		
		#region Damage
		public static void SendDamageMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte DamageSave, byte DamageTake, long DamageType, int CoordX, int CoordY, int CoordZ)
		{
			Message.Begin(destination, Message.GetUserMessageID("Damage"), floatValue, playerEntity);
			
			Message.WriteByte(DamageSave);
			Message.WriteByte(DamageTake);
			Message.WriteLong(DamageType);
			Message.WriteCoord(CoordX);
			Message.WriteCoord(CoordY);
			Message.WriteCoord(CoordZ);
			Message.End();
		}

		public static void SendDamageMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, DamageMessage val)
		{
			SendDamageMessage(destination, floatValue,playerEntity , val.DamageSave, val.DamageTake, val.DamageType, val.CoordX, val.CoordY, val.CoordZ);
		}

		public static void SendDamageMessage(MessageDestination destination, IntPtr playerEntity, DamageMessage val)
		{
			SendDamageMessage(destination, IntPtr.Zero, playerEntity , val.DamageSave, val.DamageTake, val.DamageType, val.CoordX, val.CoordY, val.CoordZ);
		}

		public static void SendDamageMessage(MessageDestination destination, DamageMessage val)
		{
			SendDamageMessage(destination, IntPtr.Zero, IntPtr.Zero , val.DamageSave, val.DamageTake, val.DamageType, val.CoordX, val.CoordY, val.CoordZ);
		}

		public static void SendDamageMessage(this Player player, IntPtr floatValue, DamageMessage val)
		{
			SendDamageMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendDamageMessage(this Player player, DamageMessage val)
		{
			SendDamageMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendDamageMessage(this Player player, byte DamageSave, byte DamageTake, long DamageType, int CoordX, int CoordY, int CoordZ)
		{
			SendDamageMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, DamageSave, DamageTake, DamageType, CoordX, CoordY, CoordZ);
		}

		#endregion
		
		#region DeathMsg
		public static void SendDeathMsgMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte KillerID, byte VictimID, byte IsHeadshot, string TruncatedWeaponName)
		{
			Message.Begin(destination, Message.GetUserMessageID("DeathMsg"), floatValue, playerEntity);
			
			Message.WriteByte(KillerID);
			Message.WriteByte(VictimID);
			Message.WriteByte(IsHeadshot);
			Message.WriteString(TruncatedWeaponName);
			Message.End();
		}

		public static void SendDeathMsgMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, DeathMsgMessage val)
		{
			SendDeathMsgMessage(destination, floatValue,playerEntity , val.KillerID, val.VictimID, val.IsHeadshot, val.TruncatedWeaponName);
		}

		public static void SendDeathMsgMessage(MessageDestination destination, IntPtr playerEntity, DeathMsgMessage val)
		{
			SendDeathMsgMessage(destination, IntPtr.Zero, playerEntity , val.KillerID, val.VictimID, val.IsHeadshot, val.TruncatedWeaponName);
		}

		public static void SendDeathMsgMessage(MessageDestination destination, DeathMsgMessage val)
		{
			SendDeathMsgMessage(destination, IntPtr.Zero, IntPtr.Zero , val.KillerID, val.VictimID, val.IsHeadshot, val.TruncatedWeaponName);
		}

		public static void SendDeathMsgMessage(this Player player, IntPtr floatValue, DeathMsgMessage val)
		{
			SendDeathMsgMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendDeathMsgMessage(this Player player, DeathMsgMessage val)
		{
			SendDeathMsgMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendDeathMsgMessage(this Player player, byte KillerID, byte VictimID, byte IsHeadshot, string TruncatedWeaponName)
		{
			SendDeathMsgMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, KillerID, VictimID, IsHeadshot, TruncatedWeaponName);
		}

		#endregion
		
		#region FlashBat
		public static void SendFlashBatMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte ChargePercents)
		{
			Message.Begin(destination, Message.GetUserMessageID("FlashBat"), floatValue, playerEntity);
			
			Message.WriteByte(ChargePercents);
			Message.End();
		}

		public static void SendFlashBatMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, FlashBatMessage val)
		{
			SendFlashBatMessage(destination, floatValue,playerEntity , val.ChargePercents);
		}

		public static void SendFlashBatMessage(MessageDestination destination, IntPtr playerEntity, FlashBatMessage val)
		{
			SendFlashBatMessage(destination, IntPtr.Zero, playerEntity , val.ChargePercents);
		}

		public static void SendFlashBatMessage(MessageDestination destination, FlashBatMessage val)
		{
			SendFlashBatMessage(destination, IntPtr.Zero, IntPtr.Zero , val.ChargePercents);
		}

		public static void SendFlashBatMessage(this Player player, IntPtr floatValue, FlashBatMessage val)
		{
			SendFlashBatMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendFlashBatMessage(this Player player, FlashBatMessage val)
		{
			SendFlashBatMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendFlashBatMessage(this Player player, byte ChargePercents)
		{
			SendFlashBatMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, ChargePercents);
		}

		#endregion
		
		#region Flashlight
		public static void SendFlashlightMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Flag, byte ChargePercents)
		{
			Message.Begin(destination, Message.GetUserMessageID("Flashlight"), floatValue, playerEntity);
			
			Message.WriteByte(Flag);
			Message.WriteByte(ChargePercents);
			Message.End();
		}

		public static void SendFlashlightMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, FlashlightMessage val)
		{
			SendFlashlightMessage(destination, floatValue,playerEntity , val.Flag, val.ChargePercents);
		}

		public static void SendFlashlightMessage(MessageDestination destination, IntPtr playerEntity, FlashlightMessage val)
		{
			SendFlashlightMessage(destination, IntPtr.Zero, playerEntity , val.Flag, val.ChargePercents);
		}

		public static void SendFlashlightMessage(MessageDestination destination, FlashlightMessage val)
		{
			SendFlashlightMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Flag, val.ChargePercents);
		}

		public static void SendFlashlightMessage(this Player player, IntPtr floatValue, FlashlightMessage val)
		{
			SendFlashlightMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendFlashlightMessage(this Player player, FlashlightMessage val)
		{
			SendFlashlightMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendFlashlightMessage(this Player player, byte Flag, byte ChargePercents)
		{
			SendFlashlightMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Flag, ChargePercents);
		}

		#endregion
		
		#region Fog
		public static void SendFogMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte FogValue1, byte FogValue2, byte Unknown)
		{
			Message.Begin(destination, Message.GetUserMessageID("Fog"), floatValue, playerEntity);
			
			Message.WriteByte(FogValue1);
			Message.WriteByte(FogValue2);
			Message.WriteByte(Unknown);
			Message.End();
		}

		public static void SendFogMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, FogMessage val)
		{
			SendFogMessage(destination, floatValue,playerEntity , val.FogValue1, val.FogValue2, val.Unknown);
		}

		public static void SendFogMessage(MessageDestination destination, IntPtr playerEntity, FogMessage val)
		{
			SendFogMessage(destination, IntPtr.Zero, playerEntity , val.FogValue1, val.FogValue2, val.Unknown);
		}

		public static void SendFogMessage(MessageDestination destination, FogMessage val)
		{
			SendFogMessage(destination, IntPtr.Zero, IntPtr.Zero , val.FogValue1, val.FogValue2, val.Unknown);
		}

		public static void SendFogMessage(this Player player, IntPtr floatValue, FogMessage val)
		{
			SendFogMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendFogMessage(this Player player, FogMessage val)
		{
			SendFogMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendFogMessage(this Player player, byte FogValue1, byte FogValue2, byte Unknown)
		{
			SendFogMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, FogValue1, FogValue2, Unknown);
		}

		#endregion
		
		#region ForceCam
		public static void SendForceCamMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte ForececamValue, byte ForcechasecamValue, byte Unknown)
		{
			Message.Begin(destination, Message.GetUserMessageID("ForceCam"), floatValue, playerEntity);
			
			Message.WriteByte(ForececamValue);
			Message.WriteByte(ForcechasecamValue);
			Message.WriteByte(Unknown);
			Message.End();
		}

		public static void SendForceCamMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ForceCamMessage val)
		{
			SendForceCamMessage(destination, floatValue,playerEntity , val.ForececamValue, val.ForcechasecamValue, val.Unknown);
		}

		public static void SendForceCamMessage(MessageDestination destination, IntPtr playerEntity, ForceCamMessage val)
		{
			SendForceCamMessage(destination, IntPtr.Zero, playerEntity , val.ForececamValue, val.ForcechasecamValue, val.Unknown);
		}

		public static void SendForceCamMessage(MessageDestination destination, ForceCamMessage val)
		{
			SendForceCamMessage(destination, IntPtr.Zero, IntPtr.Zero , val.ForececamValue, val.ForcechasecamValue, val.Unknown);
		}

		public static void SendForceCamMessage(this Player player, IntPtr floatValue, ForceCamMessage val)
		{
			SendForceCamMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendForceCamMessage(this Player player, ForceCamMessage val)
		{
			SendForceCamMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendForceCamMessage(this Player player, byte ForececamValue, byte ForcechasecamValue, byte Unknown)
		{
			SendForceCamMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, ForececamValue, ForcechasecamValue, Unknown);
		}

		#endregion
		
		#region GameMode
		public static void SendGameModeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Unknown)
		{
			Message.Begin(destination, Message.GetUserMessageID("GameMode"), floatValue, playerEntity);
			
			Message.WriteByte(Unknown);
			Message.End();
		}

		public static void SendGameModeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, GameModeMessage val)
		{
			SendGameModeMessage(destination, floatValue,playerEntity , val.Unknown);
		}

		public static void SendGameModeMessage(MessageDestination destination, IntPtr playerEntity, GameModeMessage val)
		{
			SendGameModeMessage(destination, IntPtr.Zero, playerEntity , val.Unknown);
		}

		public static void SendGameModeMessage(MessageDestination destination, GameModeMessage val)
		{
			SendGameModeMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Unknown);
		}

		public static void SendGameModeMessage(this Player player, IntPtr floatValue, GameModeMessage val)
		{
			SendGameModeMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendGameModeMessage(this Player player, GameModeMessage val)
		{
			SendGameModeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendGameModeMessage(this Player player, byte Unknown)
		{
			SendGameModeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Unknown);
		}

		#endregion
		
		#region GameTitle
		public static void SendGameTitleMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity)
		{
			Message.Begin(destination, Message.GetUserMessageID("GameTitle"), floatValue, playerEntity);
			
			Message.End();
		}

		public static void SendGameTitleMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, GameTitleMessage val)
		{
			SendGameTitleMessage(destination, floatValue,playerEntity );
		}

		public static void SendGameTitleMessage(MessageDestination destination, IntPtr playerEntity, GameTitleMessage val)
		{
			SendGameTitleMessage(destination, IntPtr.Zero, playerEntity );
		}

		public static void SendGameTitleMessage(MessageDestination destination, GameTitleMessage val)
		{
			SendGameTitleMessage(destination, IntPtr.Zero, IntPtr.Zero );
		}

		public static void SendGameTitleMessage(this Player player, IntPtr floatValue, GameTitleMessage val)
		{
			SendGameTitleMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendGameTitleMessage(this Player player, GameTitleMessage val)
		{
			SendGameTitleMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendGameTitleMessage(this Player player)
		{
			SendGameTitleMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer);
		}

		#endregion
		
		#region Geiger
		public static void SendGeigerMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Distance)
		{
			Message.Begin(destination, Message.GetUserMessageID("Geiger"), floatValue, playerEntity);
			
			Message.WriteByte(Distance);
			Message.End();
		}

		public static void SendGeigerMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, GeigerMessage val)
		{
			SendGeigerMessage(destination, floatValue,playerEntity , val.Distance);
		}

		public static void SendGeigerMessage(MessageDestination destination, IntPtr playerEntity, GeigerMessage val)
		{
			SendGeigerMessage(destination, IntPtr.Zero, playerEntity , val.Distance);
		}

		public static void SendGeigerMessage(MessageDestination destination, GeigerMessage val)
		{
			SendGeigerMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Distance);
		}

		public static void SendGeigerMessage(this Player player, IntPtr floatValue, GeigerMessage val)
		{
			SendGeigerMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendGeigerMessage(this Player player, GeigerMessage val)
		{
			SendGeigerMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendGeigerMessage(this Player player, byte Distance)
		{
			SendGeigerMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Distance);
		}

		#endregion
		
		#region Health
		public static void SendHealthMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Health)
		{
			Message.Begin(destination, Message.GetUserMessageID("Health"), floatValue, playerEntity);
			
			Message.WriteByte(Health);
			Message.End();
		}

		public static void SendHealthMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, HealthMessage val)
		{
			SendHealthMessage(destination, floatValue,playerEntity , val.Health);
		}

		public static void SendHealthMessage(MessageDestination destination, IntPtr playerEntity, HealthMessage val)
		{
			SendHealthMessage(destination, IntPtr.Zero, playerEntity , val.Health);
		}

		public static void SendHealthMessage(MessageDestination destination, HealthMessage val)
		{
			SendHealthMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Health);
		}

		public static void SendHealthMessage(this Player player, IntPtr floatValue, HealthMessage val)
		{
			SendHealthMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendHealthMessage(this Player player, HealthMessage val)
		{
			SendHealthMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendHealthMessage(this Player player, byte Health)
		{
			SendHealthMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Health);
		}

		#endregion
		
		#region HideWeapon
		public static void SendHideWeaponMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Flags)
		{
			Message.Begin(destination, Message.GetUserMessageID("HideWeapon"), floatValue, playerEntity);
			
			Message.WriteByte(Flags);
			Message.End();
		}

		public static void SendHideWeaponMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, HideWeaponMessage val)
		{
			SendHideWeaponMessage(destination, floatValue,playerEntity , val.Flags);
		}

		public static void SendHideWeaponMessage(MessageDestination destination, IntPtr playerEntity, HideWeaponMessage val)
		{
			SendHideWeaponMessage(destination, IntPtr.Zero, playerEntity , val.Flags);
		}

		public static void SendHideWeaponMessage(MessageDestination destination, HideWeaponMessage val)
		{
			SendHideWeaponMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Flags);
		}

		public static void SendHideWeaponMessage(this Player player, IntPtr floatValue, HideWeaponMessage val)
		{
			SendHideWeaponMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendHideWeaponMessage(this Player player, HideWeaponMessage val)
		{
			SendHideWeaponMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendHideWeaponMessage(this Player player, byte Flags)
		{
			SendHideWeaponMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Flags);
		}

		#endregion
		
		#region HLTV
		public static void SendHLTVMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte ClientID, byte Flags)
		{
			Message.Begin(destination, Message.GetUserMessageID("HLTV"), floatValue, playerEntity);
			
			Message.WriteByte(ClientID);
			Message.WriteByte(Flags);
			Message.End();
		}

		public static void SendHLTVMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, HLTVMessage val)
		{
			SendHLTVMessage(destination, floatValue,playerEntity , val.ClientID, val.Flags);
		}

		public static void SendHLTVMessage(MessageDestination destination, IntPtr playerEntity, HLTVMessage val)
		{
			SendHLTVMessage(destination, IntPtr.Zero, playerEntity , val.ClientID, val.Flags);
		}

		public static void SendHLTVMessage(MessageDestination destination, HLTVMessage val)
		{
			SendHLTVMessage(destination, IntPtr.Zero, IntPtr.Zero , val.ClientID, val.Flags);
		}

		public static void SendHLTVMessage(this Player player, IntPtr floatValue, HLTVMessage val)
		{
			SendHLTVMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendHLTVMessage(this Player player, HLTVMessage val)
		{
			SendHLTVMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendHLTVMessage(this Player player, byte ClientID, byte Flags)
		{
			SendHLTVMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, ClientID, Flags);
		}

		#endregion
		
		#region HostageK
		public static void SendHostageKMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte HostageID)
		{
			Message.Begin(destination, Message.GetUserMessageID("HostageK"), floatValue, playerEntity);
			
			Message.WriteByte(HostageID);
			Message.End();
		}

		public static void SendHostageKMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, HostageKMessage val)
		{
			SendHostageKMessage(destination, floatValue,playerEntity , val.HostageID);
		}

		public static void SendHostageKMessage(MessageDestination destination, IntPtr playerEntity, HostageKMessage val)
		{
			SendHostageKMessage(destination, IntPtr.Zero, playerEntity , val.HostageID);
		}

		public static void SendHostageKMessage(MessageDestination destination, HostageKMessage val)
		{
			SendHostageKMessage(destination, IntPtr.Zero, IntPtr.Zero , val.HostageID);
		}

		public static void SendHostageKMessage(this Player player, IntPtr floatValue, HostageKMessage val)
		{
			SendHostageKMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendHostageKMessage(this Player player, HostageKMessage val)
		{
			SendHostageKMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendHostageKMessage(this Player player, byte HostageID)
		{
			SendHostageKMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, HostageID);
		}

		#endregion
		
		#region HostagePos
		public static void SendHostagePosMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Flag, byte HostageID, int CoordX, int CoordY, int CoordZ)
		{
			Message.Begin(destination, Message.GetUserMessageID("HostagePos"), floatValue, playerEntity);
			
			Message.WriteByte(Flag);
			Message.WriteByte(HostageID);
			Message.WriteCoord(CoordX);
			Message.WriteCoord(CoordY);
			Message.WriteCoord(CoordZ);
			Message.End();
		}

		public static void SendHostagePosMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, HostagePosMessage val)
		{
			SendHostagePosMessage(destination, floatValue,playerEntity , val.Flag, val.HostageID, val.CoordX, val.CoordY, val.CoordZ);
		}

		public static void SendHostagePosMessage(MessageDestination destination, IntPtr playerEntity, HostagePosMessage val)
		{
			SendHostagePosMessage(destination, IntPtr.Zero, playerEntity , val.Flag, val.HostageID, val.CoordX, val.CoordY, val.CoordZ);
		}

		public static void SendHostagePosMessage(MessageDestination destination, HostagePosMessage val)
		{
			SendHostagePosMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Flag, val.HostageID, val.CoordX, val.CoordY, val.CoordZ);
		}

		public static void SendHostagePosMessage(this Player player, IntPtr floatValue, HostagePosMessage val)
		{
			SendHostagePosMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendHostagePosMessage(this Player player, HostagePosMessage val)
		{
			SendHostagePosMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendHostagePosMessage(this Player player, byte Flag, byte HostageID, int CoordX, int CoordY, int CoordZ)
		{
			SendHostagePosMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Flag, HostageID, CoordX, CoordY, CoordZ);
		}

		#endregion
		
		#region HudText
		public static void SendHudTextMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity)
		{
			Message.Begin(destination, Message.GetUserMessageID("HudText"), floatValue, playerEntity);
			
			Message.End();
		}

		public static void SendHudTextMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, HudTextMessage val)
		{
			SendHudTextMessage(destination, floatValue,playerEntity );
		}

		public static void SendHudTextMessage(MessageDestination destination, IntPtr playerEntity, HudTextMessage val)
		{
			SendHudTextMessage(destination, IntPtr.Zero, playerEntity );
		}

		public static void SendHudTextMessage(MessageDestination destination, HudTextMessage val)
		{
			SendHudTextMessage(destination, IntPtr.Zero, IntPtr.Zero );
		}

		public static void SendHudTextMessage(this Player player, IntPtr floatValue, HudTextMessage val)
		{
			SendHudTextMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendHudTextMessage(this Player player, HudTextMessage val)
		{
			SendHudTextMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendHudTextMessage(this Player player)
		{
			SendHudTextMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer);
		}

		#endregion
		
		#region HudTextArgs
		public static void SendHudTextArgsMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, string TextCode, byte Unknown1, byte Unknown2)
		{
			Message.Begin(destination, Message.GetUserMessageID("HudTextArgs"), floatValue, playerEntity);
			
			Message.WriteString(TextCode);
			Message.WriteByte(Unknown1);
			Message.WriteByte(Unknown2);
			Message.End();
		}

		public static void SendHudTextArgsMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, HudTextArgsMessage val)
		{
			SendHudTextArgsMessage(destination, floatValue,playerEntity , val.TextCode, val.Unknown1, val.Unknown2);
		}

		public static void SendHudTextArgsMessage(MessageDestination destination, IntPtr playerEntity, HudTextArgsMessage val)
		{
			SendHudTextArgsMessage(destination, IntPtr.Zero, playerEntity , val.TextCode, val.Unknown1, val.Unknown2);
		}

		public static void SendHudTextArgsMessage(MessageDestination destination, HudTextArgsMessage val)
		{
			SendHudTextArgsMessage(destination, IntPtr.Zero, IntPtr.Zero , val.TextCode, val.Unknown1, val.Unknown2);
		}

		public static void SendHudTextArgsMessage(this Player player, IntPtr floatValue, HudTextArgsMessage val)
		{
			SendHudTextArgsMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendHudTextArgsMessage(this Player player, HudTextArgsMessage val)
		{
			SendHudTextArgsMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendHudTextArgsMessage(this Player player, string TextCode, byte Unknown1, byte Unknown2)
		{
			SendHudTextArgsMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, TextCode, Unknown1, Unknown2);
		}

		#endregion
		
		#region HudTextPro
		public static void SendHudTextProMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity)
		{
			Message.Begin(destination, Message.GetUserMessageID("HudTextPro"), floatValue, playerEntity);
			
			Message.End();
		}

		public static void SendHudTextProMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, HudTextProMessage val)
		{
			SendHudTextProMessage(destination, floatValue,playerEntity );
		}

		public static void SendHudTextProMessage(MessageDestination destination, IntPtr playerEntity, HudTextProMessage val)
		{
			SendHudTextProMessage(destination, IntPtr.Zero, playerEntity );
		}

		public static void SendHudTextProMessage(MessageDestination destination, HudTextProMessage val)
		{
			SendHudTextProMessage(destination, IntPtr.Zero, IntPtr.Zero );
		}

		public static void SendHudTextProMessage(this Player player, IntPtr floatValue, HudTextProMessage val)
		{
			SendHudTextProMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendHudTextProMessage(this Player player, HudTextProMessage val)
		{
			SendHudTextProMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendHudTextProMessage(this Player player)
		{
			SendHudTextProMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer);
		}

		#endregion
		
		#region InitHUD
		public static void SendInitHUDMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity)
		{
			Message.Begin(destination, Message.GetUserMessageID("InitHUD"), floatValue, playerEntity);
			
			Message.End();
		}

		public static void SendInitHUDMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, InitHUDMessage val)
		{
			SendInitHUDMessage(destination, floatValue,playerEntity );
		}

		public static void SendInitHUDMessage(MessageDestination destination, IntPtr playerEntity, InitHUDMessage val)
		{
			SendInitHUDMessage(destination, IntPtr.Zero, playerEntity );
		}

		public static void SendInitHUDMessage(MessageDestination destination, InitHUDMessage val)
		{
			SendInitHUDMessage(destination, IntPtr.Zero, IntPtr.Zero );
		}

		public static void SendInitHUDMessage(this Player player, IntPtr floatValue, InitHUDMessage val)
		{
			SendInitHUDMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendInitHUDMessage(this Player player, InitHUDMessage val)
		{
			SendInitHUDMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendInitHUDMessage(this Player player)
		{
			SendInitHUDMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer);
		}

		#endregion
		
		#region ItemPickup
		public static void SendItemPickupMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, string ItemName)
		{
			Message.Begin(destination, Message.GetUserMessageID("ItemPickup"), floatValue, playerEntity);
			
			Message.WriteString(ItemName);
			Message.End();
		}

		public static void SendItemPickupMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ItemPickupMessage val)
		{
			SendItemPickupMessage(destination, floatValue,playerEntity , val.ItemName);
		}

		public static void SendItemPickupMessage(MessageDestination destination, IntPtr playerEntity, ItemPickupMessage val)
		{
			SendItemPickupMessage(destination, IntPtr.Zero, playerEntity , val.ItemName);
		}

		public static void SendItemPickupMessage(MessageDestination destination, ItemPickupMessage val)
		{
			SendItemPickupMessage(destination, IntPtr.Zero, IntPtr.Zero , val.ItemName);
		}

		public static void SendItemPickupMessage(this Player player, IntPtr floatValue, ItemPickupMessage val)
		{
			SendItemPickupMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendItemPickupMessage(this Player player, ItemPickupMessage val)
		{
			SendItemPickupMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendItemPickupMessage(this Player player, string ItemName)
		{
			SendItemPickupMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, ItemName);
		}

		#endregion
		
		#region ItemStatus
		public static void SendItemStatusMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte ItemBitSum)
		{
			Message.Begin(destination, Message.GetUserMessageID("ItemStatus"), floatValue, playerEntity);
			
			Message.WriteByte(ItemBitSum);
			Message.End();
		}

		public static void SendItemStatusMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ItemStatusMessage val)
		{
			SendItemStatusMessage(destination, floatValue,playerEntity , val.ItemBitSum);
		}

		public static void SendItemStatusMessage(MessageDestination destination, IntPtr playerEntity, ItemStatusMessage val)
		{
			SendItemStatusMessage(destination, IntPtr.Zero, playerEntity , val.ItemBitSum);
		}

		public static void SendItemStatusMessage(MessageDestination destination, ItemStatusMessage val)
		{
			SendItemStatusMessage(destination, IntPtr.Zero, IntPtr.Zero , val.ItemBitSum);
		}

		public static void SendItemStatusMessage(this Player player, IntPtr floatValue, ItemStatusMessage val)
		{
			SendItemStatusMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendItemStatusMessage(this Player player, ItemStatusMessage val)
		{
			SendItemStatusMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendItemStatusMessage(this Player player, byte ItemBitSum)
		{
			SendItemStatusMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, ItemBitSum);
		}

		#endregion
		
		#region Location
		public static void SendLocationMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Money)
		{
			Message.Begin(destination, Message.GetUserMessageID("Location"), floatValue, playerEntity);
			
			Message.WriteByte(Money);
			Message.End();
		}

		public static void SendLocationMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, LocationMessage val)
		{
			SendLocationMessage(destination, floatValue,playerEntity , val.Money);
		}

		public static void SendLocationMessage(MessageDestination destination, IntPtr playerEntity, LocationMessage val)
		{
			SendLocationMessage(destination, IntPtr.Zero, playerEntity , val.Money);
		}

		public static void SendLocationMessage(MessageDestination destination, LocationMessage val)
		{
			SendLocationMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Money);
		}

		public static void SendLocationMessage(this Player player, IntPtr floatValue, LocationMessage val)
		{
			SendLocationMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendLocationMessage(this Player player, LocationMessage val)
		{
			SendLocationMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendLocationMessage(this Player player, byte Money)
		{
			SendLocationMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Money);
		}

		#endregion
		
		#region Money
		public static void SendMoneyMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, long Amount, byte Flag)
		{
			Message.Begin(destination, Message.GetUserMessageID("Money"), floatValue, playerEntity);
			
			Message.WriteLong(Amount);
			Message.WriteByte(Flag);
			Message.End();
		}

		public static void SendMoneyMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, MoneyMessage val)
		{
			SendMoneyMessage(destination, floatValue,playerEntity , val.Amount, val.Flag);
		}

		public static void SendMoneyMessage(MessageDestination destination, IntPtr playerEntity, MoneyMessage val)
		{
			SendMoneyMessage(destination, IntPtr.Zero, playerEntity , val.Amount, val.Flag);
		}

		public static void SendMoneyMessage(MessageDestination destination, MoneyMessage val)
		{
			SendMoneyMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Amount, val.Flag);
		}

		public static void SendMoneyMessage(this Player player, IntPtr floatValue, MoneyMessage val)
		{
			SendMoneyMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendMoneyMessage(this Player player, MoneyMessage val)
		{
			SendMoneyMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendMoneyMessage(this Player player, long Amount, byte Flag)
		{
			SendMoneyMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Amount, Flag);
		}

		#endregion
		
		#region MOTD
		public static void SendMOTDMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Flag, string Text)
		{
			Message.Begin(destination, Message.GetUserMessageID("MOTD"), floatValue, playerEntity);
			
			Message.WriteByte(Flag);
			Message.WriteString(Text);
			Message.End();
		}

		public static void SendMOTDMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, MOTDMessage val)
		{
			SendMOTDMessage(destination, floatValue,playerEntity , val.Flag, val.Text);
		}

		public static void SendMOTDMessage(MessageDestination destination, IntPtr playerEntity, MOTDMessage val)
		{
			SendMOTDMessage(destination, IntPtr.Zero, playerEntity , val.Flag, val.Text);
		}

		public static void SendMOTDMessage(MessageDestination destination, MOTDMessage val)
		{
			SendMOTDMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Flag, val.Text);
		}

		public static void SendMOTDMessage(this Player player, IntPtr floatValue, MOTDMessage val)
		{
			SendMOTDMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendMOTDMessage(this Player player, MOTDMessage val)
		{
			SendMOTDMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendMOTDMessage(this Player player, byte Flag, string Text)
		{
			SendMOTDMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Flag, Text);
		}

		#endregion
		
		#region NVGToggle
		public static void SendNVGToggleMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Flag)
		{
			Message.Begin(destination, Message.GetUserMessageID("NVGToggle"), floatValue, playerEntity);
			
			Message.WriteByte(Flag);
			Message.End();
		}

		public static void SendNVGToggleMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, NVGToggleMessage val)
		{
			SendNVGToggleMessage(destination, floatValue,playerEntity , val.Flag);
		}

		public static void SendNVGToggleMessage(MessageDestination destination, IntPtr playerEntity, NVGToggleMessage val)
		{
			SendNVGToggleMessage(destination, IntPtr.Zero, playerEntity , val.Flag);
		}

		public static void SendNVGToggleMessage(MessageDestination destination, NVGToggleMessage val)
		{
			SendNVGToggleMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Flag);
		}

		public static void SendNVGToggleMessage(this Player player, IntPtr floatValue, NVGToggleMessage val)
		{
			SendNVGToggleMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendNVGToggleMessage(this Player player, NVGToggleMessage val)
		{
			SendNVGToggleMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendNVGToggleMessage(this Player player, byte Flag)
		{
			SendNVGToggleMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Flag);
		}

		#endregion
		
		#region Radar
		public static void SendRadarMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte PlayerID, int CoordX, int CoordY, int CoordZ)
		{
			Message.Begin(destination, Message.GetUserMessageID("Radar"), floatValue, playerEntity);
			
			Message.WriteByte(PlayerID);
			Message.WriteCoord(CoordX);
			Message.WriteCoord(CoordY);
			Message.WriteCoord(CoordZ);
			Message.End();
		}

		public static void SendRadarMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, RadarMessage val)
		{
			SendRadarMessage(destination, floatValue,playerEntity , val.PlayerID, val.CoordX, val.CoordY, val.CoordZ);
		}

		public static void SendRadarMessage(MessageDestination destination, IntPtr playerEntity, RadarMessage val)
		{
			SendRadarMessage(destination, IntPtr.Zero, playerEntity , val.PlayerID, val.CoordX, val.CoordY, val.CoordZ);
		}

		public static void SendRadarMessage(MessageDestination destination, RadarMessage val)
		{
			SendRadarMessage(destination, IntPtr.Zero, IntPtr.Zero , val.PlayerID, val.CoordX, val.CoordY, val.CoordZ);
		}

		public static void SendRadarMessage(this Player player, IntPtr floatValue, RadarMessage val)
		{
			SendRadarMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendRadarMessage(this Player player, RadarMessage val)
		{
			SendRadarMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendRadarMessage(this Player player, byte PlayerID, int CoordX, int CoordY, int CoordZ)
		{
			SendRadarMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, PlayerID, CoordX, CoordY, CoordZ);
		}

		#endregion
		
		#region ReceiveW
		public static void SendReceiveWMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity)
		{
			Message.Begin(destination, Message.GetUserMessageID("ReceiveW"), floatValue, playerEntity);
			
			Message.End();
		}

		public static void SendReceiveWMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ReceiveWMessage val)
		{
			SendReceiveWMessage(destination, floatValue,playerEntity );
		}

		public static void SendReceiveWMessage(MessageDestination destination, IntPtr playerEntity, ReceiveWMessage val)
		{
			SendReceiveWMessage(destination, IntPtr.Zero, playerEntity );
		}

		public static void SendReceiveWMessage(MessageDestination destination, ReceiveWMessage val)
		{
			SendReceiveWMessage(destination, IntPtr.Zero, IntPtr.Zero );
		}

		public static void SendReceiveWMessage(this Player player, IntPtr floatValue, ReceiveWMessage val)
		{
			SendReceiveWMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendReceiveWMessage(this Player player, ReceiveWMessage val)
		{
			SendReceiveWMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendReceiveWMessage(this Player player)
		{
			SendReceiveWMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer);
		}

		#endregion
		
		#region ReloadSound
		public static void SendReloadSoundMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Unknown1, byte Unknown2)
		{
			Message.Begin(destination, Message.GetUserMessageID("ReloadSound"), floatValue, playerEntity);
			
			Message.WriteByte(Unknown1);
			Message.WriteByte(Unknown2);
			Message.End();
		}

		public static void SendReloadSoundMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ReloadSoundMessage val)
		{
			SendReloadSoundMessage(destination, floatValue,playerEntity , val.Unknown1, val.Unknown2);
		}

		public static void SendReloadSoundMessage(MessageDestination destination, IntPtr playerEntity, ReloadSoundMessage val)
		{
			SendReloadSoundMessage(destination, IntPtr.Zero, playerEntity , val.Unknown1, val.Unknown2);
		}

		public static void SendReloadSoundMessage(MessageDestination destination, ReloadSoundMessage val)
		{
			SendReloadSoundMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Unknown1, val.Unknown2);
		}

		public static void SendReloadSoundMessage(this Player player, IntPtr floatValue, ReloadSoundMessage val)
		{
			SendReloadSoundMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendReloadSoundMessage(this Player player, ReloadSoundMessage val)
		{
			SendReloadSoundMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendReloadSoundMessage(this Player player, byte Unknown1, byte Unknown2)
		{
			SendReloadSoundMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Unknown1, Unknown2);
		}

		#endregion
		
		#region ReqState
		public static void SendReqStateMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity)
		{
			Message.Begin(destination, Message.GetUserMessageID("ReqState"), floatValue, playerEntity);
			
			Message.End();
		}

		public static void SendReqStateMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ReqStateMessage val)
		{
			SendReqStateMessage(destination, floatValue,playerEntity );
		}

		public static void SendReqStateMessage(MessageDestination destination, IntPtr playerEntity, ReqStateMessage val)
		{
			SendReqStateMessage(destination, IntPtr.Zero, playerEntity );
		}

		public static void SendReqStateMessage(MessageDestination destination, ReqStateMessage val)
		{
			SendReqStateMessage(destination, IntPtr.Zero, IntPtr.Zero );
		}

		public static void SendReqStateMessage(this Player player, IntPtr floatValue, ReqStateMessage val)
		{
			SendReqStateMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendReqStateMessage(this Player player, ReqStateMessage val)
		{
			SendReqStateMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendReqStateMessage(this Player player)
		{
			SendReqStateMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer);
		}

		#endregion
		
		#region ResetHUD
		public static void SendResetHUDMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity)
		{
			Message.Begin(destination, Message.GetUserMessageID("ResetHUD"), floatValue, playerEntity);
			
			Message.End();
		}

		public static void SendResetHUDMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ResetHUDMessage val)
		{
			SendResetHUDMessage(destination, floatValue,playerEntity );
		}

		public static void SendResetHUDMessage(MessageDestination destination, IntPtr playerEntity, ResetHUDMessage val)
		{
			SendResetHUDMessage(destination, IntPtr.Zero, playerEntity );
		}

		public static void SendResetHUDMessage(MessageDestination destination, ResetHUDMessage val)
		{
			SendResetHUDMessage(destination, IntPtr.Zero, IntPtr.Zero );
		}

		public static void SendResetHUDMessage(this Player player, IntPtr floatValue, ResetHUDMessage val)
		{
			SendResetHUDMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendResetHUDMessage(this Player player, ResetHUDMessage val)
		{
			SendResetHUDMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendResetHUDMessage(this Player player)
		{
			SendResetHUDMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer);
		}

		#endregion
		
		#region RoundTime
		public static void SendRoundTimeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, short Time)
		{
			Message.Begin(destination, Message.GetUserMessageID("RoundTime"), floatValue, playerEntity);
			
			Message.WriteShort(Time);
			Message.End();
		}

		public static void SendRoundTimeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, RoundTimeMessage val)
		{
			SendRoundTimeMessage(destination, floatValue,playerEntity , val.Time);
		}

		public static void SendRoundTimeMessage(MessageDestination destination, IntPtr playerEntity, RoundTimeMessage val)
		{
			SendRoundTimeMessage(destination, IntPtr.Zero, playerEntity , val.Time);
		}

		public static void SendRoundTimeMessage(MessageDestination destination, RoundTimeMessage val)
		{
			SendRoundTimeMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Time);
		}

		public static void SendRoundTimeMessage(this Player player, IntPtr floatValue, RoundTimeMessage val)
		{
			SendRoundTimeMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendRoundTimeMessage(this Player player, RoundTimeMessage val)
		{
			SendRoundTimeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendRoundTimeMessage(this Player player, short Time)
		{
			SendRoundTimeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Time);
		}

		#endregion
		
		#region SayText
		public static void SendSayTextMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte SenderID, string String1, string String2 = null, string String3 = null)
		{
			Message.Begin(destination, Message.GetUserMessageID("SayText"), floatValue, playerEntity);
			
			Message.WriteByte(SenderID);
			Message.WriteString(String1);
			Message.WriteString(String2);
			Message.WriteString(String3);
			Message.End();
		}

		public static void SendSayTextMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, SayTextMessage val)
		{
			SendSayTextMessage(destination, floatValue,playerEntity , val.SenderID, val.String1, val.String2, val.String3);
		}

		public static void SendSayTextMessage(MessageDestination destination, IntPtr playerEntity, SayTextMessage val)
		{
			SendSayTextMessage(destination, IntPtr.Zero, playerEntity , val.SenderID, val.String1, val.String2, val.String3);
		}

		public static void SendSayTextMessage(MessageDestination destination, SayTextMessage val)
		{
			SendSayTextMessage(destination, IntPtr.Zero, IntPtr.Zero , val.SenderID, val.String1, val.String2, val.String3);
		}

		public static void SendSayTextMessage(this Player player, IntPtr floatValue, SayTextMessage val)
		{
			SendSayTextMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendSayTextMessage(this Player player, SayTextMessage val)
		{
			SendSayTextMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendSayTextMessage(this Player player, byte SenderID, string String1, string String2 = null, string String3 = null)
		{
			SendSayTextMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, SenderID, String1, String2, String3);
		}

		#endregion
		
		#region Scenario
		public static void SendScenarioMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Active, string Sprite, byte Alpha, short FlashRate, short Unknown)
		{
			Message.Begin(destination, Message.GetUserMessageID("Scenario"), floatValue, playerEntity);
			
			Message.WriteByte(Active);
			Message.WriteString(Sprite);
			Message.WriteByte(Alpha);
			Message.WriteShort(FlashRate);
			Message.WriteShort(Unknown);
			Message.End();
		}

		public static void SendScenarioMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ScenarioMessage val)
		{
			SendScenarioMessage(destination, floatValue,playerEntity , val.Active, val.Sprite, val.Alpha, val.FlashRate, val.Unknown);
		}

		public static void SendScenarioMessage(MessageDestination destination, IntPtr playerEntity, ScenarioMessage val)
		{
			SendScenarioMessage(destination, IntPtr.Zero, playerEntity , val.Active, val.Sprite, val.Alpha, val.FlashRate, val.Unknown);
		}

		public static void SendScenarioMessage(MessageDestination destination, ScenarioMessage val)
		{
			SendScenarioMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Active, val.Sprite, val.Alpha, val.FlashRate, val.Unknown);
		}

		public static void SendScenarioMessage(this Player player, IntPtr floatValue, ScenarioMessage val)
		{
			SendScenarioMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendScenarioMessage(this Player player, ScenarioMessage val)
		{
			SendScenarioMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendScenarioMessage(this Player player, byte Active, string Sprite, byte Alpha, short FlashRate, short Unknown)
		{
			SendScenarioMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Active, Sprite, Alpha, FlashRate, Unknown);
		}

		#endregion
		
		#region ScoreAttrib
		public static void SendScoreAttribMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte PlayerID, byte Flags)
		{
			Message.Begin(destination, Message.GetUserMessageID("ScoreAttrib"), floatValue, playerEntity);
			
			Message.WriteByte(PlayerID);
			Message.WriteByte(Flags);
			Message.End();
		}

		public static void SendScoreAttribMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ScoreAttribMessage val)
		{
			SendScoreAttribMessage(destination, floatValue,playerEntity , val.PlayerID, val.Flags);
		}

		public static void SendScoreAttribMessage(MessageDestination destination, IntPtr playerEntity, ScoreAttribMessage val)
		{
			SendScoreAttribMessage(destination, IntPtr.Zero, playerEntity , val.PlayerID, val.Flags);
		}

		public static void SendScoreAttribMessage(MessageDestination destination, ScoreAttribMessage val)
		{
			SendScoreAttribMessage(destination, IntPtr.Zero, IntPtr.Zero , val.PlayerID, val.Flags);
		}

		public static void SendScoreAttribMessage(this Player player, IntPtr floatValue, ScoreAttribMessage val)
		{
			SendScoreAttribMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendScoreAttribMessage(this Player player, ScoreAttribMessage val)
		{
			SendScoreAttribMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendScoreAttribMessage(this Player player, byte PlayerID, byte Flags)
		{
			SendScoreAttribMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, PlayerID, Flags);
		}

		#endregion
		
		#region ScoreInfo
		public static void SendScoreInfoMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte PlayerID, short Frags, short Deaths, short ClassID, short TeamID)
		{
			Message.Begin(destination, Message.GetUserMessageID("ScoreInfo"), floatValue, playerEntity);
			
			Message.WriteByte(PlayerID);
			Message.WriteShort(Frags);
			Message.WriteShort(Deaths);
			Message.WriteShort(ClassID);
			Message.WriteShort(TeamID);
			Message.End();
		}

		public static void SendScoreInfoMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ScoreInfoMessage val)
		{
			SendScoreInfoMessage(destination, floatValue,playerEntity , val.PlayerID, val.Frags, val.Deaths, val.ClassID, val.TeamID);
		}

		public static void SendScoreInfoMessage(MessageDestination destination, IntPtr playerEntity, ScoreInfoMessage val)
		{
			SendScoreInfoMessage(destination, IntPtr.Zero, playerEntity , val.PlayerID, val.Frags, val.Deaths, val.ClassID, val.TeamID);
		}

		public static void SendScoreInfoMessage(MessageDestination destination, ScoreInfoMessage val)
		{
			SendScoreInfoMessage(destination, IntPtr.Zero, IntPtr.Zero , val.PlayerID, val.Frags, val.Deaths, val.ClassID, val.TeamID);
		}

		public static void SendScoreInfoMessage(this Player player, IntPtr floatValue, ScoreInfoMessage val)
		{
			SendScoreInfoMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendScoreInfoMessage(this Player player, ScoreInfoMessage val)
		{
			SendScoreInfoMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendScoreInfoMessage(this Player player, byte PlayerID, short Frags, short Deaths, short ClassID, short TeamID)
		{
			SendScoreInfoMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, PlayerID, Frags, Deaths, ClassID, TeamID);
		}

		#endregion
		
		#region ScreenFade
		public static void SendScreenFadeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, short Duration, short HoldTime, short Flags, byte ColorR, byte ColorG, byte ColorB, byte Alpha)
		{
			Message.Begin(destination, Message.GetUserMessageID("ScreenFade"), floatValue, playerEntity);
			
			Message.WriteShort(Duration);
			Message.WriteShort(HoldTime);
			Message.WriteShort(Flags);
			Message.WriteByte(ColorR);
			Message.WriteByte(ColorG);
			Message.WriteByte(ColorB);
			Message.WriteByte(Alpha);
			Message.End();
		}

		public static void SendScreenFadeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ScreenFadeMessage val)
		{
			SendScreenFadeMessage(destination, floatValue,playerEntity , val.Duration, val.HoldTime, val.Flags, val.ColorR, val.ColorG, val.ColorB, val.Alpha);
		}

		public static void SendScreenFadeMessage(MessageDestination destination, IntPtr playerEntity, ScreenFadeMessage val)
		{
			SendScreenFadeMessage(destination, IntPtr.Zero, playerEntity , val.Duration, val.HoldTime, val.Flags, val.ColorR, val.ColorG, val.ColorB, val.Alpha);
		}

		public static void SendScreenFadeMessage(MessageDestination destination, ScreenFadeMessage val)
		{
			SendScreenFadeMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Duration, val.HoldTime, val.Flags, val.ColorR, val.ColorG, val.ColorB, val.Alpha);
		}

		public static void SendScreenFadeMessage(this Player player, IntPtr floatValue, ScreenFadeMessage val)
		{
			SendScreenFadeMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendScreenFadeMessage(this Player player, ScreenFadeMessage val)
		{
			SendScreenFadeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendScreenFadeMessage(this Player player, short Duration, short HoldTime, short Flags, byte ColorR, byte ColorG, byte ColorB, byte Alpha)
		{
			SendScreenFadeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Duration, HoldTime, Flags, ColorR, ColorG, ColorB, Alpha);
		}

		#endregion
		
		#region ScreenShake
		public static void SendScreenShakeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, short Amplitude, short Duration, short Frequency)
		{
			Message.Begin(destination, Message.GetUserMessageID("ScreenShake"), floatValue, playerEntity);
			
			Message.WriteShort(Amplitude);
			Message.WriteShort(Duration);
			Message.WriteShort(Frequency);
			Message.End();
		}

		public static void SendScreenShakeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ScreenShakeMessage val)
		{
			SendScreenShakeMessage(destination, floatValue,playerEntity , val.Amplitude, val.Duration, val.Frequency);
		}

		public static void SendScreenShakeMessage(MessageDestination destination, IntPtr playerEntity, ScreenShakeMessage val)
		{
			SendScreenShakeMessage(destination, IntPtr.Zero, playerEntity , val.Amplitude, val.Duration, val.Frequency);
		}

		public static void SendScreenShakeMessage(MessageDestination destination, ScreenShakeMessage val)
		{
			SendScreenShakeMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Amplitude, val.Duration, val.Frequency);
		}

		public static void SendScreenShakeMessage(this Player player, IntPtr floatValue, ScreenShakeMessage val)
		{
			SendScreenShakeMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendScreenShakeMessage(this Player player, ScreenShakeMessage val)
		{
			SendScreenShakeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendScreenShakeMessage(this Player player, short Amplitude, short Duration, short Frequency)
		{
			SendScreenShakeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Amplitude, Duration, Frequency);
		}

		#endregion
		
		#region SendAudio
		public static void SendSendAudioMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte SenderID, string AduioCode, short Pitch)
		{
			Message.Begin(destination, Message.GetUserMessageID("SendAudio"), floatValue, playerEntity);
			
			Message.WriteByte(SenderID);
			Message.WriteString(AduioCode);
			Message.WriteShort(Pitch);
			Message.End();
		}

		public static void SendSendAudioMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, SendAudioMessage val)
		{
			SendSendAudioMessage(destination, floatValue,playerEntity , val.SenderID, val.AduioCode, val.Pitch);
		}

		public static void SendSendAudioMessage(MessageDestination destination, IntPtr playerEntity, SendAudioMessage val)
		{
			SendSendAudioMessage(destination, IntPtr.Zero, playerEntity , val.SenderID, val.AduioCode, val.Pitch);
		}

		public static void SendSendAudioMessage(MessageDestination destination, SendAudioMessage val)
		{
			SendSendAudioMessage(destination, IntPtr.Zero, IntPtr.Zero , val.SenderID, val.AduioCode, val.Pitch);
		}

		public static void SendSendAudioMessage(this Player player, IntPtr floatValue, SendAudioMessage val)
		{
			SendSendAudioMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendSendAudioMessage(this Player player, SendAudioMessage val)
		{
			SendSendAudioMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendSendAudioMessage(this Player player, byte SenderID, string AduioCode, short Pitch)
		{
			SendSendAudioMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, SenderID, AduioCode, Pitch);
		}

		#endregion
		
		#region ServerName
		public static void SendServerNameMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, string ServerName)
		{
			Message.Begin(destination, Message.GetUserMessageID("ServerName"), floatValue, playerEntity);
			
			Message.WriteString(ServerName);
			Message.End();
		}

		public static void SendServerNameMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ServerNameMessage val)
		{
			SendServerNameMessage(destination, floatValue,playerEntity , val.ServerName);
		}

		public static void SendServerNameMessage(MessageDestination destination, IntPtr playerEntity, ServerNameMessage val)
		{
			SendServerNameMessage(destination, IntPtr.Zero, playerEntity , val.ServerName);
		}

		public static void SendServerNameMessage(MessageDestination destination, ServerNameMessage val)
		{
			SendServerNameMessage(destination, IntPtr.Zero, IntPtr.Zero , val.ServerName);
		}

		public static void SendServerNameMessage(this Player player, IntPtr floatValue, ServerNameMessage val)
		{
			SendServerNameMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendServerNameMessage(this Player player, ServerNameMessage val)
		{
			SendServerNameMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendServerNameMessage(this Player player, string ServerName)
		{
			SendServerNameMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, ServerName);
		}

		#endregion
		
		#region SetFOV
		public static void SendSetFOVMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Degrees)
		{
			Message.Begin(destination, Message.GetUserMessageID("SetFOV"), floatValue, playerEntity);
			
			Message.WriteByte(Degrees);
			Message.End();
		}

		public static void SendSetFOVMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, SetFOVMessage val)
		{
			SendSetFOVMessage(destination, floatValue,playerEntity , val.Degrees);
		}

		public static void SendSetFOVMessage(MessageDestination destination, IntPtr playerEntity, SetFOVMessage val)
		{
			SendSetFOVMessage(destination, IntPtr.Zero, playerEntity , val.Degrees);
		}

		public static void SendSetFOVMessage(MessageDestination destination, SetFOVMessage val)
		{
			SendSetFOVMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Degrees);
		}

		public static void SendSetFOVMessage(this Player player, IntPtr floatValue, SetFOVMessage val)
		{
			SendSetFOVMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendSetFOVMessage(this Player player, SetFOVMessage val)
		{
			SendSetFOVMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendSetFOVMessage(this Player player, byte Degrees)
		{
			SendSetFOVMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Degrees);
		}

		#endregion
		
		#region ShadowIdx
		public static void SendShadowIdxMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, long Unknown)
		{
			Message.Begin(destination, Message.GetUserMessageID("ShadowIdx"), floatValue, playerEntity);
			
			Message.WriteLong(Unknown);
			Message.End();
		}

		public static void SendShadowIdxMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ShadowIdxMessage val)
		{
			SendShadowIdxMessage(destination, floatValue,playerEntity , val.Unknown);
		}

		public static void SendShadowIdxMessage(MessageDestination destination, IntPtr playerEntity, ShadowIdxMessage val)
		{
			SendShadowIdxMessage(destination, IntPtr.Zero, playerEntity , val.Unknown);
		}

		public static void SendShadowIdxMessage(MessageDestination destination, ShadowIdxMessage val)
		{
			SendShadowIdxMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Unknown);
		}

		public static void SendShadowIdxMessage(this Player player, IntPtr floatValue, ShadowIdxMessage val)
		{
			SendShadowIdxMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendShadowIdxMessage(this Player player, ShadowIdxMessage val)
		{
			SendShadowIdxMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendShadowIdxMessage(this Player player, long Unknown)
		{
			SendShadowIdxMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Unknown);
		}

		#endregion
		
		#region ShowMenu
		public static void SendShowMenuMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, short KeyBitSum, char Time, byte MultiPart, string Text)
		{
			Message.Begin(destination, Message.GetUserMessageID("ShowMenu"), floatValue, playerEntity);
			
			Message.WriteShort(KeyBitSum);
			Message.WriteChar(Time);
			Message.WriteByte(MultiPart);
			Message.WriteString(Text);
			Message.End();
		}

		public static void SendShowMenuMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ShowMenuMessage val)
		{
			SendShowMenuMessage(destination, floatValue,playerEntity , val.KeyBitSum, val.Time, val.MultiPart, val.Text);
		}

		public static void SendShowMenuMessage(MessageDestination destination, IntPtr playerEntity, ShowMenuMessage val)
		{
			SendShowMenuMessage(destination, IntPtr.Zero, playerEntity , val.KeyBitSum, val.Time, val.MultiPart, val.Text);
		}

		public static void SendShowMenuMessage(MessageDestination destination, ShowMenuMessage val)
		{
			SendShowMenuMessage(destination, IntPtr.Zero, IntPtr.Zero , val.KeyBitSum, val.Time, val.MultiPart, val.Text);
		}

		public static void SendShowMenuMessage(this Player player, IntPtr floatValue, ShowMenuMessage val)
		{
			SendShowMenuMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendShowMenuMessage(this Player player, ShowMenuMessage val)
		{
			SendShowMenuMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendShowMenuMessage(this Player player, short KeyBitSum, char Time, byte MultiPart, string Text)
		{
			SendShowMenuMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, KeyBitSum, Time, MultiPart, Text);
		}

		#endregion
		
		#region ShowTimer
		public static void SendShowTimerMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity)
		{
			Message.Begin(destination, Message.GetUserMessageID("ShowTimer"), floatValue, playerEntity);
			
			Message.End();
		}

		public static void SendShowTimerMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ShowTimerMessage val)
		{
			SendShowTimerMessage(destination, floatValue,playerEntity );
		}

		public static void SendShowTimerMessage(MessageDestination destination, IntPtr playerEntity, ShowTimerMessage val)
		{
			SendShowTimerMessage(destination, IntPtr.Zero, playerEntity );
		}

		public static void SendShowTimerMessage(MessageDestination destination, ShowTimerMessage val)
		{
			SendShowTimerMessage(destination, IntPtr.Zero, IntPtr.Zero );
		}

		public static void SendShowTimerMessage(this Player player, IntPtr floatValue, ShowTimerMessage val)
		{
			SendShowTimerMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendShowTimerMessage(this Player player, ShowTimerMessage val)
		{
			SendShowTimerMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendShowTimerMessage(this Player player)
		{
			SendShowTimerMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer);
		}

		#endregion
		
		#region SpecHealth
		public static void SendSpecHealthMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Health)
		{
			Message.Begin(destination, Message.GetUserMessageID("SpecHealth"), floatValue, playerEntity);
			
			Message.WriteByte(Health);
			Message.End();
		}

		public static void SendSpecHealthMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, SpecHealthMessage val)
		{
			SendSpecHealthMessage(destination, floatValue,playerEntity , val.Health);
		}

		public static void SendSpecHealthMessage(MessageDestination destination, IntPtr playerEntity, SpecHealthMessage val)
		{
			SendSpecHealthMessage(destination, IntPtr.Zero, playerEntity , val.Health);
		}

		public static void SendSpecHealthMessage(MessageDestination destination, SpecHealthMessage val)
		{
			SendSpecHealthMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Health);
		}

		public static void SendSpecHealthMessage(this Player player, IntPtr floatValue, SpecHealthMessage val)
		{
			SendSpecHealthMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendSpecHealthMessage(this Player player, SpecHealthMessage val)
		{
			SendSpecHealthMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendSpecHealthMessage(this Player player, byte Health)
		{
			SendSpecHealthMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Health);
		}

		#endregion
		
		#region SpecHealth2
		public static void SendSpecHealth2Message(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Health, byte PlayerID)
		{
			Message.Begin(destination, Message.GetUserMessageID("SpecHealth2"), floatValue, playerEntity);
			
			Message.WriteByte(Health);
			Message.WriteByte(PlayerID);
			Message.End();
		}

		public static void SendSpecHealth2Message(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, SpecHealth2Message val)
		{
			SendSpecHealth2Message(destination, floatValue,playerEntity , val.Health, val.PlayerID);
		}

		public static void SendSpecHealth2Message(MessageDestination destination, IntPtr playerEntity, SpecHealth2Message val)
		{
			SendSpecHealth2Message(destination, IntPtr.Zero, playerEntity , val.Health, val.PlayerID);
		}

		public static void SendSpecHealth2Message(MessageDestination destination, SpecHealth2Message val)
		{
			SendSpecHealth2Message(destination, IntPtr.Zero, IntPtr.Zero , val.Health, val.PlayerID);
		}

		public static void SendSpecHealth2Message(this Player player, IntPtr floatValue, SpecHealth2Message val)
		{
			SendSpecHealth2Message(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendSpecHealth2Message(this Player player, SpecHealth2Message val)
		{
			SendSpecHealth2Message(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendSpecHealth2Message(this Player player, byte Health, byte PlayerID)
		{
			SendSpecHealth2Message(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Health, PlayerID);
		}

		#endregion
		
		#region Spectator
		public static void SendSpectatorMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte ClientID, byte Unknown)
		{
			Message.Begin(destination, Message.GetUserMessageID("Spectator"), floatValue, playerEntity);
			
			Message.WriteByte(ClientID);
			Message.WriteByte(Unknown);
			Message.End();
		}

		public static void SendSpectatorMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, SpectatorMessage val)
		{
			SendSpectatorMessage(destination, floatValue,playerEntity , val.ClientID, val.Unknown);
		}

		public static void SendSpectatorMessage(MessageDestination destination, IntPtr playerEntity, SpectatorMessage val)
		{
			SendSpectatorMessage(destination, IntPtr.Zero, playerEntity , val.ClientID, val.Unknown);
		}

		public static void SendSpectatorMessage(MessageDestination destination, SpectatorMessage val)
		{
			SendSpectatorMessage(destination, IntPtr.Zero, IntPtr.Zero , val.ClientID, val.Unknown);
		}

		public static void SendSpectatorMessage(this Player player, IntPtr floatValue, SpectatorMessage val)
		{
			SendSpectatorMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendSpectatorMessage(this Player player, SpectatorMessage val)
		{
			SendSpectatorMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendSpectatorMessage(this Player player, byte ClientID, byte Unknown)
		{
			SendSpectatorMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, ClientID, Unknown);
		}

		#endregion
		
		#region StatusIcon
		public static void SendStatusIconMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Status, string SpriteName, byte ColorR = 0, byte ColorG = 0, byte ColorB = 0)
		{
			Message.Begin(destination, Message.GetUserMessageID("StatusIcon"), floatValue, playerEntity);
			
      Message.WriteByte(Status);
      Message.WriteString(SpriteName);
      if (Status != 0)
      {
        Message.WriteByte(ColorR);
        Message.WriteByte(ColorG);
        Message.WriteByte(ColorB);
      }
			Message.End();
		}

		public static void SendStatusIconMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, StatusIconMessage val)
		{
			SendStatusIconMessage(destination, floatValue,playerEntity , val.Status, val.SpriteName, val.ColorR, val.ColorG, val.ColorB);
		}

		public static void SendStatusIconMessage(MessageDestination destination, IntPtr playerEntity, StatusIconMessage val)
		{
			SendStatusIconMessage(destination, IntPtr.Zero, playerEntity , val.Status, val.SpriteName, val.ColorR, val.ColorG, val.ColorB);
		}

		public static void SendStatusIconMessage(MessageDestination destination, StatusIconMessage val)
		{
			SendStatusIconMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Status, val.SpriteName, val.ColorR, val.ColorG, val.ColorB);
		}

		public static void SendStatusIconMessage(this Player player, IntPtr floatValue, StatusIconMessage val)
		{
			SendStatusIconMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendStatusIconMessage(this Player player, StatusIconMessage val)
		{
			SendStatusIconMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendStatusIconMessage(this Player player, byte Status, string SpriteName, byte ColorR = 0, byte ColorG = 0, byte ColorB = 0)
		{
			SendStatusIconMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Status, SpriteName, ColorR, ColorG, ColorB);
		}

		#endregion
		
		#region StatusValue
		public static void SendStatusValueMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Flag, short Value)
		{
			Message.Begin(destination, Message.GetUserMessageID("StatusValue"), floatValue, playerEntity);
			
			Message.WriteByte(Flag);
			Message.WriteShort(Value);
			Message.End();
		}

		public static void SendStatusValueMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, StatusValueMessage val)
		{
			SendStatusValueMessage(destination, floatValue,playerEntity , val.Flag, val.Value);
		}

		public static void SendStatusValueMessage(MessageDestination destination, IntPtr playerEntity, StatusValueMessage val)
		{
			SendStatusValueMessage(destination, IntPtr.Zero, playerEntity , val.Flag, val.Value);
		}

		public static void SendStatusValueMessage(MessageDestination destination, StatusValueMessage val)
		{
			SendStatusValueMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Flag, val.Value);
		}

		public static void SendStatusValueMessage(this Player player, IntPtr floatValue, StatusValueMessage val)
		{
			SendStatusValueMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendStatusValueMessage(this Player player, StatusValueMessage val)
		{
			SendStatusValueMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendStatusValueMessage(this Player player, byte Flag, short Value)
		{
			SendStatusValueMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Flag, Value);
		}

		#endregion
		
		#region StatusText
		public static void SendStatusTextMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Unknown, string Text)
		{
			Message.Begin(destination, Message.GetUserMessageID("StatusText"), floatValue, playerEntity);
			
			Message.WriteByte(Unknown);
			Message.WriteString(Text);
			Message.End();
		}

		public static void SendStatusTextMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, StatusTextMessage val)
		{
			SendStatusTextMessage(destination, floatValue,playerEntity , val.Unknown, val.Text);
		}

		public static void SendStatusTextMessage(MessageDestination destination, IntPtr playerEntity, StatusTextMessage val)
		{
			SendStatusTextMessage(destination, IntPtr.Zero, playerEntity , val.Unknown, val.Text);
		}

		public static void SendStatusTextMessage(MessageDestination destination, StatusTextMessage val)
		{
			SendStatusTextMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Unknown, val.Text);
		}

		public static void SendStatusTextMessage(this Player player, IntPtr floatValue, StatusTextMessage val)
		{
			SendStatusTextMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendStatusTextMessage(this Player player, StatusTextMessage val)
		{
			SendStatusTextMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendStatusTextMessage(this Player player, byte Unknown, string Text)
		{
			SendStatusTextMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Unknown, Text);
		}

		#endregion
		
		#region TaskTime
		public static void SendTaskTimeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, short Time, byte Active, byte Fade)
		{
			Message.Begin(destination, Message.GetUserMessageID("TaskTime"), floatValue, playerEntity);
			
			Message.WriteShort(Time);
			Message.WriteByte(Active);
			Message.WriteByte(Fade);
			Message.End();
		}

		public static void SendTaskTimeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, TaskTimeMessage val)
		{
			SendTaskTimeMessage(destination, floatValue,playerEntity , val.Time, val.Active, val.Fade);
		}

		public static void SendTaskTimeMessage(MessageDestination destination, IntPtr playerEntity, TaskTimeMessage val)
		{
			SendTaskTimeMessage(destination, IntPtr.Zero, playerEntity , val.Time, val.Active, val.Fade);
		}

		public static void SendTaskTimeMessage(MessageDestination destination, TaskTimeMessage val)
		{
			SendTaskTimeMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Time, val.Active, val.Fade);
		}

		public static void SendTaskTimeMessage(this Player player, IntPtr floatValue, TaskTimeMessage val)
		{
			SendTaskTimeMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendTaskTimeMessage(this Player player, TaskTimeMessage val)
		{
			SendTaskTimeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendTaskTimeMessage(this Player player, short Time, byte Active, byte Fade)
		{
			SendTaskTimeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Time, Active, Fade);
		}

		#endregion
		
		#region TeamInfo
		public static void SendTeamInfoMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte PlayerID, string TeamName)
		{
			Message.Begin(destination, Message.GetUserMessageID("TeamInfo"), floatValue, playerEntity);
			
			Message.WriteByte(PlayerID);
			Message.WriteString(TeamName);
			Message.End();
		}

		public static void SendTeamInfoMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, TeamInfoMessage val)
		{
			SendTeamInfoMessage(destination, floatValue,playerEntity , val.PlayerID, val.TeamName);
		}

		public static void SendTeamInfoMessage(MessageDestination destination, IntPtr playerEntity, TeamInfoMessage val)
		{
			SendTeamInfoMessage(destination, IntPtr.Zero, playerEntity , val.PlayerID, val.TeamName);
		}

		public static void SendTeamInfoMessage(MessageDestination destination, TeamInfoMessage val)
		{
			SendTeamInfoMessage(destination, IntPtr.Zero, IntPtr.Zero , val.PlayerID, val.TeamName);
		}

		public static void SendTeamInfoMessage(this Player player, IntPtr floatValue, TeamInfoMessage val)
		{
			SendTeamInfoMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendTeamInfoMessage(this Player player, TeamInfoMessage val)
		{
			SendTeamInfoMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendTeamInfoMessage(this Player player, byte PlayerID, string TeamName)
		{
			SendTeamInfoMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, PlayerID, TeamName);
		}

		#endregion
		
		#region TeamScore
		public static void SendTeamScoreMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, string Score)
		{
			Message.Begin(destination, Message.GetUserMessageID("TeamScore"), floatValue, playerEntity);
			
			Message.WriteString(Score);
			Message.End();
		}

		public static void SendTeamScoreMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, TeamScoreMessage val)
		{
			SendTeamScoreMessage(destination, floatValue,playerEntity , val.Score);
		}

		public static void SendTeamScoreMessage(MessageDestination destination, IntPtr playerEntity, TeamScoreMessage val)
		{
			SendTeamScoreMessage(destination, IntPtr.Zero, playerEntity , val.Score);
		}

		public static void SendTeamScoreMessage(MessageDestination destination, TeamScoreMessage val)
		{
			SendTeamScoreMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Score);
		}

		public static void SendTeamScoreMessage(this Player player, IntPtr floatValue, TeamScoreMessage val)
		{
			SendTeamScoreMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendTeamScoreMessage(this Player player, TeamScoreMessage val)
		{
			SendTeamScoreMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendTeamScoreMessage(this Player player, string Score)
		{
			SendTeamScoreMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Score);
		}

		#endregion
		
		#region TextMsg
		public static void SendTextMsgMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte DestinationType, string MessageContent, string Submsg1 = null, string Submsg2 = null, string Submsg3 = null, string Submsg4 = null)
		{
			Message.Begin(destination, Message.GetUserMessageID("TextMsg"), floatValue, playerEntity);
			
			Message.WriteByte(DestinationType);
			Message.WriteString(MessageContent);
			Message.WriteString(Submsg1);
			Message.WriteString(Submsg2);
			Message.WriteString(Submsg3);
			Message.WriteString(Submsg4);
			Message.End();
		}

		public static void SendTextMsgMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, TextMsgMessage val)
		{
			SendTextMsgMessage(destination, floatValue,playerEntity , val.DestinationType, val.MessageContent, val.Submsg1, val.Submsg2, val.Submsg3, val.Submsg4);
		}

		public static void SendTextMsgMessage(MessageDestination destination, IntPtr playerEntity, TextMsgMessage val)
		{
			SendTextMsgMessage(destination, IntPtr.Zero, playerEntity , val.DestinationType, val.MessageContent, val.Submsg1, val.Submsg2, val.Submsg3, val.Submsg4);
		}

		public static void SendTextMsgMessage(MessageDestination destination, TextMsgMessage val)
		{
			SendTextMsgMessage(destination, IntPtr.Zero, IntPtr.Zero , val.DestinationType, val.MessageContent, val.Submsg1, val.Submsg2, val.Submsg3, val.Submsg4);
		}

		public static void SendTextMsgMessage(this Player player, IntPtr floatValue, TextMsgMessage val)
		{
			SendTextMsgMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendTextMsgMessage(this Player player, TextMsgMessage val)
		{
			SendTextMsgMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendTextMsgMessage(this Player player, byte DestinationType, string MessageContent, string Submsg1 = null, string Submsg2 = null, string Submsg3 = null, string Submsg4 = null)
		{
			SendTextMsgMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, DestinationType, MessageContent, Submsg1, Submsg2, Submsg3, Submsg4);
		}

		#endregion
		
		#region Train
		public static void SendTrainMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte Speed)
		{
			Message.Begin(destination, Message.GetUserMessageID("Train"), floatValue, playerEntity);
			
			Message.WriteByte(Speed);
			Message.End();
		}

		public static void SendTrainMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, TrainMessage val)
		{
			SendTrainMessage(destination, floatValue,playerEntity , val.Speed);
		}

		public static void SendTrainMessage(MessageDestination destination, IntPtr playerEntity, TrainMessage val)
		{
			SendTrainMessage(destination, IntPtr.Zero, playerEntity , val.Speed);
		}

		public static void SendTrainMessage(MessageDestination destination, TrainMessage val)
		{
			SendTrainMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Speed);
		}

		public static void SendTrainMessage(this Player player, IntPtr floatValue, TrainMessage val)
		{
			SendTrainMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendTrainMessage(this Player player, TrainMessage val)
		{
			SendTrainMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendTrainMessage(this Player player, byte Speed)
		{
			SendTrainMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Speed);
		}

		#endregion
		
		#region TutorClose
		public static void SendTutorCloseMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity)
		{
			Message.Begin(destination, Message.GetUserMessageID("TutorClose"), floatValue, playerEntity);
			
			Message.End();
		}

		public static void SendTutorCloseMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, TutorCloseMessage val)
		{
			SendTutorCloseMessage(destination, floatValue,playerEntity );
		}

		public static void SendTutorCloseMessage(MessageDestination destination, IntPtr playerEntity, TutorCloseMessage val)
		{
			SendTutorCloseMessage(destination, IntPtr.Zero, playerEntity );
		}

		public static void SendTutorCloseMessage(MessageDestination destination, TutorCloseMessage val)
		{
			SendTutorCloseMessage(destination, IntPtr.Zero, IntPtr.Zero );
		}

		public static void SendTutorCloseMessage(this Player player, IntPtr floatValue, TutorCloseMessage val)
		{
			SendTutorCloseMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendTutorCloseMessage(this Player player, TutorCloseMessage val)
		{
			SendTutorCloseMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendTutorCloseMessage(this Player player)
		{
			SendTutorCloseMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer);
		}

		#endregion
		
		#region TutorLine
		public static void SendTutorLineMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity)
		{
			Message.Begin(destination, Message.GetUserMessageID("TutorLine"), floatValue, playerEntity);
			
			Message.End();
		}

		public static void SendTutorLineMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, TutorLineMessage val)
		{
			SendTutorLineMessage(destination, floatValue,playerEntity );
		}

		public static void SendTutorLineMessage(MessageDestination destination, IntPtr playerEntity, TutorLineMessage val)
		{
			SendTutorLineMessage(destination, IntPtr.Zero, playerEntity );
		}

		public static void SendTutorLineMessage(MessageDestination destination, TutorLineMessage val)
		{
			SendTutorLineMessage(destination, IntPtr.Zero, IntPtr.Zero );
		}

		public static void SendTutorLineMessage(this Player player, IntPtr floatValue, TutorLineMessage val)
		{
			SendTutorLineMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendTutorLineMessage(this Player player, TutorLineMessage val)
		{
			SendTutorLineMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendTutorLineMessage(this Player player)
		{
			SendTutorLineMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer);
		}

		#endregion
		
		#region TutorState
		public static void SendTutorStateMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity)
		{
			Message.Begin(destination, Message.GetUserMessageID("TutorState"), floatValue, playerEntity);
			
			Message.End();
		}

		public static void SendTutorStateMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, TutorStateMessage val)
		{
			SendTutorStateMessage(destination, floatValue,playerEntity );
		}

		public static void SendTutorStateMessage(MessageDestination destination, IntPtr playerEntity, TutorStateMessage val)
		{
			SendTutorStateMessage(destination, IntPtr.Zero, playerEntity );
		}

		public static void SendTutorStateMessage(MessageDestination destination, TutorStateMessage val)
		{
			SendTutorStateMessage(destination, IntPtr.Zero, IntPtr.Zero );
		}

		public static void SendTutorStateMessage(this Player player, IntPtr floatValue, TutorStateMessage val)
		{
			SendTutorStateMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendTutorStateMessage(this Player player, TutorStateMessage val)
		{
			SendTutorStateMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendTutorStateMessage(this Player player)
		{
			SendTutorStateMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer);
		}

		#endregion
		
		#region TutorText
		public static void SendTutorTextMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, string Unknown1, byte Unknown2, short Unknown3, short Unknown4, short Unknown5)
		{
			Message.Begin(destination, Message.GetUserMessageID("TutorText"), floatValue, playerEntity);
			
			Message.WriteString(Unknown1);
			Message.WriteByte(Unknown2);
			Message.WriteShort(Unknown3);
			Message.WriteShort(Unknown4);
			Message.WriteShort(Unknown5);
			Message.End();
		}

		public static void SendTutorTextMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, TutorTextMessage val)
		{
			SendTutorTextMessage(destination, floatValue,playerEntity , val.Unknown1, val.Unknown2, val.Unknown3, val.Unknown4, val.Unknown5);
		}

		public static void SendTutorTextMessage(MessageDestination destination, IntPtr playerEntity, TutorTextMessage val)
		{
			SendTutorTextMessage(destination, IntPtr.Zero, playerEntity , val.Unknown1, val.Unknown2, val.Unknown3, val.Unknown4, val.Unknown5);
		}

		public static void SendTutorTextMessage(MessageDestination destination, TutorTextMessage val)
		{
			SendTutorTextMessage(destination, IntPtr.Zero, IntPtr.Zero , val.Unknown1, val.Unknown2, val.Unknown3, val.Unknown4, val.Unknown5);
		}

		public static void SendTutorTextMessage(this Player player, IntPtr floatValue, TutorTextMessage val)
		{
			SendTutorTextMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendTutorTextMessage(this Player player, TutorTextMessage val)
		{
			SendTutorTextMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendTutorTextMessage(this Player player, string Unknown1, byte Unknown2, short Unknown3, short Unknown4, short Unknown5)
		{
			SendTutorTextMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, Unknown1, Unknown2, Unknown3, Unknown4, Unknown5);
		}

		#endregion
		
		#region ViewMode
		public static void SendViewModeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity)
		{
			Message.Begin(destination, Message.GetUserMessageID("ViewMode"), floatValue, playerEntity);
			
			Message.End();
		}

		public static void SendViewModeMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, ViewModeMessage val)
		{
			SendViewModeMessage(destination, floatValue,playerEntity );
		}

		public static void SendViewModeMessage(MessageDestination destination, IntPtr playerEntity, ViewModeMessage val)
		{
			SendViewModeMessage(destination, IntPtr.Zero, playerEntity );
		}

		public static void SendViewModeMessage(MessageDestination destination, ViewModeMessage val)
		{
			SendViewModeMessage(destination, IntPtr.Zero, IntPtr.Zero );
		}

		public static void SendViewModeMessage(this Player player, IntPtr floatValue, ViewModeMessage val)
		{
			SendViewModeMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendViewModeMessage(this Player player, ViewModeMessage val)
		{
			SendViewModeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendViewModeMessage(this Player player)
		{
			SendViewModeMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer);
		}

		#endregion
		
		#region VGUIMenu
		public static void SendVGUIMenuMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte MenuID, short KeyBitSum, char Time, byte MultiPart, string Name)
		{
			Message.Begin(destination, Message.GetUserMessageID("VGUIMenu"), floatValue, playerEntity);
			
			Message.WriteByte(MenuID);
			Message.WriteShort(KeyBitSum);
			Message.WriteChar(Time);
			Message.WriteByte(MultiPart);
			Message.WriteString(Name);
			Message.End();
		}

		public static void SendVGUIMenuMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, VGUIMenuMessage val)
		{
			SendVGUIMenuMessage(destination, floatValue,playerEntity , val.MenuID, val.KeyBitSum, val.Time, val.MultiPart, val.Name);
		}

		public static void SendVGUIMenuMessage(MessageDestination destination, IntPtr playerEntity, VGUIMenuMessage val)
		{
			SendVGUIMenuMessage(destination, IntPtr.Zero, playerEntity , val.MenuID, val.KeyBitSum, val.Time, val.MultiPart, val.Name);
		}

		public static void SendVGUIMenuMessage(MessageDestination destination, VGUIMenuMessage val)
		{
			SendVGUIMenuMessage(destination, IntPtr.Zero, IntPtr.Zero , val.MenuID, val.KeyBitSum, val.Time, val.MultiPart, val.Name);
		}

		public static void SendVGUIMenuMessage(this Player player, IntPtr floatValue, VGUIMenuMessage val)
		{
			SendVGUIMenuMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendVGUIMenuMessage(this Player player, VGUIMenuMessage val)
		{
			SendVGUIMenuMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendVGUIMenuMessage(this Player player, byte MenuID, short KeyBitSum, char Time, byte MultiPart, string Name)
		{
			SendVGUIMenuMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, MenuID, KeyBitSum, Time, MultiPart, Name);
		}

		#endregion
		
		#region VoiceMask
		public static void SendVoiceMaskMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, long AudiblePlayersIndexbitSum, long ServerBannedPlayersIndexBitSum)
		{
			Message.Begin(destination, Message.GetUserMessageID("VoiceMask"), floatValue, playerEntity);
			
			Message.WriteLong(AudiblePlayersIndexbitSum);
			Message.WriteLong(ServerBannedPlayersIndexBitSum);
			Message.End();
		}

		public static void SendVoiceMaskMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, VoiceMaskMessage val)
		{
			SendVoiceMaskMessage(destination, floatValue,playerEntity , val.AudiblePlayersIndexbitSum, val.ServerBannedPlayersIndexBitSum);
		}

		public static void SendVoiceMaskMessage(MessageDestination destination, IntPtr playerEntity, VoiceMaskMessage val)
		{
			SendVoiceMaskMessage(destination, IntPtr.Zero, playerEntity , val.AudiblePlayersIndexbitSum, val.ServerBannedPlayersIndexBitSum);
		}

		public static void SendVoiceMaskMessage(MessageDestination destination, VoiceMaskMessage val)
		{
			SendVoiceMaskMessage(destination, IntPtr.Zero, IntPtr.Zero , val.AudiblePlayersIndexbitSum, val.ServerBannedPlayersIndexBitSum);
		}

		public static void SendVoiceMaskMessage(this Player player, IntPtr floatValue, VoiceMaskMessage val)
		{
			SendVoiceMaskMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendVoiceMaskMessage(this Player player, VoiceMaskMessage val)
		{
			SendVoiceMaskMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendVoiceMaskMessage(this Player player, long AudiblePlayersIndexbitSum, long ServerBannedPlayersIndexBitSum)
		{
			SendVoiceMaskMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, AudiblePlayersIndexbitSum, ServerBannedPlayersIndexBitSum);
		}

		#endregion
		
		#region WeaponList
		public static void SendWeaponListMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, string WeaponName, byte PrimaryAmmoID, byte PrimaryAmmoMaxAmount, byte SecondaryAmmoID, byte SecondaryAmmoMaxAmount, byte SlotID, byte NumberInSlot, byte WeaponID, byte Flags)
		{
			Message.Begin(destination, Message.GetUserMessageID("WeaponList"), floatValue, playerEntity);
			
			Message.WriteString(WeaponName);
			Message.WriteByte(PrimaryAmmoID);
			Message.WriteByte(PrimaryAmmoMaxAmount);
			Message.WriteByte(SecondaryAmmoID);
			Message.WriteByte(SecondaryAmmoMaxAmount);
			Message.WriteByte(SlotID);
			Message.WriteByte(NumberInSlot);
			Message.WriteByte(WeaponID);
			Message.WriteByte(Flags);
			Message.End();
		}

		public static void SendWeaponListMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, WeaponListMessage val)
		{
			SendWeaponListMessage(destination, floatValue,playerEntity , val.WeaponName, val.PrimaryAmmoID, val.PrimaryAmmoMaxAmount, val.SecondaryAmmoID, val.SecondaryAmmoMaxAmount, val.SlotID, val.NumberInSlot, val.WeaponID, val.Flags);
		}

		public static void SendWeaponListMessage(MessageDestination destination, IntPtr playerEntity, WeaponListMessage val)
		{
			SendWeaponListMessage(destination, IntPtr.Zero, playerEntity , val.WeaponName, val.PrimaryAmmoID, val.PrimaryAmmoMaxAmount, val.SecondaryAmmoID, val.SecondaryAmmoMaxAmount, val.SlotID, val.NumberInSlot, val.WeaponID, val.Flags);
		}

		public static void SendWeaponListMessage(MessageDestination destination, WeaponListMessage val)
		{
			SendWeaponListMessage(destination, IntPtr.Zero, IntPtr.Zero , val.WeaponName, val.PrimaryAmmoID, val.PrimaryAmmoMaxAmount, val.SecondaryAmmoID, val.SecondaryAmmoMaxAmount, val.SlotID, val.NumberInSlot, val.WeaponID, val.Flags);
		}

		public static void SendWeaponListMessage(this Player player, IntPtr floatValue, WeaponListMessage val)
		{
			SendWeaponListMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendWeaponListMessage(this Player player, WeaponListMessage val)
		{
			SendWeaponListMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendWeaponListMessage(this Player player, string WeaponName, byte PrimaryAmmoID, byte PrimaryAmmoMaxAmount, byte SecondaryAmmoID, byte SecondaryAmmoMaxAmount, byte SlotID, byte NumberInSlot, byte WeaponID, byte Flags)
		{
			SendWeaponListMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, WeaponName, PrimaryAmmoID, PrimaryAmmoMaxAmount, SecondaryAmmoID, SecondaryAmmoMaxAmount, SlotID, NumberInSlot, WeaponID, Flags);
		}

		#endregion
		
		#region WeapPickup
		public static void SendWeapPickupMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, byte WeaponID)
		{
			Message.Begin(destination, Message.GetUserMessageID("WeapPickup"), floatValue, playerEntity);
			
			Message.WriteByte(WeaponID);
			Message.End();
		}

		public static void SendWeapPickupMessage(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, WeapPickupMessage val)
		{
			SendWeapPickupMessage(destination, floatValue,playerEntity , val.WeaponID);
		}

		public static void SendWeapPickupMessage(MessageDestination destination, IntPtr playerEntity, WeapPickupMessage val)
		{
			SendWeapPickupMessage(destination, IntPtr.Zero, playerEntity , val.WeaponID);
		}

		public static void SendWeapPickupMessage(MessageDestination destination, WeapPickupMessage val)
		{
			SendWeapPickupMessage(destination, IntPtr.Zero, IntPtr.Zero , val.WeaponID);
		}

		public static void SendWeapPickupMessage(this Player player, IntPtr floatValue, WeapPickupMessage val)
		{
			SendWeapPickupMessage(MessageDestination.OneReliable, floatValue, player.Pointer, val);
		}

		public static void SendWeapPickupMessage(this Player player, WeapPickupMessage val)
		{
			SendWeapPickupMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
		}

		public static void SendWeapPickupMessage(this Player player, byte WeaponID)
		{
			SendWeapPickupMessage(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, WeaponID);
		}

		#endregion
		
	}

	#endregion


	// This is not an extensions, therefore I won't commit it it for now
	// Is it possible to create static extension methods?

	/*
	#region Player class functions
	public partial class Player
	{
		
		public static void SendADStopMessage(IntPtr floatValue, ADStopMessage val)
		{
			foreach (Player player in Players) {
				Message.SendADStopMessage(player, floatValue, val);
			}
		}

		public static void SendADStopMessage(ADStopMessage val)
		{
			foreach (Player player in Players) {
				Message.SendADStopMessage(player, val);
			}
		}

		public static void SendADStopMessage()
		{
			foreach (Player player in Players) {
				Message.SendADStopMessage(player, );
			}
		}
		
		public static void SendAllowSpecMessage(IntPtr floatValue, AllowSpecMessage val)
		{
			foreach (Player player in Players) {
				Message.SendAllowSpecMessage(player, floatValue, val);
			}
		}

		public static void SendAllowSpecMessage(AllowSpecMessage val)
		{
			foreach (Player player in Players) {
				Message.SendAllowSpecMessage(player, val);
			}
		}

		public static void SendAllowSpecMessage(byte Allowed)
		{
			foreach (Player player in Players) {
				Message.SendAllowSpecMessage(player, );
			}
		}
		
		public static void SendAmmoPickupMessage(IntPtr floatValue, AmmoPickupMessage val)
		{
			foreach (Player player in Players) {
				Message.SendAmmoPickupMessage(player, floatValue, val);
			}
		}

		public static void SendAmmoPickupMessage(AmmoPickupMessage val)
		{
			foreach (Player player in Players) {
				Message.SendAmmoPickupMessage(player, val);
			}
		}

		public static void SendAmmoPickupMessage(byte AmmoID, byte Ammount)
		{
			foreach (Player player in Players) {
				Message.SendAmmoPickupMessage(player, );
			}
		}
		
		public static void SendAmmoXMessage(IntPtr floatValue, AmmoXMessage val)
		{
			foreach (Player player in Players) {
				Message.SendAmmoXMessage(player, floatValue, val);
			}
		}

		public static void SendAmmoXMessage(AmmoXMessage val)
		{
			foreach (Player player in Players) {
				Message.SendAmmoXMessage(player, val);
			}
		}

		public static void SendAmmoXMessage(byte AmmoID, byte Ammount)
		{
			foreach (Player player in Players) {
				Message.SendAmmoXMessage(player, );
			}
		}
		
		public static void SendArmorTypeMessage(IntPtr floatValue, ArmorTypeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendArmorTypeMessage(player, floatValue, val);
			}
		}

		public static void SendArmorTypeMessage(ArmorTypeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendArmorTypeMessage(player, val);
			}
		}

		public static void SendArmorTypeMessage(byte Flag)
		{
			foreach (Player player in Players) {
				Message.SendArmorTypeMessage(player, );
			}
		}
		
		public static void SendBarTimeMessage(IntPtr floatValue, BarTimeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBarTimeMessage(player, floatValue, val);
			}
		}

		public static void SendBarTimeMessage(BarTimeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBarTimeMessage(player, val);
			}
		}

		public static void SendBarTimeMessage(short Duration)
		{
			foreach (Player player in Players) {
				Message.SendBarTimeMessage(player, );
			}
		}
		
		public static void SendBarTime2Message(IntPtr floatValue, BarTime2Message val)
		{
			foreach (Player player in Players) {
				Message.SendBarTime2Message(player, floatValue, val);
			}
		}

		public static void SendBarTime2Message(BarTime2Message val)
		{
			foreach (Player player in Players) {
				Message.SendBarTime2Message(player, val);
			}
		}

		public static void SendBarTime2Message(short Duratino, short startPercents)
		{
			foreach (Player player in Players) {
				Message.SendBarTime2Message(player, );
			}
		}
		
		public static void SendBatteryMessage(IntPtr floatValue, BatteryMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBatteryMessage(player, floatValue, val);
			}
		}

		public static void SendBatteryMessage(BatteryMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBatteryMessage(player, val);
			}
		}

		public static void SendBatteryMessage(short Armor)
		{
			foreach (Player player in Players) {
				Message.SendBatteryMessage(player, );
			}
		}
		
		public static void SendBlinkAcctMessage(IntPtr floatValue, BlinkAcctMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBlinkAcctMessage(player, floatValue, val);
			}
		}

		public static void SendBlinkAcctMessage(BlinkAcctMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBlinkAcctMessage(player, val);
			}
		}

		public static void SendBlinkAcctMessage(byte BlinkAmt)
		{
			foreach (Player player in Players) {
				Message.SendBlinkAcctMessage(player, );
			}
		}
		
		public static void SendBombDropMessage(IntPtr floatValue, BombDropMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBombDropMessage(player, floatValue, val);
			}
		}

		public static void SendBombDropMessage(BombDropMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBombDropMessage(player, val);
			}
		}

		public static void SendBombDropMessage(int CoordX, int CoordY, int CoordZ, byte Flag)
		{
			foreach (Player player in Players) {
				Message.SendBombDropMessage(player, );
			}
		}
		
		public static void SendBombPickupMessage(IntPtr floatValue, BombPickupMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBombPickupMessage(player, floatValue, val);
			}
		}

		public static void SendBombPickupMessage(BombPickupMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBombPickupMessage(player, val);
			}
		}

		public static void SendBombPickupMessage()
		{
			foreach (Player player in Players) {
				Message.SendBombPickupMessage(player, );
			}
		}
		
		public static void SendBotProgressMessage(IntPtr floatValue, BotProgressMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBotProgressMessage(player, floatValue, val);
			}
		}

		public static void SendBotProgressMessage(BotProgressMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBotProgressMessage(player, val);
			}
		}

		public static void SendBotProgressMessage(byte Flag, byte Progress, string Header)
		{
			foreach (Player player in Players) {
				Message.SendBotProgressMessage(player, );
			}
		}
		
		public static void SendBotVoiceMessage(IntPtr floatValue, BotVoiceMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBotVoiceMessage(player, floatValue, val);
			}
		}

		public static void SendBotVoiceMessage(BotVoiceMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBotVoiceMessage(player, val);
			}
		}

		public static void SendBotVoiceMessage(byte Status, byte PlayerIndex)
		{
			foreach (Player player in Players) {
				Message.SendBotVoiceMessage(player, );
			}
		}
		
		public static void SendBrassMessage(IntPtr floatValue, BrassMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBrassMessage(player, floatValue, val);
			}
		}

		public static void SendBrassMessage(BrassMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBrassMessage(player, val);
			}
		}

		public static void SendBrassMessage(byte Unknown, int StartX, int StartY, int StartZ, int VelocityX, int VelocityY, int VelocityZ, int UnknownX, int UnknownY, int UnknownZ, int Life, short Model, byte Unknownb1, byte Unknownb2, byte Unknownb3)
		{
			foreach (Player player in Players) {
				Message.SendBrassMessage(player, );
			}
		}
		
		public static void SendBuyCloseMessage(IntPtr floatValue, BuyCloseMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBuyCloseMessage(player, floatValue, val);
			}
		}

		public static void SendBuyCloseMessage(BuyCloseMessage val)
		{
			foreach (Player player in Players) {
				Message.SendBuyCloseMessage(player, val);
			}
		}

		public static void SendBuyCloseMessage()
		{
			foreach (Player player in Players) {
				Message.SendBuyCloseMessage(player, );
			}
		}
		
		public static void SendClCorpseMessage(IntPtr floatValue, ClCorpseMessage val)
		{
			foreach (Player player in Players) {
				Message.SendClCorpseMessage(player, floatValue, val);
			}
		}

		public static void SendClCorpseMessage(ClCorpseMessage val)
		{
			foreach (Player player in Players) {
				Message.SendClCorpseMessage(player, val);
			}
		}

		public static void SendClCorpseMessage(string ModelName, long CoordX, long CoordY, long CoordZ, int Angle0, int Angle1, int Angle2, long Delay, byte Sequence, byte ClassID, byte TeamID, byte PlayerID)
		{
			foreach (Player player in Players) {
				Message.SendClCorpseMessage(player, );
			}
		}
		
		public static void SendCrosshairMessage(IntPtr floatValue, CrosshairMessage val)
		{
			foreach (Player player in Players) {
				Message.SendCrosshairMessage(player, floatValue, val);
			}
		}

		public static void SendCrosshairMessage(CrosshairMessage val)
		{
			foreach (Player player in Players) {
				Message.SendCrosshairMessage(player, val);
			}
		}

		public static void SendCrosshairMessage(byte Flag)
		{
			foreach (Player player in Players) {
				Message.SendCrosshairMessage(player, );
			}
		}
		
		public static void SendCurWeaponMessage(IntPtr floatValue, CurWeaponMessage val)
		{
			foreach (Player player in Players) {
				Message.SendCurWeaponMessage(player, floatValue, val);
			}
		}

		public static void SendCurWeaponMessage(CurWeaponMessage val)
		{
			foreach (Player player in Players) {
				Message.SendCurWeaponMessage(player, val);
			}
		}

		public static void SendCurWeaponMessage(byte IsActive, byte WeaponID, byte ClipAmmo)
		{
			foreach (Player player in Players) {
				Message.SendCurWeaponMessage(player, );
			}
		}
		
		public static void SendCZCareerMessage(IntPtr floatValue, CZCareerMessage val)
		{
			foreach (Player player in Players) {
				Message.SendCZCareerMessage(player, floatValue, val);
			}
		}

		public static void SendCZCareerMessage(CZCareerMessage val)
		{
			foreach (Player player in Players) {
				Message.SendCZCareerMessage(player, val);
			}
		}

		public static void SendCZCareerMessage(string Type, int Parameters)
		{
			foreach (Player player in Players) {
				Message.SendCZCareerMessage(player, );
			}
		}
		
		public static void SendCZCareerHUDMessage(IntPtr floatValue, CZCareerHUDMessage val)
		{
			foreach (Player player in Players) {
				Message.SendCZCareerHUDMessage(player, floatValue, val);
			}
		}

		public static void SendCZCareerHUDMessage(CZCareerHUDMessage val)
		{
			foreach (Player player in Players) {
				Message.SendCZCareerHUDMessage(player, val);
			}
		}

		public static void SendCZCareerHUDMessage(string Type, int Parameters)
		{
			foreach (Player player in Players) {
				Message.SendCZCareerHUDMessage(player, );
			}
		}
		
		public static void SendDamageMessage(IntPtr floatValue, DamageMessage val)
		{
			foreach (Player player in Players) {
				Message.SendDamageMessage(player, floatValue, val);
			}
		}

		public static void SendDamageMessage(DamageMessage val)
		{
			foreach (Player player in Players) {
				Message.SendDamageMessage(player, val);
			}
		}

		public static void SendDamageMessage(byte DamageSave, byte DamageTake, long DamageType, int CoordX, int CoordY, int CoordZ)
		{
			foreach (Player player in Players) {
				Message.SendDamageMessage(player, );
			}
		}
		
		public static void SendDeathMsgMessage(IntPtr floatValue, DeathMsgMessage val)
		{
			foreach (Player player in Players) {
				Message.SendDeathMsgMessage(player, floatValue, val);
			}
		}

		public static void SendDeathMsgMessage(DeathMsgMessage val)
		{
			foreach (Player player in Players) {
				Message.SendDeathMsgMessage(player, val);
			}
		}

		public static void SendDeathMsgMessage(byte KillerID, byte VictimID, byte IsHeadshot, string TruncatedWeaponName)
		{
			foreach (Player player in Players) {
				Message.SendDeathMsgMessage(player, );
			}
		}
		
		public static void SendFlashBatMessage(IntPtr floatValue, FlashBatMessage val)
		{
			foreach (Player player in Players) {
				Message.SendFlashBatMessage(player, floatValue, val);
			}
		}

		public static void SendFlashBatMessage(FlashBatMessage val)
		{
			foreach (Player player in Players) {
				Message.SendFlashBatMessage(player, val);
			}
		}

		public static void SendFlashBatMessage(byte ChargePercents)
		{
			foreach (Player player in Players) {
				Message.SendFlashBatMessage(player, );
			}
		}
		
		public static void SendFlashlightMessage(IntPtr floatValue, FlashlightMessage val)
		{
			foreach (Player player in Players) {
				Message.SendFlashlightMessage(player, floatValue, val);
			}
		}

		public static void SendFlashlightMessage(FlashlightMessage val)
		{
			foreach (Player player in Players) {
				Message.SendFlashlightMessage(player, val);
			}
		}

		public static void SendFlashlightMessage(byte Flag, byte ChargePercents)
		{
			foreach (Player player in Players) {
				Message.SendFlashlightMessage(player, );
			}
		}
		
		public static void SendFogMessage(IntPtr floatValue, FogMessage val)
		{
			foreach (Player player in Players) {
				Message.SendFogMessage(player, floatValue, val);
			}
		}

		public static void SendFogMessage(FogMessage val)
		{
			foreach (Player player in Players) {
				Message.SendFogMessage(player, val);
			}
		}

		public static void SendFogMessage(byte FogValue1, byte FogValue2, byte Unknown)
		{
			foreach (Player player in Players) {
				Message.SendFogMessage(player, );
			}
		}
		
		public static void SendForceCamMessage(IntPtr floatValue, ForceCamMessage val)
		{
			foreach (Player player in Players) {
				Message.SendForceCamMessage(player, floatValue, val);
			}
		}

		public static void SendForceCamMessage(ForceCamMessage val)
		{
			foreach (Player player in Players) {
				Message.SendForceCamMessage(player, val);
			}
		}

		public static void SendForceCamMessage(byte ForececamValue, byte ForcechasecamValue, byte Unknown)
		{
			foreach (Player player in Players) {
				Message.SendForceCamMessage(player, );
			}
		}
		
		public static void SendGameModeMessage(IntPtr floatValue, GameModeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendGameModeMessage(player, floatValue, val);
			}
		}

		public static void SendGameModeMessage(GameModeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendGameModeMessage(player, val);
			}
		}

		public static void SendGameModeMessage(byte Unknown)
		{
			foreach (Player player in Players) {
				Message.SendGameModeMessage(player, );
			}
		}
		
		public static void SendGameTitleMessage(IntPtr floatValue, GameTitleMessage val)
		{
			foreach (Player player in Players) {
				Message.SendGameTitleMessage(player, floatValue, val);
			}
		}

		public static void SendGameTitleMessage(GameTitleMessage val)
		{
			foreach (Player player in Players) {
				Message.SendGameTitleMessage(player, val);
			}
		}

		public static void SendGameTitleMessage()
		{
			foreach (Player player in Players) {
				Message.SendGameTitleMessage(player, );
			}
		}
		
		public static void SendGeigerMessage(IntPtr floatValue, GeigerMessage val)
		{
			foreach (Player player in Players) {
				Message.SendGeigerMessage(player, floatValue, val);
			}
		}

		public static void SendGeigerMessage(GeigerMessage val)
		{
			foreach (Player player in Players) {
				Message.SendGeigerMessage(player, val);
			}
		}

		public static void SendGeigerMessage(byte Distance)
		{
			foreach (Player player in Players) {
				Message.SendGeigerMessage(player, );
			}
		}
		
		public static void SendHealthMessage(IntPtr floatValue, HealthMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHealthMessage(player, floatValue, val);
			}
		}

		public static void SendHealthMessage(HealthMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHealthMessage(player, val);
			}
		}

		public static void SendHealthMessage(byte Health)
		{
			foreach (Player player in Players) {
				Message.SendHealthMessage(player, );
			}
		}
		
		public static void SendHideWeaponMessage(IntPtr floatValue, HideWeaponMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHideWeaponMessage(player, floatValue, val);
			}
		}

		public static void SendHideWeaponMessage(HideWeaponMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHideWeaponMessage(player, val);
			}
		}

		public static void SendHideWeaponMessage(byte Flags)
		{
			foreach (Player player in Players) {
				Message.SendHideWeaponMessage(player, );
			}
		}
		
		public static void SendHLTVMessage(IntPtr floatValue, HLTVMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHLTVMessage(player, floatValue, val);
			}
		}

		public static void SendHLTVMessage(HLTVMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHLTVMessage(player, val);
			}
		}

		public static void SendHLTVMessage(byte ClientID, byte Flags)
		{
			foreach (Player player in Players) {
				Message.SendHLTVMessage(player, );
			}
		}
		
		public static void SendHostageKMessage(IntPtr floatValue, HostageKMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHostageKMessage(player, floatValue, val);
			}
		}

		public static void SendHostageKMessage(HostageKMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHostageKMessage(player, val);
			}
		}

		public static void SendHostageKMessage(byte HostageID)
		{
			foreach (Player player in Players) {
				Message.SendHostageKMessage(player, );
			}
		}
		
		public static void SendHostagePosMessage(IntPtr floatValue, HostagePosMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHostagePosMessage(player, floatValue, val);
			}
		}

		public static void SendHostagePosMessage(HostagePosMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHostagePosMessage(player, val);
			}
		}

		public static void SendHostagePosMessage(byte Flag, byte HostageID, int CoordX, int CoordY, int CoordZ)
		{
			foreach (Player player in Players) {
				Message.SendHostagePosMessage(player, );
			}
		}
		
		public static void SendHudTextMessage(IntPtr floatValue, HudTextMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHudTextMessage(player, floatValue, val);
			}
		}

		public static void SendHudTextMessage(HudTextMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHudTextMessage(player, val);
			}
		}

		public static void SendHudTextMessage()
		{
			foreach (Player player in Players) {
				Message.SendHudTextMessage(player, );
			}
		}
		
		public static void SendHudTextArgsMessage(IntPtr floatValue, HudTextArgsMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHudTextArgsMessage(player, floatValue, val);
			}
		}

		public static void SendHudTextArgsMessage(HudTextArgsMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHudTextArgsMessage(player, val);
			}
		}

		public static void SendHudTextArgsMessage(string TextCode, byte Unknown1, byte Unknown2)
		{
			foreach (Player player in Players) {
				Message.SendHudTextArgsMessage(player, );
			}
		}
		
		public static void SendHudTextProMessage(IntPtr floatValue, HudTextProMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHudTextProMessage(player, floatValue, val);
			}
		}

		public static void SendHudTextProMessage(HudTextProMessage val)
		{
			foreach (Player player in Players) {
				Message.SendHudTextProMessage(player, val);
			}
		}

		public static void SendHudTextProMessage()
		{
			foreach (Player player in Players) {
				Message.SendHudTextProMessage(player, );
			}
		}
		
		public static void SendInitHUDMessage(IntPtr floatValue, InitHUDMessage val)
		{
			foreach (Player player in Players) {
				Message.SendInitHUDMessage(player, floatValue, val);
			}
		}

		public static void SendInitHUDMessage(InitHUDMessage val)
		{
			foreach (Player player in Players) {
				Message.SendInitHUDMessage(player, val);
			}
		}

		public static void SendInitHUDMessage()
		{
			foreach (Player player in Players) {
				Message.SendInitHUDMessage(player, );
			}
		}
		
		public static void SendItemPickupMessage(IntPtr floatValue, ItemPickupMessage val)
		{
			foreach (Player player in Players) {
				Message.SendItemPickupMessage(player, floatValue, val);
			}
		}

		public static void SendItemPickupMessage(ItemPickupMessage val)
		{
			foreach (Player player in Players) {
				Message.SendItemPickupMessage(player, val);
			}
		}

		public static void SendItemPickupMessage(string ItemName)
		{
			foreach (Player player in Players) {
				Message.SendItemPickupMessage(player, );
			}
		}
		
		public static void SendItemStatusMessage(IntPtr floatValue, ItemStatusMessage val)
		{
			foreach (Player player in Players) {
				Message.SendItemStatusMessage(player, floatValue, val);
			}
		}

		public static void SendItemStatusMessage(ItemStatusMessage val)
		{
			foreach (Player player in Players) {
				Message.SendItemStatusMessage(player, val);
			}
		}

		public static void SendItemStatusMessage(byte ItemBitSum)
		{
			foreach (Player player in Players) {
				Message.SendItemStatusMessage(player, );
			}
		}
		
		public static void SendLocationMessage(IntPtr floatValue, LocationMessage val)
		{
			foreach (Player player in Players) {
				Message.SendLocationMessage(player, floatValue, val);
			}
		}

		public static void SendLocationMessage(LocationMessage val)
		{
			foreach (Player player in Players) {
				Message.SendLocationMessage(player, val);
			}
		}

		public static void SendLocationMessage(byte Money)
		{
			foreach (Player player in Players) {
				Message.SendLocationMessage(player, );
			}
		}
		
		public static void SendMoneyMessage(IntPtr floatValue, MoneyMessage val)
		{
			foreach (Player player in Players) {
				Message.SendMoneyMessage(player, floatValue, val);
			}
		}

		public static void SendMoneyMessage(MoneyMessage val)
		{
			foreach (Player player in Players) {
				Message.SendMoneyMessage(player, val);
			}
		}

		public static void SendMoneyMessage(long Amount, byte Flag)
		{
			foreach (Player player in Players) {
				Message.SendMoneyMessage(player, );
			}
		}
		
		public static void SendMOTDMessage(IntPtr floatValue, MOTDMessage val)
		{
			foreach (Player player in Players) {
				Message.SendMOTDMessage(player, floatValue, val);
			}
		}

		public static void SendMOTDMessage(MOTDMessage val)
		{
			foreach (Player player in Players) {
				Message.SendMOTDMessage(player, val);
			}
		}

		public static void SendMOTDMessage(byte Flag, string Text)
		{
			foreach (Player player in Players) {
				Message.SendMOTDMessage(player, );
			}
		}
		
		public static void SendNVGToggleMessage(IntPtr floatValue, NVGToggleMessage val)
		{
			foreach (Player player in Players) {
				Message.SendNVGToggleMessage(player, floatValue, val);
			}
		}

		public static void SendNVGToggleMessage(NVGToggleMessage val)
		{
			foreach (Player player in Players) {
				Message.SendNVGToggleMessage(player, val);
			}
		}

		public static void SendNVGToggleMessage(byte Flag)
		{
			foreach (Player player in Players) {
				Message.SendNVGToggleMessage(player, );
			}
		}
		
		public static void SendRadarMessage(IntPtr floatValue, RadarMessage val)
		{
			foreach (Player player in Players) {
				Message.SendRadarMessage(player, floatValue, val);
			}
		}

		public static void SendRadarMessage(RadarMessage val)
		{
			foreach (Player player in Players) {
				Message.SendRadarMessage(player, val);
			}
		}

		public static void SendRadarMessage(byte PlayerID, int CoordX, int CoordY, int CoordZ)
		{
			foreach (Player player in Players) {
				Message.SendRadarMessage(player, );
			}
		}
		
		public static void SendReceiveWMessage(IntPtr floatValue, ReceiveWMessage val)
		{
			foreach (Player player in Players) {
				Message.SendReceiveWMessage(player, floatValue, val);
			}
		}

		public static void SendReceiveWMessage(ReceiveWMessage val)
		{
			foreach (Player player in Players) {
				Message.SendReceiveWMessage(player, val);
			}
		}

		public static void SendReceiveWMessage()
		{
			foreach (Player player in Players) {
				Message.SendReceiveWMessage(player, );
			}
		}
		
		public static void SendReloadSoundMessage(IntPtr floatValue, ReloadSoundMessage val)
		{
			foreach (Player player in Players) {
				Message.SendReloadSoundMessage(player, floatValue, val);
			}
		}

		public static void SendReloadSoundMessage(ReloadSoundMessage val)
		{
			foreach (Player player in Players) {
				Message.SendReloadSoundMessage(player, val);
			}
		}

		public static void SendReloadSoundMessage(byte Unknown1, byte Unknown2)
		{
			foreach (Player player in Players) {
				Message.SendReloadSoundMessage(player, );
			}
		}
		
		public static void SendReqStateMessage(IntPtr floatValue, ReqStateMessage val)
		{
			foreach (Player player in Players) {
				Message.SendReqStateMessage(player, floatValue, val);
			}
		}

		public static void SendReqStateMessage(ReqStateMessage val)
		{
			foreach (Player player in Players) {
				Message.SendReqStateMessage(player, val);
			}
		}

		public static void SendReqStateMessage()
		{
			foreach (Player player in Players) {
				Message.SendReqStateMessage(player, );
			}
		}
		
		public static void SendResetHUDMessage(IntPtr floatValue, ResetHUDMessage val)
		{
			foreach (Player player in Players) {
				Message.SendResetHUDMessage(player, floatValue, val);
			}
		}

		public static void SendResetHUDMessage(ResetHUDMessage val)
		{
			foreach (Player player in Players) {
				Message.SendResetHUDMessage(player, val);
			}
		}

		public static void SendResetHUDMessage()
		{
			foreach (Player player in Players) {
				Message.SendResetHUDMessage(player, );
			}
		}
		
		public static void SendRoundTimeMessage(IntPtr floatValue, RoundTimeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendRoundTimeMessage(player, floatValue, val);
			}
		}

		public static void SendRoundTimeMessage(RoundTimeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendRoundTimeMessage(player, val);
			}
		}

		public static void SendRoundTimeMessage(short Time)
		{
			foreach (Player player in Players) {
				Message.SendRoundTimeMessage(player, );
			}
		}
		
		public static void SendSayTextMessage(IntPtr floatValue, SayTextMessage val)
		{
			foreach (Player player in Players) {
				Message.SendSayTextMessage(player, floatValue, val);
			}
		}

		public static void SendSayTextMessage(SayTextMessage val)
		{
			foreach (Player player in Players) {
				Message.SendSayTextMessage(player, val);
			}
		}

		public static void SendSayTextMessage(byte SenderID, string String1, string String2 = null, string String3 = null)
		{
			foreach (Player player in Players) {
				Message.SendSayTextMessage(player, );
			}
		}
		
		public static void SendScenarioMessage(IntPtr floatValue, ScenarioMessage val)
		{
			foreach (Player player in Players) {
				Message.SendScenarioMessage(player, floatValue, val);
			}
		}

		public static void SendScenarioMessage(ScenarioMessage val)
		{
			foreach (Player player in Players) {
				Message.SendScenarioMessage(player, val);
			}
		}

		public static void SendScenarioMessage(byte Active, string Sprite, byte Alpha, short FlashRate, short Unknown)
		{
			foreach (Player player in Players) {
				Message.SendScenarioMessage(player, );
			}
		}
		
		public static void SendScoreAttribMessage(IntPtr floatValue, ScoreAttribMessage val)
		{
			foreach (Player player in Players) {
				Message.SendScoreAttribMessage(player, floatValue, val);
			}
		}

		public static void SendScoreAttribMessage(ScoreAttribMessage val)
		{
			foreach (Player player in Players) {
				Message.SendScoreAttribMessage(player, val);
			}
		}

		public static void SendScoreAttribMessage(byte PlayerID, byte Flags)
		{
			foreach (Player player in Players) {
				Message.SendScoreAttribMessage(player, );
			}
		}
		
		public static void SendScoreInfoMessage(IntPtr floatValue, ScoreInfoMessage val)
		{
			foreach (Player player in Players) {
				Message.SendScoreInfoMessage(player, floatValue, val);
			}
		}

		public static void SendScoreInfoMessage(ScoreInfoMessage val)
		{
			foreach (Player player in Players) {
				Message.SendScoreInfoMessage(player, val);
			}
		}

		public static void SendScoreInfoMessage(byte PlayerID, short Frags, short Deaths, short ClassID, short TeamID)
		{
			foreach (Player player in Players) {
				Message.SendScoreInfoMessage(player, );
			}
		}
		
		public static void SendScreenFadeMessage(IntPtr floatValue, ScreenFadeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendScreenFadeMessage(player, floatValue, val);
			}
		}

		public static void SendScreenFadeMessage(ScreenFadeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendScreenFadeMessage(player, val);
			}
		}

		public static void SendScreenFadeMessage(short Duration, short HoldTime, short Flags, byte ColorR, byte ColorG, byte ColorB, byte Alpha)
		{
			foreach (Player player in Players) {
				Message.SendScreenFadeMessage(player, );
			}
		}
		
		public static void SendScreenShakeMessage(IntPtr floatValue, ScreenShakeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendScreenShakeMessage(player, floatValue, val);
			}
		}

		public static void SendScreenShakeMessage(ScreenShakeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendScreenShakeMessage(player, val);
			}
		}

		public static void SendScreenShakeMessage(short Amplitude, short Duration, short Frequency)
		{
			foreach (Player player in Players) {
				Message.SendScreenShakeMessage(player, );
			}
		}
		
		public static void SendSendAudioMessage(IntPtr floatValue, SendAudioMessage val)
		{
			foreach (Player player in Players) {
				Message.SendSendAudioMessage(player, floatValue, val);
			}
		}

		public static void SendSendAudioMessage(SendAudioMessage val)
		{
			foreach (Player player in Players) {
				Message.SendSendAudioMessage(player, val);
			}
		}

		public static void SendSendAudioMessage(byte SenderID, string AduioCode, short Pitch)
		{
			foreach (Player player in Players) {
				Message.SendSendAudioMessage(player, );
			}
		}
		
		public static void SendServerNameMessage(IntPtr floatValue, ServerNameMessage val)
		{
			foreach (Player player in Players) {
				Message.SendServerNameMessage(player, floatValue, val);
			}
		}

		public static void SendServerNameMessage(ServerNameMessage val)
		{
			foreach (Player player in Players) {
				Message.SendServerNameMessage(player, val);
			}
		}

		public static void SendServerNameMessage(string ServerName)
		{
			foreach (Player player in Players) {
				Message.SendServerNameMessage(player, );
			}
		}
		
		public static void SendSetFOVMessage(IntPtr floatValue, SetFOVMessage val)
		{
			foreach (Player player in Players) {
				Message.SendSetFOVMessage(player, floatValue, val);
			}
		}

		public static void SendSetFOVMessage(SetFOVMessage val)
		{
			foreach (Player player in Players) {
				Message.SendSetFOVMessage(player, val);
			}
		}

		public static void SendSetFOVMessage(byte Degrees)
		{
			foreach (Player player in Players) {
				Message.SendSetFOVMessage(player, );
			}
		}
		
		public static void SendShadowIdxMessage(IntPtr floatValue, ShadowIdxMessage val)
		{
			foreach (Player player in Players) {
				Message.SendShadowIdxMessage(player, floatValue, val);
			}
		}

		public static void SendShadowIdxMessage(ShadowIdxMessage val)
		{
			foreach (Player player in Players) {
				Message.SendShadowIdxMessage(player, val);
			}
		}

		public static void SendShadowIdxMessage(long Unknown)
		{
			foreach (Player player in Players) {
				Message.SendShadowIdxMessage(player, );
			}
		}
		
		public static void SendShowMenuMessage(IntPtr floatValue, ShowMenuMessage val)
		{
			foreach (Player player in Players) {
				Message.SendShowMenuMessage(player, floatValue, val);
			}
		}

		public static void SendShowMenuMessage(ShowMenuMessage val)
		{
			foreach (Player player in Players) {
				Message.SendShowMenuMessage(player, val);
			}
		}

		public static void SendShowMenuMessage(short KeyBitSum, char Time, byte MultiPart, string Text)
		{
			foreach (Player player in Players) {
				Message.SendShowMenuMessage(player, );
			}
		}
		
		public static void SendShowTimerMessage(IntPtr floatValue, ShowTimerMessage val)
		{
			foreach (Player player in Players) {
				Message.SendShowTimerMessage(player, floatValue, val);
			}
		}

		public static void SendShowTimerMessage(ShowTimerMessage val)
		{
			foreach (Player player in Players) {
				Message.SendShowTimerMessage(player, val);
			}
		}

		public static void SendShowTimerMessage()
		{
			foreach (Player player in Players) {
				Message.SendShowTimerMessage(player, );
			}
		}
		
		public static void SendSpecHealthMessage(IntPtr floatValue, SpecHealthMessage val)
		{
			foreach (Player player in Players) {
				Message.SendSpecHealthMessage(player, floatValue, val);
			}
		}

		public static void SendSpecHealthMessage(SpecHealthMessage val)
		{
			foreach (Player player in Players) {
				Message.SendSpecHealthMessage(player, val);
			}
		}

		public static void SendSpecHealthMessage(byte Health)
		{
			foreach (Player player in Players) {
				Message.SendSpecHealthMessage(player, );
			}
		}
		
		public static void SendSpecHealth2Message(IntPtr floatValue, SpecHealth2Message val)
		{
			foreach (Player player in Players) {
				Message.SendSpecHealth2Message(player, floatValue, val);
			}
		}

		public static void SendSpecHealth2Message(SpecHealth2Message val)
		{
			foreach (Player player in Players) {
				Message.SendSpecHealth2Message(player, val);
			}
		}

		public static void SendSpecHealth2Message(byte Health, byte PlayerID)
		{
			foreach (Player player in Players) {
				Message.SendSpecHealth2Message(player, );
			}
		}
		
		public static void SendSpectatorMessage(IntPtr floatValue, SpectatorMessage val)
		{
			foreach (Player player in Players) {
				Message.SendSpectatorMessage(player, floatValue, val);
			}
		}

		public static void SendSpectatorMessage(SpectatorMessage val)
		{
			foreach (Player player in Players) {
				Message.SendSpectatorMessage(player, val);
			}
		}

		public static void SendSpectatorMessage(byte ClientID, byte Unknown)
		{
			foreach (Player player in Players) {
				Message.SendSpectatorMessage(player, );
			}
		}
		
		public static void SendStatusIconMessage(IntPtr floatValue, StatusIconMessage val)
		{
			foreach (Player player in Players) {
				Message.SendStatusIconMessage(player, floatValue, val);
			}
		}

		public static void SendStatusIconMessage(StatusIconMessage val)
		{
			foreach (Player player in Players) {
				Message.SendStatusIconMessage(player, val);
			}
		}

		public static void SendStatusIconMessage(byte Status, string SpriteName, byte ColorR = 0, byte ColorG = 0, byte ColorB = 0)
		{
			foreach (Player player in Players) {
				Message.SendStatusIconMessage(player, );
			}
		}
		
		public static void SendStatusValueMessage(IntPtr floatValue, StatusValueMessage val)
		{
			foreach (Player player in Players) {
				Message.SendStatusValueMessage(player, floatValue, val);
			}
		}

		public static void SendStatusValueMessage(StatusValueMessage val)
		{
			foreach (Player player in Players) {
				Message.SendStatusValueMessage(player, val);
			}
		}

		public static void SendStatusValueMessage(byte Flag, short Value)
		{
			foreach (Player player in Players) {
				Message.SendStatusValueMessage(player, );
			}
		}
		
		public static void SendStatusTextMessage(IntPtr floatValue, StatusTextMessage val)
		{
			foreach (Player player in Players) {
				Message.SendStatusTextMessage(player, floatValue, val);
			}
		}

		public static void SendStatusTextMessage(StatusTextMessage val)
		{
			foreach (Player player in Players) {
				Message.SendStatusTextMessage(player, val);
			}
		}

		public static void SendStatusTextMessage(byte Unknown, string Text)
		{
			foreach (Player player in Players) {
				Message.SendStatusTextMessage(player, );
			}
		}
		
		public static void SendTaskTimeMessage(IntPtr floatValue, TaskTimeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTaskTimeMessage(player, floatValue, val);
			}
		}

		public static void SendTaskTimeMessage(TaskTimeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTaskTimeMessage(player, val);
			}
		}

		public static void SendTaskTimeMessage(short Time, byte Active, byte Fade)
		{
			foreach (Player player in Players) {
				Message.SendTaskTimeMessage(player, );
			}
		}
		
		public static void SendTeamInfoMessage(IntPtr floatValue, TeamInfoMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTeamInfoMessage(player, floatValue, val);
			}
		}

		public static void SendTeamInfoMessage(TeamInfoMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTeamInfoMessage(player, val);
			}
		}

		public static void SendTeamInfoMessage(byte PlayerID, string TeamName)
		{
			foreach (Player player in Players) {
				Message.SendTeamInfoMessage(player, );
			}
		}
		
		public static void SendTeamScoreMessage(IntPtr floatValue, TeamScoreMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTeamScoreMessage(player, floatValue, val);
			}
		}

		public static void SendTeamScoreMessage(TeamScoreMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTeamScoreMessage(player, val);
			}
		}

		public static void SendTeamScoreMessage(string Score)
		{
			foreach (Player player in Players) {
				Message.SendTeamScoreMessage(player, );
			}
		}
		
		public static void SendTextMsgMessage(IntPtr floatValue, TextMsgMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTextMsgMessage(player, floatValue, val);
			}
		}

		public static void SendTextMsgMessage(TextMsgMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTextMsgMessage(player, val);
			}
		}

		public static void SendTextMsgMessage(byte DestinationType, string MessageContent, string Submsg1 = null, string Submsg2 = null, string Submsg3 = null, string Submsg4 = null)
		{
			foreach (Player player in Players) {
				Message.SendTextMsgMessage(player, );
			}
		}
		
		public static void SendTrainMessage(IntPtr floatValue, TrainMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTrainMessage(player, floatValue, val);
			}
		}

		public static void SendTrainMessage(TrainMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTrainMessage(player, val);
			}
		}

		public static void SendTrainMessage(byte Speed)
		{
			foreach (Player player in Players) {
				Message.SendTrainMessage(player, );
			}
		}
		
		public static void SendTutorCloseMessage(IntPtr floatValue, TutorCloseMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTutorCloseMessage(player, floatValue, val);
			}
		}

		public static void SendTutorCloseMessage(TutorCloseMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTutorCloseMessage(player, val);
			}
		}

		public static void SendTutorCloseMessage()
		{
			foreach (Player player in Players) {
				Message.SendTutorCloseMessage(player, );
			}
		}
		
		public static void SendTutorLineMessage(IntPtr floatValue, TutorLineMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTutorLineMessage(player, floatValue, val);
			}
		}

		public static void SendTutorLineMessage(TutorLineMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTutorLineMessage(player, val);
			}
		}

		public static void SendTutorLineMessage()
		{
			foreach (Player player in Players) {
				Message.SendTutorLineMessage(player, );
			}
		}
		
		public static void SendTutorStateMessage(IntPtr floatValue, TutorStateMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTutorStateMessage(player, floatValue, val);
			}
		}

		public static void SendTutorStateMessage(TutorStateMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTutorStateMessage(player, val);
			}
		}

		public static void SendTutorStateMessage()
		{
			foreach (Player player in Players) {
				Message.SendTutorStateMessage(player, );
			}
		}
		
		public static void SendTutorTextMessage(IntPtr floatValue, TutorTextMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTutorTextMessage(player, floatValue, val);
			}
		}

		public static void SendTutorTextMessage(TutorTextMessage val)
		{
			foreach (Player player in Players) {
				Message.SendTutorTextMessage(player, val);
			}
		}

		public static void SendTutorTextMessage(string Unknown1, byte Unknown2, short Unknown3, short Unknown4, short Unknown5)
		{
			foreach (Player player in Players) {
				Message.SendTutorTextMessage(player, );
			}
		}
		
		public static void SendViewModeMessage(IntPtr floatValue, ViewModeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendViewModeMessage(player, floatValue, val);
			}
		}

		public static void SendViewModeMessage(ViewModeMessage val)
		{
			foreach (Player player in Players) {
				Message.SendViewModeMessage(player, val);
			}
		}

		public static void SendViewModeMessage()
		{
			foreach (Player player in Players) {
				Message.SendViewModeMessage(player, );
			}
		}
		
		public static void SendVGUIMenuMessage(IntPtr floatValue, VGUIMenuMessage val)
		{
			foreach (Player player in Players) {
				Message.SendVGUIMenuMessage(player, floatValue, val);
			}
		}

		public static void SendVGUIMenuMessage(VGUIMenuMessage val)
		{
			foreach (Player player in Players) {
				Message.SendVGUIMenuMessage(player, val);
			}
		}

		public static void SendVGUIMenuMessage(byte MenuID, short KeyBitSum, char Time, byte MultiPart, string Name)
		{
			foreach (Player player in Players) {
				Message.SendVGUIMenuMessage(player, );
			}
		}
		
		public static void SendVoiceMaskMessage(IntPtr floatValue, VoiceMaskMessage val)
		{
			foreach (Player player in Players) {
				Message.SendVoiceMaskMessage(player, floatValue, val);
			}
		}

		public static void SendVoiceMaskMessage(VoiceMaskMessage val)
		{
			foreach (Player player in Players) {
				Message.SendVoiceMaskMessage(player, val);
			}
		}

		public static void SendVoiceMaskMessage(long AudiblePlayersIndexbitSum, long ServerBannedPlayersIndexBitSum)
		{
			foreach (Player player in Players) {
				Message.SendVoiceMaskMessage(player, );
			}
		}
		
		public static void SendWeaponListMessage(IntPtr floatValue, WeaponListMessage val)
		{
			foreach (Player player in Players) {
				Message.SendWeaponListMessage(player, floatValue, val);
			}
		}

		public static void SendWeaponListMessage(WeaponListMessage val)
		{
			foreach (Player player in Players) {
				Message.SendWeaponListMessage(player, val);
			}
		}

		public static void SendWeaponListMessage(string WeaponName, byte PrimaryAmmoID, byte PrimaryAmmoMaxAmount, byte SecondaryAmmoID, byte SecondaryAmmoMaxAmount, byte SlotID, byte NumberInSlot, byte WeaponID, byte Flags)
		{
			foreach (Player player in Players) {
				Message.SendWeaponListMessage(player, );
			}
		}
		
		public static void SendWeapPickupMessage(IntPtr floatValue, WeapPickupMessage val)
		{
			foreach (Player player in Players) {
				Message.SendWeapPickupMessage(player, floatValue, val);
			}
		}

		public static void SendWeapPickupMessage(WeapPickupMessage val)
		{
			foreach (Player player in Players) {
				Message.SendWeapPickupMessage(player, val);
			}
		}

		public static void SendWeapPickupMessage(byte WeaponID)
		{
			foreach (Player player in Players) {
				Message.SendWeapPickupMessage(player, );
			}
		}
		
	}
	#endregion
	*/
}
