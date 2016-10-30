using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic.Generators
{
    public interface IIndividualsGenerator<T>
    {
        T generate(int seed);
    }
}
