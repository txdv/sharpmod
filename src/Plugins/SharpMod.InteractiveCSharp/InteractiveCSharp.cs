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
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Mono.CSharp;
using SharpMod;

namespace InteractiveCSharp
{

  public class Plugin : BasicPlugin
  {

    #region Miguels code from the interactive csharp evaluator in mono src code

    private static string EscapeString (string s)
    {
      return s.Replace ("\"", "\\\"");
    }

    private static void EscapeChar (TextWriter output, char c)
    {
      if (c == '\''){
        output.Write ("'\\''");
        return;
      }
      if (c > 32){
        output.Write ("'{0}'", c);
        return;
      }
      switch (c){
      case '\a':
        output.Write ("'\\a'");
        break;

      case '\b':
        output.Write ("'\\b'");
        break;

      case '\n':
        output.Write ("'\\n'");
        break;

      case '\v':
        output.Write ("'\\v'");
        break;

      case '\r':
        output.Write ("'\\r'");
        break;

      case '\f':
        output.Write ("'\\f'");
        break;

      case '\t':
        output.Write ("'\\t");
        break;

      default:
        output.Write ("'\\x{0:x}", (int) c);
        break;
      }
    }

    private static void p (TextWriter output, string s)
    {
      output.Write (s);
    }

    internal static void PrettyPrint (TextWriter output, object result)
    {
      if (result == null){
        p (output, "null");
        return;
      }

      if (result is Array){
        Array a = (Array) result;

        p (output, "{ ");
        int top = a.GetUpperBound (0);
        for (int i = a.GetLowerBound (0); i <= top; i++){
          PrettyPrint (output, a.GetValue (i));
          if (i != top)
            p (output, ", ");
        }
        p (output, " }");
      } else if (result is bool){
        if ((bool) result)
          p (output, "true");
        else
          p (output, "false");
      } else if (result is string){
        p (output, String.Format ("\"{0}\"", EscapeString ((string)result)));
      } else if (result is IDictionary){
        IDictionary dict = (IDictionary) result;
        int top = dict.Count, count = 0;
    
        p (output, "{");
        foreach (DictionaryEntry entry in dict){
          count++;
          p (output, "{ ");
          PrettyPrint (output, entry.Key);
          p (output, ", ");
          PrettyPrint (output, entry.Value);
          if (count != top)
            p (output, " }, ");
          else
            p (output, " }");
        }
        p (output, "}");
      } else if (result is IEnumerable) {
        int i = 0;
        p (output, "{ ");
        foreach (object item in (IEnumerable) result) {
          if (i++ != 0)
            p (output, ", ");

          PrettyPrint (output, item);
        }
        p (output, " }");
      } else if (result is char) {
        EscapeChar (output, (char) result);
      } else {
        p (output, result.ToString ());
      }
    }

    #endregion

    /// <summary>
    /// This function evaluates a piece of code and prints the output linewise to the chat of the player
    /// </summary>
    /// <param name="p">
    /// A player instance <see cref="Player"/>
    /// </param>
    /// <param name="code">
    /// A command <see cref="System.String"/>
    /// </param>
    protected virtual void EvaluateGoldSrc(Player p, string code)
    {
      bool result_set;
      object result;

      try
      {
        Evaluator.Evaluate(code, out result, out result_set);
        if (result_set)
        {
          System.IO.TextWriter tw = new System.IO.StringWriter();
          PrettyPrint(tw, result);
          p.ClientPrintEachLine(tw.ToString());
        }
      }
      catch (Exception e)
      {
        p.ClientPrintEachLine(e.ToString());
      }
    }

    /// <summary>
    /// Executes a piece of code and returns the output
    /// </summary>
    /// <param name="input">
    /// C# code <see cref="System.String"/>
    /// </param>
    /// <returns>
    /// Output of compiler as text <see cref="System.String"/>
    /// </returns>
    protected virtual string Evaluate (string input)
    {
      bool result_set;
      object result;

      try {
        input = Evaluator.Evaluate (input, out result, out result_set);

        if (result_set){
          PrettyPrint (Console.Out, result);
          Console.WriteLine ();
        }
      } catch (Exception e){
        Console.WriteLine (e);
        return null;
      }

      return input;
    }

    public override void Load ()
    {
      Player.RegisterCommand("say /exec", HandleSayExec);
      Mono.CSharp.Evaluator.Init(new string[0]);
      // Load assemblies which are referenced in the current running executable
      foreach (System.Reflection.Assembly a in AppDomain.CurrentDomain.GetAssemblies())
      {
        Mono.CSharp.Evaluator.ReferenceAssembly(a);
      }
      Evaluate("using System; using SharpMod;");
    }


    void HandleSayExec(Player player, Command command)
    {
      // Ommit /exec and execute the following text
      string exec_string = command.Arguments[1].Shift(' ') + ";";
      EvaluateGoldSrc(player, exec_string);
    }
  }
}
