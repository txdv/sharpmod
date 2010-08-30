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

#define DEBUG

using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using Mono.Unix;
using SharpMod.Helper;
using SharpMod.Math;

namespace SharpMod.MetaMod
{
  #region Enums

  // hlsdk/multiplayer/engine/eiface.h
  internal enum PrintType
  {
    Console,
    Center,
    Chat
  };

  internal enum MetaResult : int
  {
    Unset = 0,
    /// <summary>
    /// plugin didn't take any action
    /// </summary>
    Ignore,
    /// <summary>
    /// plugin did something, but real function should still be called
    /// </summary>
    Handled,
    /// <summary>
    /// call real function, but use my return value
    /// </summary>
    Override,
    /// <summary>
    /// skip real function; use my return value
    /// </summary>
    Supercede
  }

  #endregion

  internal unsafe struct MetaGlobals
  {
    /// <summary>
    /// writable; plugin's return flag
    /// </summary>
    internal MetaResult mres;
    /// <summary>
    /// readable; return flag of the previous plugin called
    /// </summary>
    internal MetaResult prev_mres;
    /// <summary>
    /// readable; "highest" return flag so far
    /// </summary>
    internal MetaResult status;

    /// <summary>
    /// readable; return value from "real" function
    /// </summary>
    internal void *orig_ret;
    /// <summary>
    /// readable; return value from overriding/superceding plugin
    /// </summary>
    internal void *override_ret;
  }

  // hlsdk/multiplayer/engine/progdefs.h
  internal unsafe struct GlobalVariables
  {
    internal float time;
    internal float frametime;
    internal float force_retouch;
    internal int mapname; // string as int
    internal int startspot; // string as int
    internal float deathmatch;
    internal float coop;
    internal float teamplay;
    internal float serverflags;
    internal float found_secrets;
    internal Vector3f v_forward;
    internal Vector3f v_up;
    internal Vector3f v_right;
    internal float trace_allsolid;
    internal float trace_startsolid;
    internal float trace_fraction;
    internal Vector3f trace_endpos;
    internal Vector3f trace_plane_normal;
    internal float trace_plane_dist;
    internal Edict *trace_ent;
    internal float trace_inopen;
    internal float trace_inwater;
    internal int trace_hitgroup;
    internal int trace_flags;
    internal int msg_entity;
    internal int cdAudioTrack;
    internal int maxClients;
    internal int maxEntities;
    internal char *pStringBase;
    internal void *psSaveData;
    internal Vector3f vecLandmarkOffset;
  }

  #region MetaUlity Functions

  internal delegate void LogConsoleDelegate(IntPtr PluginInfo, string text);
  internal delegate void LogMessageDelegate(IntPtr PluginInfo, string text);
  internal delegate void LogErrorDelegate(IntPtr PluginInfo, string text);
  internal delegate void LogDeveloperDelegate(IntPtr PluginInfo, string text);
  internal delegate void CenterSayDelegate(IntPtr PluginInfo, string text);

  [StructLayout(LayoutKind.Sequential)]
  internal struct MetaUtilityFunctions
  {
    internal LogConsoleDelegate LogConsole;
    internal LogMessageDelegate LogMessage;
    internal LogErrorDelegate LogError;
    internal LogDeveloperDelegate LogDeveloper;
    internal CenterSayDelegate CenterSay;
  }

  #endregion

  #region Engine Functions

  internal delegate int PrecacheModelDelegate(string filename);
  internal delegate int PrecacheSoundDelegate(string filename);

  internal delegate IntPtr FindEntityByStringDelegate(IntPtr entity, string field, string val);
  internal delegate int GetEntityIllumDelegate(IntPtr entity);
  internal unsafe delegate IntPtr FindEntityInSphereDelegate(IntPtr startSearchAfter, float *org, float rad);


  internal delegate IntPtr CreateEntityDelegate();
  internal delegate void RemoveEntityDelegate(IntPtr edict);
  internal delegate IntPtr CreateNamedEntityDelegate(int className);
  internal delegate int EntityIsOnFloorDelegate(IntPtr entity);
  internal delegate int DropToFloorDelegate(IntPtr entity);

