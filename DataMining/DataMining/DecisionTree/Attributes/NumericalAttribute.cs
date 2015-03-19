using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Splits;

namespace DataMining.DecisionTree.Attributes
{
    public class NumericalAttribute : AbstractAttributeBase
    {
        public List<double> Values { get; set; }

        public override AbstractSplitBase GetBestSplit()
        {
            AbstractSplitBase split = new NumericalSplit();

            split.CalcBestSplit(this);

            return split;
        }
    }
}
