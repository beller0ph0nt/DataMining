namespace DataMining.DecisionTree {
    public interface IBinaryNode<T> : INodeBase<T> {
		new IBinaryNode<T> Parent { get; set; }
		IBinaryNode<T> Left { get; set; }
		IBinaryNode<T> Right { get; set; }

		bool IsLeft ();
		bool IsRight ();
    }
}