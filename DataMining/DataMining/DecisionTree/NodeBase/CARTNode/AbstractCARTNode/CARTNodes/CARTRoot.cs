namespace DataMining.DecisionTree
{
    public class CARTRoot<T> : AbstractCARTNode<T>
    {
        public CARTRoot(int id)
            : base(id, NodeType.Root, null)
        {
        }

        public override string ToString()
        {
            string s;

            s = string.Format("root:{0}", Id) + "\n";
            s += " |_" + ((_left != null) ? _left.ToString() : "null") + "\n";
            s += " |_" + ((_right != null) ? _right.ToString() : "null") + "\n";

            return s;
        }
    }
}