  internal delegate void ServerCommandDelegate(string str);
  internal delegate void ServerExecuteDelegate();
  internal delegate void ExecuteClientCommandDelegate(IntPtr edict, string command);
  internal delegate int EntOffsetOfPEntityDelegate(IntPtr entity);
  internal delegate IntPtr PEntityOfEntOffsetDelegate(int iEntOffset);
  internal delegate int IndexOfEdictDelegate(IntPtr entity);
  internal delegate IntPtr PEntityOfEntIndexDelegate(int index);
  internal delegate void ClientPrintfDelegate(IntPtr pEdict, PrintType type, string message);
  internal delegate void ServerPrintDelegate(string message);
  internal delegate void RegUserMsgDelegate(string name, int size);
  internal delegate IntPtr Cmd_ArgsDelegate();
  internal delegate IntPtr Cmd_ArgvDelegate(int i);
  internal delegate int Cmd_ArgcDelegate();

  internal delegate void CRC32_InitDelegate(IntPtr crc32);
  internal delegate void CRC32_ProcessBufferDelegate(IntPtr crc32, IntPtr ptr, int size);
  internal delegate void CRC32_ProcessByteDelegate(IntPtr crc32, char ch);
  internal delegate long CRC32_FinalDelegate(long crc32);

  internal delegate void MessageBeginDelegate(MessageDestination destination, int messageType, IntPtr floatValue, IntPtr playerEntity);
  internal delegate void MessageEndDelegate();

  internal delegate void WriteByteDelegate  (int val);
  internal delegate void WriteCharDelegate  (int val);
  internal delegate void WriteShortDelegate (int val);
  internal delegate void WriteLongDelegate  (int val);
  internal delegate void WriteAngleDelegate (int val);
  internal delegate void WriteCoordDelegate (int val);
  internal delegate void WriteStringDelegate(string val);
  internal delegate void WriteEntityDelegate(int val);

  internal unsafe delegate void CVarRegisterDelegate(CVarInfo *pCvar);
  internal delegate float CVarGetFloatDelegate(string szVarName);
  internal unsafe delegate IntPtr CVarGetStringDelegate(char *name);
  internal delegate void CVarSetFloatDelegate(string szVarName, float flValue);
  internal unsafe delegate void CVarSetStringDelegate(char *name, string val);
  internal delegate void AlertMessageDelegate(short alerttype, string format);
  internal delegate IntPtr SzFromIndexDelegate(int iString);
  internal delegate int AllocStringDelegate(string szValue);

  internal unsafe delegate void GetGameDirDelegate(char *directoy);
  internal delegate IntPtr GetInfoKeyBuffer(IntPtr entity);
  internal delegate IntPtr InfoKeyValueDelegate(IntPtr infoBuffer, string key);
  internal delegate void SetServerKeyValueDelegate(IntPtr infoBuffer, string key, string val);
  internal delegate void SetClientKeyValueDelegate(int clientIndex, IntPtr infoBuffer, string key, string val);
  internal delegate int IsMapValidDelegate(string filename);
  internal delegate int IsDedicatedServerDelegate();
  internal delegate IntPtr CVarGetPointerDelegate(string cvarname);
  internal delegate void GetPlayerStatsDelegate(IntPtr playerEntity, ref int ping, ref int packet_loss);
  internal delegate void AddServerCommandDelegate(string name, IntPtr functionPointer);
  internal delegate IntPtr GetPlayerAuthIdDelegate(IntPtr playerEntity);


