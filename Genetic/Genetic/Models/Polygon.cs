using Genetic.Generators;
using System;
using System.Drawing;

namespace Genetic
{
	public class Polygon : GeneticObject<Polygon>
	{
		public Point[] Coords { get; set; }
		public Color Colour { get; set; }

		public Polygon()
		{
			Coords = new Point[ExperimentConsts.VerticesCount];
		}

		public object CrossingOver(object target)
		{
			return new Polygon();
		}

		public override void Mutate()
		{
			Random r = new Random();
			if (r.NextDouble() >= 0.5) {
				var i = (int)Math.Round(r.NextDouble() * (ExperimentConsts.VerticesCount - 1));
				int signX = r.NextDouble() >= 0.5 ? 1 : -1;
				int signY = r.NextDouble() >= 0.5 ? 1 : -1;
				Coords[i] = new Point(
					Coords[i].X + signX * r.Next(10),
					Coords[i].Y + signY * r.Next(10));
			} else {
				int signR = r.NextDouble() >= 0.5 ? 1 : -1;
				int signG = r.NextDouble() >= 0.5 ? 1 : -1;
				int signB = r.NextDouble() >= 0.5 ? 1 : -1;
				Colour = Color.FromArgb(
					(int)(ExperimentConsts.PrimitivesOpacity * 255),
					Math.Abs(Colour.R + signR * r.Next(50)) % 256,
					Math.Abs(Colour.G + signG * r.Next(50)) % 256,
					Math.Abs(Colour.B + signB * r.Next(50)) % 256);
			}
		}

		public override void Draw(Graphics g)
		{
			g.FillPolygon(new SolidBrush(Colour), Coords);
		}

		public override IGenetic Copy()
		{
			var newObject = new Polygon();
			newObject.Colour = Color.FromArgb(Colour.A, Colour.R, Colour.G, Colour.B);
			newObject.Coords = new Point[ExperimentConsts.VerticesCount];
			Coords.CopyTo(newObject.Coords, 0);
			return newObject;
		}

		private uint generateMask(int count)
		{
			Random r = new Random(Guid.NewGuid().GetHashCode());
			uint mask = 0;
			for (int i = 0; i < count * 8; i++)
			{
				mask |= (uint)r.Next(0, 2);
				mask <<= 1;
			}
			return mask;
		}
		public int ApplyMask(uint mask, uint first, uint second)
		{
			uint newValue = 0;
			for(int i=7; i !=0; i--)
			{
				var val = (mask >> i) & 1;
				if(val ==  1)
				{
					newValue |= ((first >> i) & 1);
				} else {
					newValue |= ((second >> i) & 1);
				}
				newValue <<= 1;
			}
			return (int)newValue;
		}

		public override Polygon CrossingOver(Polygon t1, Polygon t2)
		{
			Random r = new Random(Guid.NewGuid().GetHashCode());
			uint Rmask = generateMask(sizeof(byte));
			uint Gmask = generateMask(sizeof(byte));
			uint Bmask = generateMask(sizeof(byte));
			var polygon = new Polygon
			{
				Colour = Color.FromArgb(
					(int)(ExperimentConsts.PrimitivesOpacity * 255),
					ApplyMask(Rmask, t1.Colour.R, t2.Colour.R),
					ApplyMask(Gmask, t1.Colour.G, t2.Colour.G),
					ApplyMask(Bmask, t1.Colour.B, t2.Colour.B))
			};
			for (int i = 0; i < ExperimentConsts.VerticesCount; i++) {
				uint coordXMask = generateMask(sizeof(int));
				uint coordYMask = generateMask(sizeof(int));
				polygon.Coords[i] = new Point(
						ApplyMask(coordXMask, (uint)t1.Coords[i].X, (uint)t2.Coords[i].X),
						ApplyMask(coordYMask, (uint)t1.Coords[i].Y, (uint)t2.Coords[i].Y));
			}
			return polygon;
		}
	}

	public class PolygonGenerator : IIndividualsGenerator<Polygon>
	{
		private int width;
		private int height;

		public PolygonGenerator(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		public Polygon generate(int seed)
		{
			var r = new Random(seed);

			var polygon = new Polygon {
				Colour = Color.FromArgb(
					(int)(ExperimentConsts.PrimitivesOpacity * 255),
					r.Next(0, 256),
					r.Next(0, 256),
					r.Next(0, 256))
			};

			for(int i = 0; i < ExperimentConsts.VerticesCount; i++)
			{
				polygon.Coords[i] = new Point(r.Next(0, width+1), r.Next(0, height+1));
			}
			return polygon;
		}
	}
}
