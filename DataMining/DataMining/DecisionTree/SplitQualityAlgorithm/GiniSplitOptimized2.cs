using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataMining.DecisionTree.SplitQualityAlgorithm {
	[Serializable]
    public class GiniSplitOptimized2 : ISplitQualityAlgorithm {
		public double GiniIndex(DataTable table, DataColumn col) {
			double count = table.Rows.Count;
			int i;

			object key = new object ();
			Dictionary<object, long> grp = new Dictionary<object, long>();
			for (i = 0; i < count; i++) {
				key = table.Rows [i] [col.Ordinal];
				if (grp.Keys.Contains (key)) {
					grp [key]++;
				} else {
					grp [key] = 1;
				}
			}

			double sum = 0;
			double count2 = count * count;
			long e;

			for (i = grp.Count - 1; i >= 0 ; i--) {
				e = grp.Values.ElementAt (i);
				sum += (e * e / count2);
			}


			/*
			object lck = new object ();
			Parallel.For (0, grp.Count, j => {
				e = grp.Values.ElementAt (j);
				double tmp = (e * e) / count2;
				lock(lck)
				{
					sum += tmp;
				}
			});
			*/

			return 1 - sum;
		}

		public double CalcSplitQuality(List<DataTable> tables, DataColumn column) {
			double totalCount = 0, res = 0;
			int i, cnt = tables.Count - 1;

			for (i = cnt; i >= 0; i--) {
				totalCount += tables [i].Rows.Count;
			}

			for (i = cnt; i >= 0; i--) {
				res += tables [i].Rows.Count * GiniIndex (tables [i], column) / totalCount;
			}

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