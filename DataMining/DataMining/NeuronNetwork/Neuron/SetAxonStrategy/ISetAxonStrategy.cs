using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.NeuronNetwork
{
    public interface ISetAxonStrategy
    {
        void SetAxon(double newAxon, ref double axon);
    }
}
