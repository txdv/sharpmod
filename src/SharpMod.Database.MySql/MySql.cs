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
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;

using Psi;
using SharpMod.Database;
using SharpMod.Helper;

namespace SharpMod.Database.MySql
{
  public class MysqlDatabase : IDatabase
  {
    public string Hostname  { get; protected set; }
    public string Username  { get; protected set; }
    public string Password  { get; protected set; }
    public string Database  { get; protected set; }
    public string Tablename { get; protected set; }

    protected string ConnectionString {
      get {
        return String.Format("Server={0};Database={1};User ID={2};Password={3};Pooling=false",
                             Hostname, Database, Username, Password);
      }
    }

    public bool Load(XmlDocument doc)
    {
      var config = doc.GetXmlElement("mysql");

      Hostname  = config.GetInnerText("hostname");
      Username  = config.GetInnerText("username");
      Password  = config.GetInnerText("password");
      Database  = config.GetInnerText("database");
      Tablename = config.GetInnerText("tablename");


      Dictionary<string, string> properties = new Dictionary<string, string>();

      properties.Add("connection.driver_class",      "NHibernate.Driver.MySqlDataDriver");
      properties.Add("dialect",                      "NHibernate.Dialect.MySQL5Dialect");
      properties.Add("connection.provider",          "NHibernate.Connection.DriverConnectionProvider");
      properties.Add("connection.connection_string", ConnectionString);
      properties.Add("proxyfactory.factory_class",   "NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle");
      InPlaceConfigurationSource source = new InPlaceConfigurationSource();

      source.Add(typeof(ActiveRecordBase), properties);

      ActiveRecordStarter.Initialize(source,
                                     typeof(Ban),
                                     typeof(Kick),
                                     typeof(User),
                                     typeof(MapChange),
                                     typeof(PlayerIPAddress),
                                     typeof(PlayerWorldId),
                                     typeof(PlayerName),
                                     typeof(Unban),
                                     typeof(Config)
                                     );

      ActiveRecordStarter.CreateSchema();

      Config cfg = new Config("uniqueid", "authid");
      cfg.Save();

      return true;
    }

    public Privileges LoadPrivileges(IPlayerExtendedInfo player)
    {
      try {
        User user = User.Find(player);

        if (user == null)
          return null;

        return new Privileges(user.Access);
      } catch (Exception e) {
        TaskManager.Join(Server.LogError,
                         string.Format("Error while loading privileges with mysql: {0}", e.Message));
        return null;
      }
    }

    public bool SavePrivileges(IPlayerExtendedInfo player, string access)
    {
      try {
        User user = User.FindByUniqueId(player.AuthId);

        if (user == null) {
          user = new User();
          user.UniqueId = player.AuthId;
          user.Access = access;
          user.Save();
          return true;
        } else {
          user.Access = access;
          user.Save();
          return true;
        }
      } catch (Exception e) {
        TaskManager.Join(Server.LogError,
                         string.Format("Error while saving privileges with mysql: {0}", e.Message));
        return false;
      }
    }

    public BanInfo[] GetAllBans()
    {
      try {
        var list = from b in Ban.GetAll()
                   select b.GetBanInfo();

        return list.ToArray();
      } catch {
        return null;
      }
    }

    public BanInfo GetActiveBan(IPlayerExtendedInfo player)
    {
      try {
        return Ban.FindActiveBan(player.AuthId).GetBanInfo();
      } catch {
        return null;
      }
    }

    public bool AddBan(BanInfo bi)
    {
      try {
        Ban ban = new Ban(bi);
        ban.Save();
        return true;
      } catch {
        return false;
      }
    }

    public bool Unban()
    {
      try {
        return true;
      } catch {
        return false;
      }
    }

    public bool AddKick(KickInfo ki)
    {
      try {
        Kick kick = new Kick(ki);
        kick.Save();
        return true;
      } catch {
        return false;
      }
    }

    public bool AddMapChange(MapChangeInfo mi)
    {
      try {
        MapChange mc = new MapChange(mi);
        mc.Save();
        return true;
      } catch {
        return false;
      }
    }

  }
}

