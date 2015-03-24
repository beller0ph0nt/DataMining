using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Attributes;

namespace DataMining.DecisionTree.LearningAlgorithm
{
    public interface ILearningAlgorithm
    {
        void Training(List<List<double>> inputs, List<double> answers);
        void Training(List<AttributeBase> inputs, List<double> answers);
    }
}
