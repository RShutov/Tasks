using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic
{
    public abstract class GeneticObject<T> : IGenetic
    {
        public abstract IGenetic Copy();

        public abstract T CrossingOver(T t1, T t2);

        public abstract void Draw(Graphics g);

        public abstract void Mutate();
    }

    public interface IGenetic
    {
        void Mutate();

        void Draw(Graphics g);

        IGenetic Copy();
    }
}
