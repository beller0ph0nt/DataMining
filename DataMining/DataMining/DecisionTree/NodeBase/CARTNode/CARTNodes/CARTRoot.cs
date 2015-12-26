using System;

namespace DataMining.DecisionTree {
	[Serializable]
    public class CARTRoot<T> : AbstractCARTNode<T> {
        public CARTRoot(int id):base(id, NodeType.Root) {}
		/*
		public override bool IsLeft () {
			return false;
		}

		public override bool IsRight () {
			return false;
		}
		*/
    }
}