using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.NeuronNetwork
{
    public class AddSynapseAllowed : IAddSynapseStrategy
    {
        public void AddSynapse(List<ISynapse> synapses, ISynapse synapse)
        {
            synapses.Add(synapse);
        }
    }
}
