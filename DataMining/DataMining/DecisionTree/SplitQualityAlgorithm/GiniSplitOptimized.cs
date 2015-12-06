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
				GroupBy(r => r[col.Ordinal], (k, e) => new { cnt = e.Count() }).
				Sum (a => a.cnt * a.cnt);
		}

		public double CalcSplitQuality(List<DataTable> tables, DataColumn column) {
			return tables.Sum (t => GiniIndexOptimized(t, column) / t.Rows.Count); // делить надо не на 
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