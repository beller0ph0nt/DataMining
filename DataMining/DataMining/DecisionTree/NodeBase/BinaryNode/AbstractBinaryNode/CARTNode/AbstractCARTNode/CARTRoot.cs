
namespace DataMining.DecisionTree
{
    public class CARTRoot<T> : AbstractCARTNode<T>
    {
        public CARTRoot(int id)
            : base(id, NodeType.Root, null)
        {
        }
    }
}
