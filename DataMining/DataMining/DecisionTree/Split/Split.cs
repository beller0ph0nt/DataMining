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
	public class ThreadParam {
		public DataColumn col;
		public AutoResetEvent threadIsDone;

		public ThreadParam(DataColumn column, AutoResetEvent e) {
			col = column;
			threadIsDone = e;
		}
	}

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

		private bool initDone;
		private object lck1;
		private object lck2;
		private object lck3;
		private object lck4;
		private object fixLock;

		public Split(DataTable table):this(table, new GiniSplit()) {}
		public Split(DataTable table, ISplitQualityAlgorithm algo) {
			Threshold = null;
			Table = table;
			SplitQualityAlgorithm = algo;
			Splits = new List<DataTable> ();

			initDone = false;
			lck1 = new object ();
			lck2 = new object ();
			lck3 = new object ();
			lck4 = new object ();
			fixLock = new object ();
		}

		public List<DataTable> CalcBestSplit() {
			List<AutoResetEvent> locks = new List<AutoResetEvent> ();
			for (int col = 0; col < Table.Columns.Count - 1; col++) {
				if (Table.Rows [0] [col] is int) {
					AutoResetEvent e = new AutoResetEvent (false);
					ThreadPool.QueueUserWorkItem (CalcBestCatSplit, new ThreadParam(Table.Columns [col], e));
					locks.Add (e);
					//CalcBestCatSplit (Table.Columns [col]);
				} else if (Table.Rows [0] [col] is double) {
					AutoResetEvent e = new AutoResetEvent (false);
					ThreadPool.QueueUserWorkItem (CalcBestNumSplit, new ThreadParam(Table.Columns [col], e));
					locks.Add (e);
					//CalcBestNumSplit (Table.Columns [col]);
				} else {
					throw new Exception ("Bad col type");
				}
			}

			for (int i = 0; i < locks.Count; i++) {
				locks [i].WaitOne ();
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
				Console.WriteLine ("[ ERROR ] wrong type of col");
				Console.WriteLine ("type: " + row [ColOrdinal].GetType ().ToString ());
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

		//private void CalcBestNumSplit(DataColumn col) {
		private void CalcBestNumSplit(object par) {
			ThreadParam p = par as ThreadParam;
			if (Table.Rows.Count > 1) {
				DataColumn col = p.col;
				//Table.DefaultView.Sort = col.ColumnName + " asc";
				//Table = Table.DefaultView.ToTable ();

				List<double> thresholdList = new List<double> ();
				Parallel.For (0, Table.Rows.Count - 1, i => {
					double thr = ((double)Table.Rows [i] [col.Ordinal] + (double)Table.Rows [i + 1] [col.Ordinal]) / 2.0;
					lock (lck4) {
						if (!thresholdList.Contains (thr)) {
							thresholdList.Add (thr);
						}
					}
				});

				//for (int i = 0; i < Table.Rows.Count - 1; i++) {
				//for (int i = 0; i < t.Count; i++)
				Parallel.For (0, thresholdList.Count, i => {
					double tmpThreshold = thresholdList [i];
					//tmpThreshold = (Table.Rows[i].Field<double>(col.Ordinal) + Table.Rows[i + 1].Field<double>(col.Ordinal)) / 2.0;	// вычисляем порог, как среднее

					//List<DataTable> tmpSplits = new List<DataTable> () { Table.Clone(), Table.Clone() };
					List<DataTable> tmpSplits = new List<DataTable> ();
					lock (lck3) {
						tmpSplits.Add (Table.Clone ());
					}
					tmpSplits.Add (tmpSplits [0].Clone ());

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

					int cnt = Table.Rows.Count;
					int ordinal = col.Ordinal;
					lock (lck2) {
						for (int j = 0; j < cnt; j++) {
							if ((double)Table.Rows [j] [ordinal] <= tmpThreshold) {
								tmpSplits [0].ImportRow (Table.Rows [j]);
							} else {
								tmpSplits [1].ImportRow (Table.Rows [j]);
							}
						}
					}

					double tmpQuality;
					lock (lck1)
					{
						tmpQuality = SplitQualityAlgorithm.CalcSplitQuality (tmpSplits, col);
					}


					lock (fixLock) {
						if (!initDone) {
							Fix (tmpQuality, tmpThreshold, tmpSplits, col);
							initDone = true;
						} else if (SplitQualityAlgorithm.Compare (tmpQuality, Quality) < 0) {
							Fix (tmpQuality, tmpThreshold, tmpSplits, col);
						}
					}
				});
			} else {
				Quality = 0;
			}
			CalcClass ();
			p.threadIsDone.Set ();
		}

		//private void CalcBestCatSplit(DataColumn col) {
		private void CalcBestCatSplit(object par) {
			ThreadParam p = par as ThreadParam;
			DataColumn col = p.col;

			int max = Table.AsEnumerable().Max(r => (int)r[col]);
			int Categories = (int)(Math.Pow(2, (int)(Math.Log(max, 2) + 1)) - 1);	// определяем кол-во категорий как максимальное значение категориального аттрибута


			//for (int set = 1; set < Categories - 1; set++) {	// перебираем все катигории
			Parallel.For (1, Categories - 1, set => {
				//List<DataTable> tmpSplits = new List<DataTable> () { Table.Clone(), Table.Clone() };
				List<DataTable> tmpSplits = new List<DataTable> ();
				lock (lck3) {
					tmpSplits.Add (Table.Clone ());
				}
				tmpSplits.Add (tmpSplits [0].Clone ());

				lock (lck2) {
					for (int i = 0; i < Table.Rows.Count; i++) {
						if ((~set & Table.Rows [i].Field<int> (col)) == 0) {
							tmpSplits [0].ImportRow (Table.Rows [i]);
						} else {
							tmpSplits [1].ImportRow (Table.Rows [i]);
						}
					}
				}

				double tmpQuality;
				lock (lck1)
				{
					tmpQuality = SplitQualityAlgorithm.CalcSplitQuality (tmpSplits, col);
				}

				lock (fixLock) {
					if (!initDone) {
						Fix (tmpQuality, set, tmpSplits, col);
						initDone = true;
					} else if (SplitQualityAlgorithm.Compare (tmpQuality, Quality) < 0) {
						Fix (tmpQuality, set, tmpSplits, col);
					}
				}

				/*
				if (set == 1) {
					lock (_initLock) {
						if (!initLock.IsSet) {
							Fix (tmpQuality, set, tmpSplits, col);
							initLock.Set ();
						} else if (SplitQualityAlgorithm.Compare (tmpQuality, Quality) < 0) {
							Fix (tmpQuality, set, tmpSplits, col);
						}
					}
				}

				if (set != 1) {
					initLock.Wait ();
					lock (lck0) {
						if (SplitQualityAlgorithm.Compare (tmpQuality, Quality) < 0) {
							Fix (tmpQuality, set, tmpSplits, col);
						}
					}
				}
				*/
			});
			//}
			CalcClass ();
			p.threadIsDone.Set ();
		}
	}
}