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
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Mono.Unix;
using SharpMod.MetaMod;
using SharpMod.Helper;

namespace SharpMod
{

  /// <summary>
  /// A class for dealing with infobuffers (localinfo, serverinfo)
  /// </summary>
  public class BufferInfo {

    /// <summary>
    /// The constructor ...
    /// </summary>
    /// <param name="pointer">
    /// Takes a pointer according to the location of the info <see cref="IntPtr"/>
    /// </param>
    internal BufferInfo(IntPtr pointer)
    {
      Pointer = pointer;
    }

    /// <summary>
    /// Gets, sets Pointer
    /// </summary>
    public IntPtr Pointer { get; protected set; }

    /// <summary>
    /// Returns the buffer, a giant string with all information
    /// </summary>
    public string Buffer {
      get {
        return UnixMarshal.PtrToString(MetaModEngine.engineFunctions.GetInfoKeyBuffer(Pointer));
      }
    }

    /// <summary>
    /// Gets a value for a key from the infobuffer
    /// </summary>
    /// <param name="key">
    /// The key <see cref="System.String"/>
    /// </param>
    /// <returns>
    /// The value <see cref="System.String"/>
    /// </returns>
    public string Get(string key)
    {
      return UnixMarshal.PtrToString(MetaModEngine.engineFunctions.InfoKeyValue(
        MetaModEngine.engineFunctions.GetInfoKeyBuffer(Pointer),
        key));

    }
    /// <summary>
    /// Sets a value for a key in the infobuffer
    /// </summary>
    /// <param name="key">
    /// The key <see cref="System.String"/>
    /// </param>
    /// <param name="val">
    /// A value <see cref="System.String"/>
    /// </param>
    public void Set(string key, string val)
    {
        MetaModEngine.engineFunctions.SetServerKeyValue(MetaModEngine.engineFunctions.GetInfoKeyBuffer(Pointer), key, val);
    }

    /// <summary>
    /// Set and get a key value
    /// </summary>
    /// <param name="key">
    /// The key which the data is going to be associated with <see cref="System.String"/>
    /// </param>
    public string this [string key] {
      get {
        return Get(key);
      }
      set {
        Set(key, value);
      }
    }
  }

  /// <summary>
  /// A class that represents the running Server
  /// </summary>
  public class Server
  {

    private static MethodInfo mi = typeof(Server).GetMethod("CommandWrapper", BindingFlags.NonPublic | BindingFlags.Static);
    private static int maxplayers;

    /// <summary>
    /// Deals with the local information of the server
    /// </summary>
    public static BufferInfo LocalInfo { get; private set; }
    /// <summary>
    /// Deals with the server information of the server (just use localinfo)
    /// </summary>
    public static BufferInfo ServerInfo { get; private set; }

    internal static void Init()
    {
      LocalInfo = new BufferInfo(IntPtr.Zero);
      ServerInfo = new BufferInfo(MetaModEngine.engineFunctions.PEntityOfEntIndex(0));
    }

    internal static void SetMaxPlayers(int maxplayers)
    {
      Server.maxplayers = maxplayers;
    }

    /// <summary>
    /// Gets the 0 directory of the running game server
    /// 512 is the limit size of the returned name
    /// </summary>
    unsafe public static string GameDirectory {
      get {
        int length = 0;
        foreach (DirectoryInfo di in (new DirectoryInfo("./")).GetDirectories())
        {
          if (di.Name.Length > length) length = di.Name.Length;
        }
        if (length == 0) length = 256;

        // 64 should be enough, since most game dirs are like cstrike/valve/naturalselection
        IntPtr ptr = UnixMarshal.AllocHeap(length);
        MetaModEngine.engineFunctions.GetGameDir((char *)ptr.ToPointer());
        return UnixMarshal.PtrToString(ptr);
      }
    }

    public static string ModDirectory {
      get {
        return Server.GameDirectory + "/addons/sharpmod/";
      }
    }

    /// <summary>
    /// Maximum players the server can hold at any time
    /// </summary>
    public static int MaxPlayers
    {
      get { return maxplayers; }
    }

    public static void Print(string message)
    {
      MetaModEngine.engineFunctions.ServerPrint(message);
    }

