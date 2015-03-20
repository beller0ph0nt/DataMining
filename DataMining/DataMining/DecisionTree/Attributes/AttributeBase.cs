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
        public int Id { get; set; }
        public AttributType Type { get; set; }
        public List<T> Values { get; set; }

        public abstract SplitBase<T> GetBestSplit();
    }
}
