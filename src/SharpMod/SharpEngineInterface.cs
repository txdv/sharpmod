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

namespace SharpMod
{
  internal unsafe delegate void MessageBeginDelegate(int MessageDestination, int MessageType, float *val, Edict *edict);
  internal unsafe delegate void MessageEndDelegate();
  internal unsafe delegate void CVarGetStringDelegate(char *szVarName);
  internal unsafe delegate void GetGameDir(char *dir);

  // metamod/meta_eiface.h
  unsafe struct EngineFunctions
  {
    IntPtr PrechadeModel;
    IntPtr PrecacheSound;
    IntPtr SetModel;
    IntPtr ModelIndex;
    IntPtr ModelFrames;
    IntPtr SetSize;

    IntPtr ChangeLevel;
    IntPtr GetSpawnParms;
    IntPtr SaveSpawnParams;

    IntPtr VecToYaw;
    IntPtr VecToAngles;
    IntPtr MoveToOrigin;
    IntPtr ChangeYaw;
    IntPtr ChangePitch;

    IntPtr FindentityByString;
    IntPtr GetEntityIllum;
    IntPtr FindClientPVS;
    IntPtr EntitiesInPVS;

    IntPtr MakeVectors;
    IntPtr AngleVectors;

    IntPtr CreateEntity;
    IntPtr RemoveEntity;
    IntPtr CreateNamedEntity;

    IntPtr MakeStatic;
    IntPtr EntIsOnFloor;
    IntPtr DropToFloor;

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

    IntPtr ServerCommand;
    IntPtr ServerExecute;
    IntPtr ClientCommand;

    IntPtr ParticleEffect;
    IntPtr LightStyle;
    IntPtr DecalIndex;
    IntPtr Pointcontents;

    internal MessageBeginDelegate MessageBegin;
    internal MessageEndDelegate MessageEnd;

    IntPtr WriteByte;
    IntPtr WriteChar;
    IntPtr WriteShort;
    IntPtr WriteLong;
    IntPtr WriteAngle;
    IntPtr WriteCoord;
    IntPtr WriteString;
    IntPtr WriteEntity;

    IntPtr CVarRegister;
    IntPtr CVarGetFloat;
    internal CVarGetStringDelegate CVarGetString;
    IntPtr CVarSetFloat;
    IntPtr CVarSetString;

    IntPtr AllertMessage;
    IntPtr EngineFprintf;

    IntPtr PvAllocEntPrivateData;
    IntPtr PvEntPrivateData;
    IntPtr FreeEntPrivateData;

    IntPtr SzFromIndex;
    IntPtr AllocString;

    IntPtr GetVarOfEnt;
    IntPtr EntityOfEntOffset;
    IntPtr IndexOfEdict;
    IntPtr PEntityOfEntIndex;
    IntPtr FindEntityByVars;
    IntPtr GetModelPtr;

    IntPtr RegUserMsg;

    IntPtr AnimationAutomove;
    IntPtr GetBonePosition;

    IntPtr FunctionFromName;
    IntPtr NameForFunction;

    IntPtr ClientPrintf;
    IntPtr ServerPrintf;

    IntPtr Cmd_Args;
    IntPtr Cmd_Argv;
    IntPtr Cmd_Argc;

    IntPtr GetAttachment;

    IntPtr CRC32_Init;
    IntPtr CRC32_ProcessBuffer;
    IntPtr CRC32_ProcessByte;
    IntPtr CRC32_Final;

    IntPtr RandomLong;
    IntPtr RandomFloat;

    IntPtr SetView;
    IntPtr Time;
    IntPtr CrosshariAngle;

    IntPtr LoadFileForMe;
    IntPtr FreeFile;

    IntPtr EndSection;
    IntPtr CompareFileTime;

    internal GetGameDir GetGameDir;
    IntPtr Cvar_RegisterVariable;
    IntPtr FadeClientVolume;
    IntPtr SetClientMaxspeed;
    IntPtr CreateFakeClient;
    IntPtr RunPlayerMove;
    IntPtr NumberOfEntities;

    IntPtr GetInfoKeyBuffer;
    IntPtr InfoKeyValue;
    IntPtr SetKeyValue;
    IntPtr SetClientKeyValue;

    IntPtr IsMapValid;
    IntPtr StaticDecal;
    IntPtr PrecacheGeneric;
    IntPtr GetPlayerUserId;
    IntPtr BuildSoundMsg;
    IntPtr IsDedicatedServer;
    IntPtr CVarGetPointer;
    IntPtr GetPlayerWONid;

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
    IntPtr CurrentPlayer;
    IntPtr CanSkipPlayer;
    IntPtr DeltaFindField;
    IntPtr DeltaSetFieldByIndex;
    IntPtr DeltaUnsetFieldByIndex;

    IntPtr SetGroupMask;
    IntPtr CreateInstanceBaseline;
    IntPtr CVarDirectSet;

    IntPtr ForceUnmodified;

    IntPtr GetPlayerStats;

    IntPtr AddServerCommand;

    IntPtr Voice_GetClientListening;
    IntPtr Voice_SetClientListening;

    IntPtr GetPlayerAuthId;

    IntPtr SequenceGet;
    IntPtr SequencePickSentence;

    IntPtr GetFileSize;
    IntPtr GetAproxWavePlayLen;
    IntPtr IsCareerMatch;
    IntPtr GetLocalizedStringLength;
    IntPtr RegisterTutorMessageShown;
    IntPtr GetTimesTutorMessageShown;
    IntPtr ProcessTutorMessageDecayBuffer;
    IntPtr ConstructTutorMessageDecayBuffer;
    IntPtr ResetTutorMessageDecayData;

    IntPtr QueryClientCVarValue;
    IntPtr QueryClientCVarValue2;
  };

}