    public static void Print(string message, params object[] param)
    {
      Print(string.Format(message, param));
    }

    /// <summary>
    /// Logs a plain message. Doesn't use any decoration.
    /// </summary>
    /// <param name="message">
    /// Log message <see cref="System.String"/>
    /// </param>
    public static void Log(string message)
    {
      MetaMod.MetaModEngine.metaUtilityFunctions.LogMessage(MetaMod.MetaModEngine.PLID, message);
    }

    public static void Log(string message, params object[] param)
    {
      Log(String.Format(message, param));
    }

    /// <summary>
    /// Logs an error message, uses date, mod, Error prefix.
    /// </summary>
    /// <param name="message">
    /// Error message. <see cref="System.String"/>
    /// </param>
    public static void LogError(string message)
    {
      MetaMod.MetaModEngine.metaUtilityFunctions.LogError(MetaMod.MetaModEngine.PLID, message);
    }

    public static void LogError(string message, params object[] param)
    {
      LogError(String.Format(message, param));
    }

    /// <summary>
    /// Logs a plain message to the console, not in the files.
    /// </summary>
    /// <param name="message">
    /// A <see cref="System.String"/>
    /// </param>
    public static void LogConsole(string message)
    {
      MetaMod.MetaModEngine.metaUtilityFunctions.LogConsole(MetaMod.MetaModEngine.PLID, message);
    }

    public static void LogDeveloper(string message)
    {
      MetaMod.MetaModEngine.metaUtilityFunctions.LogDeveloper(MetaMod.MetaModEngine.PLID, message);
    }

    public static void LogDeveloper(string message, params object[] param)
    {
      LogDeveloper(String.Format(message, param));
    }

    public delegate void CommandDelegate(string [] args);
    
    public static void RegisterCommand(string str, CommandDelegate cmd)
    {
      Callback cl = (Callback)Delegate.CreateDelegate(typeof(Callback), cmd, mi);
      MetaModEngine.engineFunctions.AddServerCommand(str, Marshal.GetFunctionPointerForDelegate(cl));
    }

    // TODO: Surpress warning
    private static void CommandWrapper(CommandDelegate cmd)
    {
      cmd(Command.EngineArguments);
    }
    
    private delegate void Callback();

    /// <summary>
    /// Enques a command in the server command queue.
    /// Server.ExecuteEnqueuedCommands() is needed to execute all enqueued commands.
    /// If you want to call multiple commands at once, use the newline character
    /// in order to seperate the commands
    /// </summary>
    /// <param name="command">
    /// The command to execute <see cref="System.String"/>
    /// </param>
    public static void EnqueueCommand(string command)
    {
      MetaModEngine.engineFunctions.ServerCommand(command + "\n");
    }

    /// <summary>
    /// Enques a command in the server command queue.
    /// Server.ExecuteEnqueuedCommands() is needed to execute all enqueued commands.
    /// If you want to call multiple commands at once, use the newline character
    /// in order to seperate the commands
    /// </summary>
    /// <param name="command">
    /// The command to execute <see cref="System.String"/>
    /// </param>
    /// <param name="objs">
    /// A list of objects to use in the formater. <see cref="System.Object[]"/>
    /// </param>
    public static void EnqueueCommand(string command, params object[] objs)
    {
      EnqueueCommand(String.Format(command, objs));
    }

    /// <summary>
    /// Enques and executes a command.
    /// </summary>
    /// <param name="command">
    /// The command to be executed <see cref="System.String"/>
    /// </param>
    public static void ExecuteCommand(string command)
    {
      EnqueueCommand(command);
      ExecuteEnqueuedCommands();
    }

    /// <summary>
    /// Eneques and executes a command.
    /// </summary>
    /// <param name="command">
    /// The command to be exuted <see cref="System.String"/>
    /// </param>
    /// <param name="objs">
    /// A list of objects to use in the formater. <see cref="System.Object[]"/>
    /// </param>
    public static void ExecuteCommand(string command, params object[] objs)
    {
      EnqueueCommand(command, objs);
      ExecuteEnqueuedCommands();
    }

