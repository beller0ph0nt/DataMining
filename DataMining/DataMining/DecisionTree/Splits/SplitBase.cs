using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Attributes;
using DataMining.DecisionTree.SplitQualityAlgorithm;
using DataMining.DecisionTree.Elements;

namespace DataMining.DecisionTree.Splits
{
    public abstract class SplitBase<T> : IComparable
    {
        #region Свойства

        /// <summary>
        /// оценка качества разбиения
        /// </summary>
        public double Quality { get; protected set; }

        /// <summary>
        /// порог, необходим для разбиения числовых переменныъ
        /// </summary>
        public double Threshold { get; protected set; }

        /// <summary>
        /// кол-во категорий, необходим для разбиения категориальных переменных
        /// </summary>
        public int Categories { get; set; }

        /// <summary>
        /// алгоритм оценки качества разбиения
        /// </summary>
        public ISplitQualityAlgorithm<T> SplitQualityAlgorithm { get; private set; }

        /// <summary>
        /// разбиения полученные в результате
        /// </summary>
        public List<List<Cell>> Splits { get; private set; }

        #endregion

        public SplitBase()
        {
            SplitQualityAlgorithm = new GiniSplit<T>();
            Splits = new List<List<Cell>>();
        }

        protected void Fix(double quality, double threshold, List<List<Cell>> splits)
        {
            Quality = quality;
            Threshold = threshold;
            Splits.Clear();
            splits.ForEach(s => Splits.Add(s));
        }

        //public abstract void CalcBestSplit(List<T> a);
        public abstract void CalcBestSplit(List<Cell> a);

        public int CompareTo(object obj)
        {
            return SplitQualityAlgorithm.Compare(Quality, ((SplitBase<T>)obj).Quality);
        }
    }
}