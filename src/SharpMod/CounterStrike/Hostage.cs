using System;
using System.Reflection;
using SharpMod.Messages;

namespace SharpMod.CounterStrike
{
	public class Hostage : Entity
	{
		unsafe internal Hostage(void *ptr)
			: base(ptr)
		{
		}

		public int HostageIndex {
			get {
				return GetPrivateData(CounterStrikeOffset.hostageid);
			}
		}
	}
}
