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
        public double Quality { get; set; }                                     // Оценка качества расщепления
        public double Threshold { get; set; }                                   // Порог
        public ISplitQualityAlgorithm<T> SplitQualityAlgorithm { get; set; }    // Алгоритм определения оценки качества расщепления
        public List<List<T>> Splits { get; set; }                               // Полученные разбиения
        
        public SplitBase()
        {
            SplitQualityAlgorithm = new GiniSplit<T>();
            Splits = new List<List<T>>();
        }

        public abstract void CalcBestSplit(AttributeBase<T> a);
    }
}