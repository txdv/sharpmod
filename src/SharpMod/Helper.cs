using System;
using System.Text;
using System.Xml;

namespace SharpMod.Helper
{
	/// <summary>
	/// These are some extensions which let the code look nice.
	/// These functions are often found in other scripting programming languages like ruby
	/// </summary>
	public static class HelperExtensions
	{
		#region String array/string extensions

		/// <summary>
		/// Returns the last element in a string array
		/// </summary>
		/// <param name="array">
		/// A string array <see cref="System.String[]"/>
		/// </param>
		/// <returns>
		/// Last element of the array <see cref="System.String"/>
		/// </returns>
		public static string Last(this string[] array)
		{
			if (array.Length == 0) {
				return "";
			}
			return array[array.Length-1];
		}

		public static string First(this string[] array)
		{
			if (array.Length == 0) {
				return string.Empty;
			}
			return array[0];
		}

		public static string[] Split(this string text, char c)
		{
			return text.Split(new char[] { c });
		}

		public static string Join(this string[] stringArray, char c)
		{
			return stringArray.Join(new string(new char[] { c }));
		}

		public static string Join(this string[] stringArray, string delimeter)
		{
			return stringArray.Join(0, delimeter);
		}

		public static string Join(this string[] stringArray, int startIndex, char c)
		{
			return stringArray.Join(startIndex, new string(new char[] { c }));
		}

		public static string Join(this string[] stringArray, int startIndex, string delimeter)
		{
			if (stringArray.Length < startIndex) {
				return string.Empty;
			}

			System.Text.StringBuilder sb = new System.Text.StringBuilder(stringArray[startIndex]);
			for (int i = startIndex + 1; i < stringArray.Length; i++) {
				sb.Append(delimeter);
				sb.Append(stringArray[i]);
			}
			return sb.ToString();
		}

		public static string[] Shift(this string[] arr)
		{
			if (arr.Length == 1) {
				return new string[0];
			}
			string[] ret = new string[arr.Length-1];
			for (int i = 1; i < arr.Length; i++) {
				ret[i-1] = arr[i];
			}
			return ret;
		}

		public static string Shift(this string text, char c)
		{
			return text.Split(' ').Shift().Join(' ');
		}

		public static bool Contains(this string text, char c)
		{
			return text.Contains(c.ToString());
		}

		public static string Escape(this char c)
		{
			if (c == ' ') return @" "; // because its < 32
			if (c == '"') return "\\\""; // special escape
			if (c > 32)   return string.Format("{0}", c);
			switch (c) {
			case '\a':
				return @"\a";
			case '\b':
				return @"\b";
			case '\n':
				return @"\n";
			case '\v':
				return @"\v";
			case '\r':
				return @"\r";
			case '\f':
				return @"\f";
			case '\t':
				return @"\t";
			default:
				return string.Format(@"\x{0:x}", (int) c);
			}

		}

		public static string Escape(this string text)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("\"");
			foreach (char c in text) {
				sb.Append(c.Escape());
			}
			sb.Append("\"");
			return sb.ToString();
		}

		#endregion

		public static StringBuilder Append(this StringBuilder stringBuilder, string cmd, params object[] paramlist)
		{
			return stringBuilder.Append(String.Format(cmd, paramlist));
		}

		public static TimeSpan ToTimeSpan(this float time)
		{
			long total = (long)(time * TimeSpan.TicksPerSecond);
			return new TimeSpan(total);
		}

		public static float ToFloat(this TimeSpan timespan)
		{
			return (float)timespan.Ticks / TimeSpan.TicksPerSecond;
		}

		public static bool IsStruct(this Type type)
		{
			return type.IsValueType && !type.IsPrimitive;
		}

		public static bool IsMessage(this Type type)
		{
			return type.IsStruct()
				&& type.FullName.StartsWith("SharpMod.Message")
				&& type.FullName.EndsWith("Message");
		}

		public static string GetInnerText(this XmlDocument doc, string tag)
		{
			return doc.GetElementsByTagName(tag).Item(0).InnerText;
		}

		public static string GetInnerText(this XmlElement element, string tag)
		{
			return element.GetElementsByTagName(tag).Item(0).InnerText;
		}

		public static XmlElement GetXmlElement(this XmlDocument xmldoc, string name)
		{
			return xmldoc.GetElementsByTagName(name).Item(0) as XmlElement;
		}

		public static XmlElement GetXmlElement(this XmlNode xmlnode, string name)
		{
			return (xmlnode as XmlElement).GetElementsByTagName(name).Item(0) as XmlElement;
		}
	}
}