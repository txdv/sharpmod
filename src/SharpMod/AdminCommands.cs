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
using SharpMod.Helper;
using SharpMod.Database;
using System.Threading.Tasks;

namespace SharpMod.Commands
{
  public class Kick : Command
  {
    public string Target {
      get {
        return Arguments[1];
      }
    }

    public string Reason {
      get {
        if (Arguments.Length < 2) return string.Empty;
        return Arguments[2];
      }
    }

    public Kick(string[] arguments)
      : base(arguments)
    {
    }

    public Kick(string player)
      : this(new string[] { "smod_kick",  player })
    {
    }

    public Kick(Player player)
      : this(player.Name)
    {
    }

    public Kick(string player, string reason)
      : this(new string[] { "smod_kick", player, reason })
    {
    }

    public Kick(Player player, string reason)
      : this(new string[] { "smod_kick", player.Name, reason })
    {
    }

    public override void Execute(Player player)
    {
      if (player != null && !player.Privileges.HasPrivilege("kick")) {
        player.PrintConsole("You have no kick privileges\n");
        return;
      }
      
      Player target = Player.Find(Target);

      if (target == null) {
        player.PrintConsole("Couldn't find target player\n");
        return;
      }

      if (target.Privileges.HasPrivilege("immunity")) {
        player.PrintConsole("Target has general immunity\n");
        return;
      }

      if (target.Privileges.HasPrivilege("nokick")) {
        player.PrintConsole("Target has kick immunity\n");
        return;
      }

      KickInformation ki = new KickInformation();
      ki.AdminAuthId  = player.AuthID;
      ki.PlayerAuthId = target.AuthID;
      ki.Reason       = Reason;
      ki.Date         = DateTime.Now;

      target.Kick(Reason);

      Task.Factory.StartNew(delegate {
        SharpMod.Database.AddKick(ki);
      });

      OnSuccess();
    }
  }

  public class Ban : Command
  {
    public Ban(string[] arguments)
      : base(arguments)
    {
    }

    public Ban(string player)
      : this(new string[] { "smod_ban", player })
    {
    }

    public Ban(string player, string banlength)
      : this(new string[] { "smod_ban", player, banlength })
    {
    }

    public Ban(string player, string banlength, string reason)
      : this(new string[] { "smod_ban", banlength, reason })
    {
    }

    public Ban(Player player, string banlength, string reason)
      : this(new string[] { "smod_ban", player.AuthID, banlength, reason })
    {
    }

    public Ban(Player player, int banlength, string reason)
      : this(new string[] { "smod_ban", player.AuthID, String.Format("{0}m", banlength), reason })
    {
    }

    protected string Target {
      get {
        return Arguments[1];
      }
    }

    public string Reason {
      get {
        if (Arguments.Length < 3) return string.Empty;
        return Arguments[3];
      }
    }

    public override void Execute(Player player)
    {
      if (player != null && !player.Privileges.HasPrivilege("ban")) {
        player.PrintConsole("You have no ban privileges\n");
        return;
      }

      Player target = Player.Find(Target);

      if (target == null) {
        target.PrintConsole("Couldn't find target player\n");
        return;
      }

      if (target.Privileges.HasPrivilege("immunity")) {
        player.PrintConsole("Target has general immunity\n");
        return;
      }

      if (target.Privileges.HasPrivilege("noban")) {
        player.PrintConsole("Target has ban immunity\n");
        return;
      }

      BanInformation bi = new BanInformation();
      bi.AdminAuthId  = player.AuthID;
      bi.PlayerAuthId = target.AuthID;
      bi.Reason       = Reason;
      bi.Date         = DateTime.Now;
      bi.Duration     = TimeSpan.FromSeconds(int.Parse(Arguments[2]));

      Task.Factory.StartNew(delegate {
        try {
          SharpMod.Database.AddBan(bi);
          TaskManager.Join((Action)OnSuccess);
        } catch {
          TaskManager.Join((Action)OnFailure);
        }
      });
    }

    protected override void OnSuccess()
    {
      Player target = Player.Find(Target);

      if (target == null) {
        return;
      }

      target.Kick(Reason);
    }
  }

  public class Who : Command
  {
    public Who(string[] arguments)
      : base(arguments)
    {
    }

    public Who()
      : this(new string[] { "smod_who" })
    {
    }

    public override void Execute(Player player)
    {
      if (!player.Privileges.HasPrivilege("status")) {
        player.PrintConsole("You have no status privileges\n");
        return;
      }

      player.PrintConsole(" # nick\tauthid\tuserid\tprivileges\n");

      foreach (Player p in Player.Players) {
        player.PrintConsole("{0:00} {1}\t{2}\t#{3}\t{4}\n",
                            p.Index,
                            p.Name,
                            p.AuthID,
                            p.UserID,
                            p.Privileges.PrivilegesString);
      }
    }
  }

  public class AdminReload : Command
  {
    public AdminReload(string[] arguments)
      : base(arguments)
    {
    }

    public AdminReload()
      : this(new string[]  { "smod_reload" })
    {
    }

    public override void Execute(Player player)
    {
      if (player != null && !player.Privileges.HasPrivileges) {
        player.PrintConsole("You have to have at least on privilege to use this command\n");
        return;
      }

      player.PrintConsole("Reloading all admin privileges\n");
      Player.ReloadAllPrivileges();
    }
  }
}

