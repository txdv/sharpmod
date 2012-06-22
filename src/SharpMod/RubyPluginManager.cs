using System;
using System.Reflection;
using System.IO;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using IronRuby.Builtins;

namespace SharpMod
{
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

	public class RubyPluginManager
	{
		static string pluginDirectory = Path.Combine(Server.ModDirectory, "plugins", "ruby");
		static private ScriptEngine engine;

		public static void LoadPlugins()
		{
			Assembly.LoadFile(Path.Combine(Server.ModDirectory, "IronRuby.dll"));
			Assembly.LoadFile(Path.Combine(Server.ModDirectory, "IronRuby.Libraries.dll"));
			engine = IronRuby.Ruby.CreateEngine();

			engine.Runtime.LoadAssembly(Assembly.LoadFile(Assembly.GetExecutingAssembly().Location));

			if (Directory.Exists(pluginDirectory)) {
				foreach (string file in Directory.GetFiles(pluginDirectory, "*.rb")) {
					Load(file);
				}
			}
		}

		public static void Load(string filename)
		{
			ScriptSource script = engine.CreateScriptSourceFromFile(filename);
			ScriptScope scope = engine.CreateScope();

			object o = script.Execute(scope);

			if (o is IPlugin) {
				PluginManager.Load(o as IPlugin);
			} else {
				string name, author, description;
				Version version;

				if (!scope.TryGetVariable<string> ("name",        out name))        name        = "unknown";
				if (!scope.TryGetVariable<string> ("author",      out author))      author      = "unknown";
				if (!scope.TryGetVariable<string> ("description", out description)) description = "no description";
				if (!scope.TryGetVariable<Version>("version",     out version))     version     = new Version(0, 0);

				RubyPlugin.UnloadDelegate unload;
				if (!scope.TryGetVariable<RubyPlugin.UnloadDelegate>("unload", out unload)) {
					unload = null;
				}

				PluginManager.Load(new RubyPlugin(name, author, description, version, unload));
			}
		}
	}
}

