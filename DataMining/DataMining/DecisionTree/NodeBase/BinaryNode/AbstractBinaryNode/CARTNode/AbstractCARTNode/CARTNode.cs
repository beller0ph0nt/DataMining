
namespace DataMining.DecisionTree
{
    public class CARTNode<T> : AbstractCARTNode<T>
    {
        public CARTNode(int id, INodeBase<T> parent)
            : base(id, NodeType.Node, parent)
        {
        }
    }
}
