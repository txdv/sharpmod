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
using System.Text;

namespace SharpMod.Helper
{
  /// <summary>
  /// These are some extensions which let the code look nice.
  /// </summary>
  public static class HelperExtensions
  {
    public static string Last(this string[] array)
    {
      if (array.Length == 0) return "";
      return array[array.Length-1];
    }

    public static string[] Split(this string text, char c)
    {
      return text.Split(new char[] { c });
    }

    public static StringBuilder Append(this StringBuilder stringBuilder, string cmd, params object[] paramlist)
    {
      return stringBuilder.Append(String.Format(cmd, paramlist));
    }

    public static string Join(this string[] stringarray, char c)
    {
      if (stringarray.Length != 0)
      {
        System.Text.StringBuilder sb = new System.Text.StringBuilder(stringarray[0]);
        if (stringarray.Length > 1)
        {
          for (int i = 1; i < stringarray.Length; i++)
          {
            sb.Append(c);
            sb.Append(stringarray[i]);
          }
        }
        return sb.ToString();
      }
      else
      {
        return string.Empty;
      }
    }
  }
}