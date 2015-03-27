using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Splits;

namespace DataMining.DecisionTree.Attributes
{
    public class CategoricalAttribute : AttributeBase
    {
        public int CategoriesCounter { get; private set; }

        public CategoricalAttribute(int id, List<object> values, int categoriesCounter)
            : base(id, AttributType.Categorical, values)
        {
            CategoriesCounter = categoriesCounter;
        }

        public override List<AttributeBase> Split()
        {
            SplitBase split = new CategoricalSplit();

            split.CalcBestSplit(this);

            return split.Splits.ConvertAll(l => (AttributeBase)new CategoricalAttribute(Id, l, CategoriesCounter));
        }
    }
}
