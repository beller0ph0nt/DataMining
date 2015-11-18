namespace DataMining.DecisionTree
{
    public class BinaryLeaf<T> : AbstractBinaryNode<T>
    {
        public BinaryLeaf(int id):base(id, NodeType.Leaf) {}
    }
}