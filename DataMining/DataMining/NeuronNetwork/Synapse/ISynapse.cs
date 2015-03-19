
namespace DataMining.NeuronNetwork
{
    /// <summary>
    /// Интерфейс связи
    /// </summary>
    public interface ISynapse
    {
        #region Свойства

        /// <summary>
        /// Идентификатор связи
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Указатель на левый нейрон
        /// </summary>
        INeuron Left { get; }

        /// <summary>
        /// Указатель на правый нейрон
        /// </summary>
        INeuron Right { get; }

        /// <summary>
        /// Вес связи
        /// </summary>
        double Weight { get; set; }

        #endregion
    }
}
