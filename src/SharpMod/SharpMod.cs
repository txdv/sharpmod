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
using System.Reflection;
using SharpMod.Debug;
using SharpMod.Helper;

namespace SharpMod
{

	public sealed class SharpMod
	{

    /// <summary>
    /// Variable for holding the Version of sharpmod
    /// </summary>
    public static CVar Version { get; private set; }
    /// <summary>
    /// Returns the License text of SharpMod (GPL3)
    /// </summary>
    public static string License
    {
      get
      {
        StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("sharpmod.gpl.txt"));
        string ret = sr.ReadToEnd();
        sr.Close();
        return ret;
      }
    }

		public static void Init()
		{
      Version = new CVar("sharpmod_version", "0.1");

      // load plugins

      Server.Print(License);
      Server.RegisterCommand("sharp", sharp);

      // Initialize plugin system
      PluginManager.GetInstance();

      Player.RegisterCommand("say /dump", dump);
    }

    static void dump(Player player, Command cmd)
    {
      int i = 0;
      while (File.Exists("output" + i.ToHex() + ".txt"))
      {
        i++;
      }
      string fn = "output" + i.ToHex() + ".txt";

      StreamWriter sw = new StreamWriter(File.OpenWrite(fn));
      MemoryTracker.PrintPrivateData(player, sw);
      sw.Close();

      MemoryTracker.PrintPrivateData(player, Console.Out);
    }

    static void sharpHelp()
    {
      Console.WriteLine ("Usage: sharp < command > [ argument ]");
      Console.WriteLine ("   gpl\t\t - print the license");
      Console.WriteLine ("   plugins\t - lists currently loaded plugins");
    }

    static void sharp(string[] args)
    {
      if (args.Length > 1)
      switch (args[1])
      {
      case "gpl":
        Server.Print(License);
        break;

      case "list":
      case "plugins":
        PluginManager.GetInstance().ShowPlugins();
        break;

      case "debug":
        MemoryTracker.Print();
        break;

      default:
        sharpHelp();
        break;
      }
      else sharpHelp();
    }
	}
}
