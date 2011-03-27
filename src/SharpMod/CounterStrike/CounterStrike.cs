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
        if (Buyzone != null) Buyzone(player, status == 1);
        break;
      }
    }
  }
}
