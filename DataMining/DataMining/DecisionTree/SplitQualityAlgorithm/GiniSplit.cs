using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.SplitQualityAlgorithm
{
    public class GiniSplit : ISplitQualityAlgorithm
    {
        /// <summary>
        /// Метод вычисляет классический индекс Гини
        /// </summary>
        /// <param name="split">Оцениваемое множество</param>
        /// <returns>Индекс</returns>
        public double GiniIndex(List<object> split)
        {
            double count = split.Count;

            return 1 - split.
                GroupBy(k => k, (k, e) => new { Count = e.Count() }).
                Sum(a => Math.Pow(a.Count / count, 2));
        }

        /// <summary>
        /// Метод вычисляет показатель качества разбиения, используя классический метод Гини
        /// </summary>
        /// <param name="splits">Список разбиений</param>
        /// <returns>Показатель качества разбиения</returns>
        public double CalcSplitQuality(List<List<object>> splits)
        {
            double totalCount = splits.Sum(l => l.Count);
            
            return splits.Sum(a => a.Count * GiniIndex(a) / totalCount);
        }
        
        /// <summary>
        /// Метод сравнивает показатели качества разбиения, полученные классическим методом Гини
        /// </summary>
        /// <param name="firstQuality">Первый показатель</param>
        /// <param name="secondQuality">Второй показатель</param>
        /// <returns>
        /// -1 - первый показатель лучше второго
        ///  0 - показатели равны
        ///  1 - первый показатель хуже второго
        /// </returns>
        public int Compare(double firstQuality, double secondQuality)
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
