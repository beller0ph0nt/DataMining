﻿using System;

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

        public virtual IBinaryNode<T> CreateLeftNode()
        {
            if (_left == null)
            {
                _left = BinaryNodeFactory<T>.GetNode(this);

                return _left;
            }
            else
                throw new InvalidOperationException("Левый узел уже создан");
        }

        public virtual IBinaryNode<T> CreateLeftLeaf()
        {
            if (_left == null)
            {
                _left = BinaryNodeFactory<T>.GetLeaf(this);

                return _left;
            }
            else
                throw new InvalidOperationException("Левый лист уже создан");
        }

        public virtual IBinaryNode<T> CreateRightNode()
        {
            if (_right == null)
            {
                _right = BinaryNodeFactory<T>.GetNode(this);

                return _right;
            }
            else
                throw new InvalidOperationException("Правый узел уже создан");
        }

        public virtual IBinaryNode<T> CreateRightLeaf()
        {
            if (_right == null)
            {
                _right = BinaryNodeFactory<T>.GetLeaf(this);

                return _right;
            }
            else
                throw new InvalidOperationException("Правый лист уже создан");
        }

        #endregion
    }
}
