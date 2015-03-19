
namespace DataMining.NeuronNetwork
{
    /// <summary>
    /// Интерфейс активационной функции
    /// </summary>
    public interface IActivationFunction
    {
        #region Свойства

        /// <summary>
        /// Максимум функции
        /// </summary>
        double Max { get; }

        /// <summary>
        /// Минимум функции
        /// </summary>
        double Min { get; }

        #endregion

        #region Методы

        /// <summary>
        /// Метод, возвращающий новую копию текущего экземпляра функции
        /// </summary>
        /// <returns></returns>
        IActivationFunction Clone();

        /// <summary>
        /// Метод, вычисляющий функцию
        /// </summary>
        /// <param name="x">Абсциса</param>
        /// <returns>Ордината</returns>
        double Process(double x);

        /// <summary>
        /// Метод, вычисляющий первую производную
        /// </summary>
        /// <param name="x">Абсциса</param>
        /// <returns>Первая производная</returns>
        double Derivative(double x);

        /// <summary>
        /// Метод, вычисляющий первую производную от уже вычисленного знаяения функции
        /// </summary>
        /// <param name="y">Ордината</param>
        /// <returns>Первая производная</returns>
        double Derivative2(double y);

        #endregion
    }
}
