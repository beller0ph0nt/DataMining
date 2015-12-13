using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining.DecisionTree.LearningAlgorithm {
    public class CARTLearning : ILearningAlgorithm {
        private ISplitQualityAlgorithm _qualityAlgo;

		public CARTLearning() {
			_qualityAlgo = new GiniSplit();	// _qualityAlgo = new GiniSplitOptimized ();	//
        }

		public CART Training(DataTable table) {
			ICARTNode<Split> root = CARTNodeFactory<Split>.GetRoot ();
			CreateTree (root, table);
			return new CART ((CARTRoot<Split>)root);
		}

		private void CreateTree(ICARTNode<Split> node, DataTable table) {
			Split s = new Split (table, _qualityAlgo);
			s.CalcBestSplit ();
			node.Variable = s;
//			Console.WriteLine ("CUR_NODE=(" + node.ToString () + ") CUR_SPLIT=(count: " + s.Splits.Count + " quality: " + s.Quality + " threshold: " + s.Threshold + ") CUR_TABLE=(row count: " + table.Rows.Count + ")");
			if (s.IsEmpty()) {
				if (node.Type != NodeType.Root) {
					if (node.Id == node.Parent.Right.Id) {
						node.Parent.Right = CARTNodeFactory<Split>.GetLeaf ();
						node.Parent.Right.Parent = node.Parent;
						node.Parent.Right.Variable = node.Variable;
//						Console.WriteLine ("CUR_NODE=(" + node.ToString () + ")\treplace " + node.ToString () + " on " + node.Parent.Right.ToString ());
					} else if (node.Id == node.Parent.Left.Id) {
						node.Parent.Left = CARTNodeFactory<Split>.GetLeaf ();
						node.Parent.Left.Parent = node.Parent;
						node.Parent.Left.Variable = node.Variable;
//						Console.WriteLine ("CUR_NODE=(" + node.ToString () + ")\treplace " + node.ToString () + " on " + node.Parent.Left.ToString ());
					}
				}
				return;
			}
//			Console.WriteLine ("CUR_NODE=(" + node.ToString() + ")\tCUR_SPLIT=(left rows count: " + s.Splits [0].Rows.Count + " right rows count: " + s.Splits [1].Rows.Count + ")");
			if (s.Splits [1].Rows.Count > 0) {
				node.Right = CARTNodeFactory<Split>.GetNode ();
//				Console.WriteLine ("CUR_NODE=(" + node.ToString() + ")\tcreate right " + node.Right.ToString () + "\tgo to the right branch");
				node.Right.Parent = node;
				CreateTree (node.Right, s.Splits [1]);
			}
			if (s.Splits [0].Rows.Count > 0) {
				node.Left = CARTNodeFactory<Split>.GetNode ();
//				Console.WriteLine ("CUR_NODE=(" + node.ToString() + ")\tcreate left " + node.Left.ToString () + "\tgo to the left branch");
				node.Left.Parent = node;
				CreateTree (node.Left, s.Splits [0]);
			}
		}

		private void PruneTree(ICARTNode<Split> node) {
			throw new NotImplementedException ();
		}

		/*
		private bool PruneFirstStage(ICARTNode<Split> node) {
			if (node.Type == NodeType.Node) {
				if (node.Left.Type == NodeType.Leaf && node.Right.Type == NodeType.Leaf) {
					if () {
					}
				}
			} else if (node == null) {
			} else {
			}
		}
		*/

		private void PruneSecondStage(ICARTNode<Split> node) {
			throw new NotImplementedException ();
		}

		private double ClassError(ICARTNode<Split> node, int rows) {
			return (double)WrongClassCount (node) / rows;
		}

		public int WrongClassCount(ICARTNode<Split> node) {
			if (node.Type == NodeType.Leaf) {
				if (node.Variable.IsClassification ()) {
					return node.Variable.Table.AsEnumerable ().
						Where (row => (int)row [node.Variable.Table.Columns.Count - 1] != (int)node.Variable.ClassVal).Count ();
				} else if (node.Variable.IsRegression ()) {
					throw new NotImplementedException ();
				} else {
					return 0;
				}
			} else if (node == null) {
				return 0;
			} else {
				return WrongClassCount (node.Left) + WrongClassCount (node.Right);
			}
		}

		private void SelectFinalTree(List<ICARTNode<Split>> l) {
			throw new NotImplementedException ();
		}
    }
}