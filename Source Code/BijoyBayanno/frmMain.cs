using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using BijoyBayanno.My;
using BijoyBayanno.My.Resources;
using Microsoft.Win32;

namespace BijoyBayanno
{
	// Token: 0x02000008 RID: 8
	public partial class frmMain : Form
	{
		// Token: 0x06000163 RID: 355 RVA: 0x0000D0D4 File Offset: 0x0000B2D4
		public frmMain()
		{
			base.FormClosing += this.frmMain_FormClosing;
			base.Load += this.frmMain_Load;
			base.MouseDown += this.frmMain_MouseDown;
			base.MouseHover += this.frmMain_MouseHover;
			this.InitializeComponent();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00002B19 File Offset: 0x00000D19
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.UnsubscribeSystemEvents();
			this.NotifyIcon.Dispose();
			modKeyboard.UnhookKeyboard();
			modMouse.UnhookMouse();
			base.Close();
			base.Dispose();
			System.Environment.Exit(0);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000D138 File Offset: 0x0000B338
		private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Left)
				{
					if (this.comLayouts.SelectedIndex == 0)
					{
						this.comLayouts.SelectedIndex = modKeyboard.defLayout;
						mHandleInfo.AddInfo((long)modKeyboard.GetForegroundWindow(), modKeyboard.Layout[modKeyboard.defLayout]);
					}
					else
					{
						this.comLayouts.SelectedIndex = 0;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000D1AC File Offset: 0x0000B3AC
		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.UnsubscribeSystemEvents();
			try
			{
				Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options", "ApplicationMode", modKeyboard.AM);
				Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options", "TrayMode", myFunctions.TrayMode);
				Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options", "DefaultLayout", modKeyboard.defLayout);
				Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options", "Position", base.Left);
				Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options", "LAM", this.CheckHandle.Enabled);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000D284 File Offset: 0x0000B484
		private void frmMain_Load(object sender, EventArgs e)
		{
			this.ToolTip.SetToolTip(this.btnMinimize, "Goto SysTray");
			this.ToolTip.SetToolTip(this.btnClose, "Exit BijoyBayanno");
			base.Top = 0;
			base.Height = 26;
			try
			{
				base.Left = TextUtil.ToInt(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options", "Position", 0));
			}
			catch (Exception)
			{
			}
			try
			{
				modKeyboard.defLayout = TextUtil.ToInt(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options", "DefaultLayout", 0));
				this.CheckHandle.Enabled = TextUtil.ToBool(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options", "LAM", false));
				myFunctions.TrayMode = TextUtil.ToBool(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options", "TrayMode", false));
				modKeyboard.AM = TextUtil.ToInt(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options", "ApplicationMode", 1));
				if (modKeyboard.AM == 3 || (modKeyboard.AM == 1 && myFunctions.TrayMode))
				{
					this.NotifyIcon.Visible = true;
					base.Opacity = 0.0;
					base.Visible = false;
				}
				else
				{
					base.Opacity = 1.0;
					base.Visible = true;
				}
				if (modKeyboard.AM == 0)
				{
					modKeyboard.AM = 1;
					Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options", "ApplicationMode", 1);
				}
				if (Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options", "AutoRunInitialized", null) == null)
				{
					Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", "BijoyBayanno", "\"" + myFunctions.VerifyPath(Program.AppDirectory) + Program.AssemblyName + ".exe\"");
					Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options", "AutoRunInitialized", 1);
				}
			}
			catch (Exception)
			{
			}
			this.comLayouts.Items.Add("English");
			this.comLayouts.SelectedIndex = 0;
			this.SuppressComboFocusCue();
			ToolStripSeparator value = new ToolStripSeparator();
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
			toolStripMenuItem.Text = "English";
			toolStripMenuItem.Image = Resources.Main16;
			toolStripMenuItem.Tag = 1;
			toolStripMenuItem.CheckState = CheckState.Checked;
			this.NotifyMenu.Items.Insert(0, value);
			this.NotifyMenu.Items.Insert(0, toolStripMenuItem);
			modFindLayouts.Find();
			if (!modKeyboard.HookKeyboard())
			{
				TextUtil.ShowError("Keyboard hook could not be started. Typing will not work until BijoyBayanno is restarted.");
			}
			modMouse.HookMouse();
			this.SubscribeSystemEvents();
			base.Focus();
			try
			{
				NativeMethods.SendMessage(base.Handle, 295, new IntPtr(257), IntPtr.Zero);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00002B41 File Offset: 0x00000D41
		private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			base.Visible = true;
			this.FadeStatus = false;
			this.FadeTimer.Enabled = true;
			myFunctions.TrayMode = false;
			base.Focus();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000D5B8 File Offset: 0x0000B7B8
		private void FadeTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (this.FadeStatus)
				{
					if (base.Opacity > 0.0)
					{
						base.Opacity -= 0.01;
					}
					else
					{
						this.FadeTimer.Enabled = false;
						base.Visible = false;
						this.NotifyIcon.Visible = true;
					}
				}
				else if (base.Opacity < 1.0)
				{
					base.Opacity += 0.01;
				}
				else
				{
					this.FadeTimer.Enabled = false;
					this.NotifyIcon.Visible = false;
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00002B6A File Offset: 0x00000D6A
		private void btnClose_Click(object sender, EventArgs e)
		{
			this.FadeStatus = true;
			this.FadeTimer.Enabled = true;
			while (this.FadeTimer.Enabled)
			{
				Application.DoEvents();
			}
			this.ExitToolStripMenuItem.PerformClick();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00002B9E File Offset: 0x00000D9E
		private void btnMinimize_Click(object sender, EventArgs e)
		{
			this.FadeStatus = true;
			this.FadeTimer.Enabled = true;
			myFunctions.TrayMode = true;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00002BB9 File Offset: 0x00000DB9
		private void btnMinimize_MouseHover(object sender, EventArgs e)
		{
			this.btnMinimize.Width = 22;
			this.btnMinimize.Height = 22;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00002BD5 File Offset: 0x00000DD5
		private void btnMinimize_MouseLeave(object sender, EventArgs e)
		{
			this.btnMinimize.Width = 18;
			this.btnMinimize.Height = 18;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00002C29 File Offset: 0x00000E29
		private void btnClose_MouseHover(object sender, EventArgs e)
		{
			this.btnClose.Width = 22;
			this.btnClose.Height = 22;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00002C45 File Offset: 0x00000E45
		private void btnClose_MouseLeave(object sender, EventArgs e)
		{
			this.btnClose.Width = 18;
			this.btnClose.Height = 18;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000D670 File Offset: 0x0000B870
		private void comLayouts_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.SuppressComboFocusCue();
			modKeyboard.curLayout = checked(this.comLayouts.SelectedIndex - 1);
			AvroEngine.ClearBuffer();
			modKeyboard.AvroPrevOutput = 0;
			modKeyboard.AvroPrevOutputStr = "";
			if (modKeyboard.curLayout != -1)
			{
				string str = myFunctions.VerifyPath(Program.AppDirectory);
				str += "Icons\\";
				try
				{
					// v2: icons are loaded once at startup (modFindLayouts.Find); no disk access here.
					clsLayouts cachedLayout = modKeyboard.Layout[modKeyboard.curLayout];
					if (cachedLayout.Icon16 == null || cachedLayout.Bitmap24 == null)
					{
						throw new InvalidOperationException("Layout icon not available.");
					}
					this.NotifyIcon.Icon = cachedLayout.Icon16;
					this.picIco.Image = cachedLayout.Bitmap24;
					goto IL_11C;
				}
				catch (Exception)
				{
					this.NotifyIcon.Icon = Resources.BanN;
					this.picIco.Image = Resources.Main;
					mHandleInfo.AddInfo((long)modKeyboard.GetForegroundWindow(), modKeyboard.Layout[modKeyboard.defLayout]);
					goto IL_11C;
				}
			}
			this.NotifyIcon.Icon = Resources.BanN;
			this.picIco.Image = Resources.Main;
			IL_11C:
			if (this.comLayouts.SelectedIndex != 0)
			{
				modKeyboard.defLayout = this.comLayouts.SelectedIndex;
				if (this.CheckHandle.Enabled && mHandleInfo.lamTargetHandle != 0)
				{
					mHandleInfo.AddInfo((long)mHandleInfo.lamTargetHandle, modKeyboard.Layout[modKeyboard.curLayout]);
				}
			}
			try
			{
				foreach (object obj in this.NotifyMenu.Items)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					if (toolStripItem.Tag != null && toolStripItem is ToolStripMenuItem)
					{
						if (TextUtil.ToInt(toolStripItem.Tag) == this.comLayouts.SelectedIndex + 1)
						{
							((ToolStripMenuItem)toolStripItem).Checked = true;
						}
						else
						{
							((ToolStripMenuItem)toolStripItem).Checked = false;
						}
					}
				}
			}
			finally
			{
				IEnumerator enumerator2 = null;
				if (enumerator2 is IDisposable)
				{
					(enumerator2 as IDisposable).Dispose();
				}
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00002C61 File Offset: 0x00000E61
		private void NotifyMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem.Tag != null)
			{
				this.comLayouts.SelectedIndex = TextUtil.ToInt(e.ClickedItem.Tag) - 1;
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00002C9D File Offset: 0x00000E9D
		private void frmMain_MouseDown(object sender, MouseEventArgs e)
		{
			base.Select();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		private void OptionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				Program.OptionsForm.ShowDialog();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000D8FC File Offset: 0x0000BAFC
		private void picIco_Click(object sender, EventArgs e)
		{
			try
			{
				this.NotifyIcon_MouseClick(sender, (MouseEventArgs)e);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00002CDD File Offset: 0x00000EDD
		private void frmMain_MouseHover(object sender, EventArgs e)
		{
			base.Select();
			base.Focus();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00002CEC File Offset: 0x00000EEC
		private void btnDrag_MouseDown(object sender, MouseEventArgs e)
		{
			myFunctions.Drag(this);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000D964 File Offset: 0x0000BB64
		private void CheckHandle_Tick(object sender, EventArgs e)
		{
			int foregroundWindow = modKeyboard.GetForegroundWindow();
			if (foregroundWindow != (int)base.Handle && foregroundWindow != 0)
			{
				mHandleInfo.lamTargetHandle = foregroundWindow;
			}
			if (this.lastHandle != foregroundWindow)
			{
				try
				{
					this.comLayouts.SelectedIndex = checked(mHandleInfo.FindInfo(unchecked((long)modKeyboard.GetForegroundWindow())).ID + 1);
				}
				catch (Exception)
				{
					this.comLayouts.SelectedIndex = 0;
				}
				this.lastHandle = foregroundWindow;
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000029F4 File Offset: 0x00000BF4
		private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00002CF5 File Offset: 0x00000EF5
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000D9DC File Offset: 0x0000BBDC
		internal virtual NotifyIcon NotifyIcon
		{
			[CompilerGenerated]
			get
			{
				return this._NotifyIcon;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				MouseEventHandler value2 = new MouseEventHandler(this.NotifyIcon_MouseClick);
				MouseEventHandler value3 = new MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
				NotifyIcon notifyIcon = this._NotifyIcon;
				if (notifyIcon != null)
				{
					notifyIcon.MouseClick -= value2;
					notifyIcon.MouseDoubleClick -= value3;
				}
				this._NotifyIcon = value;
				notifyIcon = this._NotifyIcon;
				if (notifyIcon != null)
				{
					notifyIcon.MouseClick += value2;
					notifyIcon.MouseDoubleClick += value3;
				}
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00002CFD File Offset: 0x00000EFD
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000DA3C File Offset: 0x0000BC3C
		internal virtual ContextMenuStrip NotifyMenu
		{
			[CompilerGenerated]
			get
			{
				return this._NotifyMenu;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				ToolStripItemClickedEventHandler value2 = new ToolStripItemClickedEventHandler(this.NotifyMenu_ItemClicked);
				ContextMenuStrip notifyMenu = this._NotifyMenu;
				if (notifyMenu != null)
				{
					notifyMenu.ItemClicked -= value2;
				}
				this._NotifyMenu = value;
				notifyMenu = this._NotifyMenu;
				if (notifyMenu != null)
				{
					notifyMenu.ItemClicked += value2;
				}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00002D05 File Offset: 0x00000F05
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000DA80 File Offset: 0x0000BC80
		internal virtual ToolStripMenuItem ExitToolStripMenuItem
		{
			[CompilerGenerated]
			get
			{
				return this._ExitToolStripMenuItem;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ExitToolStripMenuItem_Click);
				ToolStripMenuItem exitToolStripMenuItem = this._ExitToolStripMenuItem;
				if (exitToolStripMenuItem != null)
				{
					exitToolStripMenuItem.Click -= value2;
				}
				this._ExitToolStripMenuItem = value;
				exitToolStripMenuItem = this._ExitToolStripMenuItem;
				if (exitToolStripMenuItem != null)
				{
					exitToolStripMenuItem.Click += value2;
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00002D0D File Offset: 0x00000F0D
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00002D15 File Offset: 0x00000F15
		internal virtual ToolStripSeparator ToolStripSeparator1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		internal virtual ToolStripMenuItem OptionToolStripMenuItem { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00002D1E File Offset: 0x00000F1E
		// (set) Token: 0x06000189 RID: 393 RVA: 0x0000DAC4 File Offset: 0x0000BCC4
		internal virtual ToolStripMenuItem ShowToolStripMenuItem
		{
			[CompilerGenerated]
			get
			{
				return this._ShowToolStripMenuItem;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ShowToolStripMenuItem_Click);
				ToolStripMenuItem showToolStripMenuItem = this._ShowToolStripMenuItem;
				if (showToolStripMenuItem != null)
				{
					showToolStripMenuItem.Click -= value2;
				}
				this._ShowToolStripMenuItem = value;
				showToolStripMenuItem = this._ShowToolStripMenuItem;
				if (showToolStripMenuItem != null)
				{
					showToolStripMenuItem.Click += value2;
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00002D26 File Offset: 0x00000F26
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0000DB08 File Offset: 0x0000BD08
		internal virtual Timer FadeTimer
		{
			[CompilerGenerated]
			get
			{
				return this._FadeTimer;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.FadeTimer_Tick);
				Timer fadeTimer = this._FadeTimer;
				if (fadeTimer != null)
				{
					fadeTimer.Tick -= value2;
				}
				this._FadeTimer = value;
				fadeTimer = this._FadeTimer;
				if (fadeTimer != null)
				{
					fadeTimer.Tick += value2;
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00002D2E File Offset: 0x00000F2E
		// (set) Token: 0x0600018D RID: 397 RVA: 0x0000DB4C File Offset: 0x0000BD4C
		internal virtual PictureBox btnClose
		{
			[CompilerGenerated]
			get
			{
				return this._btnClose;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.btnClose_Click);
				EventHandler value3 = new EventHandler(this.btnClose_MouseHover);
				EventHandler value4 = new EventHandler(this.btnClose_MouseLeave);
				PictureBox btnClose = this._btnClose;
				if (btnClose != null)
				{
					btnClose.Click -= value2;
					btnClose.MouseHover -= value3;
					btnClose.MouseLeave -= value4;
				}
				this._btnClose = value;
				btnClose = this._btnClose;
				if (btnClose != null)
				{
					btnClose.Click += value2;
					btnClose.MouseHover += value3;
					btnClose.MouseLeave += value4;
				}
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00002D36 File Offset: 0x00000F36
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000DBC8 File Offset: 0x0000BDC8
		internal virtual PictureBox btnMinimize
		{
			[CompilerGenerated]
			get
			{
				return this._btnMinimize;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.btnMinimize_Click);
				EventHandler value3 = new EventHandler(this.btnMinimize_MouseHover);
				EventHandler value4 = new EventHandler(this.btnMinimize_MouseLeave);
				PictureBox btnMinimize = this._btnMinimize;
				if (btnMinimize != null)
				{
					btnMinimize.Click -= value2;
					btnMinimize.MouseHover -= value3;
					btnMinimize.MouseLeave -= value4;
				}
				this._btnMinimize = value;
				btnMinimize = this._btnMinimize;
				if (btnMinimize != null)
				{
					btnMinimize.Click += value2;
					btnMinimize.MouseHover += value3;
					btnMinimize.MouseLeave += value4;
				}
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00002D3E File Offset: 0x00000F3E
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000DC44 File Offset: 0x0000BE44
		internal virtual PictureBox picIco
		{
			[CompilerGenerated]
			get
			{
				return this._picIco;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.picIco_Click);
				PictureBox picIco = this._picIco;
				if (picIco != null)
				{
					picIco.Click -= value2;
				}
				this._picIco = value;
				picIco = this._picIco;
				if (picIco != null)
				{
					picIco.Click += value2;
				}
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00002D46 File Offset: 0x00000F46
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000DC88 File Offset: 0x0000BE88
		internal virtual ComboBox comLayouts
		{
			[CompilerGenerated]
			get
			{
				return this._comLayouts;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.comLayouts_SelectedIndexChanged);
				ComboBox comLayouts = this._comLayouts;
				if (comLayouts != null)
				{
					comLayouts.SelectedIndexChanged -= value2;
				}
				this._comLayouts = value;
				comLayouts = this._comLayouts;
				if (comLayouts != null)
				{
					comLayouts.SelectedIndexChanged += value2;
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00002D5E File Offset: 0x00000F5E
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00002D66 File Offset: 0x00000F66
		internal virtual ToolTip ToolTip { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00002D6F File Offset: 0x00000F6F
		// (set) Token: 0x0600019B RID: 411 RVA: 0x0000DDC4 File Offset: 0x0000BFC4
		internal virtual PictureBox btnDrag
		{
			[CompilerGenerated]
			get
			{
				return this._btnDrag;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				MouseEventHandler value2 = new MouseEventHandler(this.btnDrag_MouseDown);
				PictureBox btnDrag = this._btnDrag;
				if (btnDrag != null)
				{
					btnDrag.MouseDown -= value2;
				}
				this._btnDrag = value;
				btnDrag = this._btnDrag;
				if (btnDrag != null)
				{
					btnDrag.MouseDown += value2;
				}
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00002D77 File Offset: 0x00000F77
		// (set) Token: 0x0600019D RID: 413 RVA: 0x0000DE08 File Offset: 0x0000C008
		internal virtual Timer CheckHandle
		{
			[CompilerGenerated]
			get
			{
				return this._CheckHandle;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.CheckHandle_Tick);
				Timer checkHandle = this._CheckHandle;
				if (checkHandle != null)
				{
					checkHandle.Tick -= value2;
				}
				this._CheckHandle = value;
				checkHandle = this._CheckHandle;
				if (checkHandle != null)
				{
					checkHandle.Tick += value2;
				}
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00002D7F File Offset: 0x00000F7F
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00002D87 File Offset: 0x00000F87
		internal virtual ToolStripMenuItem ToolStripMenuItem2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060001A2 RID: 418 RVA: 0x00002DAF File Offset: 0x00000FAF
		// v2: re-install the low-level hooks after resume / unlock / reconnect.
		private Timer _rehookTimer;

		private void SubscribeSystemEvents()
		{
			try
			{
				this._rehookTimer = new Timer();
				this._rehookTimer.Interval = 1500;
				this._rehookTimer.Tick += this.RehookTimer_Tick;
				SystemEvents.PowerModeChanged += this.SystemEvents_PowerModeChanged;
				SystemEvents.SessionSwitch += this.SystemEvents_SessionSwitch;
			}
			catch (Exception)
			{
			}
		}

		private void UnsubscribeSystemEvents()
		{
			try
			{
				SystemEvents.PowerModeChanged -= this.SystemEvents_PowerModeChanged;
				SystemEvents.SessionSwitch -= this.SystemEvents_SessionSwitch;
				if (this._rehookTimer != null)
				{
					this._rehookTimer.Stop();
				}
			}
			catch (Exception)
			{
			}
		}

		// SystemEvents are raised on a background thread; hooks must be installed on the UI thread.
		private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
		{
			if (e.Mode == PowerModes.Resume)
			{
				this.ScheduleRehook();
			}
		}

		private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
		{
			if (e.Reason == SessionSwitchReason.SessionUnlock || e.Reason == SessionSwitchReason.ConsoleConnect || e.Reason == SessionSwitchReason.RemoteConnect || e.Reason == SessionSwitchReason.SessionLogon)
			{
				this.ScheduleRehook();
			}
		}

		private void ScheduleRehook()
		{
			try
			{
				if (base.IsHandleCreated && !base.IsDisposed)
				{
					base.BeginInvoke(new MethodInvoker(this.StartRehookTimer));
				}
			}
			catch (Exception)
			{
			}
		}

		private void StartRehookTimer()
		{
			// Wait briefly so the system settles; repeated events just restart the wait.
			this._rehookTimer.Stop();
			this._rehookTimer.Start();
		}

		private void RehookTimer_Tick(object sender, EventArgs e)
		{
			this._rehookTimer.Stop();
			try
			{
				modKeyboard.RehookKeyboard();
				modMouse.RehookMouse();
			}
			catch (Exception)
			{
			}
		}

		private void SuppressComboFocusCue()
		{
			NativeMethods.SendMessage(base.Handle, 295, (IntPtr)65537, IntPtr.Zero);
		}

		// Token: 0x040000AE RID: 174
		private bool FadeStatus;

		// Token: 0x040000AF RID: 175
		private int lastHandle;

		// Token: 0x040000B0 RID: 176
		private NotifyIcon _NotifyIcon;

		// Token: 0x040000B1 RID: 177
		private ContextMenuStrip _NotifyMenu;

		// Token: 0x040000B2 RID: 178
		private ToolStripMenuItem _ExitToolStripMenuItem;

		// Token: 0x040000B3 RID: 179
		private ToolStripMenuItem _ShowToolStripMenuItem;

		// Token: 0x040000B4 RID: 180
		private Timer _FadeTimer;

		// Token: 0x040000B5 RID: 181
		private PictureBox _btnClose;

		// Token: 0x040000B6 RID: 182
		private PictureBox _btnMinimize;

		// Token: 0x040000B7 RID: 183
		private PictureBox _picIco;

		// Token: 0x040000B8 RID: 184
		private ComboBox _comLayouts;

		// Token: 0x040000BB RID: 187
		private PictureBox _btnDrag;

		// Token: 0x040000BC RID: 188
		private Timer _CheckHandle;

		// Token: 0x040000BE RID: 190
		private const int WM_CHANGEUISTATE = 295;

		// Token: 0x040000BF RID: 191
		private const int UIS_SET = 1;

		// Token: 0x040000C0 RID: 192
		private const int UISF_HIDEFOCUS = 1;
	}
}
