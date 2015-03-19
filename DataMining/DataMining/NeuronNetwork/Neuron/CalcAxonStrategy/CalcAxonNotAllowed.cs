using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.NeuronNetwork
{
    public class CalcAxonNotAllowed : ICalcAxonStrategy
    {
        public void CalcAxon(INeuron neuron, ref double axon)
        {
            throw new Exception("Нельзя вычислять аксон");
        }
    }
}
