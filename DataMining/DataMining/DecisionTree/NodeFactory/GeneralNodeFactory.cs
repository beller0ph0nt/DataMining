using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree
{
    public class GeneralNodeFactory<T> : AbstractNodeFactory<T>
    {
        public override INodeBase<T> GetRoot()
        {
            return new GeneralRoot<T>(Id);
        }

        public override INodeBase<T> GetNode(INodeBase<T> parent)
        {
            return new GeneralNode<T>(Id, parent);
        }

        public override INodeBase<T> GetLeaf(INodeBase<T> parent)
        {
            return new GeneralLeaf<T>(Id, parent);
        }
    }
}
