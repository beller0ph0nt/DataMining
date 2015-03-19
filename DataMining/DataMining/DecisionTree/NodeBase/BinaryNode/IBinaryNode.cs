
namespace DataMining.DecisionTree
{
    public interface IBinaryNode<T> : INodeBase<T>
    {
        new IBinaryNode<T> Parent { get; }
        IBinaryNode<T> Left { get; }
        IBinaryNode<T> Right { get; }

        void CreateLeftChild(IBinaryNode<T> child);
        void CreateRightChild(IBinaryNode<T> child);
    }
}
