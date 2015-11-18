namespace DataMining.DecisionTree
{
    public abstract class AbstractBinaryNode<T> : AbstractNodeBase<T>, IBinaryNode<T>
    {
		public new IBinaryNode<T> Parent { get; set; }
		public IBinaryNode<T> Left { get; set; }
		public IBinaryNode<T> Right { get; set; }

        public AbstractBinaryNode(int id, NodeType type):base(id, type) {}
    }
}