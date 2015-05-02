using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMining.DecisionTree;

namespace TestField
{
    class Program
    {
        static void Main(string[] args)
        {
            CARTTree<int> tree = new CARTTree<int>();

            tree.CreateRoot();
            tree.CreateLeftNode(1);
            tree.CreateRightNode(1);
            tree.CreateLeftLeaf(2);
            tree.CreateRightLeaf(2);
            //tree.Root.CreateLeftLeaf();
            //tree.Root.CreateRightNode();

            Console.Write(tree.ToString());
            Console.ReadKey();
        }
    }
}
