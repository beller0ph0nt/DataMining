using System;
using System.Collections.Generic;

namespace DataMining
{
	public static class Thrshld
	{
		public static Dictionary<int, List<double>> dict;

		static Thrshld ()
		{
			dict = new Dictionary<int, List<double>> ();
		}

		public static void Alloc(int ordinal)
		{
			dict [ordinal] = new List<double> ();
		}

		public static void Add(int ordinal, double threshold)
		{
			dict [ordinal].Add (threshold);
		}

		public static void Del(int ordinal, double threshold)
		{
			dict [ordinal].Remove (threshold);
		}

		public static void Del(int index)
		{
			foreach (var l in dict) {
				l.Value.RemoveAt (index);
			}
		}
	}
}

