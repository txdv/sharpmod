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
using SharpMod.MetaMod;
namespace SharpMod
{

  /// <summary>
  /// A class to manage models
  /// </summary>
  public class Model
  {
    public IntPtr Pointer { get; protected set; }

    public Model(IntPtr model)
    {
      Pointer = model;
    }

    /// <summary>
    /// Precaches a mode
    /// </summary>
    /// <param name="filename">
    /// A string represeting the filename <see cref="System.String"/>
    /// </param>
    /// <returns>
    /// An Index of the model <see cref="System.Int32"/>
    /// </returns>
    public static int Precache(string filename)
    {
      return MetaModEngine.engineFunctions.PrecacheModel(filename);
    }

    /// <summary>
    /// Gets the index of a loaded Model
    /// </summary>
    /// <param name="filename">
    /// A <see cref="System.String"/>
    /// </param>
    /// <returns>
    /// A <see cref="System.Int32"/>
    /// </returns>
    public static int Index(string filename)
    {
      return MetaModEngine.engineFunctions.ModelIndex(filename);
    }

    /// <summary>
    /// Gets a model from an entity
    /// </summary>
    /// <param name="entity">
    /// Entity which holds the model <see cref="Entity"/>
    /// </param>
    /// <returns>
    /// Pointer to the model <see cref="IntPtr"/>
    /// </returns>
    public static IntPtr GetPointer(Entity entity)
    {
      return GetPointer(entity.Pointer);
    }

    public static IntPtr GetPointer(IntPtr entity)
    {
      return MetaModEngine.engineFunctions.GetModelPtr(entity);
    }

    public static Model GetModel(Entity entity)
    {
      return new Model(GetPointer(entity));
    }

  }
}
