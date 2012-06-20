using System;
using SharpMod;

namespace SharpMod
{
	/// <summary>
	/// A class for interacting with dproto.
	/// </summary>
	public static class DProto
	{
		/// <summary>
		/// This enum enlists the possibilities of what application assignes the players id (dproto, revEmu, etc.)
		/// </summary>
		public enum ID : int
		{
			/// <summary>
			/// If the function fails.
			/// </summary>
			Fail = -1,
			/// <summary>
			/// If an error occures 0 is returned
			/// </summary>
			Empty = 0,
			/// <summary>
			/// steam id assigned by dproto (player uses no-steam client)
			/// </summary>
			Dproto,
			/// <summary>
			/// steam id assigned by native steam library (or by another soft that emulates this library, revEmu for example)
			/// </summary>
			NativeSteamLibrary,
			/// <summary>
			/// steam id assigned by dproto's SteamEmu emulator
			/// </summary>
			DprotoSteamEmu,
			/// <summary>
			/// steam id assigned by dproto's revEmu emulator
			/// </summary>
			DprotoRevEmu,
			/// <summary>
			/// steam id assigned by dproto's old revEmu emulator
			/// </summary>
			DprotoOldRevEmu,
			/// <summary>
			/// if client is HLTV (no other specific id is assigned)
			/// </summary>
			HLTV,
		}

		private static CVar protocol;
		private static CVar id_provider;
		private static CVar version;

		/// <summary>
		/// This has to be called in order to make this work this class proplery.
		/// If you use this class in your plugin, just call it when the plugin is loaded.
		/// </summary>
		public static void Init()
		{
			if (protocol == null) protocol    = CVar.Get("dp_r_protocol");
			if (id_provider == null) id_provider = CVar.Get("dp_r_id_provider");
			if (version == null)     version     = CVar.Get("dp_version");
		}

		/// <summary>
		/// This Field returns true if everything works fine and this class can be used.
		/// Checks if protocol and id_provider and version fields where not empty.
		/// </summary>
		public static bool Works {
			get {
				return ((protocol == null) || (id_provider == null) || (version == null));
			}
		}

		/// <summary>
		/// Updates the two dproto variables according to the player.
		/// </summary>
		/// <param name="player">
		/// The information about this player <see cref="Player"/>
		/// </param>
		public static void UpdateCVariables(Player player)
		{
			UpdateCVariables(player.Index);
		}

		/// <summary>
		/// Updates the two dproto variables according to the player id
		/// </summary>
		/// <param name="id">
		/// The player id <see cref="System.Int32"/>
		/// </param>
		public static void UpdateCVariables(int id)
		{
			Server.EnqueueCommand(String.Format("dp_clientinfo {0}\n", id));
			Server.ExecuteEnqueuedCommands();
		}

		/// <summary>
		/// This function returns true if the two dproto variables are not set to -1
		/// Handy if you really ensure that they returned something good.
		/// Generally unneeded
		/// </summary>
		public static bool Success {
			get {
				return (Convert.ToInt32(protocol.String) != -1) || (Convert.ToInt32(id_provider.String) != -1);
			}
		}

		/// <summary>
		/// Get's the version of the current used dproto.
		/// </summary>
		public static string Version {
			get {
				return version.String;
			}
		}

		/// <summary>
		/// Get's the value of the id_provider cvariable.
		/// </summary>
		public static DProto.ID AuthIDProvider {
			get {
				return (DProto.ID)Convert.ToInt32(id_provider.String);
			}
		}

		/// <summary>
		/// Get's the value of the protocol cvariable.
		/// </summary>
		public static int Protocol {
			get {
				return Convert.ToInt32(protocol.String);
			}
		}

		/// <summary>
		/// Gets the AuthID provider of the player.
		/// </summary>
		/// <param name="player">
		/// The player <see cref="Player"/>
		/// </param>
		/// <returns>
		/// The ID provider as an enum value <see cref="DprotoID"/>
		/// </returns>
		public static DProto.ID GetAuthIDProvider(this Player player)
		{
			UpdateCVariables(player);
			return AuthIDProvider;
		}

		/// <summary>
		/// Gets the protocol version of the client.
		/// </summary>
		/// <returns>
		/// Protocol version <see cref="System.Int32"/>
		/// </returns>
		public static int GetProtocol(this Player player)
		{
			UpdateCVariables(player);
			return Protocol;
		}

	}
}
