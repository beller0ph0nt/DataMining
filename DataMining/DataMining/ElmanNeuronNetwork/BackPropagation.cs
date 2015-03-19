using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.ElmanNeuronNetwork
{
    public class BackPropagation
    {
        #region Приватные данные класса

        private double _error;
        private ElmanNeuronNetwork _net = null;
        
        /// <summary>
        /// Временное хранилище для частичной ошибки
        /// </summary>
        private double[][] neuronError = null;

        /// <summary>
        /// Окочательная ошибка для конкретного порога чувствительности
        /// </summary>
        private double[][] thresholdError = null;

        /// <summary>
        /// Окончательная ошибка для конкретного веса
        /// </summary>
        private double[][][] weightError = null;

        #endregion

        public BackPropagation(ElmanNeuronNetwork net)
        {
            _error = 0;
            _net = net;

            neuronError = new double[_net.Layers.Length][];
            thresholdError = new double[_net.Layers.Length][];
            weightError = new double[_net.Layers.Length][][];

            for (int i = _net.Layers.Length - 1; i >= 0; i--)
            {
                neuronError[i] = new double[_net.Layers[i].Neurons.Length];
                thresholdError[i] = new double[_net.Layers[i].Neurons.Length];
                weightError[i] = new double[_net.Layers[i].Neurons.Length][];

                for (int j = _net.Layers[i].Neurons.Length - 1; j >= 0; j--)
                {
                    weightError[i][j] = new double[_net.Layers[i].Neurons[j].Weights.Length];
                }
            }
        }

        public void Run(double[] input, double[] answer)
        {
            // Вычисляем выход сети
            _net.Compute(input);

            // Вычисляем ошибку сети //private void ComputeNetworkError()
            int i, j, k;
            double e;
            int lastLayerIndex = _net.Layers.Length - 1;
            Neuron[] lastLayerNeurons = _net.Layers[lastLayerIndex].Neurons;

            for (i = lastLayerNeurons.Length - 1; i >= 0; i--)
            {
                e = (lastLayerNeurons[i].Axon - answer[i]);

                neuronError[lastLayerIndex][i] = e * lastLayerNeurons[i].F.dF2(lastLayerNeurons[i].Axon);

                for (j = lastLayerNeurons[i].Weights.Length - 1; j >= 0; j--)
                {
                    weightError[lastLayerIndex][i][j] = 0;
                }

                _error += (e * e);
            }

            _error /= 2.0;

            // - Посчитать значение нового веса и порога срабатывания
            // Пройтись по всем слоям начиная с последнего
            for (i = _net.Layers.Length - 1; i >= 0; i--)
            {
                // Пройтись по всем нейронам слоя
                for (j = _net.Layers[i].Neurons.Length - 1; j >= 0; j--)
                {
                    // Вычислить ошибку i-го нейрона

                    // Пройтись по всем весам нейронам
                    for (k = _net.Layers[i].Neurons[j].Weights.Length - 1; k >= 0; k--)
                    {
                        weightError[i][j][k] = _net.Layers[i - 1].Neurons[k].Axon * neuronError[i][j];
                        // Найти градиент конкретного веса и порогового состояния нейрона (т.к. это тоже вес)
                    }
                }
            }


                // - Заменить старые веса на новые
                // Пройтись по всем слоям
                // Пройтись по всем нейронам
                // Пройтись по всем весам и скорректировать их на размер вычисленного градиента

            // Обновляем контекст
            _net.UpdateContext();
        }

        public void RunEpoh(double[][] inputs, double[][] answers)
        {
            _error = 0;

            for (int i = 0; i < inputs.Length; i++)
                Run(inputs[i], answers[i]);
        }
    }
}
