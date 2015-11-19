using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining.DecisionTree.LearningAlgorithm {
    public class CARTLearning : ILearningAlgorithm {
        private ISplitQualityAlgorithm _qualityAlgo;

		public CARTLearning() {
            _qualityAlgo = new GiniSplit();
        }

		public void Training(DataTable table) {
			// !!! после обучения должен выплюнуть готовое дерево CARTTree<Split> _tree !!!

			ICARTNode<Split> root = CARTNodeFactory<Split>.GetRoot ();
			CreateTree (root, table);
		}

		private void CreateTree(ICARTNode<Split> node, DataTable table) {
			Split s = new Split (table, _qualityAlgo);
			if (s.Splits [1].Rows.Count > 0) {
				node.Right = CARTNodeFactory<Split>.GetNode ();
				CreateTree (node.Right, s.Splits [1]);
			} else {
				if (node.Id == node.Parent.Right.Id) {
					node.Parent.Right = CARTNodeFactory<Split>.GetLeaf ();
					node.Parent.Right.Variable = node.Variable;
				} else if (node.Id == node.Left.Id) {
					node.Parent.Left = CARTNodeFactory<Split>.GetLeaf ();
					node.Parent.Left.Variable = node.Variable;
				}
				return;
			}
			node.Variable = s;
			if (s.Splits [0].Rows.Count > 0) {
				node.Left = CARTNodeFactory<Split>.GetNode ();
				CreateTree (node.Left, s.Splits [0]);
			} else {
				if (node.Id == node.Parent.Right.Id) {
					node.Parent.Right = CARTNodeFactory<Split>.GetLeaf ();
					node.Parent.Right.Variable = node.Variable;
				} else if (node.Id == node.Left.Id) {
					node.Parent.Left = CARTNodeFactory<Split>.GetLeaf ();
					node.Parent.Left.Variable = node.Variable;
				}
			}
		}
    }
}