using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining.DecisionTree.LearningAlgorithm {
    public class CARTLearning : ILearningAlgorithm {
        private ISplitQualityAlgorithm _qualityAlgo;
		private DataTable Table { get; set; }

		public CARTLearning() {
			_qualityAlgo = new GiniSplit();	// _qualityAlgo = new GiniSplitOptimized ();	//
        }

		public CART Training(DataTable table) {
			Table = table;
			ICARTNode<Split> root = CARTNodeFactory<Split>.GetRoot ();
			CreateTree (root, table);
			PruneTree (root);

			//CART t1 = new CART ((CARTRoot<Split>)root);
			//Console.WriteLine ("tree 1");
			//Console.WriteLine (t1.ToString ());

			//ICARTNode<Split> root2 = CopyTree ((CARTRoot<Split>)root);
			//CART t2 = new CART ((CARTRoot<Split>)root2);
			//Console.WriteLine ("tree 2");
			//Console.WriteLine (t2.ToString ());

            //return t1;

			return new CART ((CARTRoot<Split>)root);
		}

		private ICARTNode<Split> CopyTree(CARTRoot<Split> root)
		{
			//Console.WriteLine ("coping tree...");
			ICARTNode<Split> newRoot = CARTNodeFactory<Split>.GetRoot ();
			newRoot.Variable = root.Variable;
			CopyNode (root.Left, newRoot);
			CopyNode (root.Right, newRoot);
			return newRoot;
		}

		private void CopyNode(ICARTNode<Split> origNode, ICARTNode<Split> newParentNode)
		{
			//Console.WriteLine ("copy node " + origNode.Id);
			ICARTNode<Split> newNode = (origNode.Type == NodeType.Node) ? CARTNodeFactory<Split>.GetNode () : CARTNodeFactory<Split>.GetLeaf ();
			newNode.Variable = origNode.Variable;
			newNode.Parent = newParentNode;
			if (origNode.Id == origNode.Parent.Right.Id) {
				newParentNode.Right = newNode;
			} else if (origNode.Id == origNode.Parent.Left.Id) {
				newParentNode.Left = newNode;
			}
			if (origNode.Type != NodeType.Leaf) {
				CopyNode (origNode.Left, newNode);
				CopyNode (origNode.Right, newNode);
			}
		}

		private void CreateTree(ICARTNode<Split> node, DataTable table) {
			Split s = new Split (table, _qualityAlgo);
			s.CalcBestSplit ();
			node.Variable = s;
//			Console.WriteLine ("CUR_NODE=(" + node.ToString () + ") CUR_SPLIT=(count: " + s.Splits.Count + " quality: " + s.Quality + " threshold: " + s.Threshold + ") CUR_TABLE=(row count: " + table.Rows.Count + ")");
			if (s.IsEmpty()) {
				if (node.Type != NodeType.Root) {
					if (node.Id == node.Parent.Right.Id) {	//if (node.IsRight ()) {	//
						node.Parent.Right = CARTNodeFactory<Split>.GetLeaf ();
						node.Parent.Right.Parent = node.Parent;
						node.Parent.Right.Variable = node.Variable;
//						Console.WriteLine ("CUR_NODE=(" + node.ToString () + ")\treplace " + node.ToString () + " on " + node.Parent.Right.ToString ());
					} else if (node.Id == node.Parent.Left.Id) {	//} else if (node.IsLeft ()) {	//
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

		private void PruneTree(ICARTNode<Split> root) {
			while (PruneFirstStage(root));
			/*
			List<ICARTNode<Split>> treeSequense = new List<ICARTNode<Split>> ();
			treeSequense.Add (root);
			do {
				ICARTNode<Split> newRoot = CopyTree (treeSequense.Last ());
				PruneSecondStage(newRoot);

			} while(true);
			*/
		}

		private bool PruneFirstStage(ICARTNode<Split> node) {
			if (node.Type == NodeType.Node && node.Left.Type == NodeType.Leaf && node.Right.Type == NodeType.Leaf) {
				double err = ClassError (node, Table.Rows.Count);
				//Console.WriteLine ("node id: " + node.Id + " err: " + err);
				if (err == ClassError (node.Left, Table.Rows.Count) && err == ClassError (node.Right, Table.Rows.Count)) {
					//Console.WriteLine ("creating new leaf...");
					ICARTNode<Split> newLeaf = CARTNodeFactory<Split>.GetLeaf ();
					//Console.WriteLine ("set parent");
					newLeaf.Parent = node.Parent;
					//Console.WriteLine ("set var");
					newLeaf.Variable = node.Variable;
					if (node.Id == node.Parent.Left.Id) {	//if (node.IsLeft ()) {
						//Console.WriteLine ("set parent.left");
						node.Parent.Left = newLeaf;
					} else if (node.Id == node.Parent.Right.Id) {	//} else if (node.IsRight ()) {
						//Console.WriteLine ("set parent.right");
						node.Parent.Right = newLeaf;
					} else {
						throw new Exception ("exeption...");
					}
					return true;
				}
				return false;
			} else if (node.Type == NodeType.Leaf) {
				return false;
			} else {
				return PruneFirstStage (node.Left) || PruneFirstStage (node.Right);
			}
		}

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