namespace DataMining.DecisionTree
{
    public class CARTLeaf<T> : AbstractCARTNode<T>
    {
        public CARTLeaf(int id):base(id, NodeType.Leaf) {}
    }
}