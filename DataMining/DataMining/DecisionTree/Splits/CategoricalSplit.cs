using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Attributes;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining.DecisionTree.Splits
{
    public class CategoricalSplit : SplitBase<int>
    {
        public override void CalcBestSplit(List<int> a)
        {
            double tmpQuality;

            for (int set = 1; set < Categories - 1; set++)  // перебираем все катигории
            {
                for (int j = 0; j < a.Count; j++)
                {
                    List<List<int>> tmpSplits = new List<List<int>>();
                    tmpSplits.Add(new List<int>());   // Первое разбиение
                    tmpSplits.Add(new List<int>());   // Второе разбиение

                    a.ForEach(i => (((~set & i) == 0) ? tmpSplits[0] : tmpSplits[1]).Add(i));
                    
                    tmpQuality = SplitQualityAlgorithm.CalcSplitQuality(tmpSplits);

                    // !!!Необходима оптимизация. i == 0 вызывается только 1 раз!!!
                    if (j == 0)
                        Fix(tmpQuality, set, tmpSplits);
                    else if (SplitQualityAlgorithm.Compare(tmpQuality, Quality) < 0)
                        Fix(tmpQuality, set, tmpSplits);
                }
            }
        }
    }
}
