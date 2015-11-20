using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.SplitQualityAlgorithm {
	[Serializable]
    public class GiniSplitOptimized : ISplitQualityAlgorithm {
		public double GiniIndexOptimized(DataTable table, DataColumn col) {
			return table.AsEnumerable ().
				GroupBy(k => k[col], (k, e) => new { cnt = e.Count() }).
				Sum (a => a.cnt * a.cnt);
		}

		// вычисляет показатель качества разбиения для категориального аттрибута
		public double CalcSplitQuality(List<DataTable> tables, DataColumn column) {
			return tables.Sum (a => GiniIndexOptimized(a, column) / a.Rows.Count);
		}
			
        // Сравнивает показатели качества разбиения
        // -1 - первый показатель лучше второго
        //  0 - показатели равны
        //  1 - первый показатель хуже второго
		public int Compare(double firstQuality, double secondQuality) {
			if (firstQuality > secondQuality) {
				return -1;
			} else if (firstQuality < secondQuality) {
				return 1;
			} else {
				return 0;
			}
        }
    }
}