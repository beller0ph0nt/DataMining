using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.ElmanNeuronNetwork
{
    public class ElmanNeuronNetwork
    {
        private int _inputCount = 0;
        private double[] _input = null;     // Массив для входного вектора, соединенног с контекстом
        private double[] _context = null;   // Массив контекста
        private Layer[] _layers = null;     // Массив слоев сети

        public double[] Context { get { return _context; } }
        public Layer[] Layers { get { return _layers; } }

        /// <summary>
        /// Конструктор сети Элмана
        /// </summary>
        /// <param name="f">Функция активации</param>
        /// <param name="contextCount">Количество нейронов в контекстном слое</param>
        /// <param name="inputsCount">Количество входов у сети</param>
        /// <param name="neuronsCount">Массив, содержащий количество нейронов в скрытых + выходном слоях</param>
        ElmanNeuronNetwork(IActivationFunction f, int contextCount, int inputCount, params int[] neuronsCount)
        {
            if (contextCount != neuronsCount[neuronsCount.Length - 2]) 
                throw new Exception("Размерность контекста не соответствует размерности последнего скрытого слоя");

            _inputCount = inputCount;
            _input = new double[_inputCount + contextCount];    // Массив для входного вектора
            _context = new double[contextCount];                // Массив для контекста
            _layers = new Layer[neuronsCount.Length];           // Вычеляем память под слои сети

            // Заполняем массив слоев
            for (int i = 0; i < neuronsCount.Length; i++)
            {
                _layers[i] = new Layer(
                    f,
                    neuronsCount[i],
                    (i == 0) ? _inputCount + contextCount : neuronsCount[i - 1]);
            }
        }

        public void Compute(double[] input)
        {
            int i;

            if (input.Length != _inputCount)
                throw new Exception("Входной вектор не соответствует размерности сети");
            
            // Формируем новый входной вектор = оригинальный входной вектор + вектор контекста
            input.CopyTo(_input, 0);
            _context.CopyTo(_input, _context.Length);

            for (i = _layers.Length - 1; i >= 0; i--)
                _layers[i].Calc(_input);
        }

        public void UpdateContext()
        {
            int i;
            Neuron[] neurons =_layers[_layers.Length - 2].Neurons;   // Нейроны предпоследнего слоя

            // Обходим все нейроны предпоследнего слоя
            for (i = neurons.Length - 1; i >= 0; i--)
                _context[i] = neurons[i].Axon;
        }
    }
}
