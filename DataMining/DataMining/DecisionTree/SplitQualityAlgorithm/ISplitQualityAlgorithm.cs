using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.SplitQualityAlgorithm
{
    public interface ISplitQualityAlgorithm<T>
    {
        /// <summary>
        /// Метод вычисляет показатель качества разбиения
        /// </summary>
        /// <param name="splits">Список разбиений</param>
        /// <returns>Показатель качества разбиения</returns>
        double CalcSplitQuality(List<List<T>> splits);

        /// <summary>
        /// Метод сравнивает показатели качества разбиения
        /// </summary>
        /// <param name="firstQuality">Первый показатель</param>
        /// <param name="secondQuality">Второй показатель</param>
        /// <returns>
        /// -1 - первый показатель лучше второго
        ///  0 - показатели равны
        ///  1 - первый показатель хуже второго
        /// </returns>
        int Compare(double firstQuality, double secondQuality);
    }
}
