namespace DataMining.DecisionTree
{
    // тип узла
    public enum NodeType
    {
        Node,   // узел
        Root,   // корень
        Leaf    // лист
    }
		
    // интерфейс базового класса для всех узлов
    public interface INodeBase<T>
    {
        // идентификатор узла
        int Id { get; }
        // тип узла
        NodeType Type { get; }
        // ссылка на родителя
        INodeBase<T> Parent { get; }
        // значение, хранимое в узле
        T Variable { get; }
    }
}