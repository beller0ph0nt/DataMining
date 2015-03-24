using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Attributes;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining.DecisionTree.Splits
{
    public class CategoricalSplit : SplitBase
    {
        public override void CalcBestSplit(AttributeBase a)
        {
            double tmpQuality;
            var b = a as CategoricalAttribute;

            for (int set = 1; set < b.CategoriesCounter - 1; set++)
            {
                for (int j = 0; j < b.Values.Count; j++)
                {
                    var firstSplit = b.Values.Where(i => (~set & (int)i) == 0).ToList();
                    var secondSplit = b.Values.Where(i => (~set & (int)i) != 0).ToList();

                    tmpQuality = SplitQualityAlgorithm.CalcSplitQuality(new List<List<object>>() { firstSplit, secondSplit });

                    if (j == 0)
                    {
                        Quality = tmpQuality;
                        Threshold = set;
                        Splits.Add(firstSplit);
                        Splits.Add(secondSplit);
                    }
                    else 
                    {
                        if (SplitQualityAlgorithm.Compare(tmpQuality, Quality) < 0)
                        {
                            Quality = tmpQuality;
                            Threshold = set;
                            Splits[0] = firstSplit;
                            Splits[1] = secondSplit;
                        }
                    }
                }
            }
        }
    }
}
