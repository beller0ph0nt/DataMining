using System.Collections.Generic;

namespace DataMining.NeuronNetwork
{
    public class Layer : ILayer
    {
        private int _id;                    // Идентификатор слоя
        private List<INeuron> _neurons;     // Список нейронов в слое

        #region Свойства

        /// <summary>
        /// Идентификатор слоя
        /// </summary>
        public int ID { get { return _id; } }

        /// <summary>
        /// Список нейронов в слое
        /// </summary>
        public List<INeuron> Neurons { get { return _neurons; } }

        #endregion

        /// <summary>
        /// Конструктор слоя
        /// </summary>
        /// <param name="id">Идентификатор слоя</param>
        public Layer(int id)
        {
            _id = id;
            _neurons = new List<INeuron>();
        }

        #region Методы

        /// <summary>
        /// Метод, добавляющий нейрон в слой
        /// </summary>
        /// <param name="neuron">Нейрон</param>
        public void Add(INeuron neuron)
        {
            _neurons.Add(neuron);
        }

        /// <summary>
        /// Метод, вычисляющий выходы всех нейронов в текущем слое
        /// </summary>
        public void Calc()
        {
            for (int i = 0; i < _neurons.Count; i++ )
                _neurons[i].CalcAxon();
        }

        #endregion
    }
}
