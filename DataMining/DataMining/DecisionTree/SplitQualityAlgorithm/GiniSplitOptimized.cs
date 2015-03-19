using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.SplitQualityAlgorithm
{
    public class GiniSplitOptimized : ISplitQualityAlgorithm
    {
        public double GiniIndex(List<double> split)
        {
            return split.
                GroupBy(k => k, (k, e) => new { Count = e.Count() }).
                Sum(a => a.Count * a.Count);
        }

        public double CalcSplitQuality(List<List<double>> splits, int generalSplitCount)
        {
            return splits.Sum(a => GiniIndex(a) / a.Count);
        }

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
