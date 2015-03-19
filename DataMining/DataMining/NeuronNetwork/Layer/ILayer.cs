using System.Collections.Generic;

namespace DataMining.NeuronNetwork
{
    /// <summary>
    /// Интерфейс слоя
    /// </summary>
    public interface ILayer
    {
        #region Свойства

        /// <summary>
        /// Идентификатор слоя
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Список нейронов в слое
        /// </summary>
        List<INeuron> Neurons { get; }

        #endregion

        #region Методы

        /// <summary>
        /// Метод, добавляющий нейрон в слой
        /// </summary>
        /// <param name="neuron">Нейрон</param>
        void Add(INeuron neuron);

        /// <summary>
        /// Метод, вычисляющий выходы всех нейронов в текущем слое
        /// </summary>
        void Calc();

        #endregion
    }
}
