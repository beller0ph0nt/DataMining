using System;

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

        public override void CreateLeftChild(IBinaryNode<T> child)
        {
            throw new MethodAccessException("Попытка создания левого потомка у бинарного листа");
        }

        public override void CreateRightChild(IBinaryNode<T> child)
        {
            throw new MethodAccessException("Попытка создания правого потомка у бинарного листа");
        }
    }
}
