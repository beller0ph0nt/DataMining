using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining
{
	public class Split
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
				// осуществляем разбиение по колонке
				if (Table.Rows[0][col] is long)
					CalcBestCatSplit(col);
				else
					CalcBestNumSplit(col);
			}

			return Splits;
		}

		public List<DataTable> CalcBestSplit(DataTable table)
		{
			Table = table;
			return CalcBestSplit();
		}

		//private void Fix(double quality, double threshold, List<DataTable> splits)
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
			{
				// вычисляем порог, как среднее
				tmpThreshold = (Table.Rows[i].Field<double>(col) + Table.Rows[i + 1].Field<double>(col)) / 2;

				List<DataTable> tmpSplits = new List<DataTable>();	// выделяем память под временное разбиение
				tmpSplits.Add(new DataTable());                  // первое разбиение
				tmpSplits.Add(new DataTable());                  // второе разбиение

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
			int Categories = 0;

			// определяем кол-во категорий как максимальное значение категориального аттрибута
			// ...

			for (int set = 1; set < Categories - 1; set++)  // перебираем все катигории
			{
			}

			throw new NotImplementedException ();
		}
	}
}