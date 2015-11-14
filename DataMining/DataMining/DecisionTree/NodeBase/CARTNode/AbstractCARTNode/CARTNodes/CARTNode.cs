namespace DataMining.DecisionTree
{
    public class CARTNode<T> : AbstractCARTNode<T>
    {
        public CARTNode(int id):base(id, NodeType.Node) {}
    }
}
