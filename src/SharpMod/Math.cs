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

using System.Runtime.InteropServices;

// hlsdk/multiplayer/utils/common/mathlib.h
// hlsdk/multiplayer/utils/common/mathlib.c

// TODO: Use a better vector library or make this one better?

namespace SharpMod.Math
{
  [StructLayout (LayoutKind.Sequential)]
  public struct Vector3f
  {
    public float x;
    public float y;
    public float z;

    public Vector3f(float x, float y, float z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
    }

    public override string ToString ()
    {
      return string.Format("vector3f({0}, {1}, {2})", x, y, z);
    }

  };

  [StructLayout (LayoutKind.Sequential)]
  internal struct Vector3d
  {
    public double x;
    public double y;
    public double z;

    public Vector3d(double x, double y, double z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
    }
  }

  [StructLayout (LayoutKind.Sequential)]
  internal struct Vector4d
  {
    public double x;
    public double y;
    public double z;
    public double w;

    public Vector4d(double x, double y, double z, double w)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.w = w;
    }
  }
}