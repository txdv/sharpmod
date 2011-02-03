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
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace SharpMod.Database.MySql
{
  [ActiveRecord]
  public class User : ActiveRecordBase
  {
    [PrimaryKey]
    public int Id { get; set; }

    [Property]
    public string UniqueId { get; set; }

    public IPlayerExtendedInfo Player {
      get {
        return null;
      }
    }

    [Property]
    public string Access { get; set; }

    public static User FindByUniqueId(string uniqueId)
    {
      return (User)FindOne(typeof(User), Expression.Eq("UniqueId", uniqueId));
    }

    public static User Find(IPlayerExtendedInfo pi)
    {
      switch (Config.GetValue("uniqueid")) {
      case "authid":
        return FindByUniqueId(pi.AuthId);
      default:
        throw new Exception("The uniqueid configuration should be ether authid, ipaddress or name");
      }
    }
  }

  public abstract class AdminCommand : ActiveRecordBase
  {
    public AdminCommand()
    {
    }

    public AdminCommand(AdminCommandInformation aci)
    {
      Date        = aci.Date;
      // TODO: change
      //AdminAuthId = aci.Admin.AuthId;
    }

    [PrimaryKey]
    public int Id { get; set; }

    [Property]
    public DateTime Date { get; set; }

    [Property]
    public string AdminAuthId { get; set; }
  }

  [ActiveRecord]
  public class Kick : AdminCommand
  {
    public Kick()
    {
    }

    public Kick(KickInformation ki)
      : base(ki)
    {
      PlayerAuthId = ki.PlayerAuthId;
      Reason       = ki.Reason;
    }
    
    [Property]
    public string PlayerAuthId { get; set; }

    [Property]
    public string Reason { get; set; }

    public BanInformation GetBanInformation()
    {
      BanInformation bi = new BanInformation();
      //bi.PlayerAuthId = PlayerAuthId;
      bi.Reason       = Reason;

      return bi;
    }
  }

  [ActiveRecord]
  public class Ban : AdminCommand
  {
    public Ban()
    {
    }

    public Ban(BanInformation bi)
      : base(bi)
    {
      Duration     = bi.Duration;
      //PlayerAuthId = bi.PlayerAuthId;
      Reason       = bi.Reason;
    }

    [Property]
    public TimeSpan Duration { get; set; }

    [Property]
    public string PlayerAuthId { get; set; }

    [Property]
    public string Reason { get; set; }

    public static Ban FindActiveBan(string authId)
    {
      var query = from ban in (Ban[])FindAll(typeof(Ban), Expression.Eq("PlayerAuthId", authId))
                  where ban.Date + ban.Duration >= DateTime.Now
                  select ban;

      return query.First();
    }

    public static Ban[] GetAll()
    {
      return (Ban[])FindAll(typeof(Ban));
    }

    public BanInformation GetBanInformation()
    {
      BanInformation bi = new BanInformation();

      bi.Date         = Date;
      bi.Duration     = Duration;
      //bi.AdminAuthId  = AdminAuthId;
      //bi.PlayerAuthId = PlayerAuthId;
      bi.Reason       = Reason;

      return bi;
    }

    [OneToOne]
    public Unban Unban { get; set; }
  }

  [ActiveRecord]
  public class Unban : AdminCommand
  {
    public Unban()
    {
    }

    public Unban(UnbanInformation ui)
      : base(ui)
    {
    }

    [OneToOne]
    public Ban Ban { get; set; }
  }

  [ActiveRecord]
  public class MapChange : AdminCommand
  {
    public MapChange()
    {
    }

    public MapChange(MapChangeInformation mi)
      : base(mi)
    {
      Map = mi.Map;
    }

    [Property]
    public string Map { get; set; }
  }


  public abstract class PlayerId
  {
    [PrimaryKey]
    public int Id { get; set; }

    [Property]
    public int TotalUses { get; set; }

    [Property]
    public DateTime FirstSeen { get; set; }

    [Property]
    public DateTime LastSeen { get; set; }
  }

  [ActiveRecord]
  public class PlayerName : PlayerId
  {
    [Property]
    public string Name { get; set; }

  }

  [ActiveRecord]
  public class PlayerIPAddress : PlayerId
  {
    [Property]
    public int IP { get; set; }

    public IPAddress IPAddress {
      get {
        return IPAddress.Parse(IP.ToString());
      }
    }
  }

  [ActiveRecord]
  public class PlayerWorldId : PlayerId
  {
    [Property]
    public string WorldId { get; set; }
  }

  [ActiveRecord]
  public class Config : ActiveRecordBase
  {
    public Config()
    {
    }

    public Config(string var, string value)
    {
      Var   = var;
      Value = value;
    }

    [PrimaryKey]
    public int Id { get; set; }

    [Property]
    public string Var { get; set; }

    [Property]
    public string Value { get; set; }


    public static Config GetByVar(string var)
    {
      return (Config)FindOne(typeof(Config), Expression.Eq("Var", var));
    }

    public static string GetValue(string var)
    {
      return GetByVar(var).Value;
    }

  }

}

