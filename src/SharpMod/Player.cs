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
using System.Runtime.InteropServices;
using System.Net;
using SharpMod.MetaMod;

namespace SharpMod
{

  public partial class Player : Entity
  {
    [Serializable]
    public abstract class PlayerEventArgs : EventArgs
    {
      public Player Player { get; private set; }
      public PlayerEventArgs (Player player)
      {
        Player = player;
      }

      public virtual bool Overridable { get { return false; } }
      public virtual bool Override { get; set; }
    }
    
    #region Connect
    [Serializable]
    public sealed class ConnectEventArgs : PlayerEventArgs
    {
      public string RejectReason { get; private set; }
      public ConnectEventArgs(Player player, string reject_reason)
        : base(player)
      {
          RejectReason = reject_reason;
      }
    }
    public delegate void ConnectHandler(ConnectEventArgs args);
    public static event ConnectHandler Connect;
    protected static void OnConnect(ConnectEventArgs args)
    {
      if (Connect != null) Connect(args);
    }
    internal static bool OnConnect(IntPtr entity, string name, string address, string reject_reason) 
    {
      Player player = Player.GetPlayer(entity);
      player.Name = name;
      string[] addressinformation = address.Split(new char[] { ':' });
      player.IPAddress = IPAddress.Parse(addressinformation[0]);
      player.Port = Convert.ToInt32(addressinformation[1]);
      
      ConnectEventArgs connectEventArgs = new ConnectEventArgs(player, reject_reason);
      OnConnect(connectEventArgs);
      player.OnPlayerConnect(connectEventArgs);
      return true;
    }
    #endregion
    
    #region PlayerConnect
    public event ConnectHandler PlayerConnect;
    protected void OnPlayerConnect(ConnectEventArgs args)
    {
      if (PlayerConnect != null) PlayerConnect(args);
    }
    #endregion

    #region Command
    [Serializable]
    public sealed class CommandEventArgs : PlayerEventArgs
    {
      public string[] Arguments { get; set; }
      public override bool Overridable { get { return true; } }

      public CommandEventArgs(Player player, string[] arguments)
          : base(player)
      {
        Arguments = arguments;
        Override = false;
      }
    }
    public delegate void CommandHandler(CommandEventArgs args);
    public static event CommandHandler ClientCommand;
    
    internal static CommandEventArgs clientCommandEventArgs = null;
    unsafe internal static void OnCommand(IntPtr entity)
    {
      Player player = Player.GetPlayer(entity);
      Command cmd = Command.FromGameEngine();

      switch (cmd.Arguments[0])
      {
      case "menuselect":
        player.SelectMenu(Convert.ToInt32(cmd.Arguments[1]));
        break;
      default:
        CommandManager.Client.Execute(player, cmd);
        break;
      }

      clientCommandEventArgs = new CommandEventArgs(player, Command.EngineArguments);
      OnCommand(clientCommandEventArgs);
      player.OnPlayerCommand(clientCommandEventArgs);

      if (clientCommandEventArgs.Override)
          MetaModEngine.globals->mres = MetaResult.Supercede;
      else
          MetaModEngine.globals->mres = MetaResult.Handled;
    }
    protected static void OnCommand(CommandEventArgs args)
    {
      if (ClientCommand != null) ClientCommand(args);
    }
    #endregion

    #region PlayerCommand
    public event CommandHandler PlayerCommand;
    protected void OnPlayerCommand(CommandEventArgs args)
    {
      if (PlayerCommand != null) PlayerCommand(args);
    }
    #endregion
    
    #region ClientDisconnect
    [Serializable]
    public sealed class DisconnectEventArgs : PlayerEventArgs
    {
      public DisconnectEventArgs(Player player)
        : base (player)
      {
      }
    }
    public delegate void DisconnectHandler(DisconnectEventArgs args);

    public static event DisconnectHandler Disconnect;
    protected static void OnDisconnect(DisconnectEventArgs args)
    {
      if (Disconnect != null) Disconnect(args);
    }

