using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Attributes;

namespace DataMining.DecisionTree.LearningAlgorithm
{
    public interface ILearningAlgorithm
    {
        void Training(List<AttributeBase<object>> inputs);
		void Training(DataTable table);
    }
}
