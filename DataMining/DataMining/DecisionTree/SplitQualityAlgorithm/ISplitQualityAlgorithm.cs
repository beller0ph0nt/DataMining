using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Elements;

namespace DataMining.DecisionTree.SplitQualityAlgorithm
{
    public interface ISplitQualityAlgorithm<T>
    {
		// !!! СТАРЫЙ ВАРИАНТ УДАЛИТЬ !!!
        // вычисляет показатель качества разбиения
		double CalcSplitQuality(List<List<T>> splits);	// список разбиений

		// вычисляет показатель качества разбиения для числового аттрибута
		double CalcSplitQuality (List<DataTable> tables,	// набор
			DataColumn column);								// атрибут по которому производится вычисление индекса

        // сравнивает показатели качества разбиения
        // выход:
        //  -1 - первый показатель лучше второго
        //   0 - показатели равны
        //   1 - второй показатель лучше первого
		int Compare(double firstQuality,	// первый показатель
					double secondQuality);	// второй показатель
    }
}