    internal static void OnDisconnect(IntPtr entity)
    {
      Player player = Player.GetPlayer(entity);
      DisconnectEventArgs args = new DisconnectEventArgs(player);
      OnDisconnect(args);
      player.OnPlayerClientDisconnect(args);
      player.Release();
     

    }
    #endregion
    
    #region PlayerClientDisconnect
    public event DisconnectHandler PlayerDisconnect;
    protected void OnPlayerClientDisconnect(DisconnectEventArgs args)
    {
      if (PlayerDisconnect != null) PlayerDisconnect(args);
    }
    
    #endregion
    
    #region PutInServer
    
    [Serializable]
    public sealed class PutInServerEventArgs : PlayerEventArgs
    {
      public PutInServerEventArgs(Player player)
        : base (player)
      {
      }
    }
    public delegate void PutInServerHandler(PutInServerEventArgs args);
    public static event PutInServerHandler PutInServer;
    protected static void OnPutInServer(PutInServerEventArgs args)
    {
      if (PutInServer != null) PutInServer(args);
    }
    
    internal static void OnPutInServer(IntPtr entity)
    {
      Player player = Player.GetPlayer(entity);
      PutInServerEventArgs args = new PutInServerEventArgs(player);
      OnPutInServer(args);
      player.OnPlayerPutInServer(args);
    }
    
    #endregion
    
    #region PlayerPutInServer
    
    public event PutInServerHandler PlayerPutInServer;
    protected void OnPlayerPutInServer(PutInServerEventArgs args)
    {
      if (PlayerPutInServer != null) PlayerPutInServer(args);
    }
    
    #endregion

    // TODO: create an interface for information saving
    // in the menu
    internal Menues.SimpleMenu  menu;
    internal int                menu_page;
    internal Menues.Item[]      menu_items;

    /// <summary>
    /// Releases the event handlers
    /// </summary>
    internal void Release()
    {
      int index = Index;
      if (playerlist[index] == null) return;
      
      this.PlayerCommand = null;
      this.PlayerDisconnect = null;
      this.PlayerConnect = null;
      this.PlayerPutInServer = null;
      
       playerlist[index] = null;
    }

    /// <summary>
    /// Maximum value of the players this mod will support
    /// </summary>
    internal const int PlayerMaximum = 32;
    /// <summary>
    /// The playerlist according to PlayerMaximum
    /// </summary>
    internal static Player[] playerlist = new Player[PlayerMaximum];

    public static void RegisterCommand(string str, ClientCommandDelegate handler)
    {
      CommandManager.Client.Register(str, handler);

    }

