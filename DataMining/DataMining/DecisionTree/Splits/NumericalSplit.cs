using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Attributes;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining.DecisionTree.Splits
{
    public class NumericalSplit : SplitBase
    {
        public override void CalcBestSplit(AttributeBase a)
        {
            double tmpQuality;
            double tmpThreshold;
            var b = a as NumericalAttribute;

            b.Values.Sort();

            for (int i = 0; i < b.Values.Count - 1; i++)
            {
                tmpThreshold = ((double)b.Values[i] + (double)b.Values[i + 1]) / 2;

                var firstSplit = b.Values.Where(e => (double)e <= tmpThreshold).ToList();
                var secondSplit = b.Values.Where(e => (double)e > tmpThreshold).ToList();

                tmpQuality = SplitQualityAlgorithm.CalcSplitQuality(new List<List<object>>() { firstSplit, secondSplit });

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
