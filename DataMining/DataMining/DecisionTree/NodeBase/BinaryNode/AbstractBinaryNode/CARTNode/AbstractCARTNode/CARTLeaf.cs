using System;

namespace DataMining.DecisionTree
{
    public class CARTLeaf<T> : AbstractCARTNode<T>
    {
        protected T _variable;

        #region Свойства

        /// <summary>
        /// Хранимое значение в листе
        /// </summary>
        public override T Variable { get { return _variable; } }

        #endregion

        public CARTLeaf(int id, INodeBase<T> parent)
            : base(id, NodeType.Leaf, parent)
        {
        }

        #region Методы

        public override void CreateLeftLeaf()
        {
            throw new MethodAccessException("Попытка создания левого узла у CART-листа");
        }

        public override void CreateLeftNode()
        {
            throw new MethodAccessException("Попытка создания левого листа у CART-листа");
        }

        public override void CreateRightLeaf()
        {
            throw new MethodAccessException("Попытка создания правого листа у CART-листа");
        }

        public override void CreateRightNode()
        {
            throw new MethodAccessException("Попытка создания правого узла у CART-листа");
        }

        #endregion
    }
}
