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
			throw new System.NotImplementedException();
		}
        
		public override string ToString() {
			Stack<ICARTNode<Split>> returnNodeStack = new Stack<ICARTNode<Split>> ();
			Stack<int> returnLevelStack = new Stack<int> ();
			int currentLevel = 0;
			Func<int, ICARTNode<Split>, int, bool, string> outputFormat = (l, n, m, f) => {
				string str = "";
				for (int i = 1; i < l * 2; i++) {
					str += (i % 2 == 0 && i > (m - 1) * 2) ? "|" : " ";
				}
				return str + string.Format (((f) ? "\u2514" : "\u251C") + "\u2500{0} [qlt: {1:0.000} thr: {2}]\n", n.ToString (), n.Variable.Quality, n.Variable.Threshold);
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
					s += outputFormat (currentLevel, currentNode, returnLevelStack.Min<int>(), false);
				}
				if (returnNodeStack.Count > 0) {
					currentNode = returnNodeStack.Pop ();
					int minLevel = returnLevelStack.Min<int> ();
					currentLevel = returnLevelStack.Pop ();
					s += outputFormat (currentLevel, currentNode, minLevel, true);
				} else {
					currentNode = null;
				}
			}
			return s;
		}
	}
}