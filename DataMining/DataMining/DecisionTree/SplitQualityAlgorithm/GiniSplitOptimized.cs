using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.SplitQualityAlgorithm
{
    public class GiniSplitOptimized : ISplitQualityAlgorithm
    {
		public double GiniIndexOptimized(DataTable table, DataColumn col)
		//public double GiniIndexOptimized(DataTable table, string colname)
		{
			return table.AsEnumerable ().
				GroupBy(k => k[col], (k, e) => new { cnt = e.Count() }).
				//GroupBy(k => k[colname], (k, e) => new { cnt = e.Count() }).
				//GroupBy(row => row.Field<object>(col), (k, e) => new { cnt = e.Count() }).
				Sum (a => a.cnt * a.cnt);
		}

		// вычисляет показатель качества разбиения для категориального аттрибута
		public double CalcSplitQuality(List<DataTable> tables,	// набор
			DataColumn column)									// атрибут по которому производится вычисление индекса
			//string colname)								// атрибут по которому производится вычисление индекса
		{
			return tables.Sum (a => GiniIndexOptimized(a, column) / a.Rows.Count);
			//return tables.Sum (a => GiniIndexOptimized(a, colname) / a.Rows.Count);
		}
			
        // Сравнивает показатели качества разбиения
        // -1 - первый показатель лучше второго
        //  0 - показатели равны
        //  1 - первый показатель хуже второго
		public int Compare(double firstQuality,		// Первый показатель
						   double secondQuality)	// Второй показатель
        {
            if (firstQuality > secondQuality)
                return -1;
            else if (firstQuality < secondQuality)
                return 1;
            else
                return 0;
        }
    }
}