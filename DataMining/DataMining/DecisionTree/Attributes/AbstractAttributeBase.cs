﻿using System;
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

    public abstract class AbstractAttributeBase
    {
        public int Id { get; set; }
        public AttributType Type { get; set; }

        public abstract AbstractSplitBase GetBestSplit();
    }
}
