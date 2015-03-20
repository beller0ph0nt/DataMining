using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Splits;

namespace DataMining.DecisionTree.Attributes
{
    public enum AttributType
    {
        Numerical,
        Categorical
    }

    public abstract class AttributeBase<T>
    {
        public int Id { get; private set; }
        public AttributType Type { get; private set; }
        public List<T> Values { get; private set; }

        public abstract SplitBase<T> GetBestSplit();
    }
}
