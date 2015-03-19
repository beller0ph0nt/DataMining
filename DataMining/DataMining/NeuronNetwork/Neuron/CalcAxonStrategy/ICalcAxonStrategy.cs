using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.NeuronNetwork
{
    public interface ICalcAxonStrategy
    {
        void CalcAxon(INeuron neuron, ref double axon);
    }
}
