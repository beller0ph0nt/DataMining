namespace DataMining.DecisionTree
{
    public class CARTLeaf<T> : AbstractCARTNode<T>
    {
		public override T Variable { get; set; }

        public CARTLeaf(int id):base(id, NodeType.Leaf) {}
    }
}