using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.NeuronNetwork
{
    public class SetAxonNotAllowed : ISetAxonStrategy
    {
        public void SetAxon(double newAxon, ref double axon)
        {
            throw new Exception("Нельзя устанавливать аксон");
        }
    }
}
