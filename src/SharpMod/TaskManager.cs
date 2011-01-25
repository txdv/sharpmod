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
using SharpMod.Helper;

namespace SharpMod
{
  public static class TaskManager
  {
    class Task
    {
      public bool     Repeat   { get; set; }
      public float    Time     { get; set; }
      public float    AddTime  { get; set; }
      public Delegate Function { get; set; }
      public object[] Parameters { get; set; }

      public Task() { }
    }

    private static List<Task> list = new List<Task>();

    public static void Join(Delegate function)
    {
      SetTask(function, 0.0f);
    }

    public static void SetTask(Delegate function, float time)
    {
      SetTask(function, time, false);
    }

    public static void SetTask(Delegate function, float time, bool repeat)
    {
      SetTask(function, time, repeat, new object[] { });
    }

    public static void SetTask(Delegate function, float time, bool repeat, object[] parameters)
    {
      Task t = new Task() {
        Function = function,
        AddTime = Server.TimeFloat,
        Time = time,
        Repeat = repeat,
        Parameters = parameters
      };

      lock (list) {
        list.Add(t);
      }
    }

    public static void SetTask(Delegate function, TimeSpan time)
    {
      SetTask(function, time.ToFloat());
    }

    public static void SetTask(Delegate function, TimeSpan time, bool repeat)
    {
      SetTask(function, time.ToFloat(), repeat);
    }

    public static void SetTask(Delegate function, TimeSpan time, bool repeat, object[] parameters)
    {
      SetTask(function, time.ToFloat(), repeat, parameters);
    }

    internal static void WorkFrame()
    {
      lock (list) {
        List<Task> delete = new List<Task>();
        foreach (Task task in list) {
          float exectime = task.AddTime + task.Time;
          if (exectime <= Server.TimeFloat) {

            task.Function.DynamicInvoke(task.Parameters);

            if (task.Repeat) {
              task.AddTime = exectime;
            } else {
              delete.Add(task);
            }

          }
        }

        foreach (Task task in delete) {
          list.Remove(task);
        }
      }
    }
  }
}

