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
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace SharpMod.Database.MySql
{
  [ActiveRecord("User")]
  public class User : ActiveRecordBase
  {
    public User()
    {
    }

    [PrimaryKey]
    public int Id { get; set; }

    [Property]
    public string AuthId { get; set; }

    [Property]
    public string Access { get; set; }

    public static User FindByAuthId(string authId)
    {
      return (User)FindOne(typeof(User), Expression.Eq("AuthId", authId));
    }
  }

  [ActiveRecord]
  public class Ban : ActiveRecordBase
  {
    public Ban()
    {
    }

    public Ban(BanInformation bi)
    {
      Date         = bi.Date;
      Duration     = bi.Duration;
      AdminAuthId  = bi.AdminAuthId;
      PlayerAuthId = bi.PlayerAuthId;
      Reason       = bi.Reason;
    }

    [PrimaryKey]
    public int Id { get; set; }

    [Property]
    public DateTime Date { get; set; }

    [Property]
    public TimeSpan Duration { get; set; }

    [Property]
    public string AdminAuthId { get; set; }

    [Property]
    public string PlayerAuthId { get; set; }

    [Property]
    public string Reason { get; set; }

    public static Ban FindActiveBan(string authId)
    {
      var query = from ban in (Ban[])FindAll(typeof(Ban), Expression.Eq("PlayerAuthId", authId))
                  where ban.Date + ban.Duration < DateTime.Now
                  select ban;

      return query.First();
    }

    public BanInformation GetBanInformation()
    {
      BanInformation bi = new BanInformation();

      bi.Date         = Date;
      bi.Duration     = Duration;
      bi.AdminAuthId  = AdminAuthId;
      bi.PlayerAuthId = PlayerAuthId;
      bi.Reason       = Reason;

      return bi;
    }
  }
}

