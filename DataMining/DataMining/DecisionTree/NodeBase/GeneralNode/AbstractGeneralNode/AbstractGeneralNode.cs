using System.Collections.Generic;

namespace DataMining.DecisionTree
{
    public abstract class AbstractGeneralNode<T> : AbstractNodeBase<T>, IGeneralNode<T>
    {
        private List<IGeneralNode<T>> _children;

        public new IGeneralNode<T> Parent { get { return base.Parent as IGeneralNode<T>; } }
        public IGeneralNode<T>[] Children { get { return _children.ToArray(); } }

        public AbstractGeneralNode(int id, NodeType type, INodeBase<T> parent)
            : base(id, type, parent)
        {
            _children = new List<IGeneralNode<T>>();
        }

        public virtual void CreateChild(IGeneralNode<T> child)
        {
            _children.Add(child);
        }
    }
}
