
namespace DataMining.DecisionTree
{
    public abstract class AbstractNodeBase<T> : INodeBase<T>
    {
        private readonly int _id;
        private NodeType _type;
        private INodeBase<T> _parent;

        public int Id { get { return _id; } }
        public NodeType Type { get { return _type; } }
        public INodeBase<T> Parent { get { return _parent; } }
        public virtual T Variable { get { return default(T); } }

        public AbstractNodeBase(int id, NodeType type, INodeBase<T> parent)
        {
            _id = id;
            _type = type;
            _parent = parent;
        }

        public override string ToString()
        {
            return string.Format("Id: {0} Type: {1}", Id, Type);
        }
    }
}
