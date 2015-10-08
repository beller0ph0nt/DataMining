using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.SplitQualityAlgorithm
{
    public class GiniSplitOptimized<T> : ISplitQualityAlgorithm<T>
    {
        // Вычисляет оптимизированный индекс Гини
		public double GiniIndexOptimized(List<T> split)	// Оцениваемое множество
        {
			// !!! СТАРЫЙ ВАРИАНТ УДАЛИТЬ !!!

            return split.
                GroupBy(k => k, (k, e) => new { Count = e.Count() }).
                Sum(a => a.Count * a.Count);
        }

		public double GiniIndexOptimized(DataTable table, DataColumn column)
		{
			return table.AsEnumerable ().
				GroupBy(k => k[column.ColumnName], (k, e) => new { cnt = e.Count() }).
				Sum (a => a.cnt * a.cnt);
		}

        // Вычисляет показатель качества разбиения
		public double CalcSplitQuality(List<List<T>> splits)	// Список разбиений
        {
			// !!! СТАРЫЙ ВАРИАНТ УДАЛИТЬ !!!

            return splits.Sum(a => GiniIndexOptimized(a) / a.Count);
        }

		// вычисляет показатель качества разбиения для категориального аттрибута
		public double CalcSplitQuality(List<DataTable> tables,	// набор
			DataColumn column)									// атрибут по которому производится вычисление индекса
		{
			return tables.Sum (a => GiniIndexOptimized(a, column) / a.Rows.Count);
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
