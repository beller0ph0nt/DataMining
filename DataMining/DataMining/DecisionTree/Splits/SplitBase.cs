using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.SplitQualityAlgorithm;
using DataMining.DecisionTree.Attributes;

namespace DataMining.DecisionTree.Splits
{
    public abstract class SplitBase
    {
        public double Quality { get; set; }                                 // Оценка качества расщепления
        public double Threshold { get; set; }                               // Порог
        public ISplitQualityAlgorithm SplitQualityAlgorithm { get; set; }   // Алгоритм определения оценки качества расщепления

        public SplitBase()
        {
            SplitQualityAlgorithm = new GiniSplit();
        }

        /// <summary>
        /// Метод оптимально разбивает атрибут на подмножества
        /// </summary>
        /// <param name="a">Атрибут для разбиения</param>
        public abstract void CalcBestSplit(AttributeBase a);
    }
}
