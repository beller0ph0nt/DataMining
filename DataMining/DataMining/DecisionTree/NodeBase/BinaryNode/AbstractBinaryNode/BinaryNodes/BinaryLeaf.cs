namespace DataMining.DecisionTree
{
    public class BinaryLeaf<T> : AbstractBinaryNode<T>
    {
		public override T Variable { get; set; }

        public BinaryLeaf(int id):base(id, NodeType.Leaf) {}
    }
}