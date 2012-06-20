using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SwearFilter
{
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

	public class SwearCheckerReplace : SwearChecker
	{
		public SwearCheckerReplace(SwearComposite list)
		: base(list)
		{
		}
	}
}
