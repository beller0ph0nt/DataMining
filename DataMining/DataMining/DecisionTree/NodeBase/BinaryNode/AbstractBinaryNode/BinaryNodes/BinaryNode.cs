namespace DataMining.DecisionTree
{
    public class BinaryNode<T> : AbstractBinaryNode<T>
    {
        public BinaryNode(int id):base(id, NodeType.Node) {}
    }
}