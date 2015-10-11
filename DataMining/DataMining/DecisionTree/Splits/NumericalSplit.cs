using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Attributes;
using DataMining.DecisionTree.SplitQualityAlgorithm;
using DataMining.DecisionTree.Elements;

namespace DataMining.DecisionTree.Splits
{
    public class NumericalSplit : SplitBase<double>
    {
		public override void CalcBestSplit(List<double> a)
		{
			throw new NotImplementedException ();
		}

		/*
        public override void CalcBestSplit(List<double> a)
        //public override void CalcBestSplit(List<Cell> a)
        {
            double tmpQuality;
            double tmpThreshold;

            a.Sort();

            for (int i = 0; i < a.Count - 1; i++)
            {
                tmpThreshold = (a[i] + a[i + 1]) / 2;   // вычисляем порог, как среднее

                List<List<double>> tmpSplits = new List<List<double>>();    // выделяем память под временное разбиение
                tmpSplits.Add(new List<double>());                          // первое разбиение
                tmpSplits.Add(new List<double>());                          // второе разбиение

                // заполняем временное разбиение в соответствии с порогом
                a.ForEach(e => ((e <= tmpThreshold) ? tmpSplits[0] : tmpSplits[1]).Add(e));

                // вычисляем качество полученного разбиения
                tmpQuality = SplitQualityAlgorithm.CalcSplitQuality(tmpSplits);

                // !!!Необходима оптимизация. i == 0 вызывается только 1 раз!!!
                if (i == 0)
                    Fix(tmpQuality, tmpThreshold, tmpSplits);
                else if (SplitQualityAlgorithm.Compare(tmpQuality, Quality) < 0)
                    Fix(tmpQuality, tmpThreshold, tmpSplits);
            }
        }
        */
    }
}
