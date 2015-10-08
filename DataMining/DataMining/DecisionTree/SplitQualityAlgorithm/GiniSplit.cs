using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Elements;

namespace DataMining.DecisionTree.SplitQualityAlgorithm
{
    public class GiniSplit<T> : ISplitQualityAlgorithm<T>
    {
        // вычисляет классический индекс Гини
		public double GiniIndex(List<T> split)	// оцениваемое множество
        {
			// !!! СТАРЫЙ ВАРИАНТ УДАЛИТЬ !!!

            //double count = split.Count;

            //return 1 - split.
            //    GroupBy(k => k, (k, e) => new { Count = e.Count() }).
            //    Sum(a => Math.Pow(a.Count / count, 2));

			throw new NotImplementedException();
        }

		public double GiniIndex(DataTable table, DataColumn column)
		{
			double count = table.Rows.Count;

			return 1 - table.AsEnumerable ().
				GroupBy (k => k [column.ColumnName], (k, e) => new { cnt = e.Count() }).
				Sum (a => Math.Pow (a.cnt / count, 2));
		}
			
        // вычисляет показатель качества разбиения
		public double CalcSplitQuality(List<List<T>> splits)	// список разбиений
        {
			// !!! СТАРЫЙ ВАРИАНТ УДАЛИТЬ !!!

			throw new NotImplementedException();
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