  // metamod/meta_eiface.h
  [StructLayout (LayoutKind.Sequential)]
  internal struct EngineFunctions
  {
    internal PrecacheModelDelegate PrecacheModel;
    internal PrecacheSoundDelegate PrecacheSound;
    IntPtr SetModel;
    IntPtr ModelIndex;
    IntPtr ModelFrames;
    IntPtr SetSize;
    IntPtr ChangeLevel;
    IntPtr GetSpawnParms;
    IntPtr SaveSpawnParms;
    IntPtr VecToYaw;
    IntPtr VecToAngles;
    IntPtr MoveToOrigin;
    IntPtr ChangeYaw;
    IntPtr ChangePitch;
    internal FindEntityByStringDelegate FindEntityByString;
    internal GetEntityIllumDelegate GetEntityIllum;
    internal FindEntityInSphereDelegate FindEntityInSphere;
    IntPtr FindClientInPVS;
    IntPtr EntitiesInPVS;
    IntPtr MakeVectors;
    IntPtr AngleVectors;
    internal CreateEntityDelegate CreateEntity;
    internal RemoveEntityDelegate RemoveEntity;
    internal CreateNamedEntityDelegate CreateNamedEntity;
    IntPtr MakeStatic;
    internal EntityIsOnFloorDelegate EntityIsOnFloor;
    internal DropToFloorDelegate DropToFloor;
    IntPtr WalkMove;
    IntPtr SetOrigin;
    IntPtr EmitSound;
    IntPtr EmitAmbientSound;
    IntPtr TraceLine;
    IntPtr TraceToss;
    IntPtr TraceMonsterHull;
    IntPtr TraceHull;
    IntPtr TraceModel;
    IntPtr TraceTexture;
    IntPtr TraceSphere;
    IntPtr GetAimVector;
    internal ServerCommandDelegate ServerCommand;
    internal ServerExecuteDelegate ServerExecute;
    internal ExecuteClientCommandDelegate ExecuteClientCommand;
    IntPtr ParticleEffect;
    IntPtr LightStyle;
    IntPtr DecalIndex;
    IntPtr PointContents;

    internal MessageBeginDelegate MessageBegin;
    internal MessageEndDelegate   MessageEnd;

    internal WriteByteDelegate    WriteByte;
    internal WriteCharDelegate    WriteChar;
    internal WriteShortDelegate   WriteShort;
    internal WriteLongDelegate    WriteLong;
    internal WriteAngleDelegate   WriteAngle;
    internal WriteCoordDelegate   WriteCoord;
    internal WriteStringDelegate  WriteString;
    internal WriteEntityDelegate  WriteEntity;

