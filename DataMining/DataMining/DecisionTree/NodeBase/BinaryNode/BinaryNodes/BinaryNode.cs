using System;

namespace DataMining.DecisionTree
{
	[Serializable]
    public class BinaryNode<T> : AbstractBinaryNode<T>
    {
        public BinaryNode(int id):base(id, NodeType.Node) {}
    }
}