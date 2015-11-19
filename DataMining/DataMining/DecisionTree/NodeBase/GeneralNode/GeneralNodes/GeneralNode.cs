using System;

namespace DataMining.DecisionTree {
	[Serializable]
    public class GeneralNode<T> : AbstractGeneralNode<T> {
        public GeneralNode(int id):base(id, NodeType.Node) {}
    }
}