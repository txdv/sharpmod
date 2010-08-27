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
using Mono.Unix;
using SharpMod.MetaMod;

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
    unsafe public static string GameDirectory
    {
      get
      {
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

		public static void Log(string message)
		{
			//EngineInterface.ServerLogMessage(message);
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
    /// Precache Model
    /// </summary>
    /// <param name="filename">
    /// A filename <see cref="System.String"/>
    /// </param>
    /// <returns>
    /// True for successfull load <see cref="System.Boolean"/>
    /// </returns>
    public static bool PrecacheModel(string filename)
    {
      return MetaModEngine.engineFunctions.PrecacheModel(filename) == 1;
    }

    /// <summary>
    /// Precache Sound
    /// </summary>
    /// <param name="filename">
    /// The filename of the sound <see cref="System.String"/>
    /// </param>
    /// <returns>
    /// True for successfull load <see cref="System.Boolean"/>
    /// </returns>
    public static bool PrecacheSound(string filename)
    {
      return MetaModEngine.engineFunctions.PrecacheSound(filename) == 1;
    }
	}
}
