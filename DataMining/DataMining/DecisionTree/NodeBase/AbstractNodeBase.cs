namespace DataMining.DecisionTree
{
    // базовый клас для всех узлов
    public abstract class AbstractNodeBase<T> : INodeBase<T>
    {
        private readonly int _id;       // идентификатор узла
        private NodeType _type;         // тип узла
        private INodeBase<T> _parent;   // ссылка на родителя

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
            return string.Format("{0}:{1}", Type.ToString(), Id);
        }
    }
}