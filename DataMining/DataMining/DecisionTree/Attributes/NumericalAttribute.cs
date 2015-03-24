using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Splits;

namespace DataMining.DecisionTree.Attributes
{
    public class NumericalAttribute : AttributeBase
    {
        public NumericalAttribute(int id, List<object> values)
            : base(id, AttributType.Numerical, values)
        { 
        }

        public override List<AttributeBase> Split()
        {
            SplitBase split = new NumericalSplit();

            split.CalcBestSplit(this);

            //return split;

            throw new NotImplementedException();
        }
    }
}
