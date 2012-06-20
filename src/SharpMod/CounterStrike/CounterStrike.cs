using System;
using System.Reflection;
using SharpMod.Messages;

namespace SharpMod.CounterStrike
{
	public class CounterStrike
	{
		#region team represantations
		public static string GetTeamString(Team team)
		{
			switch (team) {
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
			switch (team) {
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
			switch (color) {
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
			switch (team) {
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

		public delegate void BuyzoneDelegate(Player player, bool inzone);
		public static event BuyzoneDelegate Buyzone;

		public static void Init()
		{
			Message.Intercept("StatusIcon", (Action<Player, byte, string>)StatusIcon);
		}

		internal static void StatusIcon(Player player, byte status, string spriteName)
		{
			switch (spriteName) {
			case "buyzone":
				if (Buyzone != null) {
					Buyzone(player, status == 1);
				}
				break;
			}
		}
	}
}
