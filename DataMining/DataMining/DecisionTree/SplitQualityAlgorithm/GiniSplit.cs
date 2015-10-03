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
            double count = split.Count;

            return 1 - split.
                GroupBy(k => k, (k, e) => new { Count = e.Count() }).
                Sum(a => Math.Pow(a.Count / count, 2));
        }
			
        // вычисляет показатель качества разбиения
		public double CalcSplitQuality(List<List<T>> splits)	// список разбиений
        {
            //double totalCount = splits.Sum(l => l.Count);
            
            //return splits.Sum(a => a.Count * GiniIndex(a) / totalCount);

			throw new NotImplementedException();
        }

		// вычисляет показатель качества разбиения
		public double CalcSplitQuality(DataTable set)	// набор
		{
			throw new NotImplementedException();
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