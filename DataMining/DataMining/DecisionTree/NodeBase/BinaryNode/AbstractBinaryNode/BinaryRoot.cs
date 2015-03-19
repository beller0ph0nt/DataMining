
namespace DataMining.DecisionTree
{
    public class BinaryRoot<T> : AbstractBinaryNode<T>
    {
        public BinaryRoot(int id)
            : base(id, NodeType.Root, null)
        {
        }
    }
}
