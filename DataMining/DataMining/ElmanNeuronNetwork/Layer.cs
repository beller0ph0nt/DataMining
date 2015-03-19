using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.ElmanNeuronNetwork
{
    public class Layer
    {
        private int _inputsCount = 0;       // Количество входов
        private Neuron[] _neurons = null;   // Массив нейронов

        public Neuron[] Neurons { get { return _neurons; } }

        /// <summary>
        /// Конструктор нейронного слоя
        /// </summary>
        /// <param name="f">Функция активации</param>
        /// <param name="neuronsCount">Количество нейронов в слое</param>
        /// <param name="inputsCount">Количество входов в слой. Определяет, сколько у каждого нейрона будет весов</param>
        public Layer(IActivationFunction f, int neuronsCount, int inputsCount)
        {
            _inputsCount = inputsCount;             // Сохраняем количество входов в слой
            _neurons = new Neuron[neuronsCount];    // Вычеляем память под нейроны

            // Заполняем массив нейронов
            for (int i = 0; i < neuronsCount; i++)
            {
                _neurons[i] = new Neuron(f.Clone(), _inputsCount);
            }
        }

        public void Calc(double[] input)
        {   // Вычисляем выход каждого нейрона
            for (int i = 0; i < _neurons.Length; i++)
            {
                _neurons[i].Calc(input);
            }
        }
    }
}
