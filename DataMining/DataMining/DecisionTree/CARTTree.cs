using System;
using System.Linq;
using System.Collections.Generic;

namespace DataMining.DecisionTree
{
    // бинарное дерево CART
    public class CARTTree<T> : ITree
    {
        private IDictionary<int, ICARTNode<T>> _nodes;  // словарь всех узлов
        //private IDictionary<int, ICARTNode<T>> _leafs;  // словарь всех листов

        public CARTTree()
        {
            _nodes = new Dictionary<int, ICARTNode<T>>();
            //_leafs = new Dictionary<int, ICARTNode<T>>();
        }

        #region Методы

        public void Load(string file)
		{
			throw new System.NotImplementedException();
		}

        public void Save(string file)
		{
			throw new System.NotImplementedException();
		}

        public void Calc(List<double> input)
		{
			throw new System.NotImplementedException();
		}
        
		public override string ToString()
        {
            Stack<IBinaryNode<T>> returnNodeStack = new Stack<IBinaryNode<T>>();
            Stack<int> returnLevelStack = new Stack<int>();
            int currentLevel = 0;

            // функция вывода дочернего узла
            Func<int, ICARTNode<T>, string> outputFormat = (l, n) => 
                string.Format("").PadLeft(l << 1) +     // Умножаем кол-во лидирующий пробелов на 2
                string.Format("|_{0}\n", n.ToString());

            // делаем текущим узлом корень
            ICARTNode<T> currentNode = _nodes.Single(e => e.Value.Type == NodeType.Root).Value;
            string s = string.Format("{0}\n", currentNode.ToString());

            while (currentNode != null)
            {
                while (currentNode.Left != null)
                {
                    // сохраняем узел возврата и его уровень
                    if (currentNode.Right != null)
                    {
                        returnNodeStack.Push(currentNode.Right);
                        returnLevelStack.Push(currentLevel + 1);
                    }

                    // корректируем текущий узел и его уровень
                    currentNode = currentNode.Left as ICARTNode<T>;
                    currentLevel++;

                    // выводим текущий узел
                    s += outputFormat(currentLevel, currentNode);
                }

                // если остались узлы возврата
                if (returnNodeStack.Count > 0)
                {
                    currentNode = returnNodeStack.Pop() as ICARTNode<T>;
                    currentLevel = returnLevelStack.Pop();
                    s += outputFormat(currentLevel, currentNode);
                }
                else
                    currentNode = null;
            }
            
            return s;
        }
			
        // создает корень
        public int CreateRoot()
        {
            if (_nodes.Count(e => e.Value.Type == NodeType.Root) == 0)
            {
                var root = CARTNodeFactory<T>.GetRoot();

                _nodes[root.Id] = root;

                return root.Id;
            }
            else
                throw new InvalidOperationException("Корень уже создан");
        }
			
        // cоздает левый узел
		public int CreateLeftNode(int parentId)	// идентификатор родителя
        {
			/*
            if (_nodes.ContainsKey(parentId))   // если есть родитель
            {
                var node = _nodes[parentId].CreateLeftNode() as ICARTNode<T>;
                _nodes[node.Id] = node;

                return node.Id;
            }
            else
                throw new ArgumentOutOfRangeException("Не найден родительский узел с Id = " + parentId);
			*/
			throw new NotImplementedException ();
        }

        // создает правый узел
		public int CreateRightNode(int parentId)	// идентификатор родителя
        {
			/*
            if (_nodes.ContainsKey(parentId))   // если есть родитель
            {
                var node = _nodes[parentId].CreateRightNode() as ICARTNode<T>;
                _nodes[node.Id] = node;

                return node.Id;
            }
            else
                throw new ArgumentOutOfRangeException("Не найден родительский узел с Id = " + parentId);
			*/
			throw new NotImplementedException ();
        }
			
        // создает левый лист
		public int CreateLeftLeaf(int parentId)	// идентификатор родителя
        {
			/*
            if (_nodes.ContainsKey(parentId))   // если есть родитель
            {
                var leaf = _nodes[parentId].CreateLeftLeaf() as ICARTNode<T>;
                _leafs[leaf.Id] = leaf;

                return leaf.Id;
            }
            else
                throw new ArgumentOutOfRangeException("Не найден родительский узел с Id = " + parentId);
			*/
			throw new NotImplementedException ();
        }
			
        // создает правый лист
		public int CreateRightLeaf(int parentId)	// идентификатор родителя
        {
			/*
            if (_nodes.ContainsKey(parentId))   // если есть родитель
            {
                var leaf = _nodes[parentId].CreateRightLeaf() as ICARTNode<T>;
                _leafs[leaf.Id] = leaf;

                return leaf.Id;
            }
            else
                throw new ArgumentOutOfRangeException("Не найден родительский узел с Id = " + parentId);
			*/
			throw new NotImplementedException ();
        }

        #endregion
    }
}