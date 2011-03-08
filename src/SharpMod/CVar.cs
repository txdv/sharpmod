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
using System.Runtime.InteropServices;
using Mono.Unix;
using SharpMod.MetaMod;

namespace SharpMod
{

/*
  #define FCVAR_ARCHIVE   (1<<0)  // set to cause it to be saved to vars.rc
  #define FCVAR_USERINFO    (1<<1)  // changes the client's info string
  #define FCVAR_SERVER    (1<<2)  // notifies players when changed
  #define FCVAR_EXTDLL    (1<<3)  // defined by external DLL
  #define FCVAR_CLIENTDLL     (1<<4)  // defined by the client dll
  #define FCVAR_PROTECTED     (1<<5)  // It's a server cvar, but we don't send the data since it's a password, etc.  Sends 1 if it's not bland/zero, 0 otherwise as value
  #define FCVAR_SPONLY        (1<<6)  // This cvar cannot be changed by clients connected to a multiplayer server.
  #define FCVAR_PRINTABLEONLY (1<<7)  // This cvar's string cannot contain unprintable characters ( e.g., used for player name etc ).
  #define FCVAR_UNLOGGED    (1<<8)  // If this is a FCVAR_SERVER, don't log changes to the log file / console if we are creating a log
*/

  internal enum CVarFlags
  {
    Archive       = (1 << 0),
    UserInfo      = (1 << 1),
    Server        = (1 << 2),
    ExtDll        = (1 << 3),
    ClientDll     = (1 << 4),
    Protected     = (1 << 5),
    Sponly        = (1 << 6),
    PrintableOnly = (1 << 7),
    Unlogged      = (1 << 8),
  }

/*
  typedef struct cvar_s
  {
    char  *name;
    char  *string;
    int   flags;
    float value;
    struct cvar_s *next;
  } cvar_t;
*/

  // hlsdk/multiplayer/common/cvardef.h
  /// <summary>
  /// CVariable struct for use in the GoldSrc engine.
  /// </summary>
  internal unsafe struct CVarInfo
  {
    internal sbyte *name;
    internal sbyte *str;
    internal int flags;
    internal float val;
    internal CVarInfo* next;
  }

  /// <summary>
  /// CVariable class for GoldSrc
  /// </summary>
  public class CVar : IDisposable
  {
    // @todo: Check if there is already an instance of an cvar (a C# class)
    // and create only new ones if there isn't

    private unsafe CVarInfo* cvar;

    /// <summary>
    /// Creates a CVar class for an already in the GoldSrc engine registered cvar.
    /// </summary>
    /// <param name="name">
    /// The name of the cvar <see cref="System.String"/>
    /// </param>
    /// <returns>
    /// A CVar class <see cref="CVar"/>
    /// </returns>
    public static CVar Get(string name)
    {
      IntPtr ptr = MetaModEngine.engineFunctions.CVarGetPointer(name);
      if (ptr == IntPtr.Zero) return null;
      else return new CVar(ptr);
    }

    /// <summary>
    /// Gets the value of a CVariable
    /// </summary>
    /// <param name="name">
    /// Name of the CVariable <see cref="System.String"/>
    /// </param>
    /// <returns>
    /// Value of the CVariable <see cref="System.String"/>
    /// </returns>
    public unsafe static string GetStringValue(string name)
    {
      return new string(MetaMod.MetaModEngine.engineFunctions.CVarGetString(name));
    }

    public unsafe static void SetStringValue(string cvarname, string value)
    {
      MetaModEngine.engineFunctions.CVarSetString(cvarname, value);
    }

    public static float GetFloatValue(string name)
    {
      return MetaModEngine.engineFunctions.CVarGetFloat(name);
    }

    public static void SetFloatValue(string name, float value)
    {
      MetaModEngine.engineFunctions.CVarSetFloat(name, value);
    }

    /// <summary>
    /// Name of an instance of a CVariable
    /// </summary>
    public unsafe string Name { get { return new string(cvar->name); } }

    /// <summary>
    /// The string value of an CVariable instance
    /// </summary>
    public unsafe string String
    {
      get { return new string(cvar->name); }
      set { MetaMod.MetaModEngine.engineFunctions.CVarSetString(Name, value); }
    }

