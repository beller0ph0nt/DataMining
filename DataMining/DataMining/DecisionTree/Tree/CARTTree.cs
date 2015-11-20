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

        public void Calc(DataRow input) {
			throw new System.NotImplementedException();
		}
        
		public override string ToString() {
			Stack<ICARTNode<T>> returnNodeStack = new Stack<ICARTNode<T>> ();
			Stack<int> returnLevelStack = new Stack<int> ();
			int currentLevel = 0;
			// функция вывода дочернего узла
			Func<int, ICARTNode<T>, string> outputFormat = (l, n) => string.Format ("").PadLeft (l << 1) + string.Format ("|_{0}\n", n.ToString ());
			// делаем текущим узлом корень
			ICARTNode<T> currentNode = _root;
			string s = string.Format ("{0}\n", currentNode.ToString ());
			while (currentNode != null) {
				while (currentNode.Right != null) {
					if (currentNode.Left != null) {
						returnNodeStack.Push (currentNode.Left);
						returnLevelStack.Push (currentLevel + 1);
					}
					// корректируем текущий узел и его уровень
					currentNode = currentNode.Right;
					currentLevel++;
					// выводим текущий узел
					s += outputFormat (currentLevel, currentNode);
				}
				// если остались узлы возврата
				if (returnNodeStack.Count > 0) {
					currentNode = returnNodeStack.Pop ();
					currentLevel = returnLevelStack.Pop ();
					s += outputFormat (currentLevel, currentNode);
				} else {
					currentNode = null;
				}
			}
			return s;
		}
	}
}