    internal CVarRegisterDelegate  CVarRegister;
    internal CVarGetFloatDelegate  CVarGetFloat;
    internal CVarGetStringDelegate CVarGetString;
    internal CVarSetFloatDelegate  CVarSetFloat;
    internal CVarSetStringDelegate CVarSetString;
    internal AlertMessageDelegate  AlertMessage;
    IntPtr EngineFprintf;
    IntPtr PvAllocEntPrivateData;
    IntPtr PvEntPrivateData;
    IntPtr FreeEntPrivateData;
    internal SzFromIndexDelegate SzFromIndex;
    internal AllocStringDelegate AllocString;
    IntPtr GetVarsOfEnt;
    internal PEntityOfEntOffsetDelegate PEntityOfEntOffset;
    internal EntOffsetOfPEntityDelegate EntOffsetOfPEntity;
    internal IndexOfEdictDelegate IndexOfEdict;
    internal PEntityOfEntIndexDelegate PEntityOfEntIndex;
    IntPtr FindEntityByVars;
    IntPtr GetModelPtr;
    internal RegUserMsgDelegate RegUserMsg;
    IntPtr AnimationAutomove;
    IntPtr GetBonePosition;
    IntPtr FunctionFromName;
    IntPtr NameForFunction;
    internal ClientPrintfDelegate ClientPrintf;
    internal ServerPrintDelegate ServerPrint;
    internal Cmd_ArgsDelegate Cmd_Args;
    internal Cmd_ArgvDelegate Cmd_Argv;
    internal Cmd_ArgcDelegate Cmd_Argc;
    IntPtr GetAttachment;
    internal CRC32_InitDelegate          CRC32_Init;
    internal CRC32_ProcessBufferDelegate CRC32_ProcessBuffer;
    internal CRC32_ProcessByteDelegate   CRC32_ProcessByte;
    internal CRC32_FinalDelegate         CRC32_Final;
    IntPtr RandomLong;
    IntPtr RandomFloat;
    IntPtr SetView;
    IntPtr Time;
    IntPtr CrosshairAngle;
    IntPtr LoadFileForMe;
    IntPtr FreeFile;
    IntPtr EndSection;
    IntPtr CompareFileTime;
    internal GetGameDirDelegate GetGameDir;
    IntPtr Cvar_RegisterVariable;
    IntPtr FadeClientVolume;
    IntPtr SetClientMaxspeed;
    IntPtr CreateFakeClient;
    IntPtr RunPlayerMove;
    IntPtr NumberOfEntities;
    internal GetInfoKeyBuffer          GetInfoKeyBuffer;
    internal InfoKeyValueDelegate      InfoKeyValue;
    internal SetServerKeyValueDelegate SetServerKeyValue;
    internal SetClientKeyValueDelegate SetClientKeyValue;
    internal IsMapValidDelegate IsMapValid;
    IntPtr StaticDecal;
    IntPtr PrecacheGeneric;
    IntPtr GetPlayerUserId;
    IntPtr BuildSoundMsg;
    internal IsDedicatedServerDelegate IsDedicatedServer;
    internal CVarGetPointerDelegate CVarGetPointer;
    IntPtr GetPlayerWONId;
    IntPtr Info_RemoveKey;
    IntPtr GetPhysicsKeyValue;
    IntPtr SetPhysicsKeyValue;
    IntPtr GetPhysicsInfoString;
    IntPtr PrecacheEvent;
    IntPtr PlaybackEvent;
    IntPtr SetFatPVS;
    IntPtr SetFatPAS;
    IntPtr CheckVisibility;
    IntPtr DeltaSetField;
    IntPtr DeltaUnsetField;
    IntPtr DeltaAddEncoder;
    IntPtr GetCurrentPlayer;
    IntPtr CanSkipPlayer;
    IntPtr DeltaFindField;
    IntPtr DeltaSetFieldByIndex;
    IntPtr DeltaUnsetFieldByIndex;
    IntPtr SetGroupMask;
    IntPtr CreateInstancedBaseline;
    IntPtr Cvar_DirectSet;
    IntPtr ForceUnmodified;
    internal GetPlayerStatsDelegate GetPlayerStats;
    internal AddServerCommandDelegate AddServerCommand;
    IntPtr Voice_GetClientListening;
    IntPtr Voice_SetClientListening;
    internal GetPlayerAuthIdDelegate GetPlayerAuthId;
    IntPtr SequenceGet;
    IntPtr SequencePickSentence;
    IntPtr GetFileSize;
    IntPtr GetApproxWavePlayLen;
    IntPtr IsCareerMatch;
    IntPtr GetLocalizedStringLength;
    IntPtr RegisterTutorMessageShown;
    IntPtr GetTimesTutorMessageShown;
    IntPtr ProcessTutorMessageDecayBuffer;
    IntPtr ConstructTutorMessageDecayBuffer;
    IntPtr ResetTutorMessageDecayData;
    IntPtr QueryClientCvarValue;
    IntPtr QueryClientCvarValue2;
  };

  #endregion

  #region Entity functions

  internal delegate void GameInitDelegate();
  internal delegate void SpawnDelegate(IntPtr pent);
  internal delegate void UseDelegate(IntPtr pentUsed, IntPtr pentOther);
  internal delegate void TouchDelegate(IntPtr pentTouched, IntPtr pentOther);
  internal delegate bool ClientConnectDelegate(IntPtr pEntity, string name, string address, string reject_reason);
  internal delegate void ClientDisconnectDelegate(IntPtr pEntity);
  //internal unsafe delegate char *CVarGetStringDelegate(char *szVarName);
  internal delegate void PutInServerDelegate(IntPtr playerEntity);
  internal delegate void ClientCommandDelegate(IntPtr entity);
  internal delegate void ClientUserInfoChangedDelegate(IntPtr pEntity, string infoBuffer);
  internal delegate void ServerActivateDelegate(IntPtr pEdictList, int edictCount, int clientMax);


  [StructLayout (LayoutKind.Sequential)]
  internal struct EntityAPI
  {
    internal GameInitDelegate GameInit;
    internal SpawnDelegate Spawn;
    IntPtr Think;
    internal UseDelegate Use;
    internal TouchDelegate Touch;
    IntPtr Blocked;
    IntPtr KeyValue;
    IntPtr Save;
    IntPtr Restore;
    IntPtr SetAbsBox;

