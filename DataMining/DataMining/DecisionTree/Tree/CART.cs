using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataMining.DecisionTree {
    public class CART : ITree {
		private CARTRoot<Split> _root;
		private BinaryFormatter _formatter;

		public CARTRoot<Split> Root { get { return _root; } }

		public CART(CARTRoot<Split> root) {
			_root = root;
			_formatter = new BinaryFormatter();
        }

        public void Load(string file) {
			using (Stream fStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None))
				_root = (CARTRoot<Split>)_formatter.Deserialize (fStream);
		}

        public void Save(string file) {
			using (Stream fStream = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None))
				_formatter.Serialize (fStream, _root);
		}

        public void Calc(DataRow row) {
			object o = Search (row, _root);
			Console.WriteLine ("search [ DONE ]");
			if (o is int) {
				Console.WriteLine ("Class: " + o);
			} else if (o is double) {
				Console.WriteLine ("Regress: " + o);
			} else {
				Console.WriteLine ("exception");
				throw new System.Exception();
			}
		}

		private object Search(DataRow row, ICARTNode<Split> node) {
			Console.WriteLine ("searching...");
			if (node.Type == NodeType.Leaf) {
				Console.WriteLine ("class founded!");
				return node.Variable.ClassVal;
			} else if (node.Variable.IsLeftSplit (row)) {
				Console.WriteLine ("go to the left. " + node.Id + " -> " + node.Left.Id);
				return Search (row, node.Left);
			} else {
				Console.WriteLine ("go to the right. " + node.Id + " -> " + node.Right.Id);
				return Search (row, node.Right);
			}
		}
        
		public override string ToString() {
			Stack<ICARTNode<Split>> returnNodeStack = new Stack<ICARTNode<Split>> ();
			Stack<int> returnLevelStack = new Stack<int> ();
			int currentLevel = 0;
			Func<int, ICARTNode<Split>, Stack<int>, bool, string> outputFormat = (l, n, st, f) => {
				int spaces = 1;
				string str = "";
				for (int i = 1; i < l * (spaces + 1); i++) {
					str += (i % (spaces + 1) == 0 && st.Contains(i / (spaces + 1))) ? "|" : " ";
				}
				return str + string.Format (((f) ? "\u2514" : "\u251C") + "\u2500{0} [qlt: {1:0.000} thr: {2} rows: {3}]\n", n.ToString (), n.Variable.Quality, n.Variable.Threshold, n.Variable.Table.Rows.Count);
			};
			ICARTNode<Split> currentNode = _root;
			string s = string.Format ("{0}\n", currentNode.ToString ());
			while (currentNode != null) {
				while (currentNode.Right != null) {
					if (currentNode.Left != null) {
						returnNodeStack.Push (currentNode.Left);
						returnLevelStack.Push (currentLevel + 1);
					}
					currentNode = currentNode.Right;
					currentLevel++;
					s += outputFormat (currentLevel, currentNode, returnLevelStack, false);
				}
				if (returnNodeStack.Count > 0) {
					currentNode = returnNodeStack.Pop ();
					var levelStack = returnLevelStack;
					currentLevel = returnLevelStack.Pop ();
					s += outputFormat (currentLevel, currentNode, levelStack, true);
				} else {
					currentNode = null;
				}
			}
			return s;
		}
	}
}