    /// <summary>
    /// Executes all enqueued commands.
    /// </summary>
    public static void ExecuteEnqueuedCommands()
    {
      MetaModEngine.engineFunctions.ServerExecute();
    }

    /// <summary>
    /// Uses the engine function to check if a map is valid.
    /// </summary>
    /// <param name="map">
    /// Name of the map <see cref="System.String"/>
    /// </param>
    /// <returns>
    /// The return value weather the map is valid <see cref="System.Boolean"/>
    /// </returns>
    public static bool IsMapValid(string map)
    {
      return (MetaModEngine.engineFunctions.IsMapValid(map) == 1);
    }

    /// <summary>
    /// Returns true if the server runs on a 64 bit system machine
    /// </summary>
    public static bool Is64Bit
    {
        get { return Marshal.SizeOf(typeof(IntPtr)) == 8; }
    }

    /// <summary>
    /// Returns true if the server runs on a 32 bit system machine
    /// </summary>
    public static bool Is32Bit
    {
      get { return Marshal.SizeOf(typeof(IntPtr)) == 4; }
    }

    /// <summary>
    /// Returns true if the server is a dedicated server
    /// </summary>
    public static bool IsDedicated
    {
      get {
        return MetaModEngine.engineFunctions.IsDedicatedServer() == 1;
      }
    }

    /// <summary>
    /// Precaches sound
    /// </summary>
    /// <param name="filename">
    /// A string representing the filename <see cref="System.String"/>
    /// </param>
    /// <returns>
    /// Returns sound index <see cref="System.Int32"/>
    /// </returns>
    public static int PrecacheSound(string filename)
    {
      return MetaModEngine.engineFunctions.PrecacheSound(filename);
    }

    /// <summary>
    /// The active running server time in a float
    /// </summary>
    unsafe public static float TimeFloat {
      get {
        return MetaModEngine.globalVariables->time;
      }
    }

    /// <summary>
    /// The active running server time in a TimeSpan struct
    /// </summary>
    unsafe public static TimeSpan Time {
      get {
        return TimeFloat.ToTimeSpan();
      }
    }

    unsafe public static bool Teamplay {
      get {
        return MetaModEngine.globalVariables->teamplay == 1.0f;
      }
    }

    unsafe public static bool Coop {
      get {
        return MetaModEngine.globalVariables->coop == 1.0f;
      }
    }

    unsafe public static bool DeathMatch {
      get {
        return MetaModEngine.globalVariables->deathmatch == 1.0f;
      }
    }

    /// <summary>
    /// Get current map name
    /// </summary>
    unsafe public static string ActiveMap {
      get {
        int i = MetaModEngine.globalVariables->mapname;
        IntPtr ptr = MetaModEngine.engineFunctions.SzFromIndex(i);
        return Mono.Unix.UnixMarshal.PtrToString(ptr);
      }
    }

    /// <summary>
    /// Returns a list of all valid maps in the directory.
    /// </summary>
    /// <returns>
    /// String array of all map names <see cref="System.String[]"/>
    /// </returns>
    private string[] LoadMapListFromDirectory()
    {
      List<string> list = new List<string>();
      var files = new DirectoryInfo(Path.Combine(Server.GameDirectory, "maps")).GetFiles("*.bsp");
      foreach (FileInfo fi in files) {
        string map = fi.Name.Substring(0, fi.Name.Length - 4);
        if (Server.IsMapValid(map)) {
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
      StreamReader sr = null;
      List<string> list = new List<string>();
      try {
        string mapcyclefile = Path.Combine(Server.GameDirectory, CVar.GetStringValue("mapcyclefile"));
        sr = new StreamReader(File.Open(mapcyclefile, FileMode.Open));
        while (!sr.EndOfStream) {
          string line = sr.ReadLine();
          if (line.ToLower().EndsWith(".bsp")) {
            string map = line.Substring(0, line.Length - 4);
            if (Server.IsMapValid(map))
              list.Add(map);
          } else {
            if (Server.IsMapValid(line))
              list.Add(line);
          }
        }
        return list.ToArray();
      } catch {
        try {
          return list.ToArray();
        } catch {
          return new string[] {};
        }
      } finally {
        if (sr != null) sr.Close();
      }
    }
  }
}
