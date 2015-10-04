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
            return split.
                GroupBy(k => k, (k, e) => new { Count = e.Count() }).
                Sum(a => a.Count * a.Count);
        }

        // Вычисляет показатель качества разбиения
		public double CalcSplitQuality(List<List<T>> splits)	// Список разбиений
        {
            return splits.Sum(a => GiniIndexOptimized(a) / a.Count);
        }

		// вычисляет показатель качества разбиения для категориального аттрибута
		public double CalcSplitQuality(DataTable table,	// набор
			DataColumn column,							// атрибут по которому производится вычисление индекса
			int threshold)								// порог, относительно которого было получено разбиение
		{
			throw new NotImplementedException();
		}

		// вычисляет показатель качества разбиения для числового аттрибута
		public double CalcSplitQuality(DataTable table,	// набор
			DataColumn column,							// атрибут по которому производится вычисление индекса
			double threshold)							// порог, относительно которого было получено разбиение
		{
			throw new NotImplementedException();
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
