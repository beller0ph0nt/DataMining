using System;

namespace DataMining.DecisionTree
{
    public class CARTLeaf<T> : AbstractCARTNode<T>
    {
		protected T _variable;	// хранимое значение в листе

        public override T Variable { get { return _variable; } }

        public CARTLeaf(int id, INodeBase<T> parent)
            : base(id, NodeType.Leaf, parent)
        {
        }

        public override IBinaryNode<T> CreateLeftLeaf()
        {
            throw new MethodAccessException("Попытка создания левого узла у CART-листа");
        }

        public override IBinaryNode<T> CreateLeftNode()
        {
            throw new MethodAccessException("Попытка создания левого листа у CART-листа");
        }

        public override IBinaryNode<T> CreateRightLeaf()
        {
            throw new MethodAccessException("Попытка создания правого листа у CART-листа");
        }

        public override IBinaryNode<T> CreateRightNode()
        {
            throw new MethodAccessException("Попытка создания правого узла у CART-листа");
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", Type.ToString(), Id, _variable);
        }
    }
}
