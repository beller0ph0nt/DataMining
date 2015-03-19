using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Attributes;

namespace DataMining.DecisionTree.Splits
{
    public class NumericalSplit : AbstractSplitBase
    {
        public override void CalcBestSplit(AbstractAttributeBase a)
        {
            double tmpQuality;
            double tmpThreshold;
            var b = a as NumericalAttribute;

            // Сортировка атрибута. Необходима для определения порога
            b.Values.Sort();

            for (int i = 0; i < b.Values.Count - 1; i++)
            {
                // Вычисляем порог
                tmpThreshold = (b.Values[i] + b.Values[i + 1]) / 2;

                var firstSplit = b.Values.Where(e => e <= tmpThreshold).ToList();
                var secondSplit = b.Values.Where(e => e > tmpThreshold).ToList();

                tmpQuality = SplitQualityAlgorithm.CalcSplitQuality(
                    new List<List<double>>() { firstSplit, secondSplit },
                    b.Values.Count);

                if (i == 0)
                {
                    Quality = tmpQuality;
                    Threshold = tmpThreshold;
                    Splits.Add(firstSplit);
                    Splits.Add(secondSplit);
                }
                else
                {
                    if (SplitQualityAlgorithm.Compare(tmpQuality, Quality) < 0)
                    {
                        Quality = tmpQuality;
                        Threshold = tmpThreshold;
                        Splits[0] = firstSplit;
                        Splits[1] = secondSplit;
                    }
                }
            }
        }
    }
}