    /// <summary>
    /// The float value of the cvar
    /// </summary>
    public unsafe float Float
    {
      get { return cvar->val; }
      set { cvar->val = value; }
    }

    /// <summary>
    /// Constructor for creating a CVar in C#
    /// </summary>
    /// <param name="name">
    /// The name of the cvar <see cref="System.String"/>
    /// </param>
    /// <param name="val">
    /// An initial string value <see cref="System.String"/>
    /// </param>
    public CVar(string name, string val)
    {
      unsafe {
        cvar = (CVarInfo*)UnixMarshal.AllocHeap(sizeof(CVarInfo)).ToPointer();
        cvar->name = (sbyte *)UnixMarshal.StringToHeap(name).ToPointer();
        cvar->str  = (sbyte *)UnixMarshal.StringToHeap(val).ToPointer();
        cvar->next = (CVarInfo*)IntPtr.Zero.ToPointer();
        MetaMod.MetaModEngine.engineFunctions.CVarRegister(cvar);
      }
    }

    unsafe internal CVar(IntPtr ptr)
    {
      cvar = (CVarInfo*)ptr.ToPointer();
    }

    unsafe bool GetFlag(int field)
    {
      return (cvar->flags & field) > 0;
    }

    unsafe void SetFlag(int field, bool val)
    {
      if (val)
        cvar->flags |= field;
      else
        cvar->flags &= ~field;
    }

    #region Flagfield accessors

    /// <summary>
    /// Sets the cvar to be save in vars.rc
    /// </summary>
    public bool Archive {
      get { return GetFlag((int)CVarFlags.Archive); }
      set { SetFlag((int)CVarFlags.Archive, value); }
    }

    /// <summary>
    /// Changes the client user info
    /// </summary>
    public bool UserInfo {
      get { return GetFlag((int)CVarFlags.UserInfo); }
      set { SetFlag((int)CVarFlags.UserInfo, value); }
    }

    /// <summary>
    /// The server notifies the player when the cvar is changed
    /// </summary>
    public bool Server {
      get { return GetFlag((int)CVarFlags.Server); }
      set { SetFlag((int)CVarFlags.Server, value); }
    }

    /// <summary>
    /// The cvar is defined by an external dll
    /// </summary>
    public bool ExtDll {
      get { return GetFlag((int)CVarFlags.ExtDll); }
      set { SetFlag((int)CVarFlags.ExtDll, value); }
    }

    /// <summary>
    /// The cvar is defined by an the client
    /// </summary>
    public bool ClientDll {
      get { return GetFlag((int)CVarFlags.ClientDll); }
      set { SetFlag((int)CVarFlags.ClientDll, value); }
    }


    /// <summary>
    /// It's a server cvar, but we don't send the data since it's a password, etc.  Sends 1 if it's not bland/zero, 0 otherwise as value
    /// </summary>
    public bool Protected {
      get { return GetFlag((int)CVarFlags.Protected); }
      set { SetFlag((int)CVarFlags.Protected, value); }
    }

    /// <summary>
    /// This cvar cannot be changed by clients connected to a multiplayer server.
    /// </summary>
    public bool Sponly {
      get { return GetFlag((int)CVarFlags.Sponly); }
      set { SetFlag((int)CVarFlags.Sponly, value); }
    }

    /// <summary>
    /// This cvar's string cannot contain unprintable characters ( e.g., used for player name etc ).
    /// </summary>
    public bool PrintableOnly {
      get { return GetFlag((int)CVarFlags.PrintableOnly); }
      set { SetFlag((int)CVarFlags.PrintableOnly, value); }
    }

    /// <summary>
    /// If this is a Server cvar, don't log changes to the log file / console if we are creating a log
    /// </summary>
    public bool Unlogged {
      get { return GetFlag((int)CVarFlags.Unlogged); }
      set { SetFlag((int)CVarFlags.Unlogged, value); }
    }

    #endregion

    #region IDisposable implementation

    public void Dispose ()
    {
      unsafe {
        // TODO: Do we really need to cleanup the cvars?
        //Mono.Unix.UnixMarshal.FreeHeap(new IntPtr(cvar->name));
        //Mono.Unix.UnixMarshal.FreeHeap(new IntPtr(cvar->str));
        //Mono.Unix.UnixMarshal.FreeHeap(new IntPtr(cvar));
      }
    }

    #endregion
  }
}
