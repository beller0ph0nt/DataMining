namespace DataMining.DecisionTree {
	public static class BinaryNodeFactory<T> {
        private static int _id = 1;

        private static int NewId { get { return _id++; } }

        public static IBinaryNode<T> GetRoot() {
            return new BinaryRoot<T>(NewId);
        }

        public static IBinaryNode<T> GetNode() {
            return new BinaryNode<T>(NewId);
		}

        public static IBinaryNode<T> GetLeaf() {
            return new BinaryLeaf<T>(NewId);
        }
    }
}