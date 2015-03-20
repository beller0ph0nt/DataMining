using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.SplitQualityAlgorithm
{
    public class GiniSplit<T> : ISplitQualityAlgorithm<T>
    {
        public double GiniIndex(List<T> split)
        {
            double count = split.Count;

            return 1 - split.
                GroupBy(k => k, (k, e) => new { Count = e.Count() }).
                Sum(a => Math.Pow(a.Count / count, 2));
        }

        public double CalcSplitQuality(List<List<T>> splits)
        {
            double totalCount = splits.Sum(l => l.Count);
            
            return splits.Sum(a => a.Count * GiniIndex(a) / totalCount);
        }

        public int Compare(double firstQuality, double secondQuality)
        {
            if (firstQuality > secondQuality)
                return 1;
            else if (firstQuality < secondQuality)
                return -1;
            else
                return 0;
        }
    }
}
