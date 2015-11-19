using System;

namespace DataMining.DecisionTree {
	[Serializable]
    public class BinaryLeaf<T> : AbstractBinaryNode<T> {
        public BinaryLeaf(int id):base(id, NodeType.Leaf) {}
    }
}