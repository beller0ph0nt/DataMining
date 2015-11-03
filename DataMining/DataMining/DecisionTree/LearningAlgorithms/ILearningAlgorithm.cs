using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.LearningAlgorithm
{
    public interface ILearningAlgorithm
    {
		void Training(DataTable table);
    }
}