    IntPtr SaveWriteFields;
    IntPtr SaveReadFields;

    IntPtr SaveGlobalState;
    IntPtr RestoreGlobalState;
    IntPtr ResetGlobalState;

    internal ClientConnectDelegate ClientConnect;
    internal ClientDisconnectDelegate ClientDisconnect;
    IntPtr ClientKill;
    internal PutInServerDelegate PutInServer;
    internal ClientCommandDelegate ClientCommand;
    internal ClientUserInfoChangedDelegate ClientUserInfoChanged;
    internal ServerActivateDelegate ServerActivate;
    IntPtr ServerDeactivate;

    IntPtr PlayerPreThink;
    IntPtr PlayerPostThink;

    IntPtr StartFrame;
    IntPtr ParmsNewLevel;
    IntPtr ParmsChangeLevel;

    IntPtr GetGameDescription;
    IntPtr PlayerCustomization;

    IntPtr SpectatorConnect;
    IntPtr SpectatorDisconnect;
    IntPtr SpectatorThink;

    IntPtr Sys_Error;
    IntPtr PM_Move;
    IntPtr PM_Init;
    IntPtr PM_FindTextureType;

    IntPtr SetupVisibility;
    IntPtr UpdateClientData;
    IntPtr AddToFullPack;
    IntPtr CreateBaseline;
    IntPtr RegisterEncoders;
    IntPtr GetWeaponData;
    IntPtr CmdStart;
    IntPtr CmdEnd;
    IntPtr ConnectionlessPacket;
    IntPtr GetHullBounds;
    IntPtr CreateInstanceBaseline;
    IntPtr InconsistentFile;
    IntPtr AllowLagCompensation;
  };

  #endregion

  #region Metamod functions

  internal unsafe delegate int GetEntityApiDelegate(ref EntityAPI functions, ref int interfaceVersion);
  internal unsafe delegate int GetEngineFunctionsDelegate(ref EngineFunctions functions, ref int interfaceVersion);

  internal struct MetaFunctions
  {
    internal IntPtr GetEntityAPI;
    internal IntPtr GetEntityAPIPost;
    internal IntPtr GetEntityAPI2;
    internal IntPtr GetEntityAPI2Post;
    internal IntPtr GetNewDllFunctions;
    internal IntPtr GetNewDllFunctionsPost;
    internal IntPtr GetEngineFunctions;
    internal IntPtr GetEngineFunctionsPost;
  }

  internal struct MetaFunctionsManaged
  {
    internal GetEntityApiDelegate GetEntityApi;
    internal GetEntityApiDelegate GetEntityApiPost;
    internal GetEntityApiDelegate GetEntityApi2;
    internal GetEntityApiDelegate GetEntityApi2Post;
    internal IntPtr GetNewDllFunctions;
    internal IntPtr GetNewDllFunctionsPost;
    internal GetEngineFunctionsDelegate GetEngineFunctions;
    internal GetEngineFunctionsDelegate GetEngineFunctionsPost;
  }

  #endregion

/*
typedef enum {
  PT_NEVER = 0,
  PT_STARTUP,     // should only be loaded/unloaded at initial hlds execution
  PT_CHANGELEVEL,   // can be loaded/unloaded between maps
  PT_ANYTIME,     // can be loaded/unloaded at any time
  PT_ANYPAUSE,    // can be loaded/unloaded at any time, and can be "paused" during a map
} PLUG_LOADTIME;

typedef struct {
  char *ifvers;       // meta_interface version
  char *name;         // full name of plugin
  char *version;        // version
  char *date;         // date
  char *author;       // author name/email
  char *url;          // URL
  char *logtag;       // log message prefix (unused right now)
  PLUG_LOADTIME loadable;   // when loadable
  PLUG_LOADTIME unloadable; // when unloadable
} plugin_info_t;
*/

  // metamod/plinfo.h
  internal enum PluginLoadTime
  {
    Never = 0,
    Startup,
    Changelevel,
    Anytime,
    Anypause
  };

