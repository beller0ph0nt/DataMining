using System.Collections.Generic;

namespace DataMining.NeuronNetwork
{
    /// <summary>
    /// Интерфейс нейросети
    /// </summary>
    public interface INeuronNetwork
    {
        #region Свойства

        /// <summary>
        /// Список слоев
        /// </summary>
        List<ILayer> Layers { get; }

        /// <summary>
        /// Счетчик слоев
        /// </summary>
        int LayerCount { get; }

        /// <summary>
        /// Счетчик нейронов
        /// </summary>
        int NeuronCount { get; }

        /// <summary>
        /// Счетчик связей
        /// </summary>
        int SynapseCount { get; }

        #endregion

        #region Методы

        /// <summary>
        /// Метод, загружающий сеть
        /// </summary>
        /// <param name="file">Имя файла</param>
        void LoadNetwork(string file);

        /// <summary>
        /// Метод, сохраняющий сеть
        /// </summary>
        /// <param name="file">Имя файла</param>
        void SaveNetwork(string file);

        /// <summary>
        /// Метод, вычисляющий выходной вектор нейросети
        /// </summary>
        /// <param name="input">Входной вектор</param>
        void Calc(List<double> input);

        #endregion
    }
}
