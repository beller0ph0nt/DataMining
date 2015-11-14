namespace DataMining.DecisionTree
{
    public abstract class AbstractCARTNode<T> : AbstractBinaryNode<T>, ICARTNode<T>
    {
		public new ICARTNode<T> Parent { get; set; }

        public AbstractCARTNode(int id, NodeType type):base(id, type) {}
    }
}