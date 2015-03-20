using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Splits;

namespace DataMining.DecisionTree.Attributes
{
    public class CategoricalAttribute : AttributeBase<int>
    {
        public int CategoriesCounter { get; private set; }

        public override SplitBase<int> GetBestSplit()
        {
            SplitBase<int> split = new CategoricalSplit();

            split.CalcBestSplit(this);

            return split;
        }
    }
}
