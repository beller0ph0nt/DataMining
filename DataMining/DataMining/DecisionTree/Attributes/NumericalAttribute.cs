using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Splits;

namespace DataMining.DecisionTree.Attributes
{
    public class NumericalAttribute : AttributeBase<double>
    {
        public override SplitBase<double> GetBestSplit()
        {
            SplitBase<double> split = new NumericalSplit();

            split.CalcBestSplit(this);

            return split;
        }
    }
}
