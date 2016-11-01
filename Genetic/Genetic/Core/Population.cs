using Genetic.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace Genetic.Core
{
	partial class Population<T, TFitnesResult, TTarget> : IEnumerable<KeyValuePair<TFitnesResult, Individual<T>>>
	{ }

	partial class Population<T, TFitnesResult, TTarget>  where T : GeneticObject<T>
	{
		private SortedList<TFitnesResult, Individual<T>> population;
		private SortedList<TFitnesResult, Individual<T>> newPopulation;
		private Func<Individual<T>, TFitnesResult> fitness;
		private int elemPerThread = (ExperimentConsts.PopulationCapacity - ExperimentConsts.AliveCount) / ExperimentConsts.ThreadsCount;
		private Comparison<TFitnesResult> comparator;
		private IIndividualsGenerator<T> generator;

		public void Init(IIndividualsGenerator<T> generator)
		{
			population.Clear();
			for (int i = 0; i < ExperimentConsts.PopulationCapacity; i++) {
				var individual = new Individual<T>(generator);
				individual.generate(ExperimentConsts.PrimitivesCapacity); 
				var fitnessValue = fitness(individual);
				population.Add(fitnessValue, individual);
			}
		}

		public Population(
			Func<Individual<T>, TFitnesResult> fitness, 
			Comparison<TFitnesResult> comparator,
			IIndividualsGenerator<T> generator
		){
			this.generator = generator;
			this.fitness = fitness;
			this.comparator = comparator;
			population = new SortedList<TFitnesResult, Individual<T>>(comparator as IComparer<TFitnesResult>);
		}

		public void Generate(int count)
		{
			for(int i = 0; i< count; i++) {
				var temp = new Individual<T>(generator);
				temp.generate(ExperimentConsts.PrimitivesCapacity);
				var newKey = fitness(temp);
				population.Add(newKey, temp);
			}
		}

		public void calc(List<int> idx1, List<int> idx2, List<Task<bool>> tasklst)
		{
			for(int i = 0; i < idx1.Count; i++) {
				var tempi = i;
				tasklst.Add(Task<bool>.Factory.StartNew(() => {
					var newElem = population.ElementAt(idx1[tempi]).Value.CrossingOver(
						population.ElementAt(idx1[tempi]).Value,
						population.ElementAt(idx2[tempi]).Value);
					var newKey = fitness(newElem);
					newElem.Evolve();
					lock (newPopulation) {
						if (!newPopulation.ContainsKey(newKey))
						newPopulation.Add(newKey, newElem);
					}
					return true;
				}));
			}
		}

		public void Evolve()
		{
			List<Task<bool>> tasklst = new List<Task<bool>>();
			int indexOfLast = (int)(ExperimentConsts.SurvivalPercentage * (population.Count));
			newPopulation = new SortedList<TFitnesResult, Individual<T>>(comparator as IComparer<TFitnesResult>);
			Random r = new Random(Guid.NewGuid().GetHashCode());
			for(int i = 0; i< ExperimentConsts.PopulationCapacity - ExperimentConsts.AliveCount; i++) {
				List<int> firstEllementList = new List<int>();
				List<int> secondEllementList = new List<int>();
				for (int j = 0; j < elemPerThread; j++)
				{
					var first = r.Next(0, Math.Max(2, indexOfLast + 1));
					var second = r.Next(0, Math.Max(2, indexOfLast + 1));
					if (first == second)
					{
						j--;
						continue;
					}
					firstEllementList.Add(first);
					secondEllementList.Add(second);
				}
				calc(firstEllementList, secondEllementList, tasklst);
			}
			Task.WaitAll(tasklst.ToArray());
			for(int i = 0;i < ExperimentConsts.AliveCount; i++)
			{
				newPopulation.Add(population.ElementAt(i).Key, population.ElementAt(i).Value);
			}
			population.Clear();
			population = newPopulation;
		}

		public IEnumerator<KeyValuePair<TFitnesResult, Individual<T>>> GetEnumerator()
		{
			return this.population.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.population.GetEnumerator();
		}

		public KeyValuePair<TFitnesResult,Individual<T>> this[int i]
		{
			get { return population.ElementAt(i); }
			set { population.Add(value.Key, value.Value); }
		}
	}
}
