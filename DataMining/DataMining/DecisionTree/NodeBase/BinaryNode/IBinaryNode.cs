
namespace DataMining.DecisionTree
{
    /// <summary>
    /// Интерфейс бинарного узла
    /// </summary>
    /// <typeparam name="T">Тип хранимой информации в узлах дерева</typeparam>
    public interface IBinaryNode<T> : INodeBase<T>
    {
        /// <summary>
        /// Возвращает ссылку на бинарного родителя
        /// </summary>
        new IBinaryNode<T> Parent { get; }

        /// <summary>
        /// Возвращает ссылку на левый узел
        /// </summary>
        IBinaryNode<T> Left { get; }

        /// <summary>
        /// Возвращает ссылку на правый узел
        /// </summary>
        IBinaryNode<T> Right { get; }

        /// <summary>
        /// Создает левого потомка
        /// </summary>
        /// <param name="child"></param>
        void CreateLeftChild(IBinaryNode<T> child);

        /// <summary>
        /// Создает правого потомка
        /// </summary>
        /// <param name="child"></param>
        void CreateRightChild(IBinaryNode<T> child);
    }
}
