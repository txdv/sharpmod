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
using System.Collections.Generic;
using SharpMod.Helper;
using System.IO;

namespace SharpMod.Debug
{
  unsafe public class MemoryTracker
  {
    #region C linked list implementation

    [StructLayout (LayoutKind.Sequential)]
    internal struct linked_list_node {
      public void *ptr;
      public int size;
      public void *caller;
      public linked_list_node *next;

      public override string ToString ()
      {
        return string.Format("0x{0} size:{1} caller:{2}",
                             ((int)(ptr)).ToHex(),
                             size,
                             ((int)(caller)).ToHex());
      }

    }

    internal static linked_list_node ***llptr;

    internal static void linked_list_pointer(linked_list_node ***ll)
    {
      llptr = ll;
    }


    internal static linked_list_node *linked_list_search(void *ptr)
    {
      for (linked_list_node *iter = **llptr; iter->next != null; iter = iter->next)
      {
        if (iter->ptr == ptr) return iter;
      }
      return null;
    }

    internal static int linked_list_count()
    {
      int i = 0;
      for (linked_list_node *iter = **llptr; iter->next != null; iter = iter->next) i++;
      return i;
    }

    public static void linked_list_print()
    {
      for (linked_list_node *iter = **llptr; iter->next != null; iter = iter->next)
      {
        Console.WriteLine ("0x{0} 0x{1} {2}",
                           Convert.ToString((int)iter->ptr, 16),
                           Convert.ToString((int)iter->caller, 16),
                           iter->size);
      }
    }

    #endregion

    /// <summary>
    /// Save and slow function to check a if a pointer is an Entity
    /// </summary>
    /// <param name="ptr">
    /// A void* pointer <see cref="System.Void"/>
    /// </param>
    /// <returns>
    /// A pointer to an Edict or null if not found <see cref="Edict"/>
    /// </returns>
    internal static Edict *IsEntity(void *ptr)
    {
      for (int i = 0; i < Entity.Count; i++) {
        Edict *edict = (Edict *)Entity.GetEntity(i).ToPointer();
        if ((edict != null) && (edict == ptr)) return edict;
      }
      return null;
    }
    public static Entity IsEntity(IntPtr ptr)
    {
      void *edict = (void *)IsEntity(ptr.ToPointer());
      if (edict != null) return new Entity(new IntPtr(edict));
      else return null;
    }

    /// <summary>
    /// Save and slow function to check if a pointer is a PrivateData set
    /// </summary>
    /// <param name="ptr">
    /// A void* pointer <see cref="System.Void"/>
    /// </param>
    /// <returns>
    /// A pointer to an Edict or null if not found <see cref="Edict"/>
    /// </returns>
    internal static Edict *IsPrivateDate(void *ptr)
    {
      for (int i = 0; i < Entity.Count; i++)
      {
        Edict *edict = (Edict *)Entity.GetEntity(i).ToPointer();
        if ((edict != null) && (edict->pvPrivateData != null) && (edict->pvPrivateData == ptr)) return edict;
      }
      return null;
    }
    public static Entity IsPrivateDate(IntPtr ptr)
    {
      void *edict = (void *)IsPrivateDate(ptr.ToPointer());
      if (edict != null) return new Entity(new IntPtr(edict));
      else return null;
    }

    /// <summary>
    /// Save and slow function to check if a pointer is an Entvars struct
    /// </summary>
    /// <param name="ptr">
    /// A void* pointer <see cref="System.Void"/>
    /// </param>
    /// <returns>
    /// A pointer to an Edict or null if not found  <see cref="Edict"/>
    /// </returns>
    internal static Edict *IsEntvars(void *ptr)
    {
      for (int i = 0; i < Entity.Count; i++)
      {
        Edict *edict = (Edict *)Entity.GetEntity(i).ToPointer();
        Entvars *entvars = &(edict->v);
        if ((entvars != null) && (entvars == ptr)) return edict;
      }
      return null;
    }
    public static Entity IsEntvars(IntPtr ptr)
    {
      void *edict = (void *)IsEntvars(ptr.ToPointer());
      if (edict != null) return new Entity(new IntPtr(edict));
      else return null;
    }

