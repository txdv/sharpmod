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
using System.IO;
using System.Xml;
using System.Net;
using System.Reflection;
using System.Collections.Generic;
using Psi;
using SharpMod.Helper;

namespace SharpMod.Database
{
  public interface IPlayerExtendedInfo : IPlayerInfo
  {
    IPAddress IPAddress { get; }
  }

  public class PlayerInfo : IPlayerExtendedInfo
  {
    public PlayerInfo(Player player)
    {
      IPAddress = player.IPAddress;
      Name      = player.Name;
      UserId    = player.UserID;
      AuthId    = player.AuthID;
      // TODO: do something about this, we need
      // some serious rethinking about the class
      // model
      Team      = "";
    }

    #region IPlayerExtendedInfo implementation
    public IPAddress IPAddress { get; set; }
    #endregion

    #region IPlayerInfo implementation
    public string Name   { get; set; }
    public int    UserId { get; set; }
    public string AuthId { get; set; }
    public string Team   { get; set; }
    #endregion
  }

  public abstract class AdminCommandInfo
  {
    public AdminCommandInfo()
    {
      Date = DateTime.Now;
    }

    public AdminCommandInfo(PlayerInfo admin)
      : this()
    {
      Admin = admin;
    }

    public AdminCommandInfo(Player admin)
      : this(new PlayerInfo(admin))
    {
    }

    public DateTime            Date  { get; set; }
    public IPlayerExtendedInfo Admin { get; set; }
  }

  public class BanInfo : AdminCommandInfo
  {
    public BanInfo()
      : base()
    {
    }

    public BanInfo(Player admin, Player target, TimeSpan duration, string reason)
      : base(admin)
    {
      // PlayerAuthId = target.AuthID;
      Player = new PlayerInfo(target);
      Duration     = duration;
      Reason       = reason;
    }

    public TimeSpan            Duration { get; set; }
    public IPlayerExtendedInfo Player   { get; set; }
    public string              Reason   { get; set; }
  }

  public class UnbanInfo : AdminCommandInfo
  {
    public UnbanInfo()
      : base()
    {
    }

    public UnbanInfo(Player admin)
      : base(admin)
    {
    }
  }

  public class KickInfo : AdminCommandInfo
  {
    public KickInfo()
      : base()
    {
    }

    public KickInfo(Player admin, Player target, string reason)
      : base(admin)
    {
      PlayerAuthId = target.AuthID;
      Reason       = reason;
    }

    public string PlayerAuthId { get; set; }
    public string Reason       { get; set; }

    /// <summary>
    /// Searches by PlayerAuthId for the actual Player
    /// in the server
    /// </summary>
    public Player Player {
      get {
        return Player.FindByAuthId(PlayerAuthId);
      }
    }
  }

  public class MapChangeInfo : AdminCommandInfo
  {
    public MapChangeInfo()
      : base()
    {
    }

    public MapChangeInfo(Player admin, string map)
      : base(admin)
    {
      Map = map;
    }

    public string Map { get; set; }
  }

  public interface IDatabase
  {
    bool Load(XmlDocument doc);

    Privileges LoadPrivileges(IPlayerExtendedInfo player);
    bool SavePrivileges(IPlayerExtendedInfo player, string access);

    BanInfo[] GetAllBans();
    BanInfo GetActiveBan(IPlayerExtendedInfo player);
    bool AddBan(BanInfo bi);

    bool AddKick(KickInfo ki);

    bool AddMapChange(MapChangeInfo mi);
  }

  public class DefaultDatabase : IDatabase
  {

    public static IDatabase Load(string filename)
    {
      return Load(new FileInfo(filename));
    }

    public static IDatabase Load(FileInfo fi)
    {
      Assembly asm = Assembly.LoadFile(fi.FullName);
      foreach (Type type in asm.GetTypes()) {
        if (type.GetInterface("IDatabase") != null) {
          return (IDatabase)Activator.CreateInstance(type);
        }
      }
      return null;
    }

    public bool Load(XmlDocument doc)
    {
      return false;
    }

    public Privileges LoadPrivileges(IPlayerExtendedInfo player)
    {
      return null;
    }

    public bool SavePrivileges(IPlayerExtendedInfo player, string access)
    {
      return false;
    }

    public BanInfo[] GetAllBans()
    {
      return null;
    }

    public BanInfo GetActiveBan(IPlayerExtendedInfo player)
    {
      return null;
    }

    public bool AddBan(BanInfo bi)
    {
      return false;
    }

    public bool AddKick(KickInfo ki)
    {
      return false;
    }

    public bool AddMapChange(MapChangeInfo mi)
    {
      return false;
    }
  }

  public class Privileges
  {
    List<string> privileges;

    public Privileges(string priv)
    {
      privileges = new List<string>(priv.Split(' '));
    }

    public bool HasPrivileges {
      get {
        return privileges.Count > 0;
      }
    }

    public bool HasPrivilege(string priv)
    {
      return privileges.Contains(priv.ToLower());
    }

    public string PrivilegesString {
      get {
        return privileges.ToArray().Join(' ');
      }
      set {
        privileges = new List<string>(value.Split(' '));
      }
    }

    public bool RemovePrivilege(string priv)
    {
      return privileges.Remove(priv);
    }

    public void AddPrivilege(string priv)
    {
      privileges.Add(priv);
    }

  }
}
