using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.NeuronNetwork
{
    public class CalcAxonAllowed : ICalcAxonStrategy
    {
        public void CalcAxon(INeuron neuron, ref double axon)
        {
            double sum = 0;

            for (int i = 0; i < neuron.Synapses.Count; i++)
            {
                sum += neuron.Synapses[i].Weight * neuron.Synapses[i].Left.Axon;
            }
            sum += neuron.Threshold;

            axon = neuron.F.Process(sum);
        }
    }
}
