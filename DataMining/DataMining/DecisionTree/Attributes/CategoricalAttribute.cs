﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Splits;

namespace DataMining.DecisionTree.Attributes
{
    public class CategoricalAttribute : AbstractAttributeBase
    {
        public int CategoriesCounter { get; set; }
        public List<int> Values { get; set; }

        public override AbstractSplitBase GetBestSplit()
        {
            AbstractSplitBase split = new CategoricalSplit();

            split.CalcBestSplit(this);

            return split;
        }
    }
}
