using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.NeuronNetwork
{
    /// <summary>
    /// Интерфейс алгоритма обучения нейронной сети
    /// </summary>
    public interface ILearningAlgorithm
    {
        #region Свойства

        /// <summary>
        /// Ошибка нейросети
        /// </summary>
        double Error { get; }

        /// <summary>
        /// Скорость обучения
        /// </summary>
        double TrainingSpeed { get; set; }

        #endregion

        #region Методы

        /// <summary>
        /// Метод, обучающий нейронную сети на одной выборке
        /// </summary>
        /// <param name="input">Входной вектор</param>
        /// <param name="answer">Вектор ответов</param>
        void Training(List<double> input, List<double> answer);

        /// <summary>
        /// Метод, обучающий нейронную сеть на всем массиве векторов
        /// </summary>
        /// <param name="inputs">Список входных векторов</param>
        /// <param name="answers">Список векторов ответов</param>
        void Training(List<List<double>> inputs, List<List<double>> answers);

        #endregion
    }
}
