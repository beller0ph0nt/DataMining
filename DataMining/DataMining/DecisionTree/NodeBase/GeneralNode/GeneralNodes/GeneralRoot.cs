using System;

namespace DataMining.DecisionTree
{
	[Serializable]
    public class GeneralRoot<T> : AbstractGeneralNode<T>
    {
        public GeneralRoot(int id):base(id, NodeType.Root) {}
    }
}