using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.ElmanNeuronNetwork
{
    public class Neuron
    {
        private double _threshold = 0.0;            // Порог чувствительности нейрона
        private double _axon = 0.0;                 // Выход нейрона
        private IActivationFunction _f = null;      // Функция активаци
        private double[] _weights = null;           // Весовые коэффициенты

        public IActivationFunction F { get { return _f; } }
        public double Threshold { get { return _threshold; } }
        public double Axon { get { return _axon; } }
        public double[] Weights { get { return _weights; } }

        public Neuron(IActivationFunction f, int weightsCount)
        {
            _f = f;
            _weights = new double[weightsCount];     // Весовые коэффициенты нейрона, аналог синапсов
        }

        public void Calc(double[] input)
        {
            if (_weights.Length != input.Length)
                throw new Exception("Ошибка длины входного вектора");

            double sum = 0.0;

            for (int i = 0; i < _weights.Length; i++)
            {
                sum += _weights[i] * input[i];
            }
            sum += _threshold;

            _axon = _f.F(sum);
        }
    }
}
