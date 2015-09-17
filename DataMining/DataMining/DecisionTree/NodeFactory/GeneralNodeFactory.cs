using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree
{
    /// <summary>
    /// Фабрика узлов стандартного дерева
    /// </summary>
    /// <typeparam name="T">Тип хранимой информации в узлах дерева</typeparam>
    public static class GeneralNodeFactory<T>
    {
        private static int _id = 1;

        private static int NewId { get { return _id++; } }

        public static INodeBase<T> GetRoot()
        {
            return new GeneralRoot<T>(NewId);
        }

        public static INodeBase<T> GetNode(INodeBase<T> parent)
        {
            return new GeneralNode<T>(NewId, parent);
        }

        public static INodeBase<T> GetLeaf(INodeBase<T> parent)
        {
            return new GeneralLeaf<T>(NewId, parent);
        }
    }
}
