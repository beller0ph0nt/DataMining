using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining.ElmanNeuronNetwork
{
    public interface IActivationFunction
    {
        double Max { get; }
        double Min { get; }

        IActivationFunction Clone();
        double F(double x);
        double dF(double x);
        double dF2(double y);
    }
}