  // metamod/plinfo.h
  [StructLayout (LayoutKind.Sequential)]
  unsafe internal struct PluginInfo
  {
    internal char *ifvers;
    internal char *name;
    internal char *version;
    internal char *date;
    internal char *author;
    internal char *url;
    internal char *logtag;
    internal PluginLoadTime loadable;
    internal PluginLoadTime unloadable;
  };

  internal class MetaModEngine
  {

    internal static EngineFunctions engineFunctions;
    internal static EntityAPI dllapiFunctions;
    unsafe internal static MetaGlobals* globals;
    unsafe internal static GlobalVariables* globalVariables;

    //[MethodImplAttribute(MethodImplOptions.InternalCall)]
    unsafe internal static void handlerGiveFnptrsToDll(IntPtr engineFunctionsFromEngine, GlobalVariables* globalVariables)
    {
      #if DEBUG
      Console.WriteLine(" -- MONO: handlerGiveFnptrsToDll");
      #endif
      engineFunctions = (EngineFunctions)Marshal.PtrToStructure(engineFunctionsFromEngine, typeof(EngineFunctions));
      MetaModEngine.globalVariables = globalVariables;

      SharpMod.Init();
      Server.Init();
    }

    //internal static MetaUtilityFunctions muf;
    unsafe internal static void handlerMeta_Query(PluginLoadTime now, IntPtr PluginInfo, IntPtr MetaUtilFuncs)
//    unsafe internal static void handlerMeta_Query(PluginLoadTime now, IntPtr PluginInfo, ref MetaUtilityFunctions MetaUtilFuncs)
    {
      #if DEBUG
      Console.WriteLine(" -- MONO: handlerMeta_Query");
      #endif

      // TODO: fix that
      // The following code doesn't work because C# treats char * as a pointer to a char, while
      // in C the offsets are really as long as the text itself, or something like that
      /*
      PluginInfo* pluginInfo = (PluginInfo *)Marshal.AllocHGlobal(sizeof(PluginInfo)).ToPointer();
      pluginInfo->ifvers = (char *)UnixMarshal.StringToHeap("5:13").ToPointer();
      pluginInfo->name = (char *)UnixMarshal.StringToHeap("sharpmod").ToPointer();
      pluginInfo->version = (char *)UnixMarshal.StringToHeap("0.1").ToPointer();
      pluginInfo->date = (char *)UnixMarshal.StringToHeap("2008/01/01").ToPointer();
      pluginInfo->author = (char *)UnixMarshal.StringToHeap("Andrius Bentkus").ToPointer();
      pluginInfo->url = (char *)UnixMarshal.StringToHeap("Andrius Bentkus").ToPointer();
      pluginInfo->logtag = (char *)UnixMarshal.StringToHeap("Andrius Bentkus").ToPointer();
      pluginInfo->loadable = PluginLoadTime.Anytime;
      pluginInfo->unloadable = PluginLoadTime.Anytime;
      *pPlugInfo = pluginInfo;
      */

      //muf = (MetaUtilityFunctions)Marshal.PtrToStructure(MetaUtilFuncs, typeof(MetaUtilityFunctions));
    }

    unsafe internal static void handlerMeta_Attach(IntPtr a, MetaFunctions *pFunctionTable, MetaGlobals* pMetaGlobals, IntPtr pGamedllFuncs)
    {
      #if DEBUG
      Console.WriteLine(" -- MONO: handlerMeta_Attach");
      #endif

      pFunctionTable->GetEntityAPI            = IntPtr.Zero;
      pFunctionTable->GetEntityAPIPost        = IntPtr.Zero;
      pFunctionTable->GetEntityAPI2           = IntPtr.Zero;
      pFunctionTable->GetEntityAPI2Post       = Marshal.GetFunctionPointerForDelegate(new GetEntityApiDelegate(GetEntityAPI2Post));
      pFunctionTable->GetNewDllFunctions      = IntPtr.Zero;
      pFunctionTable->GetNewDllFunctionsPost  = IntPtr.Zero;
      pFunctionTable->GetEngineFunctions      = IntPtr.Zero;
      pFunctionTable->GetEngineFunctionsPost  = Marshal.GetFunctionPointerForDelegate(new GetEngineFunctionsDelegate(GetEngineFunctionsPost));

      globals = pMetaGlobals;


      dllapiFunctions = (EntityAPI)Marshal.PtrToStructure(pGamedllFuncs, typeof(EntityAPI));
    }

