namespace BijoyBayanno
{
	// Token: 0x02000008 RID: 8
	public partial class frmMain : global::System.Windows.Forms.Form
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00002D90 File Offset: 0x00000F90
		[global::System.Diagnostics.DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000DE4C File Offset: 0x0000C04C
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::BijoyBayanno.frmMain));
			this.NotifyIcon = new global::System.Windows.Forms.NotifyIcon(this.components);
			this.NotifyMenu = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.OptionToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ShowToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.ExitToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.FadeTimer = new global::System.Windows.Forms.Timer(this.components);
			this.btnClose = new global::System.Windows.Forms.PictureBox();
			this.btnMinimize = new global::System.Windows.Forms.PictureBox();
			this.picIco = new global::System.Windows.Forms.PictureBox();
			this.comLayouts = new global::System.Windows.Forms.ComboBox();
			this.ToolTip = new global::System.Windows.Forms.ToolTip(this.components);
			this.btnDrag = new global::System.Windows.Forms.PictureBox();
			this.CheckHandle = new global::System.Windows.Forms.Timer(this.components);
			this.NotifyMenu.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.btnClose).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.btnMinimize).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.picIco).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.btnDrag).BeginInit();
			base.SuspendLayout();
			this.NotifyIcon.BalloonTipIcon = global::System.Windows.Forms.ToolTipIcon.Info;
			this.NotifyIcon.BalloonTipText = "Bijoy Bayanno :D";
			this.NotifyIcon.BalloonTipTitle = "Bijoy Bayanno";
			this.NotifyIcon.ContextMenuStrip = this.NotifyMenu;
			this.NotifyIcon.Text = "Bijoy Bayanno";
			this.NotifyMenu.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.OptionToolStripMenuItem,
				this.ShowToolStripMenuItem,
				this.ToolStripSeparator1,
				this.ExitToolStripMenuItem
			});
			this.NotifyMenu.Name = "NotifyMenu";
			this.NotifyMenu.ShowCheckMargin = true;
			global::BijoyBayanno.DarkMenu.Apply(this.NotifyMenu);
			this.NotifyMenu.Size = new global::System.Drawing.Size(179, 76);
			this.OptionToolStripMenuItem.Image = global::BijoyBayanno.My.Resources.Resources.options_gear_icon16;
			this.OptionToolStripMenuItem.Name = "OptionToolStripMenuItem";
			this.OptionToolStripMenuItem.Size = new global::System.Drawing.Size(178, 22);
			this.OptionToolStripMenuItem.Text = "Options";
			this.OptionToolStripMenuItem.Click += new global::System.EventHandler(this.OptionToolStripMenuItem_Click);
			this.ShowToolStripMenuItem.Image = global::BijoyBayanno.My.Resources.Resources.show;
			this.ShowToolStripMenuItem.Name = "ShowToolStripMenuItem";
			this.ShowToolStripMenuItem.Size = new global::System.Drawing.Size(178, 22);
			this.ShowToolStripMenuItem.Text = "Show as Topbar";
			this.ToolStripSeparator1.Name = "ToolStripSeparator1";
			this.ToolStripSeparator1.Size = new global::System.Drawing.Size(175, 6);
			this.ExitToolStripMenuItem.Image = global::BijoyBayanno.My.Resources.Resources.Close2;
			this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
			this.ExitToolStripMenuItem.Size = new global::System.Drawing.Size(178, 22);
			this.ExitToolStripMenuItem.Text = "Close";
			this.FadeTimer.Interval = 10;
			this.btnClose.Anchor = global::System.Windows.Forms.AnchorStyles.None;
			this.btnClose.BackColor = global::System.Drawing.Color.Transparent;
			this.btnClose.Image = global::BijoyBayanno.My.Resources.Resources.Close2;
			this.btnClose.Location = new global::System.Drawing.Point(237, 3);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new global::System.Drawing.Size(18, 18);
			this.btnClose.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.btnClose.TabIndex = 4;
			this.btnClose.TabStop = false;
			this.btnMinimize.Anchor = global::System.Windows.Forms.AnchorStyles.None;
			this.btnMinimize.BackColor = global::System.Drawing.Color.Transparent;
			this.btnMinimize.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btnMinimize.Image");
			this.btnMinimize.Location = new global::System.Drawing.Point(214, 3);
			this.btnMinimize.Name = "btnMinimize";
			this.btnMinimize.Size = new global::System.Drawing.Size(18, 18);
			this.btnMinimize.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.btnMinimize.TabIndex = 5;
			this.btnMinimize.TabStop = false;
			this.picIco.Image = global::BijoyBayanno.My.Resources.Resources.Main;
			this.picIco.Location = new global::System.Drawing.Point(86, 1);
			this.picIco.Name = "picIco";
			this.picIco.Size = new global::System.Drawing.Size(24, 24);
			this.picIco.TabIndex = 6;
			this.picIco.TabStop = false;
			this.comLayouts.AutoCompleteMode = global::System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.comLayouts.AutoCompleteSource = global::System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comLayouts.CausesValidation = false;
			this.comLayouts.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comLayouts.Font = new global::System.Drawing.Font("Segoe UI", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.comLayouts.FormattingEnabled = true;
			this.comLayouts.ItemHeight = 15;
			this.comLayouts.Location = new global::System.Drawing.Point(112, 1);
			this.comLayouts.Name = "comLayouts";
			this.comLayouts.Size = new global::System.Drawing.Size(97, 23);
			this.comLayouts.TabIndex = 0;
			this.comLayouts.TabStop = false;
			this.ToolTip.AutoPopDelay = 5000;
			this.ToolTip.InitialDelay = 100;
			this.ToolTip.ReshowDelay = 100;
			this.btnDrag.BackColor = global::System.Drawing.Color.Transparent;
			this.btnDrag.Cursor = global::System.Windows.Forms.Cursors.SizeAll;
			this.btnDrag.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btnDrag.Image");
			this.btnDrag.Location = new global::System.Drawing.Point(3, 2);
			this.btnDrag.Name = "btnDrag";
			this.btnDrag.Size = new global::System.Drawing.Size(87, 22);
			this.btnDrag.TabIndex = 9;
			this.btnDrag.TabStop = false;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.MidnightBlue;
			this.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			base.ClientSize = new global::System.Drawing.Size(258, 25);
			base.ControlBox = false;
			base.Controls.Add(this.btnMinimize);
			base.Controls.Add(this.picIco);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.comLayouts);
			base.Controls.Add(this.btnDrag);
			this.ForeColor = global::System.Drawing.SystemColors.ControlText;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "frmMain";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Bijoy Bayanno";
			base.TopMost = true;
			this.NotifyMenu.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.btnClose).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.btnMinimize).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.picIco).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.btnDrag).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x040000BD RID: 189
		private global::System.ComponentModel.IContainer components;
	}
}
