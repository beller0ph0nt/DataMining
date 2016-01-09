using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using DataMining.DecisionTree.SplitQualityAlgorithm;

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DataMining {
	[Serializable]
	public class Split {
		//public enum SplitType : int { Left = 0, Right = 1 }
		public DataTable Table { get; set; }
		public List<DataTable> Splits { get; private set; }
		public object Threshold { get; private set; }
		public double Quality { get; private set;}
		public object ClassVal { get; private set;}
		public int ColOrdinal { get; private set; }
		public ISplitQualityAlgorithm SplitQualityAlgorithm { get; private set; }



		public Split(DataTable table):this(table, new GiniSplit()) {}
		public Split(DataTable table, ISplitQualityAlgorithm algo) {
			Threshold = null;
			Table = table;
			SplitQualityAlgorithm = algo;
			Splits = new List<DataTable> ();
		}

		public List<DataTable> CalcBestSplit() {
			//var sw = new Stopwatch ();
			for (int col = 0; col < Table.Columns.Count - 1; col++) {
				if (Table.Rows[0][col] is int) {
					//sw.Start ();
					CalcBestCatSplit(Table.Columns[col]);
					//sw.Stop ();
					//Delays.delays ["CalcBestCatSplit"].Add (sw.ElapsedTicks);
					//Console.WriteLine ("[" + sw.Elapsed + "]" + " CalcBestCatSplit");
					//sw.Reset ();
				} else if (Table.Rows[0][col] is double) {
					//sw.Start ();
					CalcBestNumSplit(Table.Columns[col]);
					//sw.Stop ();
					//Delays.delays ["CalcBestNumSplit"].Add (sw.ElapsedTicks);
					//Console.WriteLine ("[" + sw.Elapsed + "]" + " CalcBestNumSplit");
					//sw.Reset ();
				} else {
					throw new Exception ("Bad col type");
				}
			}

			//Console.WriteLine ("delays [\"CalcBestCatSplit\"] = " + Delays.delays ["CalcBestCatSplit"].First ());
			//Console.WriteLine ("delays [\"CalcBestNumSplit\"] = " + Delays.delays ["CalcBestNumSplit"].First ());

			return Splits;
		}

		public bool IsEmpty() {
			return Splits.Count == 0 || Splits [0].Rows.Count == 0 || Splits [1].Rows.Count == 0 || SplitQualityAlgorithm.IsTheBest (Quality);
		}

		public List<DataTable> CalcBestSplit(DataTable table) {
			Table = table;
			return CalcBestSplit();
		}

		public bool IsClassification() {
			return (Table.Rows [0] [Table.Columns.Count - 1] is int) ? true : false;
		}

		public bool IsRegression() {
			return (Table.Rows [0] [Table.Columns.Count - 1] is double) ? true : false;
		}

		public int GetSplitIndex(DataRow row) {
			if (row [ColOrdinal] is int) {
				//Console.WriteLine ("col is int");
				return ((~((int)Threshold) & ((int)row [ColOrdinal])) == 0) ? 0 : 1;
			} else if (row [ColOrdinal] is double) {
				//Console.WriteLine ("col is double");
				return (((double)row [ColOrdinal]) <= ((double)Threshold)) ? 0 : 1;
			} else {
				throw new Exception ("wrong type of col");
			}
		}

		public bool IsLeftSplit(DataRow row) {
			//Console.WriteLine ("IsLeftSplit");
			return (GetSplitIndex (row) == 0) ? true : false;
		}

		public bool IsRightSplit(DataRow row) {
			//Console.WriteLine ("IsRightSplit");
			return (GetSplitIndex (row) == 1) ? true : false;
		}

		private void Fix(double quality, object threshold, List<DataTable> splits, DataColumn col) {
			Quality = quality;
			Threshold = threshold;
			Splits = splits;
			ColOrdinal = col.Ordinal;
		}

		private void CalcClass() {
			if (IsClassification()) {
				ClassVal = Table.AsEnumerable ().GroupBy (row => (int)row [Table.Columns.Count - 1]).Aggregate ((t1, t2) => (t1.Count () > t2.Count ()) ? t1 : t2).Key;
			} else if (IsRegression()) {
			}
		}

		private void CalcBestNumSplit(DataColumn col) {
			double tmpQuality;
			double tmpThreshold;
			//double prevTmpThreshold = -1.0;

			// lock
			//var sw = new Stopwatch ();
			//sw.Start ();

			//Table.DefaultView.Sort = col.ColumnName + " asc";
			//Table = Table.DefaultView.ToTable ();

			List<double> t = new List<double> ();
			/*
			for (int i = 0; i < Table.Rows.Count - 1; i++) {
				tmpThreshold = (Table.Rows[i].Field<double>(col.Ordinal) + Table.Rows[i + 1].Field<double>(col.Ordinal)) / 2.0;
				if (prevTmpThreshold != tmpThreshold) {
					prevTmpThreshold = tmpThreshold;
					if (!t.Contains (tmpThreshold)) {
						t.Add (tmpThreshold);
					}
				}
			}
			*/

			object lck = new object ();
			Parallel.For (0, Table.Rows.Count - 1, i => {
				double thr = (Table.Rows[i].Field<double>(col.Ordinal) + Table.Rows[i + 1].Field<double>(col.Ordinal)) / 2.0;
				lock(lck)
				{
					if (!t.Contains (thr)) {
						t.Add (thr);
					}
				}
			});

			//sw.Stop ();
			//Delays.delays ["CalcBestNumSplit_Sort"].Add (sw.ElapsedTicks);
			//Console.WriteLine ("delays [\"CalcBestNumSplit_Sort\"] = " + Delays.delays ["CalcBestNumSplit_Sort"].First ());
			//Console.WriteLine ("[" + sw.Elapsed + "]" + " CalcBestNumSplit_Sort");
			//sw.Reset ();
			// unlock

			//int del_index = 0;

			//object syncLeft = new object ();
			//object syncRight = new object ();

			//for (int i = 0; i < Table.Rows.Count - 1; i++) {
			//for (int i = 0; i < Thrshld.dict[col.Ordinal].Count(); i++) {
			for (int i = 0; i < t.Count; i++) {
				tmpThreshold = t [i];
				//tmpThreshold = (Table.Rows[i].Field<double>(col.Ordinal) + Table.Rows[i + 1].Field<double>(col.Ordinal)) / 2.0;	// вычисляем порог, как среднее
				//tmpThreshold = Thrshld.dict [col.Ordinal] [i];

				//if (prevTmpThreshold != tmpThreshold) {

				//	prevTmpThreshold = tmpThreshold;

					List<DataTable> tmpSplits = new List<DataTable> () { Table.Clone(), Table.Clone() };

					// разделить данные на блоки и вывести длительновть выполнения каждого блока (попробовать вывести информацию по длительности в разрезе колонок)

					/*
					Parallel.For(0, Table.Rows.Count, (j) => {
						//Console.WriteLine("Beginning iteration {0}", j);
						if (Table.Rows [j].Field<double> (col.Ordinal) <= tmpThreshold) {
							lock (syncLeft)
							{
								tmpSplits [0].ImportRow (Table.Rows [j]);
							}
						} else {
							lock (syncRight)
							{
								tmpSplits [1].ImportRow (Table.Rows [j]);
							}
						}
					});
					*/

					//sw.Start ();


					for (int j = 0; j < Table.Rows.Count; j++) {
						if (Table.Rows [j].Field<double> (col.Ordinal) <= tmpThreshold) {
							tmpSplits [0].ImportRow (Table.Rows [j]);
						} else {
							tmpSplits [1].ImportRow (Table.Rows [j]);
						}
					}


					//sw.Stop ();
					//Delays.delays ["CalcBestNumSplit_FillSplits"].Add (sw.ElapsedTicks);
					//Console.WriteLine ("delays [\"CalcBestNumSplit_FillSplits\"] = " + Delays.delays ["CalcBestNumSplit_FillSplits"].First ());
					//Console.WriteLine ("[" + sw.Elapsed + "]" + " CalcBestNumSplit_FillSplits");
					//sw.Reset ();

					//sw.Start ();

					tmpQuality = SplitQualityAlgorithm.CalcSplitQuality (tmpSplits, col);

					//sw.Stop ();
					//Delays.delays ["CalcBestNumSplit_CalcQuality"].Add (sw.ElapsedTicks);
					//Console.WriteLine ("delays [\"CalcBestNumSplit_CalcQuality\"] = " + Delays.delays ["CalcBestNumSplit_CalcQuality"].First ());
					//Console.WriteLine ("[" + sw.Elapsed + "]" + " CalcBestNumSplit_CalcQuality");
					//sw.Reset ();

					if (i == 0) {
						//del_index = i;
						//Console.WriteLine ("first fix threshold");
						Fix (tmpQuality, tmpThreshold, tmpSplits, col);
					} else if (SplitQualityAlgorithm.Compare (tmpQuality, Quality) < 0) {
						//del_index = i;
						Fix (tmpQuality, tmpThreshold, tmpSplits, col);
					}
				//}
			}

			//if (Thrshld.dict [col.Ordinal].Count () > 0) {
				//Console.WriteLine ("del threshold " + Threshold + " from dict. dict cnt = " + Thrshld.dict [col.Ordinal].Count ());
				//Thrshld.Del (del_index);
			//}
			//Thrshld.dict [col.Ordinal].Remove ((double)Threshold);
			//Thrshld.dict [col.Ordinal].RemoveAt (del_i);

			//sw.Start ();

			CalcClass ();

			//sw.Stop ();
			//Delays.delays ["CalcBestNumSplit_CalcClass"].Add (sw.ElapsedTicks);
			//Console.WriteLine ("delays [\"CalcBestNumSplit_CalcClass\"] = " + Delays.delays ["CalcBestNumSplit_CalcClass"].First ());
			//Console.WriteLine ("[" + sw.Elapsed + "]" + " CalcBestNumSplit_CalcClass");
			//sw.Reset ();
		}

		private void CalcBestCatSplit(DataColumn col) {
			double tmpQuality;
			int max = Table.AsEnumerable().Max(r => (int)r[col]);
			int Categories = (int)(Math.Pow(2, (int)(Math.Log(max, 2) + 1)) - 1);	// определяем кол-во категорий как максимальное значение категориального аттрибута
			for (long set = 1; set < Categories - 1; set++) {	// перебираем все катигории
				List<DataTable> tmpSplits = new List<DataTable>() { Table.Clone(), Table.Clone() };
				for (int i = 0; i < Table.Rows.Count; i++) {
					if ((~set & Table.Rows [i].Field<int> (col)) == 0) {
						tmpSplits [0].ImportRow (Table.Rows [i]);
					} else {
						tmpSplits [1].ImportRow (Table.Rows [i]);
					}
				}
				tmpQuality = SplitQualityAlgorithm.CalcSplitQuality(tmpSplits, col);
				if (set == 1) {
					Fix (tmpQuality, set, tmpSplits, col);
				} else if (SplitQualityAlgorithm.Compare (tmpQuality, Quality) < 0) {
					Fix (tmpQuality, set, tmpSplits, col);
				}
			}
			CalcClass ();
		}
	}
}