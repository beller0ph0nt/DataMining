using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree.LearningAlgorithm
{
    public interface ILearningAlgorithm
    {
        void Training(List<List<double>> inputs, List<double> answers);
    }
}
