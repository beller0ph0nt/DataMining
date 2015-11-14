using System.Collections.Generic;

namespace DataMining.DecisionTree
{
    public interface IGeneralNode<T> : INodeBase<T>
    {
		new IGeneralNode<T> Parent { get; set; }
		List<IGeneralNode<T>> Children { get; set; }
    }
}