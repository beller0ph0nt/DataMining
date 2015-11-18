using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining
{
	[Serializable]
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
				if (Table.Rows [0] [col] is int)	// осуществляем разбиение по колонке
					CalcBestCatSplit (col);
				else if (Table.Rows [0] [col] is double)
					CalcBestNumSplit (col);
				else 
					throw new Exception ("Bad col type");

			return Splits;
		}

		public List<DataTable> CalcBestSplit(DataTable table)
		{
			Table = table;
			return CalcBestSplit();
		}

		private void Fix(double quality, object threshold, List<DataTable> splits, DataColumn col)
		{
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

			for (int i = 0; i < Table.Rows.Count - 1; i++)
			{	// вычисляем порог, как среднее
				tmpThreshold = (Table.Rows[i].Field<double>(col.Ordinal) + Table.Rows[i + 1].Field<double>(col.Ordinal)) / 2.0;

				List<DataTable> tmpSplits = new List<DataTable>();	// выделяем память под временное разбиение
				tmpSplits.Add(Table.Clone());                  	// первое разбиение
				tmpSplits.Add(Table.Clone());                  	// второе разбиение

				for (int j = 0; j < Table.Rows.Count; j++)
					if (Table.Rows[j].Field<double>(col.Ordinal) <= tmpThreshold)
						tmpSplits[0].ImportRow(Table.Rows[j]);
					else
						tmpSplits[1].ImportRow(Table.Rows[j]);
					
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
			int max = Table.AsEnumerable().Max(d => (int)d[col]);
			int Categories = (int)(Math.Pow(2, (int)(Math.Log(max, 2.0) + 1)) - 1);	// определяем кол-во категорий как максимальное значение категориального аттрибута

			for (long set = 1; set < Categories - 1; set++)  // перебираем все катигории
			{
				List<DataTable> tmpSplits = new List<DataTable>();	// выделяем память под временное разбиение
				tmpSplits.Add(Table.Clone());                  	// первое разбиение
				tmpSplits.Add(Table.Clone());                  	// второе разбиение

				for (int i = 0; i < Table.Rows.Count; i++)
					if ((~set & Table.Rows [i].Field<int> (col)) == 0)
						tmpSplits [0].ImportRow (Table.Rows [i]);
					else
						tmpSplits [1].ImportRow(Table.Rows [i]);

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