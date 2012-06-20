using System;
using System.Text;
using System.Collections.Generic;

namespace TextTools
{
	/// <summary>
	/// Enumerator of Alignment
	/// </summary>
	public enum Align
	{
		Left,
		Center,
		Right,
	}

	/// <summary>
	/// Header for TextTable.
	/// Allows to set Name, HeaderAlignment, CellAlignment, Min/Max Length of the row
	/// Default values of Header are defined in TextTable
	/// </summary>
	public class Header
	{
		public string Name         { get; set; }
		public Align Alignment     { get; set; }
		public Align CellAlignment { get; set; }
		public int MinimumLength   { get; set; }
		public int MaximumLength   { get; set; }

		public Header(string name, Align alignment, Align cellAlignment, int minimumLength, int maximumLength)
		{
			Name = name;
			Alignment = alignment;
			CellAlignment = cellAlignment;
			MinimumLength = minimumLength;
			MaximumLength = maximumLength;
		}

		public Header(string name)
			: this(name, TextTable.defaultHeaderAlignment, TextTable.defaultCellAlignment,
				TextTable.defaultMinimumHeaderLength, TextTable.defaultMaximumHeaderLength)
		{
		}
	}

	/// <summary>
	/// A class for rendering text based tables.
	/// This class needs just a printing function (like Console.WriteLine) and the maximum
	/// width of the table that has to be rendered. Therefore it is suitable for use in
	/// linux consoles with Mono
	/// </summary>
	public class TextTable
	{
		/// <summary>
		/// Default value of HeaderAlignment (Left)
		/// </summary>
		public const Align defaultHeaderAlignment = Align.Left;
		/// <summary>
		/// Default value of CellAlignment (Left)
		/// </summary>
		public const Align defaultCellAlignment = Align.Left;
		/// <summary>
		/// Default value of minimum header length (1)
		/// </summary>
		public const int defaultMinimumHeaderLength = 1;
		/// <summary>
		/// Default value of maximum header length (int.MaxValue)
		/// </summary>
		public const int defaultMaximumHeaderLength = int.MaxValue;
		/// <summary>
		/// The default delimeter (..)
		/// This is used when the text gets too long in the row's and has to be cut off.
		/// Can be specified for every TextTable instance with the Field Delimeter.
		/// </summary>
		public const string defaultDelimeter = "..";

		/// <summary>
		/// The delegate used for *rendering* the table.
		/// </summary>
		public delegate void OutputFunction(string text);

		public IList<Header> Header { get; set; }
		public string Delimeter { get; set; }

		/// <summary>
		/// A constructor which requires an array of Header or a class with the IList<Header> interaface implemeneted.
		/// </summary>
		/// <param name="header">
		/// A header array <see cref="IList<Header>"/>
		/// </param>
		public TextTable(IList<Header> header)
		{
			Header = header;
			Delimeter = defaultDelimeter;
		}

		/// <summary>
		/// A constructor which requires an array of strings or a class with the IList<string> interaface implemeneted.
		/// Internally the array will be converted in an array of Header with the default values used in TextTable.
		/// </summary>
		/// <param name="header">
		/// A string array <see cref="IList<System.String>"/>
		/// </param>
		public TextTable(IList<string> header)
			: this(GetHeaderArrayOfStringArray(header))
		{
		}

		/// <summary>
		/// A function for converting the string array into a header array, needed to fit in the constructor.
		/// </summary>
		/// <param name="header">
		/// An array of strings <see cref="IList<System.String>"/>
		/// </param>
		/// <returns>
		/// An array of headers <see cref="Header[]"/>
		/// </returns>
		protected static Header[] GetHeaderArrayOfStringArray(IList<string> header)
		{
			Header[] headerlist = new Header[header.Count];
			for (int i = 0; i < header.Count; i++) {
				headerlist[i] = new Header(header[i]);
			}
			return headerlist;
		}

		/// <summary>
		/// The Render function which uses Console.Write for printing and Console.WindowWith to determine the
		/// with of the console.
		/// </summary>
		/// <param name="data">
		/// A data object, the second dimension must fit the header length. <see cref="IList<IList<System.Object>>"/>
		/// </param>
		public void Render(IList<IList<object>> data)
		{
			Render(data, Console.Write, Console.WindowWidth);
		}

