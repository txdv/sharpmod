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
using SharpMod.Math;
using SharpMod.MetaMod;

// TODO: Write a mapper which would upon createn
// of an entity check against the classname and use the proper wrapper class (hostage, player, etc..)

namespace SharpMod
{
  #region Enums
  public enum EdictFlags : int
  {
    Fly = (1 << 0),
    Swim = (1 << 1),
    Conveyor = (1 << 2),
    Client = (1 << 3),
    Inwater = (1 << 4),
    Monster = (1 << 5),
    Godmode = (1 << 6),
    Notarget = (1 << 7),
    Skiplocalhost = (1 << 8),
    Onground = (1 << 9),
    Partialground = (1 << 10),
    Waterjump = (1 << 11),
    Frozen = (1 << 12),
    Fakeclient = (1 << 13),
    Ducking = (1 << 14),
    Float = (1 << 15),
    Graphed = (1 << 16),
  };

    // common/const.h
  public enum MoveType : int
  {
    /// <summary>
    /// Never moves
    /// </summary>
    None = 0,
    /// <summary>
    /// Player only, moving on the ground
    /// </summary>
    Walk = 3,
    /// <summary>
    /// gravity, special edge handling, for monsters
    /// </summary>
    Step,
    /// <summary>
    /// No gravity, but still collides with edges
    /// </summary>
    Fly,
    /// <summary>
    /// Gravity/collisions
    /// </summary>
    Toss,
    /// <summary>
    /// No clip to world, push and crush
    /// </summary>
    Push,
    /// <summary>
    /// No gravity, no collisions, still do velocity/avelocity
    /// </summary>
    Noclip,
    /// <summary>
    /// Extra size to monsters
    /// </summary>
    FlyMissle,
    /// <summary>
    /// Just like Toss, but reflect velocity when contacting surfaces
    /// </summary>
    Bounce,
    /// <summary>
    /// // bounce without gravity
    /// </summary>
    BounceMissile,
    /// <summary>
    /// track movement of aiment
    /// </summary>
    Follow,
    /// <summary>
    /// BSP model that needs physics/world collisions (uses nearest hull for world collision)
    /// </summary>
    Pushstep,
  }

  public enum DeadFlags : int
  {
    Alive = 0,
    Dying = 1,
    Dead  = 2,
    Respawnable = 3,
    Discardbody = 4,
  }

  #endregion

  // engine/edict.h
  /// <summary>
  /// The Edict struct used for almost everything in the GoldSrc engine (including the players)
  /// </summary>
  [StructLayout (LayoutKind.Sequential)]
  internal unsafe struct Edict
  {
    internal bool free;
    internal int serialnumber;
    internal Link area;
    internal int headnode;
    internal int num_leafs;
    internal fixed short leafnums[48];
    internal float freetime;
    internal void *pvPrivateData;
    internal Entvars v;
  };

  /// <summary>
  /// The end variables, additional set of variables.
  /// </summary>
  [StructLayout (LayoutKind.Sequential)]
  internal unsafe struct Entvars
  {
    internal int        classname; // string
    internal int        globalname; // string

    internal Vector3f   origin;
    internal Vector3f   oldorigin;
    internal Vector3f   velocity;
    internal Vector3f   basevelocity;
    internal Vector3f   clbasevelocity;  // Base velocity that was passed in to server physics so
                     //  client can predict conveyors correctly.  Server zeroes it, so we need to store here, too.
    internal Vector3f   movedir;

    internal Vector3f   angles;     // Model angles
    internal Vector3f   avelocity;    // angle velocity (degrees per second)
    internal Vector3f   punchangle;   // auto-decaying view angle adjustment
    internal Vector3f   v_angle;    // Viewing angle (player only)

    // For parametric entities
    internal Vector3f   endpos;
    internal Vector3f   startpos;
    internal float      impacttime;
    internal float      starttime;

    internal int        fixangle;   // 0:nothing, 1:force view angles, 2:add avelocity
    internal float      idealpitch;
    internal float      pitch_speed;
    internal float      ideal_yaw;
    internal float      yaw_speed;

    internal int        modelindex;
    internal int        model; //string

    internal int        viewmodel;    // player's viewmodel
    internal int        weaponmodel;  // what other players see

    internal Vector3f   absmin;   // BB max translated to world coord
    internal Vector3f   absmax;   // BB max translated to world coord
    internal Vector3f   mins;   // local BB min
    internal Vector3f   maxs;   // local BB max
    internal Vector3f   size;   // maxs - mins

    internal float      ltime;
    internal float      nextthink;

    internal int        movetype;
    internal int        solid;

    internal int        skin;
    internal int        body;     // sub-model selection for studiomodels
    internal int        effects;

    internal float      gravity;    // % of "normal" gravity
    internal float      friction;   // inverse elasticity of MOVETYPE_BOUNCE

    internal int        light_level;

