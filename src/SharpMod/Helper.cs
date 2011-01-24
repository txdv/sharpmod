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
  /// These functions are often found in other scripting programming languages like ruby
  /// </summary>
  public static class HelperExtensions
  {

    #region String array/string extensions

    /// <summary>
    /// Returns the last element in a string array
    /// </summary>
    /// <param name="array">
    /// A string array <see cref="System.String[]"/>
    /// </param>
    /// <returns>
    /// Last element of the array <see cref="System.String"/>
    /// </returns>
    public static string Last(this string[] array)
    {
      if (array.Length == 0) return "";
      return array[array.Length-1];
    }

    public static string First(this string[] array)
    {
      if (array.Length == 0) return string.Empty;
      return array[0];
    }

    public static string[] Split(this string text, char c)
    {
      return text.Split(new char[] { c });
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

    public static string[] Shift(this string[] arr)
    {
      if (arr.Length == 1) return new string[0];
      string[] ret = new string[arr.Length-1];
      for (int i = 1; i < arr.Length; i++) ret[i-1] = arr[i];
      return ret;
    }

    public static string Shift(this string text, char c)
    {
      // TODO: see if the commented code does exactly the same

      //string[] res = text.Split(new char[] { c }, 2);
      //if (res.Length == 2) return res[1];
      //else return string.Empty;

      return text.Split(' ').Shift().Join(' ');
    }

    public static bool Contains(this string text, char c)
    {
      return text.Contains(c.ToString());
    }

    public static string Escape(this char c)
    {
      if (c == ' ') return @" "; // because its < 32
      if (c == '"') return "\\\""; // special escape
      if (c > 32)    return string.Format("{0}", c);
      switch (c)
      {
      case '\a':
        return @"\a";
      case '\b':
        return @"\b";
      case '\n':
        return @"\n";
      case '\v':
        return @"\v";
      case '\r':
        return @"\r";
      case '\f':
        return @"\f";
      case '\t':
        return @"\t";
      default:
        return string.Format(@"\x{0:x}", (int) c);
      }

    }

    public static string Escape(this string text)
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("\"");
      foreach (char c in text) sb.Append(c.Escape());
      sb.Append("\"");
      return sb.ToString();
    }

    #endregion

    public static StringBuilder Append(this StringBuilder stringBuilder, string cmd, params object[] paramlist)
    {
      return stringBuilder.Append(String.Format(cmd, paramlist));
    }

    public static TimeSpan ToTimeSpan(this float time)
    {
      long total = (long)(time * TimeSpan.TicksPerSecond);
      return new TimeSpan(total);
    }

    public static float ToFloat(this TimeSpan timespan)
    {
      return (float)timespan.Ticks / TimeSpan.TicksPerSecond;
    }

  }
}