using System.Collections.Generic;

namespace DataMining.NeuronNetwork
{
    /// <summary>
    /// Базовый класс нейронов
    /// </summary>
    public abstract class Neuron : INeuron
    {
        private int _id;                            // Идентификатор нейрона
        private double _axon;                       // Выход нейрона
        private NeuronType _type;                   // Тип нейрона
        private double _threshold;                  // Порог чувствительности нейрона
        private List<ISynapse> _synapses;           // Список связей с другими нейронами
        private IActivationFunction _f;             // Функция активации нейрона

        protected IAddSynapseStrategy _addSynapseStrategy; // Стратегия нейрона при добавлении связи
        protected ICalcAxonStrategy _calcAxonStrategy;     // Стратегия нейрона при вычислении выхода
        protected ISetAxonStrategy _setAxonStrategy;       // Стратегия нейрона при установки выхода

        #region Свойства

        /// <summary>
        /// Идентификатор нейрона
        /// </summary>
        public int ID { get { return _id; } }

        /// <summary>
        /// Выход нейрона
        /// </summary>
        public double Axon { get { return _axon; } set { _setAxonStrategy.SetAxon(value, ref _axon); } }

        /// <summary>
        /// Порог чувствительности нейрона
        /// </summary>
        public double Threshold { get { return _threshold; } set { _threshold = value; } }

        /// <summary>
        /// Тип нейрона
        /// </summary>
        public NeuronType Type { get { return _type; } }

        /// <summary>
        /// Список связей с другими нейронами
        /// </summary>
        public List<ISynapse> Synapses { get { return _synapses; } }

        /// <summary>
        /// Функция активации нейрона
        /// </summary>
        public IActivationFunction F { get { return _f; } }

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор нейрона</param>
        /// <param name="type">Тип нейрона</param>
        /// <param name="f">Функция активации</param>
        public Neuron(int id, NeuronType type, IActivationFunction f)
        {
            _id = id;
            _type = type;
            _f = f;
            _synapses = new List<ISynapse>();
        }

        #region Методы

        /// <summary>
        /// Метод, добавляющий новую связь
        /// </summary>
        /// <param name="synapse">Новая связь</param>
        public void AddSynapse(ISynapse synapse)
        {
            _addSynapseStrategy.AddSynapse(_synapses, synapse);
        }

        /// <summary>
        /// Метод, вычисляющий выход нейрона
        /// </summary>
        public void CalcAxon()
        {
            _calcAxonStrategy.CalcAxon(this, ref _axon);
        }

        #endregion
    }
}
