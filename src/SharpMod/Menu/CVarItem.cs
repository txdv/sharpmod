using System;
using System.Text;

namespace SharpMod.Menu
{
	public class CVarItem : Item
	{
		private CVar cvar = null;
		public string[] Values { get; protected set; }
		public string CVarName { get { return cvar.Name; } protected set { cvar = CVar.Get(value); } }

		public CVarItem(string varname, string[] values)
			: base(varname)
		{
			Values = values;
			cvar = CVar.Get(varname);
		}

		public override string Text {
			get {
				int index = -1;
				StringBuilder sb = new StringBuilder();
				if (Values != null)
				for (int i = 0; i < Values.Length; i++) {
					if (Values[i].ToLower() == cvar.String.ToLower()) {
						sb.Append(MenuColor.Red);
						sb.Append(Values[i]);
						sb.Append(MenuColor.White);
						index = i;
					} else {
						sb.Append(Values[i]);
					}
					if (i != Values.Length - 1) {
						sb.Append(", ");
					}
				}
				if (index == -1) {
					return string.Format("{0} ({1}): {2}", base.Text, cvar.String, sb.ToString());
				} else {
					return string.Format("{0}: {1}", base.Text, sb.ToString());
				}
			}
			protected set {
				base.Text = value;
			}
		}

		public int Index {
			get {
				if (Values != null) {
					for (int i = 0; i < Values.Length; i++) {
						if (Values[i].ToLower() == cvar.String.ToLower()) {
							return i;
						}
					}
				}
				return -1;
			}
		}

		public int NextItem {
			get {
				if (Index == -1) {
					return 0;
				} else {
					return (Index%Values.Length);
				}
			}
		}

		public override bool DoSelect(Player player, int index)
		{
			base.DoSelect(player, index);
			cvar.String = Values[NextItem];
			return true;
		}
	}
}

