using System;
using System.Drawing;
using System.Windows.Forms;

namespace Genetic
{	public partial class MainForm : Form	{		public static int drawableWidth = 400;		public static int drawableHeight = 300;
		private bool closed;		private Bitmap resultBitmap;		public delegate void ChangedEventHandler(object sender, EventArgs e);		public event ChangedEventHandler OnRun;		public event ChangedEventHandler OnClose;
		public event ChangedEventHandler OnContinue;
		public event ChangedEventHandler OnPause;
		public Bitmap imageTarget;		public Bitmap DrawArea { get; set; }
		public MainForm()		{			InitializeComponent();
			closed = false;
		}	
		public void createResultImage()		{			resultBitmap = new Bitmap(originalImage.Image.Width, originalImage.Image.Height);			resultImage.Image = resultBitmap;		}
		public Bitmap getResultBitmap()		{			return resultBitmap;		}
		public void SetStatusBinding(Experiment exp)		{			var binding = new Binding(nameof(StatusStrip.Text), exp, nameof(Experiment.Status));			binding.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;			binding.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged;		}
		public void ChangeStatus(string status)		{
			if(!statusStrip.IsDisposed) {
				statusStrip.Invoke(new Action(() => {
					statusStrip.Items[0].Text = status;
				}));
			}		}
		public void SetImage(Bitmap b)		{			resultImage.Size = new System.Drawing.Size(drawableWidth, drawableHeight);			resultImage.SizeMode = PictureBoxSizeMode.StretchImage;			resultImage.Image = b;		}
		internal Bitmap getTarget()		{			return new Bitmap(originalImage.Image);		}
		private void openToolStripMenuItem_Click(object sender, EventArgs e)		{			openFileDialog.ShowDialog();		}
		private void RunToolStripMenuItem_Click(object sender, EventArgs e)		{
			runToolStripMenuItem.Enabled = false;
			pauseToolStripMenuItem.Enabled = true;			OnRun?.Invoke(sender, e);		}
		protected override void OnClosed(EventArgs e)		{
			OnClose?.Invoke(this, e);
			base.OnClosed(e);
			closed = true; 		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			actionToolStripMenuItem.Visible = false;
		}

		private void continueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			pauseToolStripMenuItem.Text = "Пауза";
			pauseToolStripMenuItem.Click -= continueToolStripMenuItem_Click;
			pauseToolStripMenuItem.Click += pauseToolStripMenuItem_Click;
			OnContinue?.Invoke(sender, e);
		}

		private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			pauseToolStripMenuItem.Text = "Продолжить";
			pauseToolStripMenuItem.Click -= pauseToolStripMenuItem_Click;
			pauseToolStripMenuItem.Click += continueToolStripMenuItem_Click;
			OnPause?.Invoke(sender, e);
		}
	}
}
