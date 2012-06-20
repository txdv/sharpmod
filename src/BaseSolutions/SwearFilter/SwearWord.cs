using System;

namespace SwearFilter
{
	public abstract class SwearWord : Swear
	{
		public string Word { get; protected set; }
		public SwearWord(string word)
		{
			Word = word;
		}
	}
}