    internal int        sequence;   // animation sequence
    internal int        gaitsequence; // movement animation sequence for player (0 for none)
    internal float      frame;      // % playback position in animation sequences (0..255)
    internal float      animtime;   // world time when frame was set
    internal float      framerate;    // animation playback rate (-8x to 8x)
    internal fixed byte controller[4];  // bone controller setting (0..255)
    internal fixed byte blending[2];  // blending amount between sub-sequences (0..255)

    internal float      scale;      // sprite rendering scale (0..255)

    internal int        rendermode;
    internal float      renderamt;
    internal Vector3f   rendercolor;
    internal int        renderfx;

    internal float      health;
    internal float      frags;
    internal int        weapons;  // bit mask for available weapons
    internal float      takedamage;

    internal int        deadflag;


    internal Vector3f   view_ofs; // eye position

    internal int        button;
    internal int        impulse;

    internal Edict      *chain;     // Entity pointer when linked into a linked list
    internal Edict      *dmg_inflictor;
    internal Edict      *enemy;
    internal Edict      *aiment;    // entity pointer when MOVETYPE_FOLLOW
    internal Edict      *owner;
    internal Edict      *groundentity;

    internal int        spawnflags;
    internal int        flags;

    internal int        colormap;   // lowbyte topcolor, highbyte bottomcolor
    internal int        team;

    internal float      max_health;
    internal float      teleport_time;
    internal float      armortype;
    internal float      armorvalue;
    internal int        waterlevel;
    internal int        watertype;

    internal int        target; // string
    internal int        targetname; // string
    internal int        netname; // string
    internal int        message; // string

    internal float      dmg_take;
    internal float      dmg_save;
    internal float      dmg;
    internal float      dmgtime;

    internal int        noise; // string
    internal int        noise1; // string
    internal int        noise2; // string
    internal int        noise3; // string

    internal float      speed;
    internal float      air_finished;
    internal float      pain_finished;
    internal float      radsuit_finished;

    internal Edict      *pContainingEntity;

    internal int        playerclass;
    internal float      maxspeed;

    internal float      fov;
    internal int        weaponanim;

    internal int        pushmsec;

    internal int        bInDuck;
    internal int        flTimeStepSound;
    internal int        flSwimTime;
    internal int        flDuckTime;
    internal int        iStepLeft;
    internal float      flFallVelocity;

    internal int        gamestate;

    internal int        oldbuttons;

    internal int        groupinfo;

    // For mods
    internal int        iuser1;
    internal int        iuser2;
    internal int        iuser3;
    internal int        iuser4;
    internal float      fuser1;
    internal float      fuser2;
    internal float      fuser3;
  }

  // commons/const.h
  [StructLayout (LayoutKind.Sequential)]
  internal unsafe struct Link
  {
    internal Link *prev;
    internal Link *next;
  };

  // TODO: reuse instances of this class, save the pointers in a list or something

  public unsafe class Entity
  {
    internal Edict *entity;
    public IntPtr Pointer { get; set; }

    #region Constructors

    internal Entity(Edict *entity)
    {
      this.entity = entity;
      Pointer = new IntPtr(entity);
    }

    internal Entity(void *ptr)
      : this((Edict*)ptr) { }

    internal Entity(IntPtr ptr)
    {
      Pointer = ptr;
      entity = (Edict *)ptr.ToPointer();
    }

    public Entity()
      : this(MetaModEngine.engineFunctions.CreateEntity()) { }

    public Entity(int classNameIndex)
      : this(MetaModEngine.engineFunctions.CreateNamedEntity(classNameIndex)) { }

    public Entity(string className)
      : this(MetaModEngine.engineFunctions.CreateNamedEntity(MetaModEngine.engineFunctions.AllocString(className))) { }

    #endregion

    /*
    // TODO: consider making these public instead of the constructors?
    public static Entity Create()
    {
      return CreateEntity(MetaModEngine.engineFunctions.CreateEntity());
    }

    public static Entity Create(int classNameIndex)
    {
      return CreateEntity(MetaModEngine.engineFunctions.CreateNamedEntity(classNameIndex));
    }

    public static Entity Create(string className)
    {
      return Create(MetaModEngine.engineFunctions.AllocString(className));
    }
    */

    #region Entity Handling

    private static Dictionary<int, Entity> entityDictionary = new Dictionary<int, Entity>();

    internal static Entity RegisterEntity(int index, Entity entity)
    {
      entityDictionary[index] = entity;
      return entity;
    }
    public static Entity CreateEntity(IntPtr entityPointer)
    {
      if (entityPointer.ToPointer() == null) return null;
      int index = Entity.GetIndex(entityPointer);
      if (entityDictionary.ContainsKey(index)) {
        return entityDictionary[index];
      } else {
        Edict* edict = (Edict *)entityPointer.ToPointer();
        // TODO: 1 and maxplayer can be bot or player
        if (index >= 1 && index <= Server.MaxPlayers) return RegisterEntity(index, new Player(entityPointer));
        string classname = GetClassName(edict);
        if (classname.StartsWith("weapon_")) return RegisterEntity(index, new CounterStrike.Weapon(edict));
        switch (classname) {
        case "player":
          return RegisterEntity(index, new Player(entityPointer));
        default:
          return RegisterEntity(index, new Entity(entityPointer));
        }
      }
    }

