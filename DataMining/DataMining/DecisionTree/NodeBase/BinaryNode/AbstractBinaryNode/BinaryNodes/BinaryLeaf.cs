﻿using System;

namespace DataMining.DecisionTree
{
    public class BinaryLeaf<T> : AbstractBinaryNode<T>
    {
        protected T _variable;

        public override T Variable { get { return _variable; } }

        public BinaryLeaf(int id, INodeBase<T> parent)
            : base(id, NodeType.Leaf, parent)
        {
        }

        #region Методы

        public override IBinaryNode<T> CreateLeftNode()
        {
            throw new MethodAccessException("Попытка создания левого узла у бинарного листа");
        }

        public override IBinaryNode<T> CreateLeftLeaf()
        {
            throw new MethodAccessException("Попытка создания левого листа у бинарного листа");
        }

        public override IBinaryNode<T> CreateRightNode()
        {
            throw new MethodAccessException("Попытка создания правого узла у бинарного листа");
        }

        public override IBinaryNode<T> CreateRightLeaf()
        {
            throw new MethodAccessException("Попытка создания правого листа у бинарного листа");
        }

        #endregion
    }
}