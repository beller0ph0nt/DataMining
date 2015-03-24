using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Attributes;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining.DecisionTree.Splits
{
    public abstract class SplitBase : IComparable
    {
        #region Свойства

        /// <summary>
        /// Оценка качества разбиения
        /// </summary>
        public double Quality { get; protected set; }

        /// <summary>
        /// Порог
        /// </summary>
        public object Threshold { get; protected set; }

        /// <summary>
        /// Алгоритм оценки качества разбиения
        /// </summary>
        public ISplitQualityAlgorithm SplitQualityAlgorithm { get; private set; }

        /// <summary>
        /// Разбиения
        /// </summary>
        public List<List<object>> Splits { get; private set; }

        #endregion

        public SplitBase()
        {
            SplitQualityAlgorithm = new GiniSplit();
            Splits = new List<List<object>>();
        }

        public abstract void CalcBestSplit(AttributeBase a);

        public int CompareTo(object obj)
        {
            return SplitQualityAlgorithm.Compare(Quality, ((SplitBase)obj).Quality);
        }
    }
}