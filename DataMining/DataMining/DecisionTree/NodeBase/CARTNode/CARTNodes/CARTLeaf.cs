using System;

namespace DataMining.DecisionTree
{
	[Serializable]
    public class CARTLeaf<T> : AbstractCARTNode<T>
    {
        public CARTLeaf(int id):base(id, NodeType.Leaf) {}
    }
}