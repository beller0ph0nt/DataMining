﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMining.DecisionTree.SplitQualityAlgorithm;
using DataMining.DecisionTree.Attributes;

namespace DataMining.DecisionTree.LearningAlgorithm
{
    public class CARTLearning : ILearningAlgorithm
    {
        private ITree _tree;
        //private ISplitQualityAlgorithm _splitQuality;
        //private Dictionary<int, ISplitQualityAlgorithm> _bestSplitQualityes;

        public CARTLearning(ITree tree)
        {
            _tree = tree;
            //_splitQuality = new GiniSplit();
            //_bestSplitQualityes = new Dictionary<int, ISplitQualityAlgorithm>();
        }

        private List<AttributeBase<object>> GetBestSplit(List<AttributeBase<object>> inputs)
        {
            int index = 0;
            List<List<AttributeBase<object>>> splits = new List<List<AttributeBase<object>>>();

            for (int i = 0; i < inputs.Count; i++)
            {
                splits.Add(inputs[i].Split());
                if (i == 0)
                    index = i;
                else
                {
                    // Если i-ый элемент оказался лучше
                    if (inputs[i].SplitVar.CompareTo(inputs[index].SplitVar) == -1)
                        index = i;
                }
            }

            return splits[index];
        }

        public void Training(List<AttributeBase<object>> inputs, AttributeBase<object> answers)
        {
            // Создать корень у дерева
            // Получить лучшее разбиение
            // Создать у текущего узла два листа
            // В левый лист поместить [0] разбиение
            // В правый лист поместить [1] разбиение

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

                    //_splitQuality.CalcSplitQuality(new List<List<double>>() { firstSplit, secondSplit },
                    //    attribut.Count);

                    //if (j == 0)
                        //_bestSplitQualityes[i] = _splitQuality;
                    //else
                    //{
                        //if (_splitQuality.CompareTo(_bestSplitQualityes[i]) < 0)
                        //    _bestSplitQualityes[i] = _splitQuality;
                    //}
                }
            }

            // Необходимо сохранять и порог для лучшего разбиения
            //_bestSplitQualityes.OrderBy(p => p.Value.Quality).First();
        }
    }
}
