namespace DataMining.DecisionTree
{
    public interface ICARTNode<T> : IBinaryNode<T>
    {
        new ICARTNode<T> Parent { get; }
    }
}