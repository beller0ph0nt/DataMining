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

    public abstract class AttributeBase
    {
        public int Id { get; private set; }
        public AttributType Type { get; private set; }
        public List<object> Values { get; private set; }

        public AttributeBase(int id, AttributType type, List<object> values)
        {
            Id = id;
            Type = type;
            Values = values;
        }

        public abstract List<AttributeBase> Split();
    }
}
