using System;
using System.Data;
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
			CARTTree<Split> tree = new CARTTree<Split> ((CARTRoot<Split>)root);
			Console.Write (tree.ToString ());
		}

		private void CreateTree(ICARTNode<Split> node, DataTable table) {
			Split s = new Split (table, _qualityAlgo);
			s.CalcBestSplit ();
			Console.WriteLine ("split list count: " + s.Splits.Count);
			if (s.Splits.Count == 0) {	//if (s.Splits [0].Rows.Count == 0 || s.Splits [1].Rows.Count == 0) {
				if (node.Id == node.Parent.Right.Id) {
					node.Parent.Right = CARTNodeFactory<Split>.GetLeaf ();
					node.Parent.Right.Parent = node.Parent;
					node.Parent.Right.Variable = node.Variable;
				} else if (node.Id == node.Parent.Left.Id) {
					node.Parent.Left = CARTNodeFactory<Split>.GetLeaf ();
					node.Parent.Left.Parent = node.Parent;
					node.Parent.Left.Variable = node.Variable;
				}
				Console.WriteLine ("RETURN");
				return;
			}
			if (s.Splits [1].Rows.Count > 0) {			// !!! повторяется бесконечно !!!
				node.Right = CARTNodeFactory<Split>.GetNode ();
				node.Right.Parent = node;
				Console.WriteLine ("go to the right branch");
				CreateTree (node.Right, s.Splits [1]);
			}
			node.Variable = s;
			if (s.Splits [0].Rows.Count > 0) {
				node.Left = CARTNodeFactory<Split>.GetNode ();
				node.Left.Parent = node;
				Console.WriteLine ("go to the left branch");
				CreateTree (node.Left, s.Splits [0]);
			}
		}
    }
}