
namespace DataMining.DecisionTree
{
    public abstract class AbstractBinaryNode<T> : AbstractNodeBase<T>, IBinaryNode<T>
    {
        protected IBinaryNode<T> _left;
        protected IBinaryNode<T> _right;

        #region Свойства

        /// <summary>
        /// Ссылка на родителя, преобразованая к интерфейсу IBinaryNode
        /// </summary>
        public new IBinaryNode<T> Parent { get { return base.Parent as IBinaryNode<T>; } }

        /// <summary>
        /// Ссылка на левого потомка
        /// </summary>
        public IBinaryNode<T> Left { get { return _left; } }

        /// <summary>
        /// Ссылка на правого потомка
        /// </summary>
        public IBinaryNode<T> Right { get { return _right; } }

        #endregion

        public AbstractBinaryNode(int id, NodeType type, INodeBase<T> parent)
            : base(id, type, parent)
        {
        }

        #region Методы

        /// <summary>
        /// Метод, создающий левого потомка
        /// </summary>
        /// <param name="child">левый потомок</param>
        public virtual void CreateLeftChild(IBinaryNode<T> child)
        {
            _left = child;
        }

        /// <summary>
        /// Метод, создающий правого потомка
        /// </summary>
        /// <param name="child">правый потомок</param>
        public virtual void CreateRightChild(IBinaryNode<T> child)
        {
            _right = child;
        }

        #endregion
    }
}
