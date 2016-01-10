using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataMining.DecisionTree.SplitQualityAlgorithm {
	[Serializable]
    public class GiniSplitAsync : ISplitQualityAlgorithm {
		public double GiniIndex(DataTable table, DataColumn col) {
			int count = table.Rows.Count;

			object lck = new object ();
			Dictionary<object, long> grp = new Dictionary<object, long>();


			Parallel.For (0, count, i => {
				object key = table.Rows [i] [col.Ordinal];

				bool c;
				lock (lck) {
					c = grp.Keys.Contains (key);

					if (!c) {
						grp [key] = 1;
					}
				}

				if (!c) {
					return;
				}

				lock (lck) {
					if (c) {
						grp [key]++;
					}
				}
			});

			double sum = 0;
			double count2 = count * count;
			long e;
			/*
			for (int i = grp.Count - 1; i >= 0 ; i--) {
				e = grp.Values.ElementAt (i);
				sum += (e * e / count2);
			}
			*/
			Parallel.For (0, grp.Count, i => {
				e = grp.Values.ElementAt (i);
				double tmp = (e * e) / count2;
				lock(lck)
				{
					sum += tmp;
				}
			});

			return 1 - sum;
		}

		public double CalcSplitQuality(List<DataTable> tables, DataColumn column) {
			double totalCount = 0;
			int i;
			for (i = 0; i < tables.Count; i++) {
				totalCount += tables [i].Rows.Count;
			}
			double res = 0;
			Parallel.For(0, tables.Count, j => res += tables [j].Rows.Count * GiniIndex (tables [j], column) / totalCount);
			return res;
		}
        
        // сравнивает показатели качества разбиения
        // выход:
        //  -1 - первый показатель лучше
        // 	 0 - показатели равны
        //   1 - второй показатель лучше
		public int Compare(double firstQuality, double secondQuality) {
			if (firstQuality > secondQuality) {
				return 1;
			} else if (firstQuality < secondQuality) {
				return -1;
			} else {
				return 0;
			}
        }

		public bool IsTheBest (double quality) {
			return (quality == 0.0) ? true : false;
		}
    }
}