using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Elements;

namespace DataMining.DecisionTree.SplitQualityAlgorithm
{
    public interface ISplitQualityAlgorithm<T>
    {
        // вычисляет показатель качества разбиения
		double CalcSplitQuality(List<List<T>> splits);	// список разбиений

        // сравнивает показатели качества разбиения
        // выход:
        //  -1 - первый показатель лучше второго
        //   0 - показатели равны
        //   1 - первый показатель хуже второго
		int Compare(double firstQuality,	// первый показатель
					double secondQuality);	// второй показатель
    }
}
