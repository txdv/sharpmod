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

namespace SharpMod
{

  // common/const.h
  public enum RenderType
  {
    None = 0,
    PulseSlow,
    PulseFast,
    PulseSlowWide,
    PulseFastWide,
    FadeSlow,
    FadeFast,
    SolidSlow,
    SolidFast,
    StrobeSlow,
    StrobeFast,
    StrobeFaster,
    FlickerSlow,
    FlickerFast,
    NoDissipation,
    Distort,       // Distort/scale/translate flicker
    Hologram,      // kRenderFxDistort + distance fade
    DeadPlayer,    // kRenderAmt is the player index
    Explode,       // Scale up really big!
    GlowShell,     // Glowing Shell
    ClampMinScale, // Keep this sprite from getting very small (SPRITES only!)
  };

  #region MetaMod Engine structs

  // hlsdk/multiplayer/utils/common/math.h
  [StructLayout (LayoutKind.Sequential)]
  internal struct Vector3D
  {
    public double x;
    public double y;
    public double z;
  }

  // hlsdk/multiplayer/utils/common/math.h
  [StructLayout (LayoutKind.Sequential)]
  internal struct Vector4D
  {
    public double x;
    public double y;
    public double z;
  }

  // hlsdk/multiplayer/common/usercmd.h
  [StructLayout (LayoutKind.Sequential)]
  internal struct UserCommand
  {
    public short lerp_sec;
    public byte msec;
    public Vector3D viewangles;
    
    public float forwardmove;
    public float upmove;
    public byte hightlevel;
    public ushort buttons;
    public byte impulse;
    public byte weaponselect;

    public int impact_index;
    public Vector3D impact_position;
  }
  #endregion
}
