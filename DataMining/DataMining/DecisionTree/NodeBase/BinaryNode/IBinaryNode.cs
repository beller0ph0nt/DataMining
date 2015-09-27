namespace DataMining.DecisionTree
{
    // интерфейс бинарного узла
    public interface IBinaryNode<T> : INodeBase<T>
    {
        // возвращает ссылку на бинарного родителя
        new IBinaryNode<T> Parent { get; }
        // возвращает ссылку на левый узел
        IBinaryNode<T> Left { get; }
        // возвращает ссылку на правый узел
        IBinaryNode<T> Right { get; }

        // создает левый узел
        IBinaryNode<T> CreateLeftNode();
        // создает левый лист
        IBinaryNode<T> CreateLeftLeaf();
        // Создает правый узел
        IBinaryNode<T> CreateRightNode();
        // создает правый лист
        IBinaryNode<T> CreateRightLeaf();
    }
}