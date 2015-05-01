
namespace DataMining.DecisionTree
{
    public interface IGeneralNode<T> : INodeBase<T>
    {
        new IGeneralNode<T> Parent { get; }
        IGeneralNode<T>[] Children { get; }

        IGeneralNode<T> CreateNode();
        IGeneralNode<T> CreateLeaf();
    }
}
