using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Genetic
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			var mainForm = new MainForm();
			mainForm.Init();
           
			Experiment exp = new Experiment();
			mainForm.OnRun += (s, e) => {
				exp.setSize(mainForm.getTarget().Width, mainForm.getTarget().Height);
				mainForm.createResultImage();
				mainForm.SetStatusBinding(exp);
				exp.OnDraw += (b) => {mainForm.SetImage(b); mainForm.Invalidate(); };
				exp.OnStatusChanged += mainForm.ChangeStatus;
				mainForm.OnClose += exp.OnClose;
				mainForm.OnContinue += exp.Continue;
				mainForm.OnPause += exp.Pause;
				exp.setTarget(mainForm.imageTarget);
				exp.Run(); 
			};
			Application.Run(mainForm);
		}
	}
}
