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
        return Arguments.Join(2, ' ');
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
        WriteLine(player, "You have no kick privileges");
        return;
      }

      Player target = Player.Find(Target);

      if (target == null) {
        WriteLine(player, "Couldn't find target player");
        return;
      }

      if (target.Privileges.HasPrivilege("immunity")) {
        WriteLine(player, "Target has general immunity");
        return;
      }

      if (target.Privileges.HasPrivilege("nokick")) {
        WriteLine(player, "Target has kick immunity");
        return;
      }

      KickInformation ki = new KickInformation(player, target, Reason);

      target.Kick(Reason);

      Task.Factory.StartNew(delegate {
        SharpMod.Database.AddKick(ki);
      });
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

    public string Duration {
      get {
        return Arguments[2];
      }
    }

    public bool TryParseDuration(out TimeSpan timespan) {
      int hours;
      if (int.TryParse(Arguments[2], out hours)) {
        timespan = TimeSpan.FromMinutes(hours);
        return true;
      } else {
        return false;
      }
    }

    public string Reason {
      get {
        return Arguments.Join(3, ' ');
      }
    }

    public override void Execute(Player player)
    {
      if (player != null && !player.Privileges.HasPrivilege("ban")) {
        WriteLine(player, "You have no ban privileges");
        return;
      }

      Player target = Player.Find(Target);

      if (target == null) {
        WriteLine(player, "Couldn't find target player");
        return;
      }

      if (target.Privileges.HasPrivilege("immunity")) {
        WriteLine(player, "Target has general immunity");
        return;
      }

      if (target.Privileges.HasPrivilege("noban")) {
        WriteLine(player, "Target has ban immunity");
        return;
      }

      TimeSpan duration;
      if (!TryParseDuration(out duration)) {
        WriteLine(player, "Duration was misformed");
      }

      BanInformation bi = new BanInformation(player, target, duration, Reason);

      int userid = Player.GetUserID(player);

      Task.Factory.StartNew(delegate {
        try {
          SharpMod.Database.AddBan(bi);
          TaskManager.Join(OnSuccess, userid);
        } catch {
          TaskManager.Join(OnFailure, userid);
        }
      });
    }

    protected override void OnSuccess(Player player)
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
      if (player != null && !player.Privileges.HasPrivilege("status")) {
        WriteLine(player, "You have no status privileges");
        return;
      }

      WriteLine(player, " # nick\tauthid\tuserid\tprivileges");

      foreach (Player p in Player.Players) {
        WriteLine(player, "{0:00} {1}\t{2}\t#{3}\t{4}",
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
      : this(new string[] { "smod_reload" })
    {
    }

    public override void Execute(Player player)
    {
      if (player != null && !player.Privileges.HasPrivileges) {
        WriteLine(player, "You have to have at least on privilege to use this command");
        return;
      }

      WriteLine(player, "Reloading all admin privileges");
      Player.ReloadAllPrivileges();
    }
  }

  public class ChangeMap : Command
  {
    public ChangeMap(string[] arguments)
      : base(arguments)
    {
    }

    public ChangeMap(string map)
      : this(new string[] { "smod_map", map })
    {
    }

    public string Map {
      get {
        return Arguments[1];
      }
    }

    public override void Execute(Player player)
    {
      int userid = Player.GetUserID(player);

      if (player != null && !player.Privileges.HasPrivilege("map")) {
        WriteLine(player, "You have no map privileges");
        OnFailure(userid);
        return;
      }


      if (!Server.IsMapValid(Map)) {
        WriteLine(player, "invalid map provided");
        OnFailure(userid);
        return;
      }

      MapChangeInformation mc = new MapChangeInformation(player, Map);

      Task.Factory.StartNew(delegate {
        try {
          SharpMod.Database.AddMapChange(mc);
        } catch {
        }
      });

      Server.ExecuteCommand("changelevel {0}", Map);

      // TODO: make this beautiful, 30 == SVC_INTERMISSSION
      Message.Begin(MessageDestination.AllReliable, 30, IntPtr.Zero, IntPtr.Zero);
      Message.End();

      OnSuccess(userid);
    }
  }

  public class ListMaps : Command
  {
    public ListMaps(string[] arguments)
      : base(arguments)
    {
    }

    public ListMaps()
      : this(new string[] { "smod_maps" })
    {
    }

    public override void Execute(Player player)
    {
      int userid = Player.GetUserID(player);

      if (player != null && !player.Privileges.HasPrivilege("map")) {
        WriteLine(player, "You have no map privileges");
        OnFailure(userid);
        return;
      }

      Task.Factory.StartNew(delegate {
        try {
          TaskManager.Join(List, userid, Server.LoadMapListFromDirectory());
        } catch {
          TaskManager.Join(OnFailure, userid);
        }
      });
    }

    private void List(int userid, string[] maps)
    {
      Player player = null;

      if (userid != 0) {
        player = Player.FindByUserId(userid);
        if (player == null)
          return;
      }

      foreach (string map in maps) {
        WriteLine(player, map);
      }

      OnSuccess(player);
    }
  }
}