    /// <summary>
    /// Gets the name of the Player
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// Gets the IPAddress of the player
    /// </summary>
    public IPAddress IPAddress { get; private set; }
    /// <summary>
    /// Returns the Port with witch the player is connected to the server
    /// </summary>
    public int Port { get; private set; }
    /// <summary>
    /// Returns the AuthID which might be a STEAMI ID
    /// </summary>
    public string AuthID {  get { return Mono.Unix.UnixMarshal.PtrToString(MetaModEngine.engineFunctions.GetPlayerAuthId(Pointer)); } }
    /// <summary>
    /// Gets the Ping of the Player
    /// </summary>
    public int Ping
    {
      get
      {
        int ping = 0, packet_loss = 0;
        MetaModEngine.engineFunctions.GetPlayerStats(Pointer, ref ping, ref packet_loss);
        return ping;
      }
    }
    /// <summary>
    /// Gets the lost packet count of the player.
    /// All packets which are send to the client and back during communication are counted in.
    /// </summary>
    public int PacketLoss
    {
      get
      {
        int ping = 0, packet_loss = 0;
        MetaModEngine.engineFunctions.GetPlayerStats(Pointer, ref ping, ref packet_loss);
        return packet_loss;
      }
    }
    /// <summary>
    /// Gets the InfoKeyBuffer of a player.
    /// Might hold important information like _pw and gui options.
    /// </summary>
    public string InfoKeyBuffer { get { return Mono.Unix.UnixMarshal.PtrToString(MetaModEngine.engineFunctions.GetInfoKeyBuffer(Pointer)); } }
    /// <summary>
    /// Gets a specific value for a key from the infokeybuffer
    /// </summary>
    /// <param name="key">
    /// The key name <see cref="System.String"/>
    /// </param>
    /// <returns>
    /// The value according to the key <see cref="System.String"/>
    /// </returns>
    public string GetInfoKeyValue(string key)
    {
      return Mono.Unix.UnixMarshal.PtrToString(MetaModEngine.engineFunctions.InfoKeyValue(MetaModEngine.engineFunctions.GetInfoKeyBuffer(Pointer), key));
    }
    /// <summary>
    /// Sets some specific InfoKeyValue according to a key.
    /// </summary>
    /// <param name="key">
    /// The key <see cref="System.String"/>
    /// </param>
    /// <param name="val">
    /// The value which has to be set to the key <see cref="System.String"/>
    /// </param>
    public void SetInfoKeyValue(string key, string val)
    {
      MetaModEngine.engineFunctions.SetClientKeyValue(Index, MetaModEngine.engineFunctions.GetInfoKeyBuffer(Pointer), key, val);
    }

    /// <summary>
    /// Constructor for internal building of Players
    /// </summary>
    /// <param name="entity">
    /// Pointer to the entity struct of a player <see cref="IntPtr"/>
    /// </param>
    internal Player (IntPtr entity)
      : base(entity)
    {
    }

    /// <summary>
    /// Gets a Player according to the entity pointer.
    /// Needed most of the time for internal engine calls, since the entity pointer
    /// is used a lot.
    /// </summary>
    /// <param name="entity">
    /// A pointer to the entity class <see cref="IntPtr"/>
    /// </param>
    /// <returns>
    /// A player class <see cref="Player"/>
    /// </returns>
    public static Player GetPlayer(IntPtr entity)
    {
      int index = MetaModEngine.engineFunctions.IndexOfEdict(entity);
      if (Player.playerlist[index] == null) Player.playerlist[index] = new Player(entity);
      return Player.playerlist[index];
    }

    /// <summary>
    /// Returns an enumerator for Players
    /// </summary>
    public static IEnumerable<Player> Players
    {
      get
      {
        for (int i = 0; i < playerlist.Length; i++)
          if (playerlist[i] != null) yield return playerlist[i];
      }
    }

    /// <summary>
    /// Prints some text to the Players Console.
    /// </summary>
    /// <param name="text">
    /// Text to print <see cref="System.String"/>
    /// </param>
    public void PrintConsole(string text)
    {
      MetaModEngine.engineFunctions.ClientPrintf(Pointer, PrintType.Console, text);
    }
    /// <summary>
    /// Prints some text to the Player central screen
    /// </summary>
    /// <param name="text">
    /// Text to print <see cref="System.String"/>
    /// </param>
    public void PrintCenter(string text)
    {
      MetaModEngine.engineFunctions.ClientPrintf(Pointer, PrintType.Center, text);
    }
    /// <summary>
    /// Prints some text to the players text in one color.
    /// </summary>
    /// <param name="text">
    /// Text to print <see cref="System.String"/>
    /// </param>
    public void PrintChat(string text)
    {
      MetaModEngine.engineFunctions.ClientPrintf(Pointer, PrintType.Chat, text);
    }

    /// <summary>
    /// Executes a supplied command on the client side of the player.
    /// </summary>
    /// <param name="command">
    /// A command as a string which must end with ; <see cref="System.String"/>
    /// </param>
    public void ExecuteCommand(string command)
    {
      MetaModEngine.engineFunctions.ExecuteClientCommand(Pointer, command);
    }

