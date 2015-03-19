using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Attributes;

namespace DataMining.DecisionTree.Splits
{
    public class CategoricalSplit : SplitBase
    {
        public override void CalcBestSplit(AttributeBase a)
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
                    var secondSplit = b.Values.Where(i => (~set & i) == 0);
                    //var secondSplit = a.Values.Where(e => e > tmpThreshold).ToList();

                    // Оцениваем разбиение
                    // Сравниваем разбиение с предыдущим
                    // Сохраняем наилучшее
                }
            }
        }
    }
}
