using System.Data;

namespace DataMining.DecisionTree.LearningAlgorithm {
    public interface ILearningAlgorithm {
		void Training(DataTable table);
    }
}