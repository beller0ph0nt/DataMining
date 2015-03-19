using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.SplitQualityAlgorithm;
using DataMining.DecisionTree.Attributes;

namespace DataMining.DecisionTree.Splits
{
    public abstract class AbstractSplitBase
    {
        public double Quality { get; set; }                                 // Оценка качества расщепления
        public double Threshold { get; set; }                               // Порог
        public List<List<double>> Splits { get; set; }                      // Полученные разбиения
        public ISplitQualityAlgorithm SplitQualityAlgorithm { get; set; }   // Алгоритм определения оценки качества расщепления

        public AbstractSplitBase()
        {
            Splits = new List<List<double>>();
            SplitQualityAlgorithm = new GiniSplit();
        }

        /// <summary>
        /// Метод оптимально разбивает атрибут на подмножества
        /// </summary>
        /// <param name="a">Атрибут для разбиения</param>
        public abstract void CalcBestSplit(AbstractAttributeBase a);
    }
}
