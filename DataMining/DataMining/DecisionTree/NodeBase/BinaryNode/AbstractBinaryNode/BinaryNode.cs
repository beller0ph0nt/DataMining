
namespace DataMining.DecisionTree
{
    public class BinaryNode<T> : AbstractBinaryNode<T>
    {
        public BinaryNode(int id, INodeBase<T> parent)
            : base(id, NodeType.Node, parent)
        {
        }
    }
}