    /// <summary>
    /// Returns a string representation of the player class
    /// </summary>
    /// <returns>
    /// A string representation of the player class <see cref="System.String"/>
    /// </returns>
    public override string ToString ()
    {
      return string.Format("[Player: Entity={0}, Name={1}, IPAddress={2}, Port={3}, AuthID={4}, Ping={5}, PacketLoss={6}]", Pointer, Name, IPAddress, Port, 0, 0, 0);
    }

    #region FUN Functions
/*
    get_client_listen
    set_client_listen
    set_user_godmode
    get_user_godmode
    set_user_health    x
    give_item
    spawn
    set_user_frags     x
    set_user_armor     x
    set_user_origin
    set_user_rendering
    set_user_maxspeed
    get_user_maxspeed
    set_user_gravity   x
    get_user_gravity   x
    get_user_footsteps
    set_user_hitzones
    get_user_hitzones
    set_user_noclip
    get_user_noclip
    set_user_footsteps
    strip_user_weapons
*/

    /// <summary>
    /// Sets or gets the health of a player
    /// </summary>
    public unsafe float Health
    {
      get { return entity->v.health; }
      set { entity->v.health = value; }
    }

    /// <summary>
    /// Sets or gets the fov of a player
    /// </summary>
    public unsafe float FOV
    {
      get { return entity->v.fov; }
      set { entity->v.fov = value; }
    }

    /// <summary>
    /// Sets or gets the fragcount of a player
    /// </summary>
    public unsafe float Frags
    {
      get { return entity->v.frags; }
      set { entity->v.frags = value; }
    }

    /// <summary>
    /// Sets or gets the Armor of a player
    /// </summary>
    public unsafe float Armor
    {
      get { return entity->v.armorvalue; }
      set { entity->v.armorvalue = value; }
    }

    /// <summary>
    /// Sets or gets the Gravity of a player
    /// </summary>
    public unsafe float Gravity
    {
      get { return entity->v.gravity; }
      set { entity->v.gravity = value; }
    }

    /// <summary>
    /// Sets or gets the maximum moving speed of a player
    /// </summary>
    public unsafe float MaxSpeed
    {
      get { return entity->v.maxspeed; }
      set { entity->v.maxspeed = value; }
    }

    public const float defaultTakeDamage = 2.0f;
    /// <summary>
    /// Sets or gets the multiplier of the damage, default is Player.defaultTakeDamage = 2.0f
    /// </summary>
    public unsafe float TakeDamage
    {
      get { return entity->v.takedamage; }
      set { entity->v.takedamage = value; }
    }

    /// <summary>
    /// Sets or gets wether the godmode is activated (wrapper methdo for TakeDamage)
    /// </summary>
    public unsafe bool GodMode
    {
      get { return (entity->v.takedamage == 0.0f); }
      set {
        if (value) entity->v.takedamage = 0.0f;
        else entity->v.takedamage = Player.defaultTakeDamage;
      }
    }
    #endregion

    /// <summary>
    /// Issue the server command to kick this player.
    /// </summary>
    /// <param name="message">
    /// A reason for kicking <see cref="System.String"/>
    /// </param>
    public void Kick(string message)
    {
      Server.EnqueueCommand("kick {0} {1}", Name, message);
      Server.ExecuteEnqueuedCommands();
    }

    /// <summary>
    /// Issue the server commands to ban a player by ip.
    /// </summary>
    /// <param name="message">
    /// A reason to kick/ban <see cref="System.String"/>
    /// </param>
    /// <param name="minutes">
    /// The amount of minutes to ban a player by ip <see cref="System.Int32"/>
    /// </param>
    public void Ban(string message, int minutes)
    {
      Server.EnqueueCommand("addip {0} {1}", IPAddress, minutes);
      Server.EnqueueCommand("writeip");
      Kick(message);
    }

    unsafe public int WeaponAnimation
    {
      get {
        return entity->v.weaponanim;
      }
      set {
        entity->v.weaponanim = value;
      }
    }



  }
}
