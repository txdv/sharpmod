using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

namespace SharpMod
{
	/// <summary>
	/// Interface for loading Plugins.
	/// It's recomendded to derive from BasicPlugin.
	/// </summary>
	public interface IPlugin
	{

		string Name         { get; }
		string Author       { get; }
		string Description  { get; }
		Version Version     { get; }
		string ShortVersion { get; }

		void Load();
		void Unload();
	}

	public class BasicPlugin : IPlugin
	{

		internal Assembly Image {
			get {
				return Assembly.GetAssembly(this.GetType());
			}
		}

		/// <summary>
		/// The name of the plugin
		/// </summary>
		public virtual string Name {
			get {
				return (Attribute.GetCustomAttribute(Image, typeof(AssemblyProductAttribute)) as AssemblyProductAttribute).Product;
			}
		}

		/// <summary>
		/// The Author of the Plugin
		/// </summary>
		public virtual string Author {
			get {
				return (Attribute.GetCustomAttribute(Image, typeof(AssemblyCompanyAttribute)) as AssemblyCompanyAttribute).Company;
			}
		}

		/// <summary>
		/// A short description of the plugin, what it does.
		/// </summary>
		public virtual string Description {
			get {
				return (Attribute.GetCustomAttribute(Image, typeof(AssemblyDescriptionAttribute)) as AssemblyDescriptionAttribute).Description;
			}
		}

		/// <summary>
		/// The full version of a plugin
		/// </summary>
		public virtual Version Version {
			get {
				return Assembly.GetAssembly(this.GetType()).GetName().Version;
			}
		}

		/// <summary>
		/// A short version of the plugin, major.minor
		/// </summary>
		public virtual string ShortVersion {
			get {
				return String.Format("{0}.{1}", Version.Major, Version.Minor);
			}
		}

		/// <summary>
		/// Called when the plugin is loaded
		/// </summary>
		public virtual void Load()
		{
		}

		/// <summary>
		/// Called when the plugin is unloaded
		/// </summary>
		public virtual void Unload()
		{
		}
	}

	/// <summary>
	/// The plugin manager.
	/// Manages plugins, loads, unloads them
	/// </summary>
	public class PluginManager
	{
		private static string pluginDirectory = Path.Combine(Server.ModDirectory, "plugins");
		private static List<IPlugin> plugins = new List<IPlugin>();

		public static void LoadPlugins()
		{
			if (Directory.Exists(pluginDirectory)) {
			foreach (string file in Directory.GetFiles(pluginDirectory, "SharpMod*.dll")) {
					Load(file);
				}
			}
		}

		public static bool Load(string path)
		{
			return Load(new FileInfo(path));
		}

		public static bool Load(FileInfo fi)
		{
			try {
				Assembly asm = Assembly.LoadFile(fi.FullName);
				foreach (Type type in asm.GetTypes()) {
					if (type.GetInterface("IPlugin") != null) {
						IPlugin ip = (IPlugin)Activator.CreateInstance(type);
						ip.Load();
						plugins.Add(ip);
						return true;
					}
				}
				return false;
			} catch {
				return false;
			}
		}


		protected static int numberFormatCounter = 0;
		protected static string NumberFormat(int pluginCount)
		{
			numberFormatCounter++;
			return " [ " + new string(' ', pluginCount.ToString().Length - numberFormatCounter.ToString().Length) + numberFormatCounter + "]";
		}

		public static void ShowPlugins()
		{
			int pluginCount = plugins.Count;
			TextTools.TextTable tt = new TextTools.TextTable(new string[] { "# ", "name", "author", "version" });
			tt.Header[0].Alignment = TextTools.Align.Right;

			numberFormatCounter = 0;
			var data = from h in plugins
			select new string [] { NumberFormat(pluginCount),  h.Name, h.Author, h.Version.ToString() };

			Server.Print("Currently loaded plugins:\n");
			tt.Render(data.ToArray(), Server.Print, Console.WindowWidth);
			Server.Print("{0} Plugins\n", plugins.Count);
		}
	}
}
