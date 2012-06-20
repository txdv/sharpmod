using System;
using System.Collections.Generic;

namespace SwearFilter
{
	public class SwearComposite : ISwear, IEnumerable<ISwear>
	{
		private List<ISwear> children = new List<ISwear>();
		public string Name { get; protected set; }

		public SwearComposite(string name)
		{
			Name = name;
		}

		public SwearComposite(string name, IEnumerable<ISwear> collection)
			: this(name)
		{
			children.AddRange(collection);
		}

		public IEnumerator<ISwear> Enumerator {
			get {
				return children.GetEnumerator();
			}
		}

		#region IEnumerable<ISwear> implementation
		public IEnumerator<ISwear> GetEnumerator ()
		{
			return children.GetEnumerator();
		}
		#endregion

		#region IEnumerable implementation
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return children.GetEnumerator();
		}
		#endregion
	}
}

