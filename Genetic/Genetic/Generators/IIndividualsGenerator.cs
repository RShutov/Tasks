namespace Genetic.Generators
{
	public interface IIndividualsGenerator<T>
	{
		T generate(int seed);
	}
}
