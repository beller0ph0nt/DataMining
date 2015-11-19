using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMining.DecisionTree {
    public static class GeneralNodeFactory<T> {
        private static int _id = 1;

        private static int NewId { get { return _id++; } }

        public static INodeBase<T> GetRoot() {
            return new GeneralRoot<T>(NewId);
        }

        public static INodeBase<T> GetNode() {
            return new GeneralNode<T>(NewId);
        }

        public static INodeBase<T> GetLeaf() {
            return new GeneralLeaf<T>(NewId);
        }
    }
}