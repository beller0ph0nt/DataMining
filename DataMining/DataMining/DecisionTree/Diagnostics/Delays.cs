using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace DataMining
{
	public static class Delays
	{
		public static Dictionary<string, List<long>> delays;

		static Delays ()
		{
			delays = new Dictionary<string, List<long>> ();
			delays ["CalcBestCatSplit"] = new List<long> ();
			delays ["CalcBestNumSplit"] = new List<long> ();
			delays ["CalcBestNumSplit_Sort"] = new List<long> ();
			delays ["CalcBestNumSplit_FillSplits"] = new List<long> ();
			delays ["CalcBestNumSplit_CalcQuality"] = new List<long> ();
			delays ["CalcBestNumSplit_CalcClass"] = new List<long> ();
		}
	}
}

