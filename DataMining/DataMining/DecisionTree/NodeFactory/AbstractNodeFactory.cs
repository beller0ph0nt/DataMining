using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree
{
    public abstract class AbstractNodeFactory<T> : INodeFactory<T>
    {
        private static int _id = 1;

        protected int NewId { get { return _id++; } }
        
        public abstract INodeBase<T> GetRoot();
        public abstract INodeBase<T> GetNode(INodeBase<T> parent);
        public abstract INodeBase<T> GetLeaf(INodeBase<T> parent);
    }
}
