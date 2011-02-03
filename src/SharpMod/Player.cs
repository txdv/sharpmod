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
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Net;
using System.Linq;
using SharpMod.MetaMod;
using SharpMod.Database;

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
        Override = false;
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
      MetaModEngine.SetResult(MetaResult.Handled);
      if (Connect != null) Connect(args);
    }
    internal static List<Player> pendingAuthPlayers = new List<Player>();
    internal static bool OnConnect(IntPtr entity, string name, string address, string reject_reason) 
    {
      Player player = Player.GetPlayer(entity);
      player.Name = name;
      string[] addressinformation = address.Split(':');
      player.IPAddress = IPAddress.Parse(addressinformation[0]);
      player.Port = Convert.ToInt32(addressinformation[1]);

      if (player.PendingAuth) {
        pendingAuthPlayers.Add(player);
      } else {
        Player.OnAuthorize(player);
      }
      
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

    #region Authorize
    [Serializable]
    public sealed class AuthorizeEventArgs : PlayerEventArgs
    {
      public AuthorizeEventArgs(Player player)
        : base(player)
      {
      }
    }
    public delegate void AuthorizeHandler(AuthorizeEventArgs args);
    public static event AuthorizeHandler Authorize;
    protected static void OnAuthorize(AuthorizeEventArgs args)
    {
      if (Authorize != null) Authorize(args);
    }
    internal static void OnAuthorize(Player player)
    {
      AuthorizeEventArgs auth = new AuthorizeEventArgs(player);
      OnAuthorize(auth);
      player.OnPlayerAuthorize(auth);

      player.ReloadPrivileges();
    }
    private static void ResolvedPrivileges(PlayerInfo pi, Privileges priv)
    {
      Player player = Player.FindByUserId(pi.UserId);

      // Player isn't in the server any more, just stop it.
      if (player == null)
        return;

      OnAssignPrivileges(player, priv == null ? player.Privileges : priv);
    }
    #endregion

    #region PlayerAuthorize
    public event AuthorizeHandler PlayerAuthorize;
    protected void OnPlayerAuthorize(AuthorizeEventArgs args)
    {
      if (PlayerAuthorize != null) PlayerAuthorize(args);
    }
    #endregion

    #region Command
    [Serializable]
    public sealed class CommandEventArgs : PlayerEventArgs
    {
      public Command Command { get; set; }

      public CommandEventArgs(Player player, Command command)
          : base(player)
      {
        Command = command;
      }
    }
    public delegate void CommandHandler(CommandEventArgs args);
    public static event CommandHandler ClientCommand;
    
    internal static CommandEventArgs clientCommandEventArgs = null;
    unsafe internal static void OnCommand(IntPtr entity)
    {
      MetaModEngine.SetResult(MetaResult.Handled);
      Player player = Player.GetPlayer(entity);
      Command cmd = CommandManager.CreateCommandFromGameEngine(CommandType.Player);

      switch (cmd.Arguments[0])
      {
      case "menuselect":
        player.SelectMenu(Convert.ToInt32(cmd.Arguments[1]));
        break;
      default:
        CommandManager.Execute(player, cmd);
        break;
      }

      clientCommandEventArgs = new CommandEventArgs(player, cmd);
      OnCommand(clientCommandEventArgs);
      player.OnPlayerCommand(clientCommandEventArgs);

      if (clientCommandEventArgs.Override)
        MetaModEngine.SetResult(MetaResult.Supercede);
      else
        MetaModEngine.SetResult(MetaResult.Handled);

    }
    protected static void OnCommand(CommandEventArgs args)
    {
      MetaModEngine.SetResult(MetaResult.Handled);
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
      MetaModEngine.SetResult(MetaResult.Handled);
      if (Disconnect != null) Disconnect(args);
    }

    internal static void OnDisconnect(IntPtr entity)
    {
      MetaModEngine.SetResult(MetaResult.Handled);
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
      MetaModEngine.SetResult(MetaResult.Handled);
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

    #region ResolvePrivileges
    [Serializable]
    public sealed class AssignPrivilegesEventArgs : PlayerEventArgs
    {
      public AssignPrivilegesEventArgs(Player player)
        : base(player)
      {
      }
    }
    public delegate void AssignPrivilegesHandler(AssignPrivilegesEventArgs args);
    public static event AssignPrivilegesHandler AssignPrivileges;
    protected static void OnAssignPrivileges(AssignPrivilegesEventArgs args)
    {
      if (AssignPrivileges != null) AssignPrivileges(args);
    }
    internal static void OnAssignPrivileges(Player player, Privileges privileges)
    {
      player.Privileges = privileges;
      var args = new AssignPrivilegesEventArgs(player);
      OnAssignPrivileges(args);
      player.OnPlayerAssignPrivileges(args);

      PlayerInfo pi = new PlayerInfo(player);

      Task.Factory.StartNew(delegate {
        TaskManager.Join(ResolvedBans, SharpMod.Database.GetActiveBan(pi));
      });

    }
    #endregion

    #region PlayerAssignPrivileges
    public event AssignPrivilegesHandler PlayerAssignPrivileges;
    protected void OnPlayerAssignPrivileges(AssignPrivilegesEventArgs args)
    {
      if (PlayerAssignPrivileges != null) PlayerAssignPrivileges(args);
    }
    #endregion

    private static void ResolvedBans(BanInfo information)
    {
      if (information == null)
        return;

      Player target = Player.FindByUserId(information.Player.UserId);

      if (target == null)
        return;

      if (!target.Privileges.HasPrivilege("noban"))
        target.Kick(information.Reason);
    }

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
      CommandManager.RegisterCommandHandler(str, handler);
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
    public string AuthID { get { return Mono.Unix.UnixMarshal.PtrToString(MetaModEngine.engineFunctions.GetPlayerAuthId(Pointer)); } }
    /// <summary>
    /// Returns the Privileges of the player, which are determined by external resources
    /// like databases, privilege files. In other words, holds stuff like if the user
    /// can kick or ban etc.
    /// </summary>
    public Privileges Privileges { get; protected set; }
    /// <summary>
    /// Returns the UserId of the player.
    /// It is unique per connection, therefore good for logging purposes.
    /// The UserId is equal 1 for the first player and +1 for all subsequent.
    /// Count startsfrom 1 on every server start/restart, but not mapchange/
    /// </summary>
    public int UserID {
      get {
        return MetaModEngine.engineFunctions.GetPlayerUserId(Pointer);
      }
    }

    public static int GetUserID(Player player)
    {
      return player == null ? 0 : player.UserID;
    }

    public bool PendingAuth {
      get {
        return AuthID == "STEAM_ID_PENDING";
      }
    }
    /// <summary>
    /// Gets the Ping of the Player
    /// </summary>
    public int Ping {
      get {
        int ping = 0, packet_loss = 0;
        MetaModEngine.engineFunctions.GetPlayerStats(Pointer, ref ping, ref packet_loss);
        return ping;
      }
    }
    /// <summary>
    /// Gets the lost packet count of the player.
    /// All packets which are send to the client and back during communication are counted in.
    /// </summary>
    public int PacketLoss {
      get {
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
      Privileges = new Privileges("");
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
      if (Player.playerlist[index] == null) Player.playerlist[index] = Entity.CreateEntity(entity) as Player;
      return Player.playerlist[index];
    }

    /// <summary>
    /// Returns an enumerator for Players
    /// </summary>
    public static IEnumerable<Player> Players {
      get {
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
    public void PrintConsole(string text, params object[] param)
    {
      PrintConsole(string.Format(text, param));
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
    public void PrintCenter(string text, params object[] param)
    {
      PrintCenter(string.Format(text, param));
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
    public void PrintChat(string text, params object[] param)
    {
      PrintChat(string.Format(text, param));
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
    public unsafe float Health {
      get { return entity->v.health; }
      set { entity->v.health = value; }
    }

    /// <summary>
    /// Sets or gets the fov of a player
    /// </summary>
    public unsafe float FOV {
      get { return entity->v.fov; }
      set { entity->v.fov = value; }
    }

    /// <summary>
    /// Sets or gets the fragcount of a player
    /// </summary>
    public unsafe float Frags {
      get { return entity->v.frags; }
      set { entity->v.frags = value; }
    }

    /// <summary>
    /// Sets or gets the Armor of a player
    /// </summary>
    public unsafe float Armor {
      get { return entity->v.armorvalue; }
      set { entity->v.armorvalue = value; }
    }

    /// <summary>
    /// Sets or gets the Gravity of a player
    /// </summary>
    public unsafe float Gravity {
      get { return entity->v.gravity; }
      set { entity->v.gravity = value; }
    }

    /// <summary>
    /// Sets or gets the maximum moving speed of a player
    /// </summary>
    public unsafe float MaxSpeed {
      get { return entity->v.maxspeed; }
      set { entity->v.maxspeed = value; }
    }

    public const float defaultTakeDamage = 2.0f;
    /// <summary>
    /// Sets or gets the multiplier of the damage, default is Player.defaultTakeDamage = 2.0f
    /// </summary>
    public unsafe float TakeDamage {
      get { return entity->v.takedamage; }
      set { entity->v.takedamage = value; }
    }

    /// <summary>
    /// Sets or gets wether the godmode is activated (wrapper methdo for TakeDamage)
    /// </summary>
    public unsafe bool GodMode {
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

    public static Player Find(string target)
    {
      Player player = null;
      player = FindByUserId(target);
      if (player != null) return player;
      player = FindByAuthId(target);
      if (player != null) return player;
      player = FindByName(target);
      if (player != null) return player;
      return player;

    }

    public static Player FindByUserId(string id)
    {
      if (id.Length < 2 || id[0] != '#')
        return null;

      int result;
      if (int.TryParse(id.Substring(1), out result)) {
        return FindByUserId(result);
      } else {
        return null;
      }

    }

    public static Player FindByUserId(int id)
    {
      foreach (Player player in Player.Players) {
        if (player.UserID == id)
          return player;
      }
      return null;
    }

    /// <summary>
    /// Finds an active Player by authid.
    /// </summary>
    /// <param name="authId">
    /// AuthId to search by <see cref="System.String"/>
    /// </param>
    /// <returns>
    /// The player if found, null if not <see cref="Player"/>
    /// </returns>
    public static Player FindByAuthId(string authId)
    {
      foreach (Player player in Player.Players) {
        if (player.AuthID == authId)
          return player;
      }

      var potentiallist = from s in Player.Players
                          where s.AuthID.Contains(authId)
                          select s;

      if (potentiallist.Count() == 1) {
        return potentiallist.First();
      }

      return null;
    }

    /// <summary>
    /// Finds an active Player by name
    /// </summary>
    /// <param name="name">
    /// Name to search by <see cref="System.String"/>
    /// </param>
    /// <returns>
    /// A player if found, null if not <see cref="Player"/>
    /// </returns>
    public static Player FindByName(string name)
    {
      foreach (Player player in Player.Players) {
        if (player.Name == name)
          return player;
      }

      var potentiallist = from s in Player.Players
                          where s.Name.Contains(name)
                          select s;

      if (potentiallist.Count() == 1) {
        return potentiallist.First();
      }

      return null;
    }

    public void ReloadPrivileges()
    {
      PlayerInfo pi = new PlayerInfo(this);

      Task.Factory.StartNew(delegate {
        TaskManager.Join<PlayerInfo, Privileges>(ResolvedPrivileges, pi, SharpMod.Database.LoadPrivileges(pi));
      });
    }

    public static void ReloadAllPrivileges()
    {
      foreach (Player player in Players)
        player.ReloadPrivileges();
    }

    unsafe public int WeaponAnimation {
      get { return entity->v.weaponanim; }
      set { entity->v.weaponanim = value; }
    }

    public void UpdateUserInfo(string infoString)
    {
      MetaModEngine.dllapiFunctions.ClientUserInfoChanged(Pointer, infoString);
    }

    public void UpdateUserInfo()
    {
      UpdateUserInfo(InfoKeyBuffer);
    }

    public void StripUserWeapons()
    {
      Entity strip = new Entity("player_weaponstrip");
      strip.Spawn();
      strip.Use(this);
      strip.Remove();

      // TODO: set active weapon to 0
    }

    public Entity GiveItem(string itemname)
    {
      Entity item = new Entity(itemname);

      if (item.IsNull) {
        return null;
      }

      item.Origin = Origin;
      item.Spawnflags |= (1 << 30);

      item.Spawn();

      item.Touch(this);

      return item;
    }
  }
}
