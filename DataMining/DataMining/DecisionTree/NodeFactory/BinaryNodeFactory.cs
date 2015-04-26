using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree
{
    /// <summary>
    /// Фабрика узлов бинарного дерева
    /// </summary>
    /// <typeparam name="T">Тип хранимой информации в узлах дерева</typeparam>
    public static class BinaryNodeFactory<T>
    {
        private static int _id = 1;     // Идентификатор узлов

        private static int Id { get { return _id++; } }

        public static INodeBase<T> GetRoot()
        {
            return new BinaryRoot<T>(Id);
        }

        public static INodeBase<T> GetNode(INodeBase<T> parent)
        {
            return new BinaryNode<T>(Id, parent);
        }

        public static INodeBase<T> GetLeaf(INodeBase<T> parent)
        {
            return new BinaryLeaf<T>(Id, parent);
        }
    }
}
