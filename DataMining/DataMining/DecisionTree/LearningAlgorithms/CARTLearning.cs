using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMining.DecisionTree.LearningAlgorithm
{
    public class CARTLearning : ILearningAlgorithm
    {
        private ITree _tree;
        private ISplitQualityAlgorithm _splitQuality;
        private Dictionary<int, ISplitQualityAlgorithm> _bestSplitQualityes;

        public CARTLearning(ITree tree)
        {
            _tree = tree;
            _splitQuality = new GiniSplit();
            _bestSplitQualityes = new Dictionary<int, ISplitQualityAlgorithm>();
        }

        public void Training(List<List<double>> inputs, List<double> answers)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                var attribut = inputs[i];

                attribut.Sort();

                for (int j = 0; j < attribut.Count - 1; j++)
                {
                    double threshold = (attribut[j] + attribut[j + 1]) / 2;
                    var firstSplit = attribut.Where(e => e <= threshold).ToList();
                    var secondSplit = attribut.Where(e => e > threshold).ToList();

                    _splitQuality.CalcSplitQuality(new List<List<double>>() { firstSplit, secondSplit },
                        attribut.Count);

                    if (j == 0)
                        _bestSplitQualityes[i] = _splitQuality;
                    else
                    {
                        //if (_splitQuality.CompareTo(_bestSplitQualityes[i]) < 0)
                        //    _bestSplitQualityes[i] = _splitQuality;
                    }
                }
            }

            // Необходимо сохранять и порог для лучшего разбиения
            //_bestSplitQualityes.OrderBy(p => p.Value.Quality).First();
        }
    }

    
    public enum AttributType
    {
        Numerical,
        Categorical
    }

    public abstract class Attribute
    {
        public int Id { get; set; }
        public AttributType Type { get; set; }

        public abstract Split GetBestSplit();
    }

    public class NumericalAttribute : Attribute
    {
        public List<double> Values { get; set; }

        public override Split GetBestSplit()
        {
            Split split = new NumericalSplit();

            split.CalcBestSplit(this);

            return split;
        }
    }

    public class CategoricalAttribute : Attribute
    {
        public int CategoriesCounter { get; set; }
        public List<int> Values { get; set; }

        public override Split GetBestSplit()
        {
            Split split = new CategoricalSplit();

            split.CalcBestSplit(this);

            return split;
        }
    }

    public abstract class Split
    {
        public double Quality { get; set; }                                 // Оценка качества расщепления
        public double Threshold { get; set; }                               // Порог
        public List<List<double>> Splits { get; set; }                      // Полученные разбиения
        public ISplitQualityAlgorithm SplitQualityAlgorithm { get; set; }   // Алгоритм определения оценки качества расщепления

        public Split()
        {
            Splits = new List<List<double>>();
            SplitQualityAlgorithm = new GiniSplit();
        }

        /// <summary>
        /// Метод оптимально разбивает атрибут на подмножества
        /// </summary>
        /// <param name="a">Атрибут для разбиения</param>
        public abstract void CalcBestSplit(Attribute a);
    }

    public class NumericalSplit : Split
    {
        public override void CalcBestSplit(Attribute a)
        {
            double tmpQuality;
            double tmpThreshold;
            var b = a as NumericalAttribute;

            // Сортировка атрибута. Необходима для определения порога
            b.Values.Sort();

            for (int i = 0; i < b.Values.Count - 1; i++)
            {
                // Вычисляем порог
                tmpThreshold = (b.Values[i] + b.Values[i + 1]) / 2;

                var firstSplit = b.Values.Where(e => e <= tmpThreshold).ToList();
                var secondSplit = b.Values.Where(e => e > tmpThreshold).ToList();

                tmpQuality = SplitQualityAlgorithm.CalcSplitQuality(
                    new List<List<double>>() { firstSplit, secondSplit },
                    b.Values.Count);

                if (i == 0)
                {
                    Quality = tmpQuality;
                    Threshold = tmpThreshold;
                    Splits.Add(firstSplit);
                    Splits.Add(secondSplit);
                }
                else
                {
                    if (SplitQualityAlgorithm.Compare(tmpQuality, Quality) < 0)
                    {
                        Quality = tmpQuality;
                        Threshold = tmpThreshold;
                        Splits[0] = firstSplit;
                        Splits[1] = secondSplit;
                    }
                }
            }
        }
    }

    public class CategoricalSplit : Split
    {
        public override void CalcBestSplit(Attribute a)
        {
            //double tmpQuality;
            //double tmpThreshold;
            var b = a as CategoricalAttribute;

            // Сортировка атрибута. Необходима для определения порога
            b.Values.Sort();

            for (int set = 1; set < b.CategoriesCounter - 1; set++)
            {
                for (int j = 0; j < b.Values.Count; j++)
                {
                    // Вычисляем порог. Порог уже есть - это set

                    // Разбиваем множество
                    var secondSplit = b.Values.Where(i => (~set & i) == 0);
                    //var secondSplit = a.Values.Where(e => e > tmpThreshold).ToList();

                    // Оцениваем разбиение
                    // Сравниваем разбиение с предыдущим
                    // Сохраняем наилучшее
                }
            }
        }
    }
}
