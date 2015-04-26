
namespace DataMining.DecisionTree
{
    public interface IGeneralNode<T> : INodeBase<T>
    {
        new IGeneralNode<T> Parent { get; }
        IGeneralNode<T>[] Children { get; }

        //void CreateChild(IGeneralNode<T> child);

        void CreateNode();
        void CreateLeaf();
    }
}
