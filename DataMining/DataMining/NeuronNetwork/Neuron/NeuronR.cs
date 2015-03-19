
namespace DataMining.NeuronNetwork
{
    /// <summary>
    /// Класс нейрона типа R
    /// </summary>
    public class NeuronR : Neuron
    {
        /// <summary>
        /// Конструктор нейрона типа R
        /// </summary>
        /// <param name="id">Идентификатор нейрона</param>
        /// <param name="f">Функция активации</param>
        public NeuronR(int id, IActivationFunction f)
            : base(id, NeuronType.R, f)
        {
            _addSynapseStrategy = new AddSynapseAllowed();
            _calcAxonStrategy = new CalcAxonAllowed();
            _setAxonStrategy = new SetAxonNotAllowed();
        }
    }
}
