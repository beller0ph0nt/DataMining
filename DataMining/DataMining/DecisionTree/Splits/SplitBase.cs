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

        // оценка качества разбиения
        public double Quality { get; protected set; }
        // порог, необходим для разбиения числовых переменныъ
        public double Threshold { get; protected set; }
        // кол-во категорий, необходим для разбиения категориальных переменных
        public int Categories { get; set; }
        // алгоритм оценки качества разбиения
        public ISplitQualityAlgorithm<T> SplitQualityAlgorithm { get; private set; }
        // разбиения полученные в результате
        public List<List<T>> Splits { get; private set; }

        #endregion

        public SplitBase()
        {
            SplitQualityAlgorithm = new GiniSplit<T>();
            Splits = new List<List<T>>();
        }

		protected void Fix(double quality, double threshold, List<List<T>> splits)
        {
            Quality = quality;
            Threshold = threshold;
            Splits.Clear();
            splits.ForEach(s => Splits.Add(s));
        }

        public abstract void CalcBestSplit(List<T> a);
        //public abstract void CalcBestSplit(List<Cell> a);

        public int CompareTo(object obj)
        {
            return SplitQualityAlgorithm.Compare(Quality, ((SplitBase<T>)obj).Quality);
        }
    }
}