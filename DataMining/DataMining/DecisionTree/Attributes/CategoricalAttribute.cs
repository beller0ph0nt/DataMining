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

        public CategoricalAttribute(int id, List<int> values, int categoriesCounter)
            : base(id, AttributType.Categorical, values)
        {
            CategoriesCounter = categoriesCounter;
        }

        public override List<AttributeBase<int>> Split()
        {
            SplitBase<int> split = new CategoricalSplit();

            split.CalcBestSplit(this);

            return split.Splits.ConvertAll(l => (AttributeBase<int>)new CategoricalAttribute(Id, l, CategoriesCounter));
        }
    }
}
