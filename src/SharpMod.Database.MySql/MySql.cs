using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Dynamic;

using SharpMod;
using SharpMod.Database;
using SharpMod.Helper;

using Psi;

using Manos.IO;
using Manos.MySql;

namespace SharpMod.Database.MySql
{
  public class MySqlDatabase : IDatabase
  {
    private static string createUserTable = @"CREATE TABLE IF NOT EXISTS users (id int PRIMARY KEY NOT NULL AUTO_INCREMENT, uniqueid varchar(50) NOT NULL, access text NOT NULL);";

    private void CreateTables()
    {
      Client.Query(createUserTable);

    }

    public void FindByUniqueId(string uniqueId)
    {
      Client.Query(string.Format("SELECT * FROM users WHERE uniqueid = {0} LIMIT 1", uniqueId)).On(drow: (drow) => {
      });
    }

    MySqlClient Client { get; set; }

    public MySqlDatabase()
    {
      Client = new MySqlClient(SharpMod.Context);

    }

    public string Hostname  { get; protected set; }
    public string Username  { get; protected set; }
    public string Password  { get; protected set; }
    public string Database  { get; protected set; }

    public bool Load(XmlDocument doc)
    {
      try {
        var config = doc.GetXmlElement("mysql");

        Client.IPEndPoint = new IPEndPoint(IPAddress.Parse(config.GetInnerText("hostname")),
                                               int.Parse(config.GetInnerText("port")));
        Client.Username  = config.GetInnerText("username");
        Client.Password  = config.GetInnerText("password");
        Client.Database  = config.GetInnerText("database");

        Client.Connect();
        Client.Query(string.Format("use {0}", Client.Database));
        CreateTables();

      } catch {
        return false;
      }
      return true;
    }

    public void AddBan(BanInfo bi, Action<Exception, bool> callback)
    {
      callback(null, false);
    }

    public void AddKick(KickInfo ki, Action<bool> callback)
    {
      callback(false);
    }

    public void AddMapChange(MapChangeInfo mi, Action<bool> callback)
    {
      callback(false);
    }

    public void GetActiveBan(IPlayerExtendedInfo player, Action<BanInfo> callback)
    {
      callback(null);
    }

    public void GetAllBans(Action<Exception, BanInfo[]> callback)
    {
      callback(null, new BanInfo[0]);
    }

    public void LoadPrivileges(IPlayerExtendedInfo player, Action<Privileges> callback)
    {
      Privileges priv = new Privileges("");
      Client.Query(string.Format("SELECT * FROM users WHERE uniqueid = '{0}' LIMIT 1", player.AuthId))
      .On(row: (row) => {
          priv = new Privileges(row.GetValue("access") as string);
      }, end: () => {
          callback(priv);
      });
    }

    public void SavePrivileges(IPlayerExtendedInfo player, string access, Action<bool> callback)
    {
      Client.Query(string.Format("UPDATE users SET access = '{0}' where uniqueid = '{1}'", access, player.AuthId))
      .OnEnd(delegate {
          callback(true);
      });
    }
  }
}

