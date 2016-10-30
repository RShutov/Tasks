using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Genetic
{
    public partial class MainForm : Form
    {
        public static int drawableWidth = 400;
        public static int drawableHeight = 300;

        private Bitmap resultBitmap;
        public delegate void ChangedEventHandler(object sender, EventArgs e);
        public event ChangedEventHandler OnRun;
        public event ChangedEventHandler OnClose;
        public Bitmap imageTarget;
        public Bitmap DrawArea { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        public void createResultImage()
        {
            resultBitmap = new Bitmap(originalImage.Image.Width, originalImage.Image.Height);
            resultImage.Image = resultBitmap;
        }

        public Bitmap getResultBitmap()
        {
            return resultBitmap;
        }

        public void SetStatusBinding(Experiment exp)
        {
            var binding = new Binding(nameof(StatusStrip.Text), exp, nameof(Experiment.Status));
            binding.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
            binding.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged;
        }

        public void ChangeStatus(string status)
        {
            statusStrip.Invoke(new Action(() => { statusStrip.Items[0].Text = status; }));
        }

        public void SetImage(Bitmap b)
        {
            resultImage.Size = new System.Drawing.Size(drawableWidth, drawableHeight);
            resultImage.SizeMode = PictureBoxSizeMode.StretchImage;
            resultImage.Image = b;
        }

        internal Bitmap getTarget()
        {
            return new Bitmap(originalImage.Image);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void запускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnRun?.Invoke(sender, e);
        }

        protected override void OnClosed(EventArgs e)
        {     
            base.OnClosed(e);
            OnClose?.Invoke(this, e);
            
        }
    }
}
