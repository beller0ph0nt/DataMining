using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.SplitQualityAlgorithm
{
    public interface ISplitQualityAlgorithm
    {
		double CalcSplitQuality (List<DataTable> tables, DataColumn column);
		int Compare(double firstQuality, double secondQuality);
		bool IsTheBest (double quality);
    }
}
