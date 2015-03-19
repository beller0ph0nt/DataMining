
namespace DataMining.DecisionTree
{
    public class GeneralNode<T> : AbstractGeneralNode<T>
    {
        public GeneralNode(int id, INodeBase<T> parent)
            : base(id, NodeType.Node, parent)
        {
        }
    }
}
