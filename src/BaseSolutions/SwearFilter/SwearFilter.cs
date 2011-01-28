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
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SwearFilter
{
  #region stringextensions
  
  public static class StringExtensions
  {
    public static string[] Split(this string text, char c)
    {
      return text.Split(new char[] { c });
    }

    public static string Shift(this string text, char c)
    {
      string[] res = text.Split(new char[] { c }, 2);
      if (res.Length == 2) return res[1];
      else return string.Empty;
    }

    public static string Join(this string[] stringarray, char c)
    {
      if (stringarray.Length != 0) {
        StringBuilder sb = new StringBuilder(stringarray[0]);
        if (stringarray.Length > 1) {
          for (int i = 1; i < stringarray.Length; i++) {
            sb.Append(c);
            sb.Append(stringarray[i]);
          }
        }
        return sb.ToString();
      } else {
        return string.Empty;
      }
    }

    public static bool Contains(this string text, char c)
    {
      return text.Contains(c.ToString());
    }
  }
  
  #endregion

  public interface ISwear
  {
    
  }
  
  public class SwearComposite : ISwear, IEnumerable<ISwear>
  {
    private List<ISwear> children = new List<ISwear>();
    public string Name { get; protected set; }
    
    public SwearComposite(string name)
    {
      Name = name;
    }

    public SwearComposite(string name, IEnumerable<ISwear> collection)
      : this(name)
    {
      children.AddRange(collection);
    }
    
    public IEnumerator<ISwear> Enumerator
    {
      get 
      {
        return children.GetEnumerator();
      }
    }
    
    #region IEnumerable<ISwear> implementation
    public IEnumerator<ISwear> GetEnumerator ()
    {
      return children.GetEnumerator();
    }
    
    #endregion
    
    #region IEnumerable implementation
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
    {
      return children.GetEnumerator();
    }
    
    #endregion
  }

  public abstract class Swear : ISwear
  {
  }
  
  public abstract class SwearWord : Swear
  {
    public string Word { get; protected set; }
    public SwearWord(string word)
    {
      Word = word;
    }
  }
  
  public class LongSwearWord : SwearWord
  {
    public LongSwearWord(string word)
      : base(word)
    {
    }
    
  }
  
  public class ShortSwearWord : SwearWord
  {
    public ShortSwearWord(string word)
      : base(word)
    {
    }
  }
  
  public class RegexSwear : Swear
  {
    public Regex Regex { get; protected set; }
    public RegexSwear(Regex regex)
    {
      Regex = regex;
    }
  }
  
  public class SwearChecker
  {
    private char[] ignoreChars = new char[] { ',', '.', '/', '\'', '\"', '|', '[', ']', '\\', '`', '!', '@', '#', '$', '%', '^', '&', '*', '-', '_', '+', '=' };
    private List<LongSwearWord> longSwearWordList = null;
    private List<ShortSwearWord> shortSwearWordList = null;
    private List<RegexSwear> regexSwearList = null;
    
    public SwearChecker(SwearComposite list)
    {
      longSwearWordList = new List<LongSwearWord>();
      shortSwearWordList = new List<ShortSwearWord>();
      regexSwearList = new List<RegexSwear>();
      Add(list);
    }
    protected void Add(ISwear swear)
    {
      if (swear is RegexSwear)          regexSwearList.Add(swear as RegexSwear);
      else if (swear is LongSwearWord)  longSwearWordList.Add(swear as LongSwearWord);
      else if (swear is ShortSwearWord) shortSwearWordList.Add(swear as ShortSwearWord);
      else if (swear is SwearComposite) 
      {
        foreach (ISwear sw in ((SwearComposite)swear))
        {
          Add(sw);
        }
      }
    }
    
    protected string IgnoreChars(string text)
    {
      StringBuilder sb = new StringBuilder(text);
      for (int i = 0; i < sb.Length; i++) {
        foreach (char ignoreChar in ignoreChars) {
          if (ignoreChar == sb[i]) {
            sb.Remove(i, 1);
            i--;
          }
        }
      }
      return sb.ToString();
    }
    
    protected bool CheckLongSwearWords(string text)
    {
      foreach (LongSwearWord lsw in longSwearWordList) {
        if (text.IndexOf(lsw.Word) > -1) return true;
      }
      return false;
    }
    
    protected bool CheckShortSwearWords(string text)
    {
      foreach (string token in text.Split(' ')) {
        string lowercasetoken = token.ToLower();
        foreach (ShortSwearWord ssw in shortSwearWordList) {
          if (ssw.Word.ToLower() == lowercasetoken) return true;
        }
      }
      return false;
    }
    
    
    public bool Check(string text)
    {
      string i = IgnoreChars(text);
      Console.WriteLine(i);
      Console.WriteLine(CheckLongSwearWords(i));
      Console.WriteLine(CheckShortSwearWords(i));
      return false;
    }
  }
  
  public class SwearCheckerReplace : SwearChecker
  {
    public SwearCheckerReplace(SwearComposite list)
      : base(list)
    {
      
    }
  }
}
