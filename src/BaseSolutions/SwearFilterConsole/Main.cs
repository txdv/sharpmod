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