    public static void PrintHexCode(TextWriter sw, void *ptr, int size, int tab, int columns, int[] extraSpaces)
    {
      int i = 0;
      while (i < size)
      {
        sw.WriteBunch(tab);
        sw.Write("0x{0}: ", i.ToHex());
        int j = 0;
        while ((i < size) && (j < columns))
        {
          int *offset = (int *)ptr + i;
          sw.Write("{0} ", (*offset).ToHex());
          for (int n = 0; n < extraSpaces.Length; n++) {
            if ((j+1) % extraSpaces[n] == 0) sw.Write(" ");
          }
          i++;
          j++;
        }
        sw.WriteLine();
      }
    }
    public static void PrintHexCode(TextWriter sw, void *ptr, int size, int tab)
    {
      PrintHexCode(sw, ptr, size, tab, 8, new int[] { 2, 4 });
    }

    public static void PrintPrivateInfo(TextWriter sw, Entity entity)
    {
      PrintPrivateInfo(sw, entity, new List<Entity>(), 0);
    }

    public static void PrintPrivateInfo(TextWriter sw, Entity entity, List<Entity> list, int tab)
    {
      foreach (Entity e in list) {
        if (e.entity == entity.entity) return;
      }
      list.Add(entity);
      linked_list_node *private_data = linked_list_search(entity.entity->pvPrivateData);
      if (private_data == null) {
        sw.WriteBunch(tab);
        sw.WriteLine ("The allocation size couldn't be found");
        return;
      }


      sw.WriteBunch(tab);
      sw.WriteLine ("entity pointer:{0}", private_data->ToString());

      PrintHexCode(sw, entity.entity->pvPrivateData, private_data->size, tab);
      sw.WriteLine();
      for (int i = 0; i < private_data->size; i++)
      {
        IntPtr ptr = new IntPtr(*((int *)entity.entity->pvPrivateData + i));
        Entity en;
        if ((en = IsEntity(ptr)) != null) {
          sw.WriteBunch(tab);
          sw.WriteLine ("0x{0}: Entity({1}) ", i.ToHex(), en.Classname);
          PrintPrivateInfo(sw, en, list, tab+1);
        } else if ((en = IsPrivateDate(ptr)) != null) {
          sw.WriteBunch(tab);
          sw.WriteLine("0x{0}: PrivateData, Entity({1})", i.ToHex(), en.Classname);
          PrintPrivateInfo(sw, en, list, tab+1);
        } else if ((en = IsEntvars(ptr)) != null) {
          sw.WriteBunch(tab);
          sw.WriteLine("0x{0}: Entvars, Entity({1})", i.ToHex(), en.Classname);
          PrintPrivateInfo(sw, en, list, tab+1);
        } else {
          linked_list_node *node;
          if ((node = linked_list_search(ptr.ToPointer())) != null) {
            sw.WriteBunch(tab);
            sw.WriteLine ("0x{0}: memory:{0}", i.ToHex(), node->ToString());
          }
        }
      }
    }

    private static string dumpDirectory = @"dump/";
    public static void DumpPrivateInfo(Entity entity)
    {
      #if DEBUG
      MemoryTracker.PrintPrivateInfo(Console.Out, entity);
      #endif

      if (!Directory.Exists(dumpDirectory)) Directory.CreateDirectory(dumpDirectory);

      int i = 0;
      bool done = false;
      do {
        string fn = dumpDirectory + i.ToHex() + ".txt";
        if (!File.Exists(fn)) {
          StreamWriter sw = new StreamWriter(File.OpenWrite(fn));
          MemoryTracker.PrintPrivateInfo(sw, entity);
          sw.Close();
          done = true;
        }
        i++;
      } while (!done);
    }
  }
}
