using System;

namespace DataMining.DecisionTree {
	[Serializable]
    public class CARTRoot<T> : AbstractCARTNode<T> {
        public CARTRoot(int id):base(id, NodeType.Root) {}
    }
}