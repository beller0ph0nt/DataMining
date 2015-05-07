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
        public override void CalcBestSplit(List<double> a)
        {
            double tmpQuality;
            double tmpThreshold;

            a.Sort();

            for (int i = 0; i < a.Count - 1; i++)
            {
                tmpThreshold = (a[i] + a[i + 1]) / 2;

                List<List<double>> tmpSplits = new List<List<double>>();
                tmpSplits.Add(new List<double>());   // Первое разбиение
                tmpSplits.Add(new List<double>());   // Второе разбиение

                a.ForEach(e => ((e <= tmpThreshold) ? tmpSplits[0] : tmpSplits[1]).Add(e));

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
