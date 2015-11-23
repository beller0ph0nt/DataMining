﻿using System;
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

			DataColumn column;
			DataRow row;

			column = new DataColumn();
			column.DataType = System.Type.GetType("System.Int32");
			column.ColumnName = "cat";
			column.ReadOnly = true;
			column.Unique = false;
			table.Columns.Add(column);

			column = new DataColumn();
			column.DataType = System.Type.GetType("System.Double");
			column.ColumnName = "num";
			column.ReadOnly = true;
			column.Unique = false;
			table.Columns.Add(column);

			int count = 10;

			Console.WriteLine("gen table...");
			for (int i = 0; i <= count; i++)
			{
				row = table.NewRow();
				//row["cat"] = rnd.Next();
				row["cat"] = i % 3;
				row["num"] = i;

				Console.WriteLine(row["cat"].ToString() + ", " + row["num"].ToString());

				table.Rows.Add(row);
			}



			CARTLearning learn = new CARTLearning ();
			CARTTree<Split> tree = learn.Training (table);
			Console.WriteLine (tree.ToString ());
			tree.Save ("tst_tree.bin");

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
