using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.SplitQualityAlgorithm
{
    public interface ISplitQualityAlgorithm
    {
        //double Quality { get; set; }

        double CalcSplitQuality(List<List<double>> splits, int generalSplitCount);
        int Compare(double firstQuality, double secondQuality);
        //int CompareTo(object obj);
    }
}
