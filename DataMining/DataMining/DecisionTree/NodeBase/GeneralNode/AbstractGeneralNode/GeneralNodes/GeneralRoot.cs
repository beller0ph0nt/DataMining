
namespace DataMining.DecisionTree
{
    public class GeneralRoot<T> : AbstractGeneralNode<T>
    {
        public GeneralRoot(int id)
            : base(id, NodeType.Root, null)
        {
        }
    }
}
