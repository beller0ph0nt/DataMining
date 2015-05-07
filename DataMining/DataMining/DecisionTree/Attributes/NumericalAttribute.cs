using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Splits;

namespace DataMining.DecisionTree.Attributes
{
    public class NumericalAttribute : AttributeBase<double>
    {
        public NumericalAttribute(int id, List<double> values)
            : base(id, AttributType.Numerical, values)
        { 
        }

        public override List<AttributeBase<double>> Split()
        {
            SplitBase<double> split = new NumericalSplit();

            split.CalcBestSplit(this.Values);

            return split.Splits.ConvertAll(l => (AttributeBase<double>)new NumericalAttribute(Id, l));
        }
    }
}
