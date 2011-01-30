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
using SharpMod.Helper;

namespace SharpMod.Database
{
  public class BanInformation
  {
    public DateTime Date       { get; set; }
    public TimeSpan Duration   { get; set; }
    public string AdminAuthId  { get; set; }
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

    /// <summary>
    /// Searches by AdminAuthId for the actual Admin
    /// in the server
    /// </summary>
    public Player Admin {
      get {
        return Player.FindByAuthId(AdminAuthId);
      }
    }
  }

  public class KickInformation
  {
    public DateTime Date       { get; set; }
    public string AdminAuthId  { get; set; }
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

    /// <summary>
    /// Searches by AdminAuthId for the actual Admin
    /// in the server
    /// </summary>
    public Player Admin {
      get {
        return Player.FindByAuthId(AdminAuthId);
      }
    }
  }

  public interface IDatabase
  {
    bool Load(XmlDocument doc);

    Privileges LoadPrivileges(string authId);
    bool SavePrivileges(string authId, string access);

    BanInformation GetActiveBan(string authId);
    bool AddBan(BanInformation bi);

    bool AddKick(KickInformation ki);
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

    public Privileges LoadPrivileges(string authId)
    {
      return null;
    }

    public bool SavePrivileges(string authId, string access)
    {
      return false;
    }

    public BanInformation GetActiveBan(string authId)
    {
      return null;
    }

    public bool AddBan(BanInformation bi)
    {
      return false;
    }

    public bool AddKick(KickInformation ki)
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
