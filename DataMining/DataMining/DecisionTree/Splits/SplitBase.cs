using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.Attributes;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining.DecisionTree.Splits
{
    public abstract class SplitBase<T>
    {
        #region Свойства

        /// <summary>
        /// Оценка качества расщепления
        /// </summary>
        public double Quality { get; protected set; }

        /// <summary>
        /// Порог
        /// </summary>
        public T Threshold { get; protected set; }

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

        public abstract void CalcBestSplit(AttributeBase<T> a);
    }
}