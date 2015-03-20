using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.SplitQualityAlgorithm
{
    public interface ISplitQualityAlgorithm<T>
    {
        double CalcSplitQuality(List<List<T>> splits);
        int Compare(double firstQuality, double secondQuality);
    }
}
