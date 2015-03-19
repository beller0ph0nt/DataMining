using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree
{
    public class BinaryNodeFactory<T> : AbstractNodeFactory<T>
    {
        public override INodeBase<T> GetRoot()
        {
            return new BinaryRoot<T>(Id);
        }

        public override INodeBase<T> GetNode(INodeBase<T> parent)
        {
            return new BinaryNode<T>(Id, parent);
        }

        public override INodeBase<T> GetLeaf(INodeBase<T> parent)
        {
            return new BinaryLeaf<T>(Id, parent);
        }
    }
}
