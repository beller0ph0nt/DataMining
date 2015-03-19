
namespace DataMining.NeuronNetwork
{
    /// <summary>
    /// Класс нейрона типа A
    /// </summary>
    public class NeuronA : Neuron
    {
        /// <summary>
        /// Конструктор нейрона типа A
        /// </summary>
        /// <param name="id">Идентификатор нейрона</param>
        /// <param name="f">Функция активации</param>
        public NeuronA(int id, IActivationFunction f)
            : base(id, NeuronType.A, f)
        {
            _addSynapseStrategy = new AddSynapseAllowed();
            _calcAxonStrategy = new CalcAxonAllowed();
            _setAxonStrategy = new SetAxonNotAllowed();
        }
    }
}