		/// <summary>
		/// Gets the longest data entry length in a data set of a specific row.
		/// </summary>
		/// <param name="data">
		/// A data object <see cref="IList<IList<System.Object>>"/>
		/// </param>
		/// <param name="row">
		/// The row to examine <see cref="System.Int32"/>
		/// </param>
		/// <returns>
		/// The maximum length <see cref="System.Int32"/>
		/// </returns>
		protected int GetLongestDataEntryLength(IList<IList<object>> data, int row)
		{
			int max = data[0][row].ToString().Length;
			for (int i = 1; i < data.Count; i++) {
				int length = data[i][row].ToString().Length;
				if (length > max) {
					max = length;
				}
			}
			return max;
		}

		/// <summary>
		/// Gets the longest data entry lengths of all rows.
		/// </summary>
		/// <param name="data">
		/// A data object <see cref="IList<IList<System.Object>>"/>
		/// </param>
		/// <returns>
		/// An array of integers with the longest lengths <see cref="System.Int32[]"/>
		/// </returns>
		protected int[] GetLongestDataEntryLengthArray(IList<IList<object>> data)
		{
			int[] lengths = new int[Header.Count];

			for (int i = 0; i < Header.Count; i++) {
				lengths[i] = GetLongestDataEntryLength(data, i);
			}
			return lengths;
		}

		/// <summary>
		/// Trims the text according to length and alignment.
		/// </summary>
		/// <param name="alignment">
		/// Alignment value <see cref="Align"/>
		/// </param>
		/// <param name="text">
		/// Text to trim <see cref="System.String"/>
		/// </param>
		/// <param name="length">
		/// The maximum length <see cref="System.Int32"/>
		/// </param>
		/// <returns>
		/// A trimmed string <see cref="System.String"/>
		/// </returns>
		protected string Trim(Align alignment, string text, int length)
		{
			if (text.Length <= length) {
				switch (alignment) {
				case Align.Left:
					return text + new string(' ', length - text.Length);
				case Align.Right:
					return new string(' ', length - text.Length) + text;
				case Align.Center:
				default:
					length = length - text.Length;

				int one = length%2;
				int side = length/2;

				return new string(' ', one + side) + text + new string(' ', side);
				}
			} else {
				return text.Substring(0, length - Delimeter.Length) + Delimeter;
			}
		}

		/// <summary>
		/// The classic Render functions, which lets you supply the maximum width and the function
		/// which has to be used for printing.
		/// </summary>
		/// <param name="data">
		/// A data object <see cref="IList<IList<System.Object>>"/>
		/// </param>
		/// <param name="outputFunction">
		/// A delegate to the output function <see cref="OutputFunction"/>
		/// </param>
		/// <param name="maximumWidth">
		/// the maximum width of the table <see cref="System.Int32"/>
		/// </param>
		public void Render(IList<IList<object>> data, OutputFunction outputFunction, int maximumWidth)
		{
			int[] lengths = null;
			List<int> tmp = new List<int>(Header.Count);
			foreach (TextTools.Header headerEntry in Header) tmp.Add(headerEntry.Name.Length);

			lengths = tmp.ToArray();

			// it's always the empty list that gets the rockets down
			if (data.Count > 0) {
				lengths = GetLongestDataEntryLengthArray(data);

			// determine the maximum length
				for (int i = 0; i < lengths.Length; i++) {
					if (Header[i].Name.Length > lengths[i]) {
						lengths[i] = Header[i].Name.Length;
					}
					if (Header[i].MinimumLength > lengths[i]) {
						lengths[i] = Header[i].MinimumLength;
					} else if (Header[i].MaximumLength < lengths[i]) {
						lengths[i] = Header[i].MaximumLength;
					}
				}
			}

			// build and print the header
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < lengths.Length; i++) {
				sb.Append(Trim(Header[i].Alignment, Header[i].Name, lengths[i]));
				sb.Append(" ");
			}
			sb.Append("\n");
			outputFunction(sb.ToString());

			// build and print the rows
			foreach (IList<object> obj in data) {
				sb = new StringBuilder();
				for (int i = 0; i < lengths.Length; i++) {
					sb.Append(Trim(Header[i].CellAlignment, obj[i].ToString(), lengths[i]));
					sb.Append(" ");
				}
				sb.Append("\n");
				outputFunction(sb.ToString());
			}
		}
	}
}
