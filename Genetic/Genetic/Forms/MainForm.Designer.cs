using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Genetic
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
       
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 

        public void Init()
        {
            openFileDialog.FileOk += (s, e) =>
            {
                imageTarget = new Bitmap(openFileDialog.OpenFile());
                originalImage.Size = new System.Drawing.Size(drawableWidth, drawableHeight);
                this.originalImage.Image = imageTarget;
                originalImage.Visible = true;
                //Width += 2*imageTarget.Width + (tableLayoutPanel1.Margin.Left + tableLayoutPanel1.Margin.Right) + (tableLayoutPanel1.Padding.Left + tableLayoutPanel1.Padding.Right);
                //Height += imageTarget.Height + 100;
            };
			
            openFileDialog.FileOk += (s, ev) => { menuStrip.Items[1].Visible = true; };
            //this.runButton.Click += (s, e) => { OnRun?.Invoke(s, e); };
        }

        public void Redraw()
        {
            this.Invalidate();
        }

        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.pictureWrapper = new System.Windows.Forms.TableLayoutPanel();
			this.originalImage = new System.Windows.Forms.PictureBox();
			this.resultImage = new System.Windows.Forms.PictureBox();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.pictureWrapper.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.originalImage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.resultImage)).BeginInit();
			this.statusStrip.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureWrapper
			// 
			resources.ApplyResources(this.pictureWrapper, "pictureWrapper");
			this.pictureWrapper.BackColor = System.Drawing.SystemColors.Control;
			this.pictureWrapper.Controls.Add(this.originalImage, 0, 0);
			this.pictureWrapper.Controls.Add(this.resultImage, 1, 0);
			this.pictureWrapper.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
			this.pictureWrapper.Name = "pictureWrapper";
			// 
			// originalImage
			// 
			resources.ApplyResources(this.originalImage, "originalImage");
			this.originalImage.Name = "originalImage";
			this.originalImage.TabStop = false;
			// 
			// resultImage
			// 
			resources.ApplyResources(this.resultImage, "resultImage");
			this.resultImage.Name = "resultImage";
			this.resultImage.TabStop = false;
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
			resources.ApplyResources(this.statusStrip, "statusStrip");
			this.statusStrip.Name = "statusStrip";
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			resources.ApplyResources(this.toolStripStatusLabel, "toolStripStatusLabel");
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.actionToolStripMenuItem});
			resources.ApplyResources(this.menuStrip, "menuStrip");
			this.menuStrip.Name = "menuStrip";
			// 
			// openFileToolStripMenuItem
			// 
			this.openFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
			this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
			resources.ApplyResources(this.openFileToolStripMenuItem, "openFileToolStripMenuItem");
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
			this.saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			// 
			// actionToolStripMenuItem
			// 
			this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.pauseToolStripMenuItem});
			this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
			resources.ApplyResources(this.actionToolStripMenuItem, "actionToolStripMenuItem");
			// 
			// runToolStripMenuItem
			// 
			this.runToolStripMenuItem.Name = "runToolStripMenuItem";
			resources.ApplyResources(this.runToolStripMenuItem, "runToolStripMenuItem");
			this.runToolStripMenuItem.Click += new System.EventHandler(this.RunToolStripMenuItem_Click);
			// 
			// pauseToolStripMenuItem
			// 
			resources.ApplyResources(this.pauseToolStripMenuItem, "pauseToolStripMenuItem");
			this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
			this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "*.png";
			resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip);
			this.Controls.Add(this.pictureWrapper);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.pictureWrapper.ResumeLayout(false);
			this.pictureWrapper.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.originalImage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.resultImage)).EndInit();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        


        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private TableLayoutPanel pictureWrapper;
        private PictureBox originalImage;
        private PictureBox resultImage;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private MenuStrip menuStrip;
        private ToolStripMenuItem openFileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem actionToolStripMenuItem;
        private ToolStripMenuItem runToolStripMenuItem;
		private ToolStripMenuItem pauseToolStripMenuItem;
		private ToolStripMenuItem saveToolStripMenuItem;
		private SaveFileDialog saveFileDialog;
	}
}

