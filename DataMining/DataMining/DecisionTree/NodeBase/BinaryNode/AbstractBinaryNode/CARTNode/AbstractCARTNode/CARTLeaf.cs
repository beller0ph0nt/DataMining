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

        /// <summary>
        /// Метод, создающий левого потомка. Но лист не может иметь потомков. Он бездетный
        /// </summary>
        /// <param name="child">Потомок, которог пытаются выдать за ребенка листа</param>
        public override void CreateLeftChild(IBinaryNode<T> child)
        {
            throw new MethodAccessException("Попытка создания левого потомка у CART листа. Все листы - бездетные");
        }

        /// <summary>
        /// Метод, создающий правого потомка. Но лист не может иметь потомков. Он бездетный
        /// </summary>
        /// <param name="child">Потомок, которог пытаются выдать за ребенка листа</param>
        public override void CreateRightChild(IBinaryNode<T> child)
        {
            throw new MethodAccessException("Попытка создания правого потомка у CART листа. Все листы - бездетные");
        }

        #endregion
    }
}
