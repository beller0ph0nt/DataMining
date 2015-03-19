using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.NeuronNetwork
{
    public interface IAddSynapseStrategy
    {
        void AddSynapse(List<ISynapse> synapses, ISynapse synapse);
    }
}
