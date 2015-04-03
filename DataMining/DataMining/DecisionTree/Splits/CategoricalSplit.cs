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
        public override void CalcBestSplit(AttributeBase<int> a)
        {
            double tmpQuality;
            var b = a as CategoricalAttribute;

            for (int set = 1; set < b.CategoriesCounter - 1; set++)
            {
                for (int j = 0; j < b.Values.Count; j++)
                {
                    // !!!Возможна оптимизация. Не 2 прохода по массиву, а 1.!!!
                    // !!!Пример b.Values.ForEach(i => (((~set & i) == 0)) ? tmpSplits[0] : tmpSplits[1]).Add(i));!!!
                    var firstSplit = b.Values.Where(i => (~set & i) == 0).ToList();
                    var secondSplit = b.Values.Where(i => (~set & i) != 0).ToList();
                    var tmpSplits = new List<List<int>>() { firstSplit, secondSplit };

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
