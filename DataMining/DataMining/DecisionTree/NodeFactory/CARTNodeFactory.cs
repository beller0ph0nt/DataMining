namespace DataMining.DecisionTree {
    public static class CARTNodeFactory<T> {
        private static int _id = 1;

        private static int NewId { get { return _id++; } }

        public static ICARTNode<T> GetRoot() {
            return new CARTRoot<T>(NewId);
        }

        public static ICARTNode<T> GetNode() {
            return new CARTNode<T>(NewId);
        }

        public static ICARTNode<T> GetLeaf() {
            return new CARTLeaf<T>(NewId);
        }
    }
}