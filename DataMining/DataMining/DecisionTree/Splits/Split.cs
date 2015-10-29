using System;
using System.Data;
using System.Collections.Generic;

namespace DataMining
{
	public class Split
	{
		public DataTable Table { get; set; }
		public List<DataTable> Splits { get; private set; }

		public Split():this(new DataTable()) { }

		public Split (DataTable table)
		{
			Table = table;
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

				// считаем качество разбиения
				// сохраняем разбиение
			}

			return Splits;
		}

		public List<DataTable> CalcBestSplit(DataTable table)
		{
			Table = table;
			return CalcBestSplit();
		}

		public List<DataTable> CalcBestNumSplit(DataColumn col)
		{
			throw new NotImplementedException ();
		}

		public List<DataTable> CalcBestCatSplit(DataColumn col)
		{
			throw new NotImplementedException ();
		}
	}
}