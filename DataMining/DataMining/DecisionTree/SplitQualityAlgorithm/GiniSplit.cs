using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.SplitQualityAlgorithm {
	[Serializable]
    public class GiniSplit : ISplitQualityAlgorithm {
		public double GiniIndex(DataTable table, DataColumn col) {
			double count = table.Rows.Count;
			return 1 - table.AsEnumerable ().GroupBy (row => row [col.Ordinal], (k, e) => new { cnt = e.Count () }).Sum (a => Math.Pow (a.cnt / count, 2));
		}

		public double CalcSplitQuality(List<DataTable> tables, DataColumn column) {
			double totalCount = tables.Sum(t => t.Rows.Count);
			return tables.Sum(t => t.Rows.Count * GiniIndex(t, column) / totalCount);
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