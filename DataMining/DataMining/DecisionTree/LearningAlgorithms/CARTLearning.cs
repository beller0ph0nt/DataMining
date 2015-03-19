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
}
