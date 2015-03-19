using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree
{
    public class CARTNodeFactory<T> : AbstractNodeFactory<T>
    {
        public override INodeBase<T> GetRoot()
        {
            return new CARTRoot<T>(Id);
        }

        public override INodeBase<T> GetNode(INodeBase<T> parent)
        {
            return new CARTNode<T>(Id, parent);
        }

        public override INodeBase<T> GetLeaf(INodeBase<T> parent)
        {
            return new CARTLeaf<T>(Id, parent);
        }
    }
}