    #region Engine Functions Post

    internal static unsafe int GetEngineFunctionsPost(ref EngineFunctions functions, ref int interfaceVersion)
    {
      functions = new EngineFunctions();

      functions.RegUserMsg = RegisterUserMessagePost;

      functions.MessageBegin = MessageBeginPost;
      functions.MessageEnd = MessageEndPost;

      functions.WriteByte   = WriteBytePost;
      functions.WriteChar   = WriteCharPost;
      functions.WriteShort  = WriteShortPost;
      functions.WriteLong   = WriteLongPost;
      functions.WriteAngle  = WriteAnglePost;
      functions.WriteCoord  = WriteCoordPost;
      functions.WriteString = WriteStringPost;
      functions.WriteEntity = WriteEntityPost;

      functions.Cmd_Args = CmdArgs;
      functions.Cmd_Argc = CmdArgc;
      functions.Cmd_Argv = CmdArgv;

      functions.RemoveEntity = RemoveEntityPost;

      return 0;
    }

    #region Message handling

    #if DEBUG
    internal static MessageInformation messageInformation;
    #endif

    internal static MessageHeader message_header;
    internal static List<object> message_elements;
    internal static int message_type;

    internal static void InvokeFunction(Delegate function, List<object> argList)
    {
      var param = function.Method.GetParameters();
      object[] argumentList = new object[param.Length];
      argList.CopyTo(0, argumentList, 0, param.Length);
      for (int i = argList.Count; i < param.Length; i++) argumentList[i] = param[i].DefaultValue;
      function.Method.Invoke(null, argumentList);
    }

    internal static void MessageBeginPost(MessageDestination destination, int messageType, IntPtr floatValue, IntPtr playerEntity)
    {
      #if DEBUG
      messageInformation = new MessageInformation(destination, messageType, floatValue, playerEntity);
      messageInformation.CallTimeBegin = DateTime.Now;
      #endif


      message_header = new MessageHeader(destination, messageType, floatValue, playerEntity);

      message_elements = new List<object>();

      if (playerEntity.ToInt32() == 0)
      {
        // message send to all players
        message_elements.Add(null);
      }
      else
      {
        // message send to a specific player
        message_elements.Add(Player.GetPlayer(playerEntity));
      }

    }
    internal static void MessageEndPost()
    {
      #if DEBUG
      messageInformation.CallTimeEnd = DateTime.Now;
      Console.WriteLine (messageInformation);
      #endif

      BinaryTree.Node node = Message.TypeNames[message_type];

      if ((node != null) && (node.invoker != null))
      {
        InvokeFunction(node.invoker, message_elements);
      }

    }

    internal static void WriteBytePost(int val)
    {
      #if DEBUG
      messageInformation.Arguments.Add(new MessageArgument(typeof(byte), (byte)val));
      #endif
      message_elements.Add((byte)val);
    }
    internal static void WriteCharPost(int val)
    {
      #if DEBUG
      messageInformation.Arguments.Add(new MessageArgument(typeof(char), (char)val));
      #endif
      message_elements.Add((char)val);
    }
    internal static void WriteShortPost(int val)
    {
      #if DEBUG
      messageInformation.Arguments.Add(new MessageArgument(typeof(short), (short)val));
      #endif
      message_elements.Add((short)val);
    }
    internal static void WriteLongPost(int val)
    {
      #if DEBUG
      messageInformation.Arguments.Add(new MessageArgument(typeof(long), (long)val));
      #endif
      message_elements.Add((long)val);
    }

    internal static void WriteAnglePost(int val)
    {
      // TODO: get an idea of what is past with this function
      Console.WriteLine("WriteAnglePost");
    }

