using System;

namespace DataMining.DecisionTree {
	[Serializable]
    public class CARTNode<T> : AbstractCARTNode<T> {
        public CARTNode(int id):base(id, NodeType.Node) {}
    }
}