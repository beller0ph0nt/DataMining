
namespace DataMining.DecisionTree
{
    /// <summary>
    /// Базовый клас для всех узлов
    /// </summary>
    /// <typeparam name="T">Тип хранимой информации в узлах дерева</typeparam>
    public abstract class AbstractNodeBase<T> : INodeBase<T>
    {
        private readonly int _id;       // Идентификатор узла
        private NodeType _type;         // Тип узла
        private INodeBase<T> _parent;   // Ссылка на родителя

        #region Свойства

        public int Id { get { return _id; } }
        public NodeType Type { get { return _type; } }
        public INodeBase<T> Parent { get { return _parent; } }
        public virtual T Variable { get { return default(T); } }

        #endregion

        public AbstractNodeBase(int id, NodeType type, INodeBase<T> parent)
        {
            _id = id;
            _type = type;
            _parent = parent;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Type.ToString(), Id);
        }
    }
}
