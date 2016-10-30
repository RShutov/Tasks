using System;
using System.Diagnostics;
using System.Drawing;
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
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originalImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultImage)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.pictureWrapper, "tableLayoutPanel1");
            this.pictureWrapper.BackColor = System.Drawing.SystemColors.Control;
            this.pictureWrapper.Controls.Add(this.originalImage, 0, 0);
            this.pictureWrapper.Controls.Add(this.resultImage, 1, 0);
            this.pictureWrapper.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.pictureWrapper.Name = "tableLayoutPanel1";
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
            // statusStrip1
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            resources.ApplyResources(this.toolStripStatusLabel, "toolStripStatusLabel");
            // 
            // menuStrip1
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
            this.openToolStripMenuItem});
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            resources.ApplyResources(this.openFileToolStripMenuItem, "openFileToolStripMenuItem");
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // действиеToolStripMenuItem
            // 
            this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem});
            this.actionToolStripMenuItem.Name = "ActionToolStripMenuItem";
            resources.ApplyResources(this.actionToolStripMenuItem, "ActionToolStripMenuItem");
            // 
            // запускToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "RunToolStripMenuItem";
            resources.ApplyResources(this.runToolStripMenuItem, "RunToolStripMenuItem");
            this.runToolStripMenuItem.Click += new System.EventHandler(this.запускToolStripMenuItem_Click);
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
    }
}

