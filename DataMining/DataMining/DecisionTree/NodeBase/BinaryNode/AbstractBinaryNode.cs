﻿using System;

namespace DataMining.DecisionTree {
	[Serializable]
    public abstract class AbstractBinaryNode<T> : AbstractNodeBase<T>, IBinaryNode<T> {
		public new IBinaryNode<T> Parent { get; set; }
		public IBinaryNode<T> Left { get; set; }
		public IBinaryNode<T> Right { get; set; }

        public AbstractBinaryNode(int id, NodeType type):base(id, type) {
			Left = null;
			Right = null;
		}
		/*
		public virtual bool IsLeft () {
			return (Id == Parent.Left.Id) ? true : false;
		}

		public virtual bool IsRight () {
			return (Id == Parent.Right.Id) ? true : false;
		}
		*/
    }
}