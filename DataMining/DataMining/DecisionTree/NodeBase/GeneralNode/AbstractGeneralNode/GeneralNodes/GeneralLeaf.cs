﻿using System;

namespace DataMining.DecisionTree
{
    public class GeneralLeaf<T> : AbstractGeneralNode<T>
    {
        protected T _variable;

        public override T Variable { get { return _variable; } }

        public GeneralLeaf(int id, INodeBase<T> parent)
            : base(id, NodeType.Leaf, parent)
        {
        }

        public override IGeneralNode<T> CreateLeaf()
        {
            throw new MethodAccessException("Попытка создания дочернего листа у листа");
        }

        public override IGeneralNode<T> CreateNode()
        {
            throw new MethodAccessException("Попытка создания дочернего узла у листа");
        }
    }
}
