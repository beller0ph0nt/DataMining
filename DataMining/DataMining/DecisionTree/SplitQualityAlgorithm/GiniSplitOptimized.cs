using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.SplitQualityAlgorithm
{
    public class GiniSplitOptimized<T> : ISplitQualityAlgorithm<T>
    {
        /// <summary>
        /// Вычисляет оптимизированный индекс Гини
        /// </summary>
        /// <param name="split">Оцениваемое множество</param>
        /// <returns>Индекс</returns>
        public double GiniIndexOptimized(List<T> split)
        {
            return split.
                GroupBy(k => k, (k, e) => new { Count = e.Count() }).
                Sum(a => a.Count * a.Count);
        }

        /// <summary>
        /// Вычисляет показатель качества разбиения
        /// </summary>
        /// <param name="splits">Список разбиений</param>
        /// <returns>Показатель качества разбиения</returns>
        public double CalcSplitQuality(List<List<T>> splits)
        {
            return splits.Sum(a => GiniIndexOptimized(a) / a.Count);
        }

        /// <summary>
        /// Сравнивает показатели качества разбиения
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
                return -1;
            else if (firstQuality < secondQuality)
                return 1;
            else
                return 0;
        }
    }
}
