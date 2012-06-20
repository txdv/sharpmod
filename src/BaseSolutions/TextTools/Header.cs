using System;

namespace TextTools
{
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
}

