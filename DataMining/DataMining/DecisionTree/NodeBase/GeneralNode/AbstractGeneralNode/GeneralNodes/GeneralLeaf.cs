namespace DataMining.DecisionTree
{
    public class GeneralLeaf<T> : AbstractGeneralNode<T>
    {
		public override T Variable { get; set; }

        public GeneralLeaf(int id):base(id, NodeType.Leaf) {}
    }
}