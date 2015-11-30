using System.Data;

namespace DataMining.DecisionTree.LearningAlgorithm {
    public interface ILearningAlgorithm {
		CART Training(DataTable table);
    }
}