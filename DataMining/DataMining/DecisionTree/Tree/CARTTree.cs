using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace DataMining.DecisionTree
{
    // бинарное дерево CART
    public class CARTTree<T> : ITree
    {
		private CARTRoot<T> _root;
        //private IDictionary<int, ICARTNode<T>> _leafs;  // словарь всех листов

        public CARTTree()
        {
        }

        public void Load(string file)
		{
			throw new System.NotImplementedException();
		}

        public void Save(string file)
		{
			throw new System.NotImplementedException();
		}

        public void Calc(DataRow input)
		{
			throw new System.NotImplementedException();
		}
        
		public override string ToString()
        {
			Stack<ICARTNode<T>> returnNodeStack = new Stack<ICARTNode<T>>();
            Stack<int> returnLevelStack = new Stack<int>();
            int currentLevel = 0;

            // функция вывода дочернего узла
            Func<int, ICARTNode<T>, string> outputFormat = (l, n) => string.Format("").PadLeft(l << 1) + string.Format("|_{0}\n", n.ToString());

            // делаем текущим узлом корень
			ICARTNode<T> currentNode = _root;
            string s = string.Format("{0}\n", currentNode.ToString());

            while (currentNode != null)
            {
				while (currentNode.Right != null)
                {	// сохраняем узел возврата и его уровень
					if (currentNode.Left != null)
                    {
						returnNodeStack.Push(currentNode.Left);
                        returnLevelStack.Push(currentLevel + 1);
                    }

                    // корректируем текущий узел и его уровень
					currentNode = currentNode.Right;
                    currentLevel++;

                    // выводим текущий узел
                    s += outputFormat(currentLevel, currentNode);
                }

                // если остались узлы возврата
                if (returnNodeStack.Count > 0)
                {
                    currentNode = returnNodeStack.Pop();
                    currentLevel = returnLevelStack.Pop();
                    s += outputFormat(currentLevel, currentNode);
                }
                else
                    currentNode = null;
            }
            
            return s;
        }
    }
}