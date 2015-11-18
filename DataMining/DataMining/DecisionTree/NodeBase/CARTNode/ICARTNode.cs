namespace DataMining.DecisionTree
{
    public interface ICARTNode<T> : IBinaryNode<T>
    {
        new ICARTNode<T> Parent { get; }
		new ICARTNode<T> Left { get; }
		new ICARTNode<T> Right { get; }
    }
}