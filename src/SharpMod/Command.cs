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
using System.Collections.Generic;
using SharpMod.MetaMod;

namespace SharpMod
{

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
      if (stringarray.Length != 0)
      {
        StringBuilder sb = new StringBuilder(stringarray[0]);
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

    public static bool Contains(this string text, char c)
    {
      return text.Contains(c.ToString());
    }
  }

  /// <summary>
  /// A class for handling commands send by the players
  /// </summary>
  public class Command
  {
    internal static bool overrideArguments = false;
    internal static Command instance = null;

    /// <summary>
    /// Calls the GoldSrc engine (Cmd_Argc, Cmd_Argv) and returns the values as a C# array
    /// </summary>
    public static string[] EngineArguments
    {
     get
      {
        string[] args = new string[MetaModEngine.engineFunctions.Cmd_Argc()];
        for (int i = 0; i < args.Length; i++)
        {
          args[i] = Mono.Unix.UnixMarshal.PtrToString(MetaModEngine.engineFunctions.Cmd_Argv(i));
        }
        return args;
      }
    }

    /// <summary>
    /// Creates a command class instance using the gameengine command calls
    /// </summary>
    /// <returns>
    /// A command class instance with the original gameengine commadn values <see cref="Command"/>
    /// </returns>
    public static Command FromGameEngine()
    {
      return new Command(Command.EngineArguments);
    }

    /// <summary>
    /// The arguments of the specific Command instance
    /// </summary>
    public string[] Arguments { set; get; }


    private string Escape(string text)
    {
      if (text.Contains(" "))
      {
        return String.Format("\"{0}\"", text);
      }
      else
      {
        return text;
      }
    }

    /// <summary>
    /// Constructor of the class
    /// </summary>
    /// <param name="arguments">
    /// A C# array with the arguments <see cref="System.String[]"/>
    /// </param>
    public Command(string[] arguments)
    {
      Arguments = arguments;
    }

    /// <summary>
    /// Emulates a player calling a command.
    /// </summary>
    /// <param name="p">
    /// Instance of the player <see cref="Player"/>
    /// </param>
    public void FakeCall(Player p)
    {
      overrideArguments = true;
      instance = this;
      MetaModEngine.dllapiFunctions.ClientCommand(p.Pointer);
      instance = null;
      overrideArguments = false;
    }

    /// <summary>
    /// A string representation of the class.
    /// Returns a online command string, which could be written into the console.
    /// </summary>
    /// <returns>
    /// <see cref="System.String"/>
    /// </returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder(Arguments[0]);
      for (int i = 1; i < Arguments.Length; i++)
        // sb.Append(String.Format(" {0}", Escape(Arguments[i])));
        sb.Append(String.Format(" {0}", Arguments[i]));
      return sb.ToString();
    }

  }

  public delegate void ClientCommandDelegate(Player player, Command cmd);

  /// <summary>
  /// A class for managing all the command associations
  /// </summary>
  internal class CommandManager
  {
    private Dictionary<string, ClientCommandDelegate>events;

    private static CommandManager client = null;
      private CommandManager()
    {
      events = new Dictionary<string, ClientCommandDelegate>();
    }

    /// <summary>
    /// Singleton, the only way to get one instance
    /// </summary>
    public static CommandManager Client
    {
      get
      {
        if (client == null) client = new CommandManager();
        return client;
      }
    }

    /// <summary>
    /// Registers a ClientCommand handler to a specific command
    /// </summary>
    /// <param name="str">
    /// The beginning of the string to witch it has to match (for example "say /death") <see cref="System.String"/>
    /// </param>
    /// <param name="handler">
    /// A handler which to invoke if command occures <see cref="ClientCommandDelegate"/>
    /// </param>
    public void Register(string str, ClientCommandDelegate handler)
    {
      if (events.ContainsKey(str))
      {
        ClientCommandDelegate cmddelegate = events[str];
        if (cmddelegate != null) cmddelegate += handler;
        else cmddelegate = handler;
      }
      else events.Add(str, handler);
    }

    /// <summary>
    /// This function is used internally.
    /// It executes all needed handlers when a specific command occures.
    /// </summary>
    /// <param name="str">
    /// A <see cref="System.String"/>
    /// </param>
    /// <param name="cmd">
    /// A <see cref="Command"/>
    /// </param>
    internal void Execute(Player player, Command cmd)
    {
      foreach (KeyValuePair<string, ClientCommandDelegate> keyval in events)
      {
        if (cmd.ToString().StartsWith(keyval.Key))
        {
          if (keyval.Value != null) keyval.Value(player, cmd);
        }
      }
    }
  }

  /// <summary>
  /// A special class for handling SayCommand (say, say_team)
  /// </summary>
  public abstract class TextCommand : Command
  {
    public TextCommand(string[] arguments)
      : base(arguments)
    {
    }

    /// <summary>
    /// A special Field for getting and setting the text
    /// </summary>
    public string Text
    {
      get { return Arguments[1]; }
      set { Arguments[1] = value; }
    }
  }

  /// <summary>
  /// The say command
  /// </summary>
  public class SayCommand : TextCommand
  {
    public SayCommand(string[] arguments)
      : base(arguments)
    {
      if (arguments[0] != "say") throw new Exception();
    }
  }

  public class SayTeamCommand : TextCommand
  {
    public SayTeamCommand(string[] arguments)
      : base(arguments)
    {
      if (arguments[0] != "say") throw new Exception();
    }
  }

}
