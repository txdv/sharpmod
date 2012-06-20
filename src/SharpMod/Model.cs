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
