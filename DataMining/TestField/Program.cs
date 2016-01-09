using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMining.DecisionTree;
using DataMining;
using DataMining.DecisionTree.LearningAlgorithm;

namespace TestField
{
    class Program
    {
        static void Main(string[] args)
        {
			//Random rnd = new Random();
			DataTable table = new DataTable("TEST");
			DataRow row;

			table.Columns.Add(new DataColumn("cat", System.Type.GetType("System.Int32")));
			table.Columns.Add(new DataColumn("num", System.Type.GetType("System.Double")));
			table.Columns.Add(new DataColumn("func", System.Type.GetType("System.Int32")));

			int count = 500;

			/*
			List<int> list = new List<int> () { 1, 2, 2, 3, 3, 4, 6, 7, 7, 7 };

			var tmp = list.GroupBy (i => i).Aggregate ((t1, t2) => (t1.Count() > t2.Count()) ? t1 : t2);
			Console.WriteLine ("MAX GRP key: " + tmp.Key + " cnt: " + tmp.Count() + " err: " + ((double)tmp.Count() / list.Count));

			foreach (var lst in list.GroupBy(i => i, (key, grp) => new { cnt = grp.Count(), val = key })) {
				Console.WriteLine ("key: " + lst.val.ToString () + " cnt: " + lst.cnt);
			}
			*/

			Console.WriteLine("gen table...");
			for (int i = 0; i <= count; i++) {
				row = table.NewRow();
				row["cat"] = i % 3;		//row["cat"] = rnd.Next();
				row["num"] = i;
				row["func"] = (i < 5) ? 1 : 2;
				//Console.WriteLine(row["cat"].ToString() + ", " + row["num"].ToString() + ", " + row["func"].ToString());
				table.Rows.Add(row);
			}

			CARTLearning learn = new CARTLearning ();
			CART tree = learn.Training (table);
			Console.WriteLine (tree.ToString ());
			tree.Save ("cart.bin");
			//Console.WriteLine("wrong class count = " + learn.WrongClassCount (tree.Root));

			row = table.NewRow ();
			row ["cat"] = 1;
			row ["num"] = 8.0;
			//row ["func"] = 0;
			Console.WriteLine("key col: " + tree.Calc (row));

			/*
			int j = 0;
			Console.WriteLine("Index;CalcBestCatSplit;");
			foreach (var t in Delays.delays["CalcBestCatSplit"]) {
				Console.WriteLine((j++) + ";" + t + ";");
			}

			j = 0;
			Console.WriteLine("Index;CalcBestNumSplit;");
			foreach (var t in Delays.delays["CalcBestNumSplit"]) {
				Console.WriteLine((j++) + ";" + t + ";");
			}

			j = 0;
			Console.WriteLine("Index;CalcBestNumSplit_Sort;");
			foreach (var t in Delays.delays["CalcBestNumSplit_Sort"]) {
				Console.WriteLine((j++) + ";" + t + ";");
			}

			j = 0;
			Console.WriteLine("Index;CalcBestNumSplit_FillSplits;");
			foreach (var t in Delays.delays["CalcBestNumSplit_FillSplits"]) {
				Console.WriteLine((j++) + ";" + t + ";");
			}

			j = 0;
			Console.WriteLine("Index;CalcBestNumSplit_CalcQuality;");
			foreach (var t in Delays.delays["CalcBestNumSplit_CalcQuality"]) {
				Console.WriteLine((j++) + ";" + t + ";");
			}

			j = 0;
			Console.WriteLine("Index;CalcBestNumSplit_CalcClass;");
			foreach (var t in Delays.delays["CalcBestNumSplit_CalcClass"]) {
				Console.WriteLine((j++) + ";" + t + ";");
			}
			*/

			/*
			Split s = new Split (table);
			List<DataTable> spl = s.CalcBestSplit ();

			Console.WriteLine("split 0");
			for (int i = 0; i < spl[0].Rows.Count; i++)
			{
				Console.WriteLine(spl[0].Rows[i]["cat"].ToString() + "\t|" + spl[0].Rows[i][1].ToString());
			}

			Console.WriteLine("split 1");
			for (int i = 0; i < spl[1].Rows.Count; i++)
			{
				Console.WriteLine(spl[1].Rows[i]["cat"].ToString() + "\t|" + spl[1].Rows[i][1].ToString());
			}

			Console.WriteLine("*************************************************");

			table.DefaultView.Sort = "num desc";
			table = table.DefaultView.ToTable();

			for (int i = 0; i <= count; i++)
			{
				Console.WriteLine(table.Rows[i]["cat"].ToString() + "\t|" + table.Rows[i][1].ToString());
			}

			var result = table.AsEnumerable().GroupBy(r => r ["cat"], d => new { key = d ["cat"] });
			var cnt = table.AsEnumerable().GroupBy(r => r ["cat"], d => new { key = d ["cat"] }).Count();

			Console.WriteLine();

			foreach (var t in result)
				Console.WriteLine(t.Key);

			Console.WriteLine("-------");
			Console.WriteLine("cnt = " + cnt);
			*/

			// примеры группировок

			/*
			--1

			 var result = from row in dt.AsEnumerable()
              group row by row.Field<int>("TeamID") into grp
               select new
                 {
                 TeamID = grp.Key,
                  MemberCount = grp.Count()
                  };
			 foreach (var t in result)
			     Console.WriteLine(t.TeamID + " " + t.MemberCount);

			--2
			
			var listInfo = (from infoMember in context.Members
                where infoMember.TeamID  == TeamID 
                group infoMember by new
                { infoMember.TeamID, infoMember.MemberIDCount } into newInfoMemeber
                select new ClassName
                {
                   TeamID = newInfo.Key.TeamID,
                   MemberIDCount = newInfo.Key.MemberIDCOunt,
                   Count = newInfo.Count(),
                   TotalCount = (from infoMemeber2 in context.Members
                                 where infoMemeber2.TeamID== TeamID
                                 select infoResult2).Count()
                }).AsEnumerable();

			 */


			Console.Write("Press any key...");
			Console.ReadKey();

			// Тест дереdf
			/*
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
            */
        }
    }
}
