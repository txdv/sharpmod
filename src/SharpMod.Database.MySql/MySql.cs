using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;

using Psi;

using SharpMod.Database;
using SharpMod.Helper;

namespace SharpMod.Database.MySql
{
  public class MySqlDatabase : IDatabase
  {
    public string Hostname  { get; protected set; }
    public string Username  { get; protected set; }
    public string Password  { get; protected set; }
    public string Database  { get; protected set; }
    public string Tablename { get; protected set; }

    public bool Load(XmlDocument doc)
    {
      try {
        var config = doc.GetXmlElement("mysql");

        Hostname  = config.GetInnerText("hostname");
        Username  = config.GetInnerText("username");
        Password  = config.GetInnerText("password");
        Database  = config.GetInnerText("database");
        Tablename = config.GetInnerText("tablename");
      } catch {
        return false;
      }
      return true;
    }

    public void AddBan(BanInfo bi, Action<Exception, bool> callback)
    {
      throw new NotImplementedException ();
    }

    public void AddKick(KickInfo ki, Action<bool> callback)
    {
      throw new NotImplementedException ();
    }

    public void AddMapChange(MapChangeInfo mi, Action<bool> callback)
    {
      throw new NotImplementedException ();
    }

    public void GetActiveBan(IPlayerExtendedInfo player, Action<BanInfo> callback)
    {
      throw new NotImplementedException ();
    }

    public void GetAllBans(Action<Exception, BanInfo[]> callback)
    {
      throw new NotImplementedException ();
    }


    public void LoadPrivileges(IPlayerExtendedInfo player, Action<Privileges> callback)
    {
      throw new NotImplementedException ();
    }

    public void SavePrivileges(IPlayerExtendedInfo player, string access, Action<bool> callback)
    {
      throw new NotImplementedException ();
    }
  }
}

