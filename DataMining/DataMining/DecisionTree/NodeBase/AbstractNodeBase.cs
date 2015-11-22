using System;

namespace DataMining.DecisionTree {
	[Serializable]
    public abstract class AbstractNodeBase<T> : INodeBase<T> {
		public int Id { get; private set; }
		public NodeType Type { get; private set; }
		public INodeBase<T> Parent { get; set; }
		public T Variable { get; set; }

        public AbstractNodeBase(int id, NodeType type) {
            Id = id;
            Type = type;
			Parent = null;
			Variable = default(T);
        }

        public override string ToString() {
            return string.Format("{0} id: {1}", Type.ToString(), Id);
        }
    }
}