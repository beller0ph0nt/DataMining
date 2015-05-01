using System;
using System.Linq;
using System.Collections.Generic;

namespace DataMining.DecisionTree
{
    /// <summary>
    /// Бинарное дерево CART
    /// </summary>
    /// <typeparam name="T">Тип даных, содержащийся в узле</typeparam>
    public class CARTTree<T> : ITree
    {
        private IDictionary<int, ICARTNode<T>> _nodes;  // Словарь всех узлов
        private IDictionary<int, ICARTNode<T>> _leafs;  // Словарь всех листов

        public CARTTree()
        {
            _nodes = new Dictionary<int, ICARTNode<T>>();
            _leafs = new Dictionary<int, ICARTNode<T>>();
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
            string s = "";
            // Сделать текущим узлом - корень
            ICARTNode<T> currentNode = _nodes.Single(e => e.Value.Type == NodeType.Root).Value;

            // Вывести информацию о текущем узле
            s += string.Format("{0}:{1}",currentNode.Type.ToString() , currentNode.Id) + "\n";

            while (currentNode.Type != NodeType.Leaf)
            {
                currentNode = currentNode.Left as ICARTNode<T>;
                s += string.Format("{0}:{1}", currentNode.Type.ToString(), currentNode.Id) + "\n";
            }

            currentNode = currentNode.Parent.Right as ICARTNode<T>;

            //s += " |_" + ((_left != null) ? _left.ToString() : "null") + "\n";
            //s += " |_" + ((_right != null) ? _right.ToString() : "null") + "\n";

            // Вывести информацию о левом элементе текущего узла

            // Сделать текущим элементом левый элемент

            // Вывести правый элемент

            return _nodes.Single(e => e.Value.Type == NodeType.Root).Value.ToString();
        }

        /// <summary>
        /// Создает корень
        /// </summary>
        /// <returns>Идентификатор созданного корня</returns>
        public int CreateRoot()
        {
            var root = CARTNodeFactory<T>.GetRoot();

            _nodes[root.Id] = root;

            return root.Id;
        }

        /// <summary>
        /// Создает левый узел
        /// </summary>
        /// <param name="parentId">Идентификатор родителя</param>
        /// <returns>Идентификатор созданного узла</returns>
        public int CreateLeftNode(int parentId)
        {
            if (_nodes.ContainsKey(parentId))   // Если есть родитель
            {
                var node = _nodes[parentId].CreateLeftNode() as ICARTNode<T>;
                _nodes[node.Id] = node;

                return node.Id;
            }
            else
                throw new ArgumentOutOfRangeException("Не найден родительский узел с Id = " + parentId);
        }

        /// <summary>
        /// Создает правый узел
        /// </summary>
        /// <param name="parentId">Идентификатор родителя</param>
        /// <returns>Идентификатор созданного узла</returns>
        public int CreateRightNode(int parentId)
        {
            if (_nodes.ContainsKey(parentId))   // Если есть родитель
            {
                var node = _nodes[parentId].CreateRightNode() as ICARTNode<T>;
                _nodes[node.Id] = node;

                return node.Id;
            }
            else
                throw new ArgumentOutOfRangeException("Не найден родительский узел с Id = " + parentId);
        }

        /// <summary>
        /// Создает левый лист
        /// </summary>
        /// <param name="parentId">Идентификатор родителя</param>
        /// <returns>Идентификатор созданного листа</returns>
        public int CreateLeftLeaf(int parentId)
        {
            if (_nodes.ContainsKey(parentId))   // Если есть родитель
            {
                var leaf = _nodes[parentId].CreateLeftLeaf() as ICARTNode<T>;
                _leafs[leaf.Id] = leaf;

                return leaf.Id;
            }
            else
                throw new ArgumentOutOfRangeException("Не найден родительский узел с Id = " + parentId);
        }

        /// <summary>
        /// Создает правый лист
        /// </summary>
        /// <param name="parentId">Идентификатор родителя</param>
        /// <returns>Идентификатор созданного листа</returns>
        public int CreateRightLeaf(int parentId)
        {
            if (_nodes.ContainsKey(parentId))   // Если есть родитель
            {
                var leaf = _nodes[parentId].CreateRightLeaf() as ICARTNode<T>;
                _leafs[leaf.Id] = leaf;

                return leaf.Id;
            }
            else
                throw new ArgumentOutOfRangeException("Не найден родительский узел с Id = " + parentId);
        }

        #endregion
    }
}
