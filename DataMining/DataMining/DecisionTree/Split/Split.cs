using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining {
	[Serializable]
	public class Split {
		public DataTable Table { get; set; }
		public List<DataTable> Splits { get; private set; }
		public object Threshold { get; private set; }
		public double Quality { get; private set;}
		public double ClassErr { get; private set;}
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
			return Splits.Count == 0 || Splits [0].Rows.Count == 0 || Splits [1].Rows.Count == 0;
		}

		public List<DataTable> CalcBestSplit(DataTable table) {
			Table = table;
			return CalcBestSplit();
		}

		private void Fix(double quality, object threshold, List<DataTable> splits, DataColumn col) {
			Quality = quality;
			Threshold = threshold;
			Splits = splits;
			ColOrdinal = col.Ordinal;
		}

		private void CalcClassErr() {
			if (Table.Rows.Count > 0) {
				//Table.AsEnumerable().GroupBy(row => row[ColOrdinal], (key, grp) => new { row = grp.First(), cnt = grp.Count }).Max(
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
		}

		public override string ToString() {
			return string.Format("qlt: {0} thrld: {1}", Quality.ToString(), Threshold.ToString());
		}
	}
}