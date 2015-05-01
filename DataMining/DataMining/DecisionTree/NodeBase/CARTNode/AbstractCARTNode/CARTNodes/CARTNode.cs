namespace DataMining.DecisionTree
{
    public class CARTNode<T> : AbstractCARTNode<T>
    {
        public CARTNode(int id, INodeBase<T> parent)
            : base(id, NodeType.Node, parent)
        {
        }

        public override string ToString()
        {
            string s;

            s = string.Format("node:{0}", Id) + "\n";
            s += " |_" + ((_left != null) ? _left.ToString() : "null") + "\n";
            s += " |_" + ((_right != null) ? _right.ToString() : "null") + "\n";

            return s;
        }
    }
}
