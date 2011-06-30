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
using SharpMod.Database;
using HLTools;

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
    public static string License {
      get {
        StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("sharpmod.gpl.txt"));
        string ret = sr.ReadToEnd();
        sr.Close();
        return ret;
      }
    }

    internal static IDatabase Database { get; set; }

    public static Verifier Verifier { get; private set; }

    public static void Init()
    {
      Version = new CVar("smod_version", "0.1");

      // load plugins

      Server.Print(License);
      Server.RegisterCommand("smod", smod);

      Verifier = new Verifier(Directory.GetCurrentDirectory(), Server.GameDirectory);

      try {
        var doc = new System.Xml.XmlDocument();
        doc.Load(Path.Combine(Server.ModDirectory, "cfg/databases.xml"));
        try {
          Database = DefaultDatabase.Load(Path.Combine(Server.ModDirectory, "SharpMod.Database.MySql.dll"));
          Database.Load(doc);
        } catch (Exception e) {
          Server.LogError("Database Interface failed to load, using default: {0}", e.Message);
          Database = new DefaultDatabase();
          Database.Load(doc);
        }
      } catch (Exception e) {
        Server.LogError("Failed to load cfg/databases.xml: {0}", e.Message);
      }
    }

    static void smodHelp()
    {
      Console.WriteLine ("Usage: smod < command > [ argument ]");
      Console.WriteLine ("   gpl\t\t - print the license");
      Console.WriteLine ("   plugins\t - lists currently loaded plugins");
    }

    static void smod(string[] args)
    {
      if (args.Length > 1)
      switch (args[1]) {
      case "gpl":
        Server.Print(License);
        break;

      case "list":
      case "plugins":
        PluginManager.ShowPlugins();
        break;

      default:
        smodHelp();
        break;
      }
      else smodHelp();
    }
	}
}
