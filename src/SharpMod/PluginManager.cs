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
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using IronRuby.Builtins;

namespace SharpMod
{

  #region Plugin Code

  /// <summary>
  /// Interface for loading Plugins.
  /// It's recomendded to derive from BasicPlugin.
  /// </summary>
  public interface IPlugin
  {

    string Name { get; }
    string Author { get; }
    string Description { get; }
    Version Version { get; }
    string ShortVersion { get; }

    void Load();
    void Unload();
  }

  public class BasicPlugin : IPlugin
  {

    internal Assembly Image
    {
      get { return Assembly.GetAssembly(this.GetType()); }
    }

    /// <summary>
    /// The name of the plugin
    /// </summary>
    public virtual string Name
    {
      get
      {
        return (Attribute.GetCustomAttribute(Image, typeof(AssemblyProductAttribute)) as AssemblyProductAttribute).Product;
      }
    }
    /// <summary>
    /// The Author of the Plugin
    /// </summary>
    public virtual string Author
    {
      get
      {
        return (Attribute.GetCustomAttribute(Image, typeof(AssemblyCompanyAttribute)) as AssemblyCompanyAttribute).Company;
      }
    }
    /// <summary>
    /// A short description of the plugin, what it does.
    /// </summary>
    public virtual string Description
    {
      get
      {
        return (Attribute.GetCustomAttribute(Image, typeof(AssemblyDescriptionAttribute)) as AssemblyDescriptionAttribute).Description;
      }
    }
    /// <summary>
    /// The full version of a plugin
    /// </summary>
    public virtual Version Version { get { return Assembly.GetAssembly(this.GetType()).GetName().Version; } }
    /// <summary>
    /// A short version of the plugin, major.minor
    /// </summary>
    public virtual string ShortVersion { get { return String.Format("{0}.{1}", Version.Major, Version.Minor); } }

    /// <summary>
    /// Called when the plugin is loaded
    /// </summary>
    public virtual void Load() { }
    /// <summary>
    /// Called when the plugin is unloaded
    /// </summary>
    public virtual void Unload() { }
  }

  #endregion

  #region Base Plugin Manager

  /// <summary>
  /// The plugin manager.
  /// Manages plugins, loads, unloads them
  /// </summary>
  public class PluginManager
  {
    #region Static code

    private static PluginManager pm = null;
    public static PluginManager GetInstance()
    {
      if (pm == null) pm = new PluginManager();
      return pm;
    }

    #endregion

    private static string pluginDirectory = SharpMod.MODDirectory + "plugins/";
    private List<IPlugin> plugins = null;

    private PluginManager()
    {
      plugins = new List<IPlugin>();
    }

    public void LoadPlugins()
    {
      if (Directory.Exists(pluginDirectory))
        foreach (string file in Directory.GetFiles(pluginDirectory, "SharpMod*.dll")) Load(file);
    }

    public bool Load(string path)
    {
      return Load(new FileInfo(path));
    }

    public bool Load(FileInfo fi)
    {
      try
      {
        Assembly asm = Assembly.LoadFile(fi.FullName);
        foreach (Type type in asm.GetTypes())
        {
          if (type.GetInterface("IPlugin") != null)
          {
            IPlugin ip = (IPlugin)Activator.CreateInstance(type);
            //ip.Load();
            //plugins.Add(ip);
            Load(ip);
            return true;
          }
        }
        return false;
      } catch { return false; }
    }

    public void Load(IPlugin plugin)
    {
      plugin.Load();
      plugins.Add(plugin);
    }


    protected int numberFormatCounter = 0;
    protected string NumberFormat(int pluginCount)
    {
      numberFormatCounter++;
      return " [ " + new string(' ', pluginCount.ToString().Length - numberFormatCounter.ToString().Length) + numberFormatCounter + "]";
    }

    public void ShowPlugins()
    {
      int pluginCount = plugins.Count;
      TextTools.TextTable tt = new TextTools.TextTable(new string[] { "# ", "name", "author", "version" });
      tt.Header[0].Alignment = TextTools.Align.Right;

      numberFormatCounter = 0;
      var data = from h in plugins
                 select new string [] { NumberFormat(pluginCount),  h.Name, h.Author, h.Version.ToString() };

      Console.WriteLine ("Currently loaded plugins:");
      tt.Render(data.ToArray(), Server.Print, Console.WindowWidth);
      Console.WriteLine ("{0} Plugins", plugins.Count);
    }
  }

  #endregion

  #region IronRuby

  // Kinda a hack, isn't it? :D
  public class RubyPlugin : BasicPlugin
  {
    private string name, author, description;
    private Version version;
    public delegate void UnloadDelegate();
    private UnloadDelegate unload;

    public RubyPlugin(string name, string author, string description, Version version, UnloadDelegate unload)
    {
      this.name = name;
      this.author = author;
      this.description = description;
      this.version = version;
      this.unload = unload;
    }

    public override void Unload() { unload(); }

    public override string Name { get { return name; } }
    public override string Author { get { return author; } }
    public override string Description { get { return description; } }
    public override Version Version { get { return version; } }


  }

  #endregion

  public class RubyPluginManager
  {

    #region Static code

    private static RubyPluginManager pm = null;
    public static RubyPluginManager GetInstance()
    {
      if (pm == null) pm = new RubyPluginManager();
      return pm;
    }

    #endregion

    private static string pluginDirectory = SharpMod.MODDirectory + "plugins/ruby/";
    private ScriptEngine engine;

    private RubyPluginManager()
    {
      Assembly.LoadFile(SharpMod.MODDirectory + "IronRuby.dll");
      Assembly.LoadFile(SharpMod.MODDirectory + "IronRuby.Libraries.dll");
      engine = IronRuby.Ruby.CreateEngine();

      // Load the current assembly, so we can access all the SharpMod goodies
      engine.Runtime.LoadAssembly(Assembly.LoadFile(Assembly.GetExecutingAssembly().Location));
    }


    public void Load(string filename)
    {

      ScriptSource script = engine.CreateScriptSourceFromFile(filename);
      ScriptScope scope = engine.CreateScope();
      object o = script.Execute(scope);

      if (o is IPlugin) PluginManager.GetInstance().Load(o as IPlugin);
      else {

        string name, author, description;
        Version version;
        RubyPlugin.UnloadDelegate unload;

        if (!scope.TryGetVariable<string> ("name",        out name))        name        = "unknown";
        if (!scope.TryGetVariable<string> ("author",      out author))      author      = "unknown";
        if (!scope.TryGetVariable<string> ("description", out description)) description = "no description";
        if (!scope.TryGetVariable<Version>("version",     out version))     version     = new Version(0, 0);

        // Load the methods
        if (!scope.TryGetVariable<RubyPlugin.UnloadDelegate>("unload", out unload)) unload = null;

        PluginManager.GetInstance().Load(new RubyPlugin(name, author, description, version, unload));

      }
    }

    public void LoadPlugins()
    {
      foreach (string file in Directory.GetFiles(pluginDirectory, "*.rb")) Load(file);
    }
  }
}