using Genetic.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Genetic
{
   public class Experiment
    {
        private int oldValue;
        public delegate void ChangedEventHandler(Bitmap b);
        public event ChangedEventHandler OnDraw;
        public delegate void ChangedStatus(string status);
        public event ChangedStatus OnStatusChanged;

        public Bitmap drawable { get; set; }
        private string status;

        Population<Polygon, int, Bitmap> population;
        private Bitmap target;

        public Experiment()
        {
        }

        public KeyValuePair<int, Individual<Polygon>> getBest()
        {
            return population.First();
        }

        public void Evolve()
        {
            population.Evolve();
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnStatusChanged.Invoke(status);
            }
        }

        public bool IsClosed { get; private set; }

        internal void OnClose(object sender, EventArgs e)
        {
            this.IsClosed = true;
        }

        public static int Fitness(Individual<Polygon> popul, Bitmap t)
		{
            Graphics g = null;
            Bitmap b = null;
            lock(t)
            {
                b = new Bitmap(t.Width, t.Height);
            }

            g = Graphics.FromImage(b);
            Bitmap tempTarget = new Bitmap(1,1);
			popul.Draw(g);
			lock (t) 
			{
				tempTarget = new Bitmap(t);
			}
            int value = 0;
            for(int i = 0; i < tempTarget.Width; i++)
            {
                for (int j = 0; j < tempTarget.Height; j++)
                {
                    var c1 = tempTarget.GetPixel(i, j);
                    var c2 = b.GetPixel(i, j);
                    value += Math.Abs(c1.R - c2.R) +
                             Math.Abs(c1.G - c2.G) +
                             Math.Abs(c1.B - c2.B);
                }
            }
            tempTarget.Dispose();
            return value;
        }


        public void setTarget(Bitmap b)
        {
            target = b;           
        }

        public void MakeEvolution()
        {
            int i = 0;
			while(true)
			{
               if(IsClosed) {
                   return;
                }

                population.Evolve();
				Status = string.Format("step {0} , fitness: {1}", i, population.First().Key);
				Debug.Print(Status);
				oldValue = getBest().Key;
				Draw();
                i++;
			}
		}

		public void Run()
		{
            population = new Population<Polygon, int, Bitmap>(
                (popul) => { return Fitness(popul, target); },
                 (x, y) => {
                     int result = x.CompareTo(y);

                     if (result == 0)
                         return 1;   // Handle equality as beeing greater
                     else
                         return result;
                 },
                 new PolygonGenerator(target.Width, target.Height));
			population.Generate(ExperimentConsts.PopulationCapacity);
			Draw();
			oldValue = getBest().Key;
			Thread oThread = new Thread(new ThreadStart(MakeEvolution));
			oThread.Start();
		}

        private int width;
        private int height;

        public void Draw()
        {
            var tempBitmap = new Bitmap(drawable.Width, drawable.Height);
            var g = Graphics.FromImage(tempBitmap);
            population.First().Value.Draw(g);
            OnDraw?.Invoke(tempBitmap);
        }

        internal void setSize(int width, int height)
        {
            this.width = width;
            this.height = height;
            drawable = new Bitmap(width, height);
        }
    }
}
