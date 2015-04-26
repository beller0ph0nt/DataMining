
namespace DataMining.DecisionTree
{
    /// <summary>
    /// Тип узла
    /// </summary>
    public enum NodeType
    {
        Node,   // Узел
        Root,   // Корень
        Leaf    // Лист
    }

    /// <summary>
    /// Интерфейс базового класса для всех узлов
    /// </summary>
    /// <typeparam name="T">Тип хранимой информации в узлах дерева</typeparam>
    public interface INodeBase<T>
    {
        /// <summary>
        /// Идентификатор узла
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Тип узла
        /// </summary>
        NodeType Type { get; }

        /// <summary>
        /// Ссылка на родителя
        /// </summary>
        INodeBase<T> Parent { get; }

        /// <summary>
        /// Значение, хранимое в узле
        /// </summary>
        T Variable { get; }
    }
}
