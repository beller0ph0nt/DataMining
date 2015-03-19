using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Debugging;

namespace DataMining.NeuronNetwork
{
    /// <summary>
    /// Класс алгоритма обратного распространения ошибки
    /// </summary>
    public class BackPropagation : ILearningAlgorithm
    {
        private double _error;                                      // Ошибка нейросети
        private double _trainingSpeed;                              // Скорость обучения
        private INeuronNetwork _neuronNetwork;                      // Нейронная сеть
        private double _a;                                          // Коэффициент момента

        #region Свойства

        /// <summary>
        /// 
        /// </summary>
        public double Error { get { return _error; } }

        /// <summary>
        /// 
        /// </summary>
        public double TrainingSpeed { get { return _trainingSpeed; } set { _trainingSpeed = value; } }

        #endregion

        /// <summary>
        /// Конструктор алгоритма обратного растпространения ошибки
        /// </summary>
        /// <param name="neuronNetwork">Нейронная сеть</param>
        /// <param name="trainingSpeed">Скорость обучения</param>
        /// <param name="a">Коэффициент момента</param>
        public BackPropagation(INeuronNetwork neuronNetwork, double trainingSpeed, double a)
        {
            _neuronNetwork = neuronNetwork;
            _trainingSpeed = trainingSpeed;
            _a = a;
        }

        #region Методы

        /// <summary>
        /// Метод, обучающий нейронную сети на одной выборке
        /// </summary>
        /// <param name="input">Входной вектор</param>
        /// <param name="answers">Вектор ответов</param>
        public void Training(List<double> input, List<double> answer)
        {
            try
            {
                if (input.Count == 0)
                    throw new Exception("Вектор input пустой");
                else if (answer.Count == 0)
                    throw new Exception("Вектор answer пустой");
                else if (answer.Count != _neuronNetwork.Layers[_neuronNetwork.Layers.Count - 1].Neurons.Count)
                    throw new Exception("Вектор answer не совпадает с размерностью сети");

                _neuronNetwork.Calc(input);

                INeuron neuron;
                for (int i = 0; i < _neuronNetwork.Layers[_neuronNetwork.Layers.Count - 1].Neurons.Count; i++)
                {
                    neuron = _neuronNetwork.Layers[_neuronNetwork.Layers.Count - 1].Neurons[i];
                    ErrorPropagation(neuron, (neuron.Axon - answer[i]));    // Распространяем ошибку по сети
                }
                
                CalcError(answer);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Метод, обучающий нейронную сеть на всем массиве векторов
        /// </summary>
        /// <param name="inputs">Список входных векторов</param>
        /// <param name="answers">Список векторов ответов</param>
        public void Training(List<List<double>> inputs, List<List<double>> answers)
        {
            try
            {
                if (inputs.Count == 0)
                    throw new Exception("Список inputs пустой");
                else if (answers.Count == 0)
                    throw new Exception("Список answers пустой");
                else if (inputs.Count != answers.Count)
                    throw new Exception("Размеры inputs и answers не совпадают");

                for (int i = 0; i < inputs.Count - 1; i++)
                    Training(inputs[i], answers[i]);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Метод, асинхронно  распространяющий ошибку
        /// </summary>
        /// <param name="obj">Параметры</param>
        private void ErrorPropagationAsync(object obj)
        {
        }

        /// <summary>
        /// Метод, распространяющий ошибку по нейронам
        /// </summary>
        /// <param name="neuron">Нейрон</param>
        /// <param name="error">Распространяемая ошибка</param>
        private void ErrorPropagation(INeuron neuron, double error)
        {
            if (neuron.Type != NeuronType.S)
            {
                double neuronError = error * neuron.F.Derivative2(neuron.Axon); // Вычисляем ошибку текущего нейрона
                
                foreach (ISynapse synapse in neuron.Synapses)
                {
                    ErrorPropagation(synapse.Left, neuronError * synapse.Weight);    // Распространяем новую ошибку
                    synapse.Weight = synapse.Weight - (_trainingSpeed * (1 - _a) * neuronError * synapse.Left.Axon); // Корректируем вес синапса
                }

                neuron.Threshold = neuron.Threshold - (_trainingSpeed * (1 - _a) * neuronError);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="answer"></param>
        private void CalcError(List<double> answer)
        {
            _error = 0;
            ILayer lastLayer = _neuronNetwork.Layers[_neuronNetwork.Layers.Count - 1];

            for (int i = lastLayer.Neurons.Count - 1; i >= 0; i--)
                _error += 0.5 * (Math.Pow(lastLayer.Neurons[i].Axon - answer[i], 2));
        }

        #endregion
    }
}
