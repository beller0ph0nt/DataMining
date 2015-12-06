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
        // сравнивает показатели качества разбиения
        // выход:
        //  -1 - первый показатель лучше второго
        //   0 - показатели равны
        //   1 - второй показатель лучше первого
		int Compare(double firstQuality, double secondQuality);
    }
}
