using System;

namespace DataMining.NeuronNetwork
{
    /// <summary>
    /// Класс связи
    /// </summary>
    public class Synapse : ISynapse
    {
        private int _id;            // Идентификатор связи
        private INeuron _left;      // Ссылка на левый нейрон
        private INeuron _right;     // Ссылка на правый нейрон
        private double _weight;     // Вес связи

        #region Свойства

        /// <summary>
        /// Идентификатор связи
        /// </summary>
        public int ID { get { return _id; } }

        /// <summary>
        /// Указатель на левый нейрон
        /// </summary>
        public INeuron Left { get { return _left; } }

        /// <summary>
        /// Указатель на правый нейрон
        /// </summary>
        public INeuron Right { get { return _right; } }

        /// <summary>
        /// Вес связи
        /// </summary>
        public double Weight { get { return _weight; } set { _weight = value; } }

        #endregion

        /// <summary>
        /// Конструктор связи
        /// </summary>
        /// /// <param name="id">Идентификатор</param>
        /// <param name="left">Левый нейрон</param>
        /// <param name="right">Правый нейрон</param>
        /// <param name="weight">Вес связи</param>
        public Synapse(int id, INeuron left, INeuron right, double weight)
        {
            if (left == null)
                throw new Exception("Пустая ссылка на левый нейрон");
            else if (right == null)
                throw new Exception("Пустая ссылка на правый нейрон");

            _id = id;
            _left = left;
            _right = right;
            _weight = weight;

            // Добавляем синапс в список правого нейрона
            right.AddSynapse(this);
        }
    }
}
