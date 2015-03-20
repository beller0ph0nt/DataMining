using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Attributes;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining.DecisionTree.Splits
{
    public class CategoricalSplit : SplitBase<int>
    {
        public override void CalcBestSplit(AttributeBase<int> a)
        {
            //double tmpQuality;
            //double tmpThreshold;
            var b = a as CategoricalAttribute;

            // Сортировка атрибута. Необходима для определения порога
            b.Values.Sort();

            for (int set = 1; set < b.CategoriesCounter - 1; set++)
            {
                for (int j = 0; j < b.Values.Count; j++)
                {
                    // Вычисляем порог. Порог уже есть - это set
                    // Разбиваем множество
                    var firstSplit = b.Values.Where(i => (~set & i) == 0).ToList();
                    var secondSplit = b.Values.Where(i => (~set & i) != 0).ToList();

                    // Оцениваем разбиение
                    //base.SplitQualityAlgorithm.CalcSplitQuality(
                    //    new List<List<int>>() { firstSplit, secondSplit },
                    //    b.Values.Count);

                    // Сравниваем разбиение с предыдущим
                    // Сохраняем наилучшее
                }
            }
        }
    }
}
