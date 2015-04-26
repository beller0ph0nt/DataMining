
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

        public virtual void CreateLeftNode()
        {
            _left = BinaryNodeFactory<T>.GetNode(this) as IBinaryNode<T>;
        }

        public virtual void CreateLeftLeaf()
        {
            _left = BinaryNodeFactory<T>.GetLeaf(this) as IBinaryNode<T>;
        }

        public virtual void CreateRightNode()
        {
            _right = BinaryNodeFactory<T>.GetNode(this) as IBinaryNode<T>;
        }

        public virtual void CreateRightLeaf()
        {
            _right = BinaryNodeFactory<T>.GetLeaf(this) as IBinaryNode<T>;
        }

        #endregion
    }
}
