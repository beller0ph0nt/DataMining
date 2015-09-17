﻿using System;
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

        private static int NewId { get { return _id++; } }

        public static IBinaryNode<T> GetRoot()
        {
            return new BinaryRoot<T>(NewId);
        }

        public static IBinaryNode<T> GetNode(IBinaryNode<T> parent)
        {
            return new BinaryNode<T>(NewId, parent);
        }

        public static IBinaryNode<T> GetLeaf(IBinaryNode<T> parent)
        {
            return new BinaryLeaf<T>(NewId, parent);
        }
    }
}
