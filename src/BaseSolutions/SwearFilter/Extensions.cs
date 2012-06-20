using System;
using System.Text;

namespace SwearFilter
{
	public static class StringExtensions
	{
		public static string[] Split(this string text, char c)
		{
			return text.Split(new char[] { c });
		}

		public static string Shift(this string text, char c)
		{
			string[] res = text.Split(new char[] { c }, 2);
			if (res.Length == 2) {
				return res[1];
			} else {
				return string.Empty;
			}
		}

		public static string Join(this string[] stringarray, char c)
		{
			if (stringarray.Length != 0) {
				StringBuilder sb = new StringBuilder(stringarray[0]);
				if (stringarray.Length > 1) {
					for (int i = 1; i < stringarray.Length; i++) {
						sb.Append(c);
						sb.Append(stringarray[i]);
					}
				}
				return sb.ToString();
			} else {
				return string.Empty;
			}
		}

		public static bool Contains(this string text, char c)
		{
			return text.Contains(c.ToString());
		}
	}
}

