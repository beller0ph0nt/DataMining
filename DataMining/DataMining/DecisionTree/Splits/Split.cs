using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining
{
	public class Split : IComparable
	{
		public DataTable Table { get; set; }
		public List<DataTable> Splits { get; private set; }
		public object Threshold { get; private set;}
		public double Quality { get; private set;}
		public DataColumn Column { get; private set; }
		public ISplitQualityAlgorithm SplitQualityAlgorithm { get; private set; }

		public Split(DataTable table):this(table, new GiniSplit())	{ }
		public Split(DataTable table, ISplitQualityAlgorithm algo)
		{
			Table = table;
			SplitQualityAlgorithm = algo;
			Splits = new List<DataTable> ();
		}

		public List<DataTable> CalcBestSplit()
		{
			foreach (DataColumn col in Table.Columns)
			{
				Console.Write(col.ColumnName);
				// осуществляем разбиение по колонке
				if (Table.Rows [0] [col] is int) {
					Console.WriteLine (" is long");
					CalcBestCatSplit (col);
				} else if (Table.Rows [0] [col] is double) {
					Console.WriteLine (" is double");
					CalcBestNumSplit (col);
				} else {
					Console.WriteLine (" is unknown type");
					throw new Exception ("Bad col type");
				}
			}

			return Splits;
		}

		public List<DataTable> CalcBestSplit(DataTable table)
		{
			Table = table;
			return CalcBestSplit();
		}

		private void Fix(double quality, object threshold, List<DataTable> splits, DataColumn col)
		{
			Console.WriteLine ("Fixing...");
			Quality = quality;
			Threshold = threshold;
			Splits = splits;
			Column = col;
		}

		private void CalcBestNumSplit(DataColumn col)
		{
			double tmpQuality;
			double tmpThreshold;

			Table.DefaultView.Sort = col.ColumnName + " asc";
			Table = Table.DefaultView.ToTable ();

			Console.WriteLine ("col name == " + col.ColumnName);
			Console.WriteLine ("row cnt == " + Table.Rows.Count);
			for (int i = 0; i < (Table.Rows.Count - 1); i++)
			{
				Console.WriteLine ("i == " + i);
				Console.WriteLine ("Table.Rows[i].Field<double>(col) == " + Table.Rows[1].Field<Double>(col));
				// вычисляем порог, как среднее
				tmpThreshold = ((Table.Rows[i].Field<double>(col)) + (Table.Rows[(i + 1)].Field<double>(col))) / 2.0;

				List<DataTable> tmpSplits = new List<DataTable>();	// выделяем память под временное разбиение
				//tmpSplits.Add(new DataTable());                  	// первое разбиение
				tmpSplits.Add(Table.Clone());                  	// первое разбиение
				//tmpSplits.Add(new DataTable());                  	// второе разбиение
				tmpSplits.Add(Table.Clone());                  	// второе разбиение

				for (int j = 0; j < Table.Rows.Count; j++)
				{
					if (Table.Rows[j].Field<double>(col) <= tmpThreshold)
						tmpSplits[0].Rows.Add(Table.Rows[j]);
					else
						tmpSplits[1].Rows.Add(Table.Rows[j]);
				}

				tmpQuality = SplitQualityAlgorithm.CalcSplitQuality (tmpSplits, col);

				if (i == 0)
					Fix(tmpQuality, tmpThreshold, tmpSplits, col);
				else if(SplitQualityAlgorithm.Compare(tmpQuality, Quality) < 0)
					Fix(tmpQuality, tmpThreshold, tmpSplits, col);
			}
		}

		public void CalcBestCatSplit(DataColumn col)
		{
			double tmpQuality;
			int max = Table.AsEnumerable ().Max (d => (int)d [col]);
			int Categories = (int)(Math.Log(max, 2.0) + 1);	// определяем кол-во категорий как максимальное значение категориального аттрибута
			Categories = (int)(Math.Pow(2, Categories) - 1);

			Console.WriteLine ("111");
			Console.WriteLine ("Categories == " + Categories);

			for (long set = 1; set < Categories - 1; set++)  // перебираем все катигории
			{
				Console.WriteLine ("222");

				List<DataTable> tmpSplits = new List<DataTable>();	// выделяем память под временное разбиение
				//tmpSplits.Add(new DataTable());                  	// первое разбиение
				tmpSplits.Add(Table.Clone());                  	// первое разбиение
				//tmpSplits.Add(new DataTable());                  	// второе разбиение
				tmpSplits.Add(Table.Clone());                  	// второе разбиение

				for (int i = 0; i < Table.Rows.Count; i++) {
					Console.WriteLine ("Table.Rows [i].Field<int> (col) == " + Table.Rows [i].Field<int> (col));
					if ((~set & Table.Rows [i].Field<int> (col)) == 0)
						tmpSplits [0].ImportRow (Table.Rows [i]);	//tmpSplits [0].Rows.Add (Table.Rows [i]);
					else
						tmpSplits [1].ImportRow(Table.Rows [i]);	//tmpSplits [1].Rows.Add (Table.Rows [i]);
				}

				Console.WriteLine("1st split count: " + tmpSplits[0].Rows.Count);
				Console.WriteLine("1st split col count: " + tmpSplits[0].Columns.Count);

				for (int i = 0; i <= tmpSplits[0].Rows.Count; i++)
				{
					//Console.WriteLine (tmpSplits [0].Rows.ToString());
					Console.WriteLine(tmpSplits[0].Rows[i][0].ToString() +
						"\t|" + tmpSplits[0].Rows[i][1].ToString());
				}

				Console.WriteLine("2nd split count: " + tmpSplits[1].Rows.Count);

				for (int i = 0; i <= tmpSplits[1].Rows.Count; i++)
				{
					Console.WriteLine(tmpSplits[1].Rows[i]["cat"].ToString() +
						"\t|" + tmpSplits[1].Rows[i][1].ToString());
				}

				tmpQuality = SplitQualityAlgorithm.CalcSplitQuality(tmpSplits, col);

				if (set == 1)
					Fix (tmpQuality, set, tmpSplits, col);
				else if(SplitQualityAlgorithm.Compare(tmpQuality, Quality) < 0)
					Fix(tmpQuality, set, tmpSplits, col);
			}
		}

		public int CompareTo(object obj)
		{
			return SplitQualityAlgorithm.Compare(Quality, ((Split)obj).Quality);
		}
	}
}