using System;

namespace DataMining.DecisionTree {
	[Serializable]
    public class BinaryRoot<T> : AbstractBinaryNode<T> {
        public BinaryRoot(int id):base(id, NodeType.Root) {}

		public override bool IsLeft () {
			return false;
		}

		public override bool IsRight () {
			return false;
		}
    }
}