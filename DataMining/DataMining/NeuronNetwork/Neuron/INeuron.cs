using System.Collections.Generic;

namespace DataMining.NeuronNetwork
{
    /// <summary>
    /// Тип нейрона
    /// </summary>
    public enum NeuronType { S, A, R }

    /// <summary>
    /// Интерфейс базового класса нейронов
    /// </summary>
    public interface INeuron
    {
        #region Свойства

        /// <summary>
        /// Идентификатор нейрона
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Выход нейрона
        /// </summary>
        double Axon { get; set; }

        /// <summary>
        /// Порог чувствительности нейрона
        /// </summary>
        double Threshold { get; set; }

        /// <summary>
        /// Тип нейрона
        /// </summary>
        NeuronType Type { get; }

        /// <summary>
        /// Список связей с другими нейронами
        /// </summary>
        List<ISynapse> Synapses { get; }

        /// <summary>
        /// Функция активации нейрона
        /// </summary>
        IActivationFunction F { get; }

        #endregion

        #region Методы

        /// <summary>
        /// Метод, добавляющий новую связь
        /// </summary>
        /// <param name="synapse">Новая связь</param>
        void AddSynapse(ISynapse synapse);

        /// <summary>
        /// Метод, вычисляющий выход нейрона
        /// </summary>
        void CalcAxon();

        #endregion
    }
}
