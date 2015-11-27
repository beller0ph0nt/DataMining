using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataMining.DecisionTree {
    public class CARTTree<T> : ITree {
		private CARTRoot<T> _root;
		private BinaryFormatter _formatter;

		public CARTTree(CARTRoot<T> root) {
			_root = root;
			_formatter = new BinaryFormatter();
        }

        public void Load(string file) {
			using (Stream fStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None))
				_root = (CARTRoot<T>)_formatter.Deserialize (fStream);
		}

        public void Save(string file) {
			using (Stream fStream = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None))
				_formatter.Serialize (fStream, _root);
		}

        public void Calc(DataRow row) {
			throw new System.NotImplementedException();
		}
        
		public override string ToString() {
			Stack<ICARTNode<T>> returnNodeStack = new Stack<ICARTNode<T>> ();
			Stack<int> returnLevelStack = new Stack<int> ();
			int currentLevel = 0;
			Func<int, ICARTNode<T>, int, string> outputFormat = (l, n, m) => {
				string str = "";
				for (int i = 1; i < l * 2; i++) {
					str += (i % 2 == 0 && i > (m - 1) * 2) ? "|" : " ";
				}
				return str + string.Format ("|--{0}\n", n.ToString ());
			};
			ICARTNode<T> currentNode = _root;
			string s = string.Format ("{0}\n", currentNode.ToString ());
			while (currentNode != null) {
				while (currentNode.Right != null) {
					if (currentNode.Left != null) {
						returnNodeStack.Push (currentNode.Left);
						returnLevelStack.Push (currentLevel + 1);
					}
					currentNode = currentNode.Right;
					currentLevel++;
					s += outputFormat (currentLevel, currentNode, returnLevelStack.Min<int>());
				}
				if (returnNodeStack.Count > 0) {
					currentNode = returnNodeStack.Pop ();
					int minLevel = returnLevelStack.Min<int> ();
					currentLevel = returnLevelStack.Pop ();
					s += outputFormat (currentLevel, currentNode, minLevel);
				} else {
					currentNode = null;
				}
			}
			return s;
		}
	}
}