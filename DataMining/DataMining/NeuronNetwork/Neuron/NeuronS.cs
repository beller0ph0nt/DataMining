
namespace DataMining.NeuronNetwork
{
    /// <summary>
    /// Класс нейрона типа S (Сенсор)
    /// </summary>
    public class NeuronS : Neuron
    {
        /// <summary>
        /// Конструктор нейрона типа S (Сенсор)
        /// </summary>
        /// <param name="id">Идентификатор нейрона</param>
        /// <param name="f">Функция активации</param>
        public NeuronS(int id, IActivationFunction f)
            : base(id, NeuronType.S, f)
        {
            _addSynapseStrategy = new AddSynapseNotAllowed();
            _calcAxonStrategy = new CalcAxonNotAllowed();
            _setAxonStrategy = new SetAxonAllowed();
        }
    }
}
