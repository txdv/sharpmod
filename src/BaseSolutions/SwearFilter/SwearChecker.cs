using System;
using System.Text;
using System.Collections.Generic;

namespace SwearFilter
{
	public class SwearChecker
	{
		private char[] ignoreChars = new char[] { ',', '.', '/', '\'', '\"', '|', '[', ']', '\\', '`', '!', '@', '#', '$', '%', '^', '&', '*', '-', '_', '+', '=' };
		private List<LongSwearWord> longSwearWordList = null;
		private List<ShortSwearWord> shortSwearWordList = null;
		private List<RegexSwear> regexSwearList = null;

		public SwearChecker(SwearComposite list)
		{
			longSwearWordList = new List<LongSwearWord>();
			shortSwearWordList = new List<ShortSwearWord>();
			regexSwearList = new List<RegexSwear>();
			Add(list);
		}

		protected void Add(ISwear swear)
		{
			if      (swear is RegexSwear)     regexSwearList.Add(swear as RegexSwear);
			else if (swear is LongSwearWord)  longSwearWordList.Add(swear as LongSwearWord);
			else if (swear is ShortSwearWord) shortSwearWordList.Add(swear as ShortSwearWord);
			else if (swear is SwearComposite) {
				foreach (ISwear sw in ((SwearComposite)swear)) {
					Add(sw);
				}
			}
		}

		protected string IgnoreChars(string text)
		{
			StringBuilder sb = new StringBuilder(text);
			for (int i = 0; i < sb.Length; i++) {
				foreach (char ignoreChar in ignoreChars) {
					if (ignoreChar == sb[i]) {
						sb.Remove(i, 1);
						i--;
					}
				}
			}
			return sb.ToString();
		}

		protected bool CheckLongSwearWords(string text)
		{
			foreach (LongSwearWord lsw in longSwearWordList) {
				if (text.IndexOf(lsw.Word) > -1) {
					return true;
				}
			}
			return false;
		}

		protected bool CheckShortSwearWords(string text)
		{
			foreach (string token in text.Split(' ')) {
				string lowercasetoken = token.ToLower();
				foreach (ShortSwearWord ssw in shortSwearWordList) {
					if (ssw.Word.ToLower() == lowercasetoken) {
						return true;
					}
				}
			}
			return false;
		}


		public bool Check(string text)
		{
			string i = IgnoreChars(text);
			Console.WriteLine(i);
			Console.WriteLine(CheckLongSwearWords(i));
			Console.WriteLine(CheckShortSwearWords(i));
			return false;
		}
	}
}

