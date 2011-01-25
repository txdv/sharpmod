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
using System.Collections.Generic;
using SharpMod.Helper;
using MySql.Data.MySqlClient;

namespace SharpMod
{
  public class DefaultDatabase
  {
    public virtual Privileges LoadPrivileges(string playerauth)
    {
      return null;
    }

    public virtual bool SavePrivileges(string playerauth, string privileges)
    {
      return false;
    }
  }

  public class MysqlDatabase : DefaultDatabase
  {

    public string Hostname  { get; protected set; }
    public string Username  { get; protected set; }
    public string Password  { get; protected set; }
    public string Database  { get; protected set; }
    public string Tablename { get; protected set; }

    protected string ConnectionString {
      get {
        return String.Format("server={0};database={1};uid={2};password={3}",
                             Hostname, Database, Username, Password);
      }
    }

    public MysqlDatabase(XmlDocument doc)
    {

      var config = doc.GetXmlElement("mysql");

      Hostname  = config.GetInnerText("hostname");
      Username  = config.GetInnerText("username");
      Password  = config.GetInnerText("password");
      Database  = config.GetInnerText("database");
      Tablename = config.GetInnerText("tablename");

    }

    public override Privileges LoadPrivileges(string playerauth)
    {
      var con = new MySqlConnection(ConnectionString);
      var cmd = con.CreateCommand();
      cmd.CommandText = "SELECT auth, access FROM users";
      MySqlDataReader reader = null;

      try {
        con.Open();

        if (!con.TableExists("users")) {
          var tcmd = con.CreateCommand();
          tcmd.CommandText = "CREATE TABLE users (id INT NOT NULL AUTO_INCREMENT PRIMARY KEY, auth varchar(255), access varchar(255));";
          tcmd.ExecuteNonQuery();
        }

        reader = cmd.ExecuteReader();
        while (reader.Read()) {

          string auth   = reader.GetValue(0) as string;
          string access = reader.GetValue(1) as string;

          if (auth == playerauth) {
            return new Privileges(access);
          }
        }
        return null;
      } catch (Exception e) {
        Server.LogError("MySql error while reading admin privileges: {0}", e.Message);
        return null;
      } finally {
        if (reader != null) reader.Close();
        con.Close();
      }
    }

    public override bool SavePrivileges(string playerauth, string privileges)
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
