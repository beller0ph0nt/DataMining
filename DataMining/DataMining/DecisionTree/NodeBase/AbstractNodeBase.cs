namespace DataMining.DecisionTree
{
    public abstract class AbstractNodeBase<T> : INodeBase<T>
    {
		public int Id { get; private set; }
		public NodeType Type { get; private set; }
		public INodeBase<T> Parent { get; set; }
		public T Variable { get; set; }

        public AbstractNodeBase(int id, NodeType type)
        {
            Id = id;
            Type = type;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Type.ToString(), Id);
        }
    }
}