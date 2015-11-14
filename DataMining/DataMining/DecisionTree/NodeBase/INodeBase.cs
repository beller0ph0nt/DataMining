namespace DataMining.DecisionTree
{
    public enum NodeType { Node, Root, Leaf }

    public interface INodeBase<T>
    {
		int Id { get; }
		NodeType Type { get; }
		INodeBase<T> Parent { get; set; }
		T Variable { get; set; }
	}
}