using System.Collections.Generic;

namespace DataMining.DecisionTree
{
    public abstract class AbstractCARTNode<T> : AbstractBinaryNode<T>, ICARTNode<T>
    {
        protected long _elementsCount;
        protected IRule _rule;
        protected IDictionary<string, long> _classElementsCount;

        #region Свойства

        /// <summary>
        /// Ссылка на родителя, преобразованая к интерфейсу ICARTNode
        /// </summary>
        public new ICARTNode<T> Parent { get { return base.Parent as ICARTNode<T>; } }

        /// <summary>
        /// Количество элементов, прошедших через узел
        /// </summary>
        public long ElementsCount { get { return _elementsCount; } }

        /// <summary>
        /// Правило узла
        /// </summary>
        public IRule Rule { get { return _rule; } set { _rule = value; } }

        /// <summary>
        /// Количество элементов каждого класса, прошедщих через узел
        /// </summary>
        public IDictionary<string, long> ClassElementsCount { get { return _classElementsCount; } }

        #endregion

        public AbstractCARTNode(int id, NodeType type, INodeBase<T> parent)
            : base(id, type, parent)
        {
            _classElementsCount = new Dictionary<string, long>();
            _elementsCount = 0;
            _rule = null;
        }

        #region Методы

        public override void CreateLeftNode()
        {
            _left = CARTNodeFactory<T>.GetNode(this) as ICARTNode<T>;
        }

        public override void CreateLeftLeaf()
        {
            _left = CARTNodeFactory<T>.GetLeaf(this) as ICARTNode<T>;
        }

        public override void CreateRightNode()
        {
            _right = CARTNodeFactory<T>.GetNode(this) as ICARTNode<T>;
        }

        public override void CreateRightLeaf()
        {
            _right = CARTNodeFactory<T>.GetLeaf(this) as ICARTNode<T>;
        }

        #endregion
    }
}
