
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
        void CreateLeftNode();

        /// <summary>
        /// Создает левый лист
        /// </summary>
        void CreateLeftLeaf();

        /// <summary>
        /// Создает правый узел
        /// </summary>
        void CreateRightNode();

        /// <summary>
        /// Создает правый лист
        /// </summary>
        void CreateRightLeaf();

        #endregion
    }
}
