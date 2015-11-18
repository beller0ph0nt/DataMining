using System.Collections.Generic;

namespace DataMining.DecisionTree
{
    public abstract class AbstractGeneralNode<T> : AbstractNodeBase<T>, IGeneralNode<T>
    {
		public new IGeneralNode<T> Parent { get; set; }
		public List<IGeneralNode<T>> Children { get; set; }

        public AbstractGeneralNode(int id, NodeType type):base(id, type)
        {
			Children = new List<IGeneralNode<T>>();
        }
    }
}