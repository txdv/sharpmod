using System;
using TextTools;

namespace TextToolsConsole
{
	class MainClass
	{
		#region Periodic Table in an Array
		public static string[][] data = new string[][] {
			new string[] {  "89",  "Ac",      "Actinium",       "227" },
			new string[] {  "13",  "Al",      "Aluminum",  "26.98154" },
			new string[] {  "56",  "Ba",        "Barium",    "137.33" },
			new string[] {   "4",  "Be",     "Beryllium",   "9.01218" },
			new string[] {  "83",  "Bi",       "Bismuth",  "208.9804" },
			new string[] { "107",  "Bh",       "Bohrium",       "262" },
			new string[] {  "48",  "Cd",       "Cadmium",     "112.4" },
			new string[] {  "20",  "Ca",       "Calcium",     "40.08" },
			new string[] {  "55",  "Cs",        "Cesium",  "132.9054" },
			new string[] {  "24",  "Cr",      "Chromium",    "51.996" },
			new string[] {  "27",  "Co",        "Cobalt",   "58.9332" },
			new string[] {  "29",  "Cu",        "Copper",    "63.546" },
			new string[] { "110",  "Ds",  "Darmstadtium",     "261.9" },
			new string[] { "105",  "Db",       "Dubnium",     "261.9" },
			new string[] {  "87",  "Fr",      "Francium",     "(233)" },
			new string[] {  "31",  "Ga",       "Gallium",     "69.72" },
			new string[] {  "79",  "Au",          "Gold",  "196.9655" },
			new string[] {  "72",  "Hf",       "Hafnium",    "178.49" },
			new string[] { "108",  "Hs",       "Hassium",     "264.8" },
			new string[] {  "49",  "In",        "Indium",    "114.82" },
			new string[] {  "77",  "Ir",       "Iridium",     "192.2" },
			new string[] {  "26",  "Fe",          "Iron",     "55.85" },
			new string[] {  "57",  "La",     "Lanthanum",    "138.91" },
			new string[] {  "82",  "Pb",          "Lead",     "207.2" },
			new string[] {   "3",  "Li",       "Lithium",     "6.941" },
			new string[] {  "12",  "Mg",     "Magnesium",    "24.305" },
			new string[] {  "25",  "Mn",     "Manganese",   "54.9380" },
			new string[] { "109",  "Mt",    "Meitnerium",     "265.9" },
			new string[] {  "80",  "Hg",       "Mercury",    "200.59" },
			new string[] {  "42",  "Mo",    "Molybdenum",     "95.94" },
			new string[] {  "28",  "Ni",        "Nickel",     "58.71" },
			new string[] {  "41",  "Nb",       "Niobium",     "92.91" },
			new string[] {  "76",  "Os",        "Osmium",     "190.2" },
			new string[] {  "46",  "Pd",     "Palladium",    "106.42" },
			new string[] {  "78",  "Pt",      "Platinum",    "195.09" },
			new string[] {  "19",   "K",     "Potassium",   "39.0983" },
			new string[] {  "88",  "Ra",        "Radium",  "226.0254" },
			new string[] {  "75",  "Re",       "Rhenium",    "186.23" },
			new string[] {  "45",  "Rh",       "Rhodium",    "102.91" },
			new string[] {  "37",  "Rb",      "Rubidium",   "85.4678" },
			new string[] {  "44",  "Ru",     "Ruthenium",     "101.1" },
			new string[] { "104",  "Rf", "Rutherfordium",     "260.9" },
			new string[] {  "21",  "Sc",      "Scandium",   "44.9559" },
			new string[] { "106",  "Sg",    "Seaborgium",    "262.94" },
			new string[] {  "47",  "Ag",        "Silver",    "107.87" },
			new string[] {  "11",  "Na",        "Sodium",  "22.98977" },
			new string[] {  "38",  "Sr",     "Strontium",     "87.62" },
			new string[] {  "73",  "Ta",      "Tantalum",    "180.95" },
			new string[] {  "43",  "Tc",    "Technetium",      "(99)" },
			new string[] {  "81",  "Tl",      "Thallium",   "204.383" },
			new string[] {  "50",  "Sn",           "Tin",    "118.69" },
			new string[] {  "22",  "Ti",      "Titanium",     "47.90" },
			new string[] {  "74",   "W",      "Tungsten",    "183.85" },
			new string[] { "112", "Uub",      "Ununbium",     "276.8" },
			new string[] { "116", "Uuh",    "Ununhexium",          "" },
			new string[] { "114", "Uuq",   "Ununquadium",       "289" },
			new string[] {  "23",   "V",      "Vanadium",   "50.9414" },
			new string[] {  "39",   "Y",       "Yttrium",   "88.9059" },
			new string[] {  "30",  "Zn",          "Zinc",     "65.37" },
			new string[] {  "40",  "Zr",     "Zirconium",     "91.22" },
			new string[] {  "51",  "Sb",      "Antimony",    "121.75" },
			new string[] {  "33",  "As",       "Arsenic",   "74.9216" },
			new string[] {  "85",  "At",      "Astatine",     "(210)" },
			new string[] {   "5",   "B",         "Boron",     "10.81" },
			new string[] {  "32",  "Ge",     "Germanium",     "72.59" },
			new string[] {  "84",  "Po",      "Polonium",     "(210)" },
			new string[] {  "14",  "Si",       "Silicon",   "28.0855" },
			new string[] {  "52",  "Te",     "Tellurium",     "127.6" },
			new string[] {  "35",  "Br",       "Bromine",    "79.904" },
			new string[] {   "6",   "C",        "Carbon",    "12.011" },
			new string[] {  "17",  "Cl",      "Chlorine",    "35.453" },
			new string[] {   "9",   "F",      "Fluorine", "18.998403" },
			new string[] {   "1",   "H",      "Hydrogen",  "1.007825" },
			new string[] {  "53",   "I",        "Iodine",  "126.9045" },
			new string[] {   "7",   "N",      "Nitrogen",   "14.0067" },
			new string[] {   "8",   "O",        "Oxygen",    "15.999" },
			new string[] {  "15",   "P",    "Phosphorus",          "" },
			new string[] {  "34",  "Se",      "Selenium",     "78.96" },
			new string[] {  "16",   "S",       "Sulphur",     "32.06" },
			new string[] {  "18",  "Ar",         "Argon",    "39.948" },
			new string[] {   "2",  "He",        "Helium",   "4.00260" },
			new string[] {  "36",  "Kr",       "Krypton",     "83.80" },
			new string[] {  "10",  "Ne",          "Neon",    "20.179" },
			new string[] {  "86",  "Rn",         "Radon",     "(222)" },
			new string[] {  "54",  "Xe",         "Xenon",    "131.29" },
			new string[] {  "95",  "Am",     "Americium",     "(243)" },
			new string[] {  "97",  "Bk",     "Berkelium",     "(247)" },
			new string[] {  "98",  "Cf",   "Californium",     "(251)" },
			new string[] {  "58",  "Ce",        "Cerium",    "140.12" },
			new string[] {  "96",  "Cm",        "Curium",     "(247)" },
			new string[] {  "66",  "Dy",    "Dysprosium",    "162.50" },
			new string[] {  "99",  "Es",   "Einsteinium",       "254" },
			new string[] {  "68",  "Er",        "Erbium",    "167.26" },
			new string[] {  "63",  "Eu",      "Europium",    "167.26" },
			new string[] { "100",  "Fm",       "Fermium",     "(257)" },
			new string[] {  "64",  "Gd",    "Gadolinium",    "157.25" },
			new string[] {  "67",  "Ho",       "Holmium",     "164.9" },
			new string[] { "103",  "Lr",    "Lawrencium",     "(262)" },
			new string[] {  "71",  "Lu",      "Lutetium",    "174.97" },
			new string[] { "101",  "Md",   "Mendelevium",     "(258)" },
			new string[] {  "60",  "Nd",     "Neodymium",          "" },
			new string[] {  "93",  "Np",     "Neptunium",     "(237)" },
			new string[] { "102",  "No",      "Nobelium",       "259" },
			new string[] {  "94",  "Pu",     "Plutonium",     "(244)" },
			new string[] {  "59",  "Pr",  "Praseodymium",    "140.91" },
			new string[] {  "61",  "Pm",    "Promethium",     "(147)" },
			new string[] {  "91",  "Pa",  "Protactinium",  "231.0359" },
			new string[] {  "62",  "Sm",      "Samarium",    "150.35" },
			new string[] {  "65",  "Tb",       "Terbium", "158.92534" },
			new string[] {  "90",  "Th",       "Thorium",    "232.04" },
			new string[] {  "69",  "Tm",       "Thulium",    "168.93" },
			new string[] {  "92",   "U",       "Uranium",    "238.03" },
			new string[] {  "70",  "Yb",     "Ytterbium",    "173.04" }
		};
		#endregion

		public static void Main (string[] args)
		{
			// The header
			string[] headers = new string[] {"Atomic Number", "Abreviation", "Name", "Atomic Weight"};
			// The table
			TextTable tt = new TextTable(headers);
			// Adjust lengths and alignment
			tt.Header[0].CellAlignment = Align.Right;
			tt.Header[1].CellAlignment = Align.Center;
			tt.Header[2].CellAlignment = Align.Left;
			tt.Header[2].Alignment = Align.Center;
			tt.Header[2].MaximumLength = 6;
			tt.Header[3].CellAlignment = Align.Right;
			// Print the data into the console!
			tt.Render(data);
		}
	}
}
