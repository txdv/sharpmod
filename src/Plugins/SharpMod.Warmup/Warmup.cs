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
using SharpMod;
using SharpMod.Helper;
using SharpMod.CounterStrike;

using Manos;
using Manos.IO;

namespace SharpMod.Warmup
{
  public static class HelperMethods
  {
    public static bool Bool(this string str) {
      return !((str == "0") || (str == "off"));
    }

    public static bool Bool(this CVar cvar) {
      return cvar.String.Bool();
    }

    public static float GetFloat(this CVar cvar) {
      return float.Parse(cvar.String);
    }

    public static int GetInt(this CVar cvar) {
      return int.Parse(cvar.String);
    }
  }

  public class Warmup : BasicPlugin
  {
    CVar warmup    = new CVar("warmup",              "on");
    CVar time      = new CVar("warmup_time",         "5" );
    CVar message   = new CVar("warmup_message",      "1" );
    CVar knifeonly = new CVar("warmup_knifeonly",    "1" );
    CVar armvis    = new CVar("warmup_armoury_vis",  "0" );
    CVar armpick   = new CVar("warmup_armoury_pick", "0" );

    ITimerWatcher messageWatcher = null;

    public bool Enabled { get; protected set; }
    public int Timeout { get; protected set; }

    public Warmup()
    {
      Message.Intercept("TextMsG", (Action<string, string>)EventGameStart);

      Player.RegisterCommand("warmup_start", delegate { StartWarmup(time.GetInt()); });

      messageWatcher = SharpMod.Context.CreateTimerWatcher(TimeSpan.FromSeconds(1), delegate {
        if (Timeout == 0) {
          EndWarmup();
        } else {
          if (message.Bool()) {
            foreach (var player in Player.Players) {
              SendMessage(player, Timeout);
            }
          }
          Timeout--;
        }
      });
    }

    private void EventGameStart(string str1, string str2)
    {
      if (str2 == "#Game_Commencing") {
        if (warmup.Bool()) {
          StartWarmup(time.GetInt());
        }
      }
    }

    public void StartWarmup(int length)
    {
      Enabled = true;
      Timeout = length;

      foreach (var player in Player.Players) {
        ClearPlayer(player);
      }

      SetArmouryVisibility(armvis.Bool(), armpick.Bool());

      messageWatcher.Start();
    }

    public void EndWarmup()
    {
      Enabled = false;
      Server.ExecuteCommand("sv_restart 1");

    SetArmouryVisibility(true, true);

      messageWatcher.Stop();
    }

    public void ClearPlayer(Player player)
    {
      if (knifeonly.Bool()) {
        player.StripUserWeapons();
        player.GiveItem("weapon_knife");
        player.SetMoney(0);
      }
    }

    public void SetArmouryVisibility(bool vis, bool pick)
    {
      foreach (var entity in Entity.Find("classname", "armoury_entity")) {
        entity.Solid = (pick ? Solid.Trigger : Solid.Not);
        entity.Nodraw = !vis;
      }
    }

    public void SendMessage(Player player, float time)
    {
      Message.Begin(MessageDestination.OneReliable, Message.GetUserMessageID("TextMsg"), IntPtr.Zero, player.Pointer);
      Message.WriteByte(4);
      Message.WriteString("#Game_will_restart_in");
      Message.WriteString(time.ToString());
      Message.WriteString("SECOND");
      Message.End();

      Message.Begin(MessageDestination.OneReliable, Message.GetUserMessageID("TextMsg"), IntPtr.Zero, player.Pointer);
      Message.WriteByte(2);
      Message.WriteString("#Game_will_restart_in_console");
      Message.WriteString(time.ToString());
      Message.WriteString("SECOND");
      Message.End();
    }
  }
}