    internal static void RemoveEntity(IntPtr entityPointer)
    {
      int index = GetIndex(entityPointer);
      if (entityDictionary.ContainsKey(index)) {
        Entity entity = entityDictionary[index];
        if (entity is IDisposable) (entity as IDisposable).Dispose();
        entityDictionary.Remove(index);
      }
    }

    #endregion

    /// <summary>
    /// Returns the index of the entity
    /// </summary>
    public int Index {
      get {
        return GetIndex(Pointer);
      }
    }

    /// <summary>
    /// returns the entity class name
    /// </summary>
    unsafe public string Classname {
      get {
        IntPtr ptr = MetaModEngine.engineFunctions.SzFromIndex(entity->v.classname);
        return Mono.Unix.UnixMarshal.PtrToString(ptr);
      }
    }

    public int GetIllumination()
    {
      return MetaModEngine.engineFunctions.GetEntityIllum(Pointer);
    }

    public int Offset {
      get {
        return MetaModEngine.engineFunctions.EntOffsetOfPEntity(Pointer);
      }
    }

    public bool Valid
    {
      get {
        return true;
      }
    }

    public bool IsNull {
      get {
        return (Pointer == IntPtr.Zero) && (Offset == 0);
      }
    }

    // TODO: rewrite this function
    public EdictFlags Flags {
      get {
        return (EdictFlags)entity->v.flags;
      }
      set {
        entity->v.flags = (int)value;
      }
    }

    public MoveType MoveType {
      get {
        return (MoveType)entity->v.movetype;
      }
      set {
        entity->v.movetype = (int)value;
      }
    }

    public bool SilentFootsteps {
      get {
        return (entity->v.flTimeStepSound == 999);
      }
      set {
        entity->v.flTimeStepSound = (value ? 999 : 400);
      }
    }

    public bool OnFloor {
      get {
        return MetaModEngine.engineFunctions.EntityIsOnFloor(Pointer) == 1;
      }
    }

    // TODO: add this function to entity collector
    public Entity Owner {
      get {
        return CreateEntity(new IntPtr(entity));
      }
    }

    public bool DropToFloor()
    {
      return MetaModEngine.engineFunctions.DropToFloor(Pointer) == 1;
    }

    public bool IsDead {
      get { return !(DeadFlag == DeadFlags.Alive); }
    }

    public DeadFlags DeadFlag {
      get {
        return (DeadFlags)entity->v.deadflag;
      }
      set {
        entity->v.deadflag = (int)value;
      }
    }

    unsafe public void SetPrivateData(int offset, int value)
    {
      *((int *)this.entity->pvPrivateData + offset) = value;
    }

    unsafe public int GetPrivateData(int offset)
    {
     return *((int *)this.entity->pvPrivateData + offset);
    }

    public void Spawn()
    {
      MetaModEngine.dllapiFunctions.Spawn(this.Pointer);
    }

    public void Use(Entity entity)
    {
      MetaModEngine.dllapiFunctions.Use(this.Pointer, entity.Pointer);
    }

    public void Touch(Entity entity)
    {
      MetaModEngine.dllapiFunctions.Touch(this.Pointer, entity.Pointer);
    }

    public void Remove()
    {
      Entity.Remove(Pointer);
    }

    #region static methods

    /// <summary>
    /// Returns the total entity count in the engine
    /// </summary>
    public static int Count
    {
      get {
        return MetaModEngine.engineFunctions.NumberOfEntities();
      }
    }

    internal static IntPtr GetEntity(int index)
    {
      return MetaModEngine.engineFunctions.PEntityOfEntIndex(index);
    }

    public static int GetIndex(IntPtr ptr)
    {
       return MetaModEngine.engineFunctions.IndexOfEdict(ptr);
    }

    internal static int GetIndex(void *ptr)
    {
       return MetaModEngine.engineFunctions.IndexOfEdict(new IntPtr(ptr));
    }

    public static Entity Find(Entity startSearchAfter, string field, string val)
    {
      return new Entity(MetaModEngine.engineFunctions.FindEntityByString(startSearchAfter.Pointer, field, val));
    }

    internal static Entity Find(Edict *startSearchAfter, string field, string val)
    {
      return Find(new Entity(startSearchAfter), field, val);
    }

    internal static void Remove(IntPtr entity)
    {
      MetaModEngine.engineFunctions.RemoveEntity(entity);
    }

    internal static string GetClassName(Edict *entity)
    {
      // TODO: consider returning empty string?
      if (entity->v.classname == 0) return null;

      IntPtr ptr = MetaModEngine.engineFunctions.SzFromIndex(entity->v.classname);
      return Mono.Unix.UnixMarshal.PtrToString(ptr);
    }

    #endregion

    #region Entity Entvar Fields
    
    public Vector3f Origin {
      get {
        return entity->v.origin;
      }
      set {
        entity->v.origin = value;
      }
    }

    public int Spawnflags {
      get {
        return entity->v.spawnflags;
      }
      set {
        entity->v.spawnflags = value;
      }
    }

    #endregion
  }
}