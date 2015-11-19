using System;

namespace DataMining.DecisionTree {
	[Serializable]
    public class GeneralLeaf<T> : AbstractGeneralNode<T> {
        public GeneralLeaf(int id):base(id, NodeType.Leaf) {}
    }
}