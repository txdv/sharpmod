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
using SwearFilter;

namespace SwearFilterConsole
{
  class MainClass
  {
    public static SwearComposite exampleComposite()
    {
      List<ISwear> list;
      list = new List<ISwear>();
      list.Add(new ShortSwearWord("bl"));
      list.Add(new LongSwearWord("nx"));
      list.Add(new LongSwearWord("gaidys"));
      SwearComposite lithuanian = new SwearComposite("Lithuanian", list);

      list = new List<ISwear>();
      SwearComposite internetAds = new SwearComposite("Internet ads", list);

      SwearComposite index = new SwearComposite("Index", new SwearComposite[] { lithuanian });

      return index;

    }
    public static void Main (string[] args)
    {
      SwearChecker sc = new SwearChecker(exampleComposite());
      sc.Check("bl n- b-l-e-t g-a-i-y-s");
    }
  }
}
