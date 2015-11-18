using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining.DecisionTree.LearningAlgorithm
{
    public class CARTLearning : ILearningAlgorithm
    {
        private ISplitQualityAlgorithm _splitQuality;

		public CARTLearning()
        {
            _splitQuality = new GiniSplit();
        }

		public void Training(DataTable table)
		{
			// !!! после обучения должен выплюнуть готовое дерево CARTTree<Split> _tree !!!

			ICARTNode<Split> root = CARTNodeFactory<Split>.GetRoot ();
			CreateTree (root, table);
		}

		private void CreateTree(ICARTNode<Split> node, DataTable table)
		{
			Split s = new Split (table, _splitQuality);
			node.Variable = s;
		}
    }
}