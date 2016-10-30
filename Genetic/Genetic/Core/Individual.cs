using Genetic.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Genetic
{
    public class Individual<T> where T: GeneticObject<T>
    {
        private List<T> components;
        public IIndividualsGenerator<T> generator ;

        public Individual(IIndividualsGenerator<T> generator)
        {
            this.generator = generator;
            components = new List<T>();
        }

        public Individual(List<T> population, IIndividualsGenerator<T> generator)
        {
            this.components = population;
            this.generator = generator;
        }

        public List<T> ToList()
        {
            return components;
        }

		public Individual<T> Clone()
		{
			var newComponents = new List<T>();
			foreach (var elem in components.ToList()) {
				newComponents.Add((T)elem.Copy());
			}
			return new Individual<T>(newComponents, generator);
		}

		public void Evolve()
        {
			Random r = new Random(Guid.NewGuid().GetHashCode());
			/*var newComponents = new List<T>();
            foreach (var elem in components.ToList())
            {
                newComponents.Add((T)elem.Copy());
            }*/
            foreach (IGenetic elem in components)
            {
               var el = elem;
               if(r.NextDouble() <= ExperimentConsts.MutationProbability)
               {
                    elem.Mutate();
               }
            }

            /*if (r.NextDouble() <= ExperimentConsts.AddChance)
            {
                newComponents.Add((generator.generate(new Random().Next())));
            }

            if (r.NextDouble() <= ExperimentConsts.MutationProbability)
            {
                newComponents.RemoveAt(new Random().Next(0, newComponents.Count-1));
            }*/
           // return new Individual<T>(newComponents, generator);
        }

        public void generate(int count)
        {
            components = new List<T>();
            for(int i=0; i< count; i++)
            {
                components.Add(generator.generate(Guid.NewGuid().GetHashCode()));
            }
        }

        public Bitmap ToBitmap(int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            var g = Graphics.FromImage(b);
            Draw(g);
            return b;

        }
        internal void Draw(Graphics g)
        {
            foreach (var elem in components)
            {
                elem.Draw(g);
            };
        }

        internal void Dispose()
        {
            components.Clear();
        }

        private void Add(T item)
        {
            this.components.Add(item);
        }

        public Individual<T> CrossingOver(Individual<T> individual1, Individual<T> individual2)
        {
            
            var individual = new Individual<T>(individual1.generator);
            for(int i =0; i < ExperimentConsts.PrimitivesCapacity; i++)
            {
                individual.Add(
                    individual1.components[0].CrossingOver(individual1.components[i], individual2.components[i]));
            }
            return individual;
        }
    }
}
