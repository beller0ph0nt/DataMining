using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.NeuronNetwork
{
    public class AddSynapseNotAllowed : IAddSynapseStrategy
    {
        public void AddSynapse(List<ISynapse> synapses, ISynapse synapse)
        {
            throw new Exception("Нельзя добавлять синапс");
        }
    }
}
