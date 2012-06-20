using System;
using System.Runtime.InteropServices;
using SharpMod.Math;

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

	// hlsdk/multiplayer/common/usercmd.h
	[StructLayout (LayoutKind.Sequential)]
	internal struct UserCommand
	{
		public short lerp_sec;
		public byte msec;
		public Vector3d viewangles;

		public float forwardmove;
		public float upmove;
		public byte hightlevel;
		public ushort buttons;
		public byte impulse;
		public byte weaponselect;

		public int impact_index;
		public Vector3d impact_position;
	}
	#endregion
}
