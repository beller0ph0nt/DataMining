
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
            s += " |_" + _left.ToString() + "\n";
            s += " |_" + _right.ToString() + "\n";

            return s;
        }
    }
}
