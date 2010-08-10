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
using System.Collections;
using System.Collections.Generic;
using SharpMod;

namespace SharpMod.Nextmap
{
  public class MainClass : BasicPlugin
  {
    /// <summary>
    /// Returns a list of all valid maps in the directory.
    /// </summary>
    /// <returns>
    /// String array of all map names <see cref="System.String[]"/>
    /// </returns>
    private string[] LoadMapListFromDirectory()
    {
      List<string> list = new List<string>();
      foreach (FileInfo fi in (new DirectoryInfo(Server.GameDirectory + "/maps").GetFiles("*.bsp")))
      {
        string map = fi.Name.Substring(0, fi.Name.Length-4);
        if (Server.IsMapValid(map))
        {
          list.Add(map);
        }
      }
      return list.ToArray();
    }


    /// <summary>
    /// Returns a list of all valid maps in the mapcycle.
    /// </summary>
    /// <returns>
    /// String array of all map names <see cref="System.String[]"/>
    /// </returns>
    private string[] LoadMapListFromMapcycle()
    {
      try {

        List<string> list = new List<string>();
        StreamReader sr = new StreamReader(File.Open(Server.GameDirectory + "/" + CVar.GetValue("mapcyclefile"), FileMode.Open));
        while (!sr.EndOfStream)
        {
          string line = sr.ReadLine();
          if (line.ToLower().EndsWith(".bsp"))
          {
            string map = line.Substring(0, line.Length-4);
            if (Server.IsMapValid(map))
            {
              list.Add(map);
            }
          }
        }
        return list.ToArray();
      } catch {
        return new string[] {};
      }

    }

    public override void Load ()
    {
    }

  }
}
