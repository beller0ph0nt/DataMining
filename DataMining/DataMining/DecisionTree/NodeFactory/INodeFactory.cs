using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree
{
    public interface INodeFactory<T>
    {
        INodeBase<T> GetRoot();
        INodeBase<T> GetNode(INodeBase<T> parent);
        INodeBase<T> GetLeaf(INodeBase<T> parent);
    }
}
