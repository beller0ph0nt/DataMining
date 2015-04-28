﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.DecisionTree
{
    /// <summary>
    /// Фабрика узлов CART-дерева
    /// </summary>
    /// <typeparam name="T">Тип хранимой информации в узлах дерева</typeparam>
    public static class CARTNodeFactory<T>
    {
        private static int _id = 1;

        private static int Id { get { return _id++; } }

        public static ICARTNode<T> GetRoot()
        {
            return new CARTRoot<T>(Id);
        }

        public static ICARTNode<T> GetNode(ICARTNode<T> parent)
        {
            return new CARTNode<T>(Id, parent);
        }

        public static ICARTNode<T> GetLeaf(ICARTNode<T> parent)
        {
            return new CARTLeaf<T>(Id, parent);
        }
    }
}
