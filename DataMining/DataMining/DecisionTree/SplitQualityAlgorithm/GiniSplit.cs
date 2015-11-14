using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.SplitQualityAlgorithm
{
    public class GiniSplit : ISplitQualityAlgorithm
    {
		public double GiniIndex(DataTable table, DataColumn col)
		{
			double count = table.Rows.Count;

			return 1 - table.AsEnumerable ().
				GroupBy (k => k [col.Ordinal], (k, e) => new { cnt = e.Count() }).
				Sum (a => Math.Pow (a.cnt / count, 2));
		}

		// вычисляет показатель качества разбиения для категориального аттрибута
		public double CalcSplitQuality(List<DataTable> tables,	// набор
			DataColumn column)									// атрибут по которому производится вычисление индекса
		{
			double totalCount = tables.Sum(tbl => tbl.Rows.Count);

			return tables.Sum(tbl => tbl.Rows.Count * GiniIndex(tbl, column) / totalCount);
		}
        
        // сравнивает показатели качества разбиения
        // выход:
        //  -1 - первый показатель лучше второго
        // 	 0 - показатели равны
        //   1 - первый показатель хуже второго
		public int Compare(double firstQuality,		// первый показатель
						   double secondQuality)	// второй показатель
        {
            if (firstQuality > secondQuality)
                return 1;
            else if (firstQuality < secondQuality)
                return -1;
            else
                return 0;
        }
    }
}