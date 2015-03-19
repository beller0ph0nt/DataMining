using System;

namespace DataMining.NeuronNetwork
{
    /// <summary>
    /// Класс сигмойды
    /// </summary>
    public class Sigmoid : IActivationFunction
    {
        private double _a;                  // Коэффициент а
        private const double _max = 1.0;    // Максимум функции
        private const double _min = 0.0;    // Минимум функции

        #region Свойства

        /// <summary>
        /// Максимум функции
        /// </summary>
        public double Max { get { return _max; } }

        /// <summary>
        /// Минимум функции
        /// </summary>
        public double Min { get { return _min; } }

        #endregion

        /// <summary>
        /// Конструкор сигмойды
        /// </summary>
        /// <param name="a">Коэффициент а</param>
        public Sigmoid(double a)
        {
            _a = a;
        }

        #region Методы

        /// <summary>
        /// Метод, вычисляющий функцию сигмойды
        /// </summary>
        /// <param name="x">Абсциса</param>
        /// <returns>Ордината</returns>
        public double Process(double x)
        {
            return (1 / (1 + Math.Exp(-_a * x)));
        }

        /// <summary>
        /// Метод, вычисляющий первую производную сигмойды
        /// </summary>
        /// <param name="x">Абсциса</param>
        /// <returns>Первая производная</returns>
        public double Derivative(double x)
        {
            double y = Process(x);

            return (_a * y * (1 - y));
        }

        /// <summary>
        /// Метод, вычисляющий первую производную сигмойды от уже вычисленного знаяения функции
        /// </summary>
        /// <param name="y">Ордината</param>
        /// <returns>Первая производная</returns>
        public double Derivative2(double y)
        {
            return (_a * y * (1 - y));
        }

        /// <summary>
        /// Метод, возвращающий новую копию текущего экземпляра сигмойды
        /// </summary>
        /// <returns>Новая копия</returns>
        public IActivationFunction Clone()
        {
            return new Sigmoid(_a);
        }

        #endregion
    }
}
