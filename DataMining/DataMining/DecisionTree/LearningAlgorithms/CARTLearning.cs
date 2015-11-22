using System;
using System.Data;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining.DecisionTree.LearningAlgorithm {
    public class CARTLearning : ILearningAlgorithm {
        private ISplitQualityAlgorithm _qualityAlgo;

		public CARTLearning() {
			_qualityAlgo = new GiniSplit();
			//_qualityAlgo = new GiniSplitOptimized ();
        }

		public CARTTree<Split> Training(DataTable table) {
			ICARTNode<Split> root = CARTNodeFactory<Split>.GetRoot ();
			CreateTree (root, table);
			return new CARTTree<Split> ((CARTRoot<Split>)root);	//CARTTree<Split> tree = new CARTTree<Split> ((CARTRoot<Split>)root);
			//Console.Write (tree.ToString ());
		}

		private void CreateTree(ICARTNode<Split> node, DataTable table) {
			Split s = new Split (table, _qualityAlgo);
			s.CalcBestSplit ();
			node.Variable = s;
			Console.WriteLine (node.ToString() + " split count: " + s.Splits.Count + " quality: " + s.Quality);
			if (s.Splits.Count == 0 || s.Splits [0].Rows.Count == 0 || s.Splits [1].Rows.Count == 0) {
				if (node.Id == node.Parent.Right.Id) {
					node.Parent.Right = CARTNodeFactory<Split>.GetLeaf ();
					node.Parent.Right.Parent = node.Parent;
					node.Parent.Right.Variable = node.Variable;
					Console.WriteLine ("replace " + node.ToString () + " on " + node.Parent.Right.ToString ());
				} else if (node.Id == node.Parent.Left.Id) {
					node.Parent.Left = CARTNodeFactory<Split>.GetLeaf ();
					node.Parent.Left.Parent = node.Parent;
					node.Parent.Left.Variable = node.Variable;
					Console.WriteLine ("replace " + node.ToString () + " on " + node.Parent.Left.ToString ());
				}
				return;
			}
			Console.WriteLine ("\t1st split rows count: " + s.Splits [0].Rows.Count + " || 2nd split rows count: " + s.Splits [1].Rows.Count);
			if (s.Splits [1].Rows.Count > 0 && node.Right == null) {
				node.Right = CARTNodeFactory<Split>.GetNode ();
				node.Right.Parent = node;
				Console.WriteLine ("\tgo to the right branch");
				CreateTree (node.Right, s.Splits [1]);
			}
			if (s.Splits [0].Rows.Count > 0 && node.Left == null) {
				node.Left = CARTNodeFactory<Split>.GetNode ();
				node.Left.Parent = node;
				Console.WriteLine ("\tgo to the left branch");
				CreateTree (node.Left, s.Splits [0]);
			}
		}
    }
}