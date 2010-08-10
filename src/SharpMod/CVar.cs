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
    internal char *name;
    internal char *str;
    internal int flags;
    internal float val;
    internal CVarInfo* next;
  }

  /// <summary>
  /// CVariable class for GoldSrc
  /// </summary>
  public class CVar : IDisposable
  {
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
    public unsafe static string GetValue(string name)
    {
      IntPtr ptr = UnixMarshal.StringToHeap(name);
      ptr = MetaMod.MetaModEngine.engineFunctions.CVarGetString((char *)ptr.ToPointer());
      return Mono.Unix.UnixMarshal.PtrToString(ptr);
    }

    /// <summary>
    /// Name of an instance of a CVariable
    /// </summary>
    public unsafe string Name { get { return Mono.Unix.UnixMarshal.PtrToString(new IntPtr(cvar->name)); } }

    /// <summary>
    /// The string value of an CVariable instance
    /// </summary>
    public unsafe string String
    {
      get { return Mono.Unix.UnixMarshal.PtrToString(MetaMod.MetaModEngine.engineFunctions.CVarGetString(cvar->name)); }
      set { MetaMod.MetaModEngine.engineFunctions.CVarSetString(cvar->name, value); }
    }

    /// <summary>
    /// The float value of an CVariable instance
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
        cvar = (CVarInfo*)Mono.Unix.UnixMarshal.AllocHeap(sizeof(CVarInfo)).ToPointer();
        cvar->name = (char *)Mono.Unix.UnixMarshal.StringToHeap(name).ToPointer();
        cvar->str = (char *)Mono.Unix.UnixMarshal.StringToHeap(val).ToPointer();
        cvar->next = (CVarInfo*)IntPtr.Zero.ToPointer();
        MetaMod.MetaModEngine.engineFunctions.CVarRegister(cvar);
      }
    }
    unsafe internal CVar(IntPtr ptr)
    {
      cvar = (CVarInfo*)ptr.ToPointer();
    }


    #region IDisposable implementation

    public void Dispose ()
    {
      unsafe
      {
        // TODO: Do we really need to cleanup the cvars?
        //Mono.Unix.UnixMarshal.FreeHeap(new IntPtr(cvar->name));
        //Mono.Unix.UnixMarshal.FreeHeap(new IntPtr(cvar->str));
        //Mono.Unix.UnixMarshal.FreeHeap(new IntPtr(cvar));
      }
    }

    #endregion
  }
}
