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
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpMod.Helper;
using System.IO;

namespace SharpMod.Debug
{
  unsafe public class MemoryTracker
  {
    [StructLayout (LayoutKind.Sequential)]
    public struct linked_list_node {
      public void *ptr;
      public int size;
      public void *caller;
      public linked_list_node *next;
    }

    private static linked_list_node ***llptr;

    private static void linked_list_pointer(linked_list_node ***ll)
    {
      llptr = ll;
    }

    public static Entity IsEntity(void *ptr)
    {
      for (int i = 0; i < Entity.Count; i++) {
        Entity e = new Entity(Entity.GetEntity(i));
        Edict *edict = e.entity;
        if (edict != null) {
          if ((void *)edict == ptr) return e;
        }
      }
      return null;
    }

    public static Entity IsPrivateDate(void *ptr)
    {
      for (int i = 0; i < Entity.Count; i++)
      {

        Entity e = new Entity(Entity.GetEntity(i));
        Edict *edict = e.entity;
        if (edict != null) {
          if (edict->pvPrivateData != null) {
            if (ptr == edict->pvPrivateData) {
              return e;
            }
          }
        }
      }
      return null;
    }

    public static Entity IsEntvars(void *ptr)
    {
      for (int i = 0; i < Entity.Count; i++)
      {
        Entity e = new Entity(Entity.GetEntity(i));

        Entvars *entvars = &(e.entity->v);
        if (entvars != null) {
          if (entvars == ptr) return e;
        }
      }
      return null;
    }

    public static bool IsEntity2(void *ptr)
    {
      for (int i = 0; i < Entity.Count; i++)
      {

        Entity e = new Entity(Entity.GetEntity(i));
        Edict *edict = e.entity;
        if ((int)edict != 0) {
          if ((void *)edict == ptr) {
            Console.WriteLine ("its a pointer to an entity");

          }
          if ((int)edict->pvPrivateData != 0) {
            if (ptr == edict->pvPrivateData) {
              if ((void *)edict->pvPrivateData == ptr) {
                Console.WriteLine ("its a pointer to private data");
              }
            }
          }
        }
      }
      return false;
    }

    public static void PrintPrivateData(Entity entity, TextWriter sw)
    {

      linked_list_node *private_data = GetListElement(entity.entity->pvPrivateData);
      if (private_data == null) {
        sw.WriteLine ("Sadly it I haven't got in my lsit");
        return;
      }
      sw.WriteLine ("0x{0} 0x{1} {2}",
                         Convert.ToString((int)private_data->ptr, 16),
                         Convert.ToString((int)private_data->caller, 16),
                         private_data->size);

      int i = 0;
      for (; i < private_data->size; )
      {
        sw.Write ("0x{0}: ", i.ToHex());
        int j = 0;
        while ((i < private_data->size) && (j < 4))
        {
          int *ptr = (int *)entity.entity->pvPrivateData + i;
          sw.Write("{0} ", Convert.ToString(*ptr, 16).PadLeft(8, '0'));
          i++;
          j++;
        }
        sw.WriteLine ();
      }

      Entity en;
      for (i = 0; i < private_data->size; i++)
      {
        int *ptr = (int *)entity.entity->pvPrivateData + i;
        if ((en = IsEntity((void *)(*ptr))) != null) {
          sw.WriteLine ("0x{0}: Entity({1}) ", i.ToHex(), en.Classname);
        }
        if ((en = IsPrivateDate((void *)(*ptr))) != null) {
          sw.WriteLine("0x{0}: PrivateData, Entity({1})", i.ToHex(), en.Classname);
        }
        if ((en = IsEntvars((void *)(*ptr))) != null) {
          Entvars *vars = (Entvars *)*ptr;
          sw.WriteLine("0x{0}: Entvars, Entity({1})", i.ToHex(), en.Classname);
        }
      }
    }

    public static linked_list_node *GetListElement(void *ptr)
    {
      for (linked_list_node *iter = **llptr; iter->next != null; iter = iter->next)
      {
        if (iter->ptr == ptr) return iter;
      }
      return null;
    }

    public static void Print()
    {
      for (linked_list_node *iter = **llptr; iter->next != null; iter = iter->next)
      {
        Console.WriteLine ("0x{0} 0x{1} {2}",
                           Convert.ToString((int)iter->ptr, 16),
                           Convert.ToString((int)iter->caller, 16),
                           iter->size);
      }
    }
  }
}
