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
        private IDictionary<int, ICARTNode<T>> _nodes;      // Словарь всех узлов в дереве

        public CARTTree()
        {
            _nodes = new Dictionary<int, ICARTNode<T>>();
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

        public int CreateRoot()
        {
            var root = CARTNodeFactory<T>.GetRoot() as ICARTNode<T>;
            _nodes[root.Id] = root;

            return root.Id;
        }

        public int CreateNode(int parentId)
        {
            if (_nodes.ContainsKey(parentId))
                throw new ArgumentOutOfRangeException("Не найден родительский узел с Id = " + parentId);

            var node = CARTNodeFactory<T>.GetNode(_nodes[parentId]) as ICARTNode<T>;
            _nodes[node.Id] = node;

            return node.Id;
        }

        public int CreateLeaf(int parentId)
        {
            if (_nodes.ContainsKey(parentId))
                throw new ArgumentOutOfRangeException("Не найден родительский узел с Id = " + parentId);

            var leaf = CARTNodeFactory<T>.GetLeaf(_nodes[parentId]) as ICARTNode<T>;
            _nodes[leaf.Id] = leaf;

            return leaf.Id;
        }

        public override string ToString()
        {
            var root = _nodes.Single(e => e.Value.Type == NodeType.Root).Value;

            return root.ToString();
        }

        #endregion
    }
}
