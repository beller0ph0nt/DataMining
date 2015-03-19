using System;
using System.Linq;
using System.Collections.Generic;

namespace DataMining.DecisionTree
{
    public class CARTTree<T> : ITree
    {
        private INodeFactory<T> _nodeFactory;
        private IDictionary<int, ICARTNode<T>> _nodes;

        public CARTTree()
        {
            _nodes = new Dictionary<int, ICARTNode<T>>();
            _nodeFactory = new CARTNodeFactory<T>();
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
            var root = _nodeFactory.GetRoot() as ICARTNode<T>;
            _nodes[root.Id] = root;

            return root.Id;
        }

        public int CreateNode(int parentId)
        {
            if (_nodes.ContainsKey(parentId))
                throw new ArgumentOutOfRangeException("Не найден родительский узел с Id = " + parentId);

            var node = _nodeFactory.GetNode(_nodes[parentId]) as ICARTNode<T>;
            _nodes[node.Id] = node;

            return node.Id;
        }

        public int CreateLeaf(int parentId)
        {
            if (_nodes.ContainsKey(parentId))
                throw new ArgumentOutOfRangeException("Не найден родительский узел с Id = " + parentId);

            var leaf = _nodeFactory.GetLeaf(_nodes[parentId]) as ICARTNode<T>;
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
