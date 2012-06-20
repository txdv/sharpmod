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