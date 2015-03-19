using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.NeuronNetwork
{
    public class SetAxonAllowed : ISetAxonStrategy
    {
        public void SetAxon(double newAxon, ref double axon)
        {
            axon = newAxon;
        }
    }
}
