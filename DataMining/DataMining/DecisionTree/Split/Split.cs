using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using DataMining.DecisionTree.SplitQualityAlgorithm;

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
			for (int col = 0; col < Table.Columns.Count - 1; col++) {
				if (Table.Rows[0][col] is int) {
					CalcBestCatSplit(Table.Columns[col]);
				} else if (Table.Rows[0][col] is double) {
					CalcBestNumSplit(Table.Columns[col]);
				} else {
					throw new Exception ("Bad col type");
				}
			}
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
			double tmpQuality, tmpThreshold;
			Table.DefaultView.Sort = col.ColumnName + " asc";
			Table = Table.DefaultView.ToTable ();
			for (int i = 0; i < Table.Rows.Count - 1; i++) {
				tmpThreshold = (Table.Rows[i].Field<double>(col.Ordinal) + Table.Rows[i + 1].Field<double>(col.Ordinal)) / 2.0;	// вычисляем порог, как среднее
				List<DataTable> tmpSplits = new List<DataTable> () { Table.Clone(), Table.Clone() };
				for (int j = 0; j < Table.Rows.Count; j++) {
					if (Table.Rows [j].Field<double> (col.Ordinal) <= tmpThreshold) {
						tmpSplits [0].ImportRow (Table.Rows [j]);
					} else {
						tmpSplits [1].ImportRow (Table.Rows [j]);
					}
				}
				tmpQuality = SplitQualityAlgorithm.CalcSplitQuality (tmpSplits, col);
				if (i == 0) {
					Fix (tmpQuality, tmpThreshold, tmpSplits, col);
				} else if (SplitQualityAlgorithm.Compare (tmpQuality, Quality) < 0) {
					Fix (tmpQuality, tmpThreshold, tmpSplits, col);
				}
			}
			CalcClass ();
		}

		public void CalcBestCatSplit(DataColumn col) {
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