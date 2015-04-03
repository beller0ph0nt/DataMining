using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Attributes;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining.DecisionTree.Splits
{
    public class NumericalSplit : SplitBase<double>
    {
        public override void CalcBestSplit(AttributeBase<double> a)
        {
            double tmpQuality;
            double tmpThreshold;
            var b = a as NumericalAttribute;

            b.Values.Sort();

            for (int i = 0; i < b.Values.Count - 1; i++)
            {
                tmpThreshold = (b.Values[i] + b.Values[i + 1]) / 2;

                // !!!Возможна оптимизация. Не 2 прохода по массиву, а 1.!!!
                // !!!Пример b.Values.ForEach(e => ((e <= tmpThreshold) ? tmpSplits[0] : tmpSplits[1]).Add(e));!!!
                var firstSplit = b.Values.Where(e => e <= tmpThreshold).ToList();
                var secondSplit = b.Values.Where(e => e > tmpThreshold).ToList();
                var tmpSplits = new List<List<double>>() { firstSplit, secondSplit };

                tmpQuality = SplitQualityAlgorithm.CalcSplitQuality(tmpSplits);

                // !!!Необходима оптимизация. i == 0 вызывается только 1 раз!!!
                if (i == 0)
                    Fix(tmpQuality, tmpThreshold, tmpSplits);
                else if (SplitQualityAlgorithm.Compare(tmpQuality, Quality) < 0)
                    Fix(tmpQuality, tmpThreshold, tmpSplits);
            }
        }
    }
}
