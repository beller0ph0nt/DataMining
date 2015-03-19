using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.SplitQualityAlgorithm
{
    public class GiniSplit : ISplitQualityAlgorithm
    {
        public double GiniIndex(List<double> split)
        {
            double count = split.Count;

            return 1 - split.
                GroupBy(k => k, (k, e) => new { Count = e.Count() }).
                Sum(a => Math.Pow(a.Count / count, 2));
        }

        public double CalcSplitQuality(List<List<double>> splits, int parentDataSetCount)
        {
            double N = parentDataSetCount;
            
            return splits.Sum(a => a.Count * GiniIndex(a) / N);
        }

        public int Compare(double quality1, double quality2)
        {
            if (quality1 > quality2)
                return 1;
            else if (quality1 < quality2)
                return -1;
            else
                return 0;
        }
    }
}
