using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Attributes;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining.DecisionTree.Splits
{
    public abstract class SplitBase<T> : IComparable
    {
        #region Свойства

        /// <summary>
        /// Оценка качества разбиения
        /// </summary>
        public double Quality { get; protected set; }

        /// <summary>
        /// Порог
        /// </summary>
        public double Threshold { get; protected set; }

        /// <summary>
        /// Кол-во категорий
        /// </summary>
        public int Categories { get; set; }

        /// <summary>
        /// Алгоритм оценки качества разбиения
        /// </summary>
        public ISplitQualityAlgorithm<T> SplitQualityAlgorithm { get; private set; }

        /// <summary>
        /// Разбиения
        /// </summary>
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

        public int CompareTo(object obj)
        {
            return SplitQualityAlgorithm.Compare(Quality, ((SplitBase<T>)obj).Quality);
        }
    }
}