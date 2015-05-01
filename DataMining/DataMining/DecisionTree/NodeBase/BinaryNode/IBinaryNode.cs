
namespace DataMining.DecisionTree
{
    /// <summary>
    /// Интерфейс бинарного узла
    /// </summary>
    /// <typeparam name="T">Тип хранимой информации в узлах дерева</typeparam>
    public interface IBinaryNode<T> : INodeBase<T>
    {
        #region Свойства

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

        #endregion

        #region Методы

        /// <summary>
        /// Создает левый узел
        /// </summary>
        IBinaryNode<T> CreateLeftNode();

        /// <summary>
        /// Создает левый лист
        /// </summary>
        IBinaryNode<T> CreateLeftLeaf();

        /// <summary>
        /// Создает правый узел
        /// </summary>
        IBinaryNode<T> CreateRightNode();

        /// <summary>
        /// Создает правый лист
        /// </summary>
        IBinaryNode<T> CreateRightLeaf();

        #endregion
    }
}
