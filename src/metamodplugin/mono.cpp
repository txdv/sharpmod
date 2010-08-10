/*
    This file is part of sharpmod.
    sharpmod is a metamod plugin which enables you to write plugins
    for Valve GoldSrc using .NET programms.

    Copyright (C) 2010  Andrius Bentkus

    csharpmod is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    csharpmod is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with csharpmod.  If not, see <http://www.gnu.org/licenses/>.
*/

#define __DEBUG__

#include <extdll.h>
#include <sdk_util.h>
#include <meta_api.h>
#include <h_export.h>
#include <enginecallback.h>

#include <mono/jit/jit.h>
#include <mono/metadata/object.h>
#include <mono/metadata/environment.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/debug-helpers.h>

#include "mono.h"

enginefuncs_t g_engfuncs;
globalvars_t  *gpGlobals;

MonoDomain *domain;
MonoAssembly *assembly;
MonoImage *image;
MonoClass *klass;
MonoClass *engine_interface;

char *file = "cstrike/addons/sharpmod/sharpmod.dll";

meta_globals_t *gpMetaGlobals;    // metamod globals
gamedll_funcs_t *gpGamedllFuncs;  // gameDLL function tables
mutil_funcs_t *gpMetaUtilFuncs;   // metamod utility functions

plugin_info_t Plugin_info = {
  META_INTERFACE_VERSION, // ifvers
  "sharpmod",// name
  "0.1",  // version
  "2008/01/01", // date
  "Andrius Bentkus <Andrius.Bentkus@rwth-aaachen.de>",  // author
  "http://www.sharpmod.org/", // url
  "CSHARPMOD",// logtag, all caps please
  PT_ANYTIME, // (when) loadable
  PT_ANYPAUSE,  // (when) unloadable
};

MonoMethod *search_method(MonoClass *klass, char *methodname);

MonoMethod *handlerGiveFnptrsToDll;
MonoMethod *handlerMeta_Query; 
MonoMethod *handlerMeta_Attach; 
MonoMethod *handlerGetEntityAPI2;

void WINAPI GiveFnptrsToDll(enginefuncs_t* pengfuncsFromEngine, globalvars_t *pGlobals)
{

#ifdef __DEBUG__
  printf(" -- C: GiveFnptrsToDll (%u)\n", sizeof(enginefuncs_t));
#endif

  domain = mono_jit_init(file);
  assembly = mono_domain_assembly_open(domain, file);
  if (!assembly)
  {
    // somehow we need to tell the plugin to stop now
    //LOG_CONSOLE(PLID, "Cant find the .NET assembly file in directory");
    printf("Cant find the .NET assembly file in directory");
    //return FALSE;
  }
  image = mono_assembly_get_image(assembly);
  MonoClass *metamod_class = mono_class_from_name(image,"SharpMod.MetaMod", "MetaModEngine");

  handlerGiveFnptrsToDll = search_method(metamod_class, "handlerGiveFnptrsToDll");
  handlerMeta_Attach     = search_method(metamod_class, "handlerMeta_Attach"    );
  handlerMeta_Query      = search_method(metamod_class, "handlerMeta_Query"     );
  handlerGetEntityAPI2   = search_method(metamod_class, "handlerGetEntityAPI2"  );

  void *args[2];
  args[0] = &pengfuncsFromEngine;
  args[1] = pGlobals;
  printf("pGlobals pointer: %u\n", pGlobals);
  mono_runtime_invoke(handlerGiveFnptrsToDll, NULL, args, NULL);
}

C_DLLEXPORT int Meta_Query(char *ifvers, plugin_info_t **pPlugInfo, mutil_funcs_t *pMetaUtilFuncs)
{
#ifdef __DEBUG__
  printf(" -- C: Meta_Query\n");
#endif
  void *args[3];
  args[0] = ifvers;
  args[1] = &pPlugInfo;
  args[2] = &pMetaUtilFuncs;
  *pPlugInfo=&Plugin_info;
  mono_runtime_invoke(handlerMeta_Query, NULL, args, NULL);
  return(TRUE);
}


C_DLLEXPORT int GetEntityAPI2(DLL_FUNCTIONS *pFunctionTable, int *interfaceVersion)
{
#ifdef __DEBUG__
  printf(" -- C: GetEntityAPI2\n");
#endif
  //printf("%u\n",sizeof(DLL_FUNCTIONS));
  /*
  if(!pFunctionTable) {
    UTIL_LogPrintf("GetEntityAPI2 called with null pFunctionTable");
    return(FALSE);
  }
  else if(*interfaceVersion != INTERFACE_VERSION) {
    UTIL_LogPrintf("GetEntityAPI2 version mismatch; requested=%d ours=%d", *interfaceVersion, INTERFACE_VERSION);
    // Tell metamod what version we had, so it can figure out who is out of date.
    *interfaceVersion = INTERFACE_VERSION;
    return(FALSE);
  }
  memcpy(pFunctionTable, &gFunctionTable, sizeof(DLL_FUNCTIONS));
  */
  void *args[2];
  args[0] = &pFunctionTable;
  args[1] = &interfaceVersion;
  mono_runtime_invoke(handlerGetEntityAPI2, NULL, args, NULL);
  return(TRUE);
}


// Metamod attaching plugin to the server.
//  now       (given) current phase, ie during map, during changelevel, or at startup
//  pFunctionTable  (requested) table of function tables this plugin catches
//  pMGlobals   (given) global vars from metamod
//  pGamedllFuncs (given) copy of function tables from game dll
C_DLLEXPORT int Meta_Attach(PLUG_LOADTIME now, META_FUNCTIONS *pFunctionTable, 
  meta_globals_t *pMetaGlobals, gamedll_funcs_t *pGamedllFuncs)
{
#ifdef __DEBUG__
  printf(" -- C: Meta_Attach\n");
#endif
/*
  printf(" -- Meta_Attach\n");
  if(!pMGlobals) {
    LOG_ERROR(PLID, "Meta_Attach called with null pMGlobals");
    return(FALSE);
  }
  gpMetaGlobals=pMGlobals;
  if(!pFunctionTable) {
    LOG_ERROR(PLID, "Meta_Attach called with null pFunctionTable");
    return(FALSE);
  }
  memcpy(pFunctionTable, &gMetaFunctionTable, sizeof(META_FUNCTIONS));
  gpGamedllFuncs=pGamedllFuncs;
*/
  gpMetaGlobals=pMetaGlobals;
  void *args[4];
  args[0] = &now;
  args[1] = pFunctionTable;
  args[2] = pMetaGlobals;
  args[3] = &(pGamedllFuncs->dllapi_table);

  mono_runtime_invoke(handlerMeta_Attach, NULL, args, NULL);

  return(TRUE);
}

MonoMethod *search_method(MonoClass *klass, char *methodname)
{
  MonoMethod *m = NULL;
  gpointer iter = NULL;
  while ((m = mono_class_get_methods(klass, &iter)))
  {
    if (strcmp(mono_method_get_name(m), methodname) == 0)
    {
      return m;
    }
  }
  return NULL;
}

C_DLLEXPORT int Meta_Detach(PLUG_LOADTIME /* now */, 
		PL_UNLOAD_REASON /* reason */) 
{
  mono_jit_cleanup(domain);
  return(TRUE);
}
