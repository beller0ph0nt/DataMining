using System.Data;

namespace DataMining.DecisionTree.LearningAlgorithm {
    public interface ILearningAlgorithm {
		CARTTree<Split> Training(DataTable table);
    }
}