    internal static int writeCoordCount = 0;
    internal static Vector3f coord = new Vector3f();
    unsafe internal static void WriteCoordPost(int val)
    {
      float *flValue = (float *)&val;
      switch (writeCoordCount)
      {
      case 0:
        coord.x = *flValue;
        break;
      case 1:
        coord.y = *flValue;
        break;
      default:
        coord.z = *flValue;
        break;
      }

      if (writeCoordCount >= 2) {
        #if DEBUG
        messageInformation.Arguments.Add(new MessageArgument(typeof(Vector3f), coord));
        #endif
        writeCoordCount = 0;
      } else { writeCoordCount++; }
      // TODO: get an idea of what is past with this function, floats maybe?
    }
    internal static void WriteStringPost(string val)
    {
      #if DEBUG
      messageInformation.Arguments.Add(new MessageArgument(typeof(string), (string)val));
      #endif
      message_elements.Add(val);
    }
    internal static void WriteEntityPost(int entity)
    {
      #if DEBUG
      messageInformation.Arguments.Add(new MessageArgument(typeof(Entity), new Entity(new IntPtr(entity))));
      #endif
      message_elements.Add(entity);
    }

    internal static void RegisterUserMessagePost(string name, int size)
    {
      int val = Message.Types.Count + 64;
      BinaryTree.Node node = new BinaryTree.Node(name, val);
      Message.Types.Add(node);
      Message.TypeNames[val] = node;
      #if DEBUG
      Console.WriteLine ("Registering: {0} {1}", name, val);
      #endif
    }
    #endregion

    #region Command Argument Modifier

    unsafe internal static IntPtr CmdArgs()
    {
      if (Command.overrideArguments && Command.instance != null)
      {
        globals->mres = MetaResult.Supercede;
        return UnixMarshal.StringToHeap(Command.instance.Arguments[1]);
      }
      else
      {
        globals->mres = MetaResult.Ignore;
        return IntPtr.Zero;
      }
    }
    unsafe internal static int CmdArgc()
    {
      if (Command.overrideArguments && Command.instance != null)
      {
        globals->mres = MetaResult.Supercede;
        return Command.instance.Arguments.Length;
      }
      else
      {
        globals->mres = MetaResult.Ignore;
        return 0;
      }
    }
    unsafe internal static IntPtr CmdArgv(int i)
    {
      if (Command.overrideArguments && Command.instance != null)
      {
        globals->mres = MetaResult.Supercede;
        return UnixMarshal.StringToHeap(Command.instance.Arguments[i]);
      }
      else
      {
        globals->mres = MetaResult.Ignore;
        return IntPtr.Zero;
      }
    }

    #endregion

    public static void RemoveEntityPost(IntPtr entity)
    {
      Entity.RemoveEntity(entity);
    }

    #endregion

    #region EntityAPI2 Post

    unsafe internal static int GetEntityAPI2Post(ref EntityAPI functionTable, ref int interfaceVersion)
    {
      #if DEBUG
      Console.WriteLine(" -- MONO: GetEntityAPI2Post");
      #endif
      functionTable = new EntityAPI();
      functionTable.Spawn = Spawn;
      functionTable.Use = UsePost;
      functionTable.ClientConnect    = Player.OnConnect;
      functionTable.ClientDisconnect = Player.OnDisconnect;
      functionTable.ServerActivate   = ServerActivatePost;
      functionTable.ClientCommand    = Player.OnCommand;
      functionTable.PutInServer      = Player.OnPutInServer;

      Server.Init();
      return 0;
    }

    internal static void ServerActivatePost(IntPtr pEdictList, int edictCount, int clientMax)
    {
      #if DEBUG
      Console.WriteLine(" -- MONO: ServerActivate");
      #endif
      Server.SetMaxPlayers(clientMax);

      // Load plugins here
      PluginManager.GetInstance().LoadPlugins();

      // TODO: check if it is really counter strike
      if (Server.GameDirectory == "cstrike") CounterStrike.CounterStrike.Init();
    }

    internal static bool spawnInitialized = false;
    internal static void Spawn(IntPtr pent)
    {
      if (spawnInitialized) return;
      spawnInitialized = true;
      // TODO: call precache method for plugins
    }
    internal static void UsePost(IntPtr pentUsed, IntPtr pentOther)
    {
      // TODO: do something in here
    }

    #endregion
  }
}
