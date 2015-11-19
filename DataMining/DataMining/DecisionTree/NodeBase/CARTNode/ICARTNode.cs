namespace DataMining.DecisionTree {
    public interface ICARTNode<T> : IBinaryNode<T> {
		new ICARTNode<T> Parent { get; set; }
		new ICARTNode<T> Left { get; set; }
		new ICARTNode<T> Right { get; set; }
    }
}