using System;
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
	// Token: 0x02000009 RID: 9
	public partial class frmOptions : Form
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00002DD1 File Offset: 0x00000FD1
		public frmOptions()
		{
			base.Load += this.frmSettings_Load;
			this.InitializeComponent();
			this.chkShift.CheckedChanged += this.chkCtrl_CheckedChanged;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00002E08 File Offset: 0x00001008
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00002E10 File Offset: 0x00001010
		internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00002E19 File Offset: 0x00001019
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000E674 File Offset: 0x0000C874
		internal virtual Button OK_Button
		{
			[CompilerGenerated]
			get
			{
				return this._OK_Button;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.OK_Button_Click);
				Button ok_Button = this._OK_Button;
				if (ok_Button != null)
				{
					ok_Button.Click -= value2;
				}
				this._OK_Button = value;
				ok_Button = this._OK_Button;
				if (ok_Button != null)
				{
					ok_Button.Click += value2;
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00002E21 File Offset: 0x00001021
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x0000E6B8 File Offset: 0x0000C8B8
		internal virtual Button Cancel_Button
		{
			[CompilerGenerated]
			get
			{
				return this._Cancel_Button;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Cancel_Button_Click);
				Button cancel_Button = this._Cancel_Button;
				if (cancel_Button != null)
				{
					cancel_Button.Click -= value2;
				}
				this._Cancel_Button = value;
				cancel_Button = this._Cancel_Button;
				if (cancel_Button != null)
				{
					cancel_Button.Click += value2;
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00002E29 File Offset: 0x00001029
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00002E31 File Offset: 0x00001031
		internal virtual TabControl TabControl1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00002E3A File Offset: 0x0000103A
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00002E42 File Offset: 0x00001042
		internal virtual TabPage TabPage1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00002E4B File Offset: 0x0000104B
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00002E53 File Offset: 0x00001053
		internal virtual TabPage TabPage2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00002E64 File Offset: 0x00001064
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00002E6C File Offset: 0x0000106C
		internal virtual CheckBox chkStart { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00002E75 File Offset: 0x00001075
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00002E7D File Offset: 0x0000107D
		internal virtual GroupBox GroupBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00002E86 File Offset: 0x00001086
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00002E8E File Offset: 0x0000108E
		internal virtual RadioButton rWindow { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00002E97 File Offset: 0x00001097
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00002E9F File Offset: 0x0000109F
		internal virtual RadioButton rGlobal { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00002EA8 File Offset: 0x000010A8
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00002EB0 File Offset: 0x000010B0
		internal virtual GroupBox GroupBox2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00002EB9 File Offset: 0x000010B9
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00002EC1 File Offset: 0x000010C1
		internal virtual RadioButton rAM3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00002ECA File Offset: 0x000010CA
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00002ED2 File Offset: 0x000010D2
		internal virtual RadioButton rAM2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00002EDB File Offset: 0x000010DB
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00002EE3 File Offset: 0x000010E3
		internal virtual RadioButton rAM1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00002EEC File Offset: 0x000010EC
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00002EF4 File Offset: 0x000010F4
		internal virtual PictureBox PictureBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00002EFD File Offset: 0x000010FD
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00002F05 File Offset: 0x00001105
		internal virtual PictureBox PictureBox2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00002F0E File Offset: 0x0000110E
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00002F16 File Offset: 0x00001116
		internal virtual GroupBox GroupBox3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00002F1F File Offset: 0x0000111F
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00002F27 File Offset: 0x00001127
		internal virtual ComboBox comKey { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00002F30 File Offset: 0x00001130
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00002F38 File Offset: 0x00001138
		internal virtual CheckBox chkShift { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00002F41 File Offset: 0x00001141
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000E740 File Offset: 0x0000C940
		internal virtual CheckBox chkCtrl
		{
			[CompilerGenerated]
			get
			{
				return this._chkCtrl;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.chkCtrl_CheckedChanged);
				CheckBox chkCtrl = this._chkCtrl;
				if (chkCtrl != null)
				{
					chkCtrl.CheckedChanged -= value2;
				}
				this._chkCtrl = value;
				chkCtrl = this._chkCtrl;
				if (chkCtrl != null)
				{
					chkCtrl.CheckedChanged += value2;
				}
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00002F49 File Offset: 0x00001149
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000E784 File Offset: 0x0000C984
		internal virtual CheckBox chkAlt
		{
			[CompilerGenerated]
			get
			{
				return this._chkAlt;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.chkAlt_CheckedChanged);
				CheckBox chkAlt = this._chkAlt;
				if (chkAlt != null)
				{
					chkAlt.CheckedChanged -= value2;
				}
				this._chkAlt = value;
				chkAlt = this._chkAlt;
				if (chkAlt != null)
				{
					chkAlt.CheckedChanged += value2;
				}
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00002F51 File Offset: 0x00001151
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00002F59 File Offset: 0x00001159
		internal virtual Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00002F62 File Offset: 0x00001162
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00002F6A File Offset: 0x0000116A
		internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00002F73 File Offset: 0x00001173
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
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
				MouseEventHandler value2 = new MouseEventHandler(this.comLayouts_MouseClick);
				EventHandler value3 = new EventHandler(this.comLayouts_SelectedIndexChanged);
				ComboBox comLayouts = this._comLayouts;
				if (comLayouts != null)
				{
					comLayouts.MouseClick -= value2;
					comLayouts.SelectedIndexChanged -= value3;
				}
				this._comLayouts = value;
				comLayouts = this._comLayouts;
				if (comLayouts != null)
				{
					comLayouts.MouseClick += value2;
					comLayouts.SelectedIndexChanged += value3;
				}
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00002F7B File Offset: 0x0000117B
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00002F83 File Offset: 0x00001183
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060001D8 RID: 472 RVA: 0x0000E828 File Offset: 0x0000CA28
		private void OK_Button_Click(object sender, EventArgs e)
		{
			if (this.rWindow.Checked)
			{
				Program.MainForm.CheckHandle.Enabled = true;
			}
			else
			{
				Program.MainForm.CheckHandle.Enabled = false;
			}
			if (this.rAM1.Checked)
			{
				modKeyboard.AM = 1;
			}
			else if (this.rAM2.Checked)
			{
				modKeyboard.AM = 2;
			}
			else
			{
				modKeyboard.AM = 3;
			}
			try
			{
				if (this.chkStart.Checked)
				{
					Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", "BijoyBayanno", "\"" + myFunctions.VerifyPath(Program.AppDirectory) + Program.AssemblyName + ".exe\"");
				}
				else
				{
					RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
					if (registryKey != null)
					{
						registryKey.DeleteValue("BijoyBayanno", false);
					}
				}
			}
			catch (Exception)
			{
				TextUtil.ShowError("Not enough privilege");
			}
			checked
			{
				if (this.chkAlt.Enabled)
				{
					int num = 0;
					if (this.chkAlt.Checked)
					{
						num++;
					}
					if (this.chkCtrl.Checked)
					{
						num++;
					}
					if (this.chkShift.Checked)
					{
						num++;
					}
					if (num == 1)
					{
						TextUtil.ShowError("Please select at least two keys: Ctrl+Shift, Alt+Shift, or Ctrl+Alt, along with a letter.");
						return;
					}
					if (num >= 2 && this.comKey.Text == "")
					{
						TextUtil.ShowError("Please also select a letter for the shortcut.");
						return;
					}
					this.WorkLay.Shortcut.Alt = this.chkAlt.Checked;
					this.WorkLay.Shortcut.Ctrl = this.chkCtrl.Checked;
					this.WorkLay.Shortcut.Shift = this.chkShift.Checked;
					if (this.chkAlt.Checked | this.chkCtrl.Checked)
					{
						this.WorkLay.Shortcut.KeyCode = TextUtil.AscW(this.comKey.Text);
					}
					else
					{
						this.WorkLay.Shortcut.KeyCode = myFunctions.DefineKey(this.comKey.Text);
					}
					int num2 = 0;
					int num3 = modKeyboard.Layout.GetUpperBound(0);
					for (int i = num2; i <= num3; i++)
					{
						if (modKeyboard.Layout[i].Shortcut.Equals(this.WorkLay.Shortcut) & !modKeyboard.Layout[i].Equals(this.WorkLay))
						{
							TextUtil.ShowError((this.WorkLay.Shortcut.Alt ? "Alt + " : "") + (this.WorkLay.Shortcut.Ctrl ? "Ctrl + " : "") + (this.WorkLay.Shortcut.Shift ? "Shift + " : "") + this.comKey.Text + " is already assigned to " + modKeyboard.Layout[i].Name + " Layout" + "\r\n" + "Please, Change this Shortcut Key.");
							return;
						}
					}
					try
					{
						Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options\\" + this.WorkLay.Name, "Alt", this.WorkLay.Shortcut.Alt);
						Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options\\" + this.WorkLay.Name, "Ctrl", this.WorkLay.Shortcut.Ctrl);
						Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options\\" + this.WorkLay.Name, "Shift", this.WorkLay.Shortcut.Shift);
						Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options\\" + this.WorkLay.Name, "KeyCode", this.WorkLay.Shortcut.KeyCode);
					}
					catch (Exception)
					{
						TextUtil.ShowError("Not enough privilege");
					}
				}
				int num4 = 0;
				int num5 = modKeyboard.Layout.GetUpperBound(0);
				for (int j = num4; j <= num5; j++)
				{
					modKeyboard.Layout[j].Update();
				}
				this.ShowApplied();
			}
		}

		private void ShowApplied()
		{
			this.OK_Button.Text = "Applied!";
			if (this._appliedTimer == null)
			{
				this._appliedTimer = new Timer();
				this._appliedTimer.Interval = 1500;
				this._appliedTimer.Tick += delegate(object s, EventArgs ev)
				{
					this._appliedTimer.Stop();
					this.OK_Button.Text = "Apply";
				};
			}
			this._appliedTimer.Stop();
			this._appliedTimer.Start();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000021F8 File Offset: 0x000003F8
		private void Cancel_Button_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
		private void frmSettings_Load(object sender, EventArgs e)
		{
			this.rWindow.Checked = Program.MainForm.CheckHandle.Enabled;
			this.rAM2.Checked = (modKeyboard.AM == 2);
			this.rAM3.Checked = (modKeyboard.AM == 3);
			this.chkStart.Checked = !string.IsNullOrEmpty(TextUtil.ToStr(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", "BijoyBayanno", "")));
			myFunctions.SetControlStyle(this.comKey, myFunctions.Style.ES_UPPERCASE);
			this.comLayouts.Items.Clear();
			checked
			{
				int num = Program.MainForm.comLayouts.Items.Count - 1;
				for (int i = 1; i <= num; i++)
				{
					this.comLayouts.Items.Add(Program.MainForm.comLayouts.Items[i]);
					if (this.comLayouts.Items.Count > 0)
					{
						this.comLayouts.SelectedIndex = 0;
					}
				}
				if (this.comKey.Items.Count == 0)
				{
					this.comKey.Items.Clear();
					int num2 = 1;
					do
					{
						this.comKey.Items.Add("F" + TextUtil.ToStr(num2));
						num2++;
					}
					while (num2 <= 12);
					this.comKey.Text = "";
				}
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000EE60 File Offset: 0x0000D060
		private void chkAlt_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkAlt.Checked | this.chkCtrl.Checked | this.chkShift.Checked)
			{
				this.comKey.Items.Clear();
				short num = 48;
				do
				{
					this.comKey.Items.Add(TextUtil.Chr((int)num));
					num += 1;
				}
				while (num <= 57);
				num = 65;
				do
				{
					this.comKey.Items.Add(TextUtil.Chr((int)num));
					num += 1;
				}
				while (num <= 90);
				this.comKey.Text = "";
				return;
			}
			if (!(this.chkCtrl.Checked & this.chkShift.Checked))
			{
				this.comKey.Items.Clear();
				short num2 = 1;
				do
				{
					this.comKey.Items.Add("F" + TextUtil.ToStr((int)num2));
					num2 += 1;
				}
				while (num2 <= 12);
				this.comKey.Text = "";
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000EF6C File Offset: 0x0000D16C
		private void chkCtrl_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkAlt.Checked | this.chkCtrl.Checked | this.chkShift.Checked)
			{
				this.comKey.Items.Clear();
				short num = 48;
				do
				{
					this.comKey.Items.Add(TextUtil.Chr((int)num));
					num += 1;
				}
				while (num <= 57);
				num = 65;
				do
				{
					this.comKey.Items.Add(TextUtil.Chr((int)num));
					num += 1;
				}
				while (num <= 90);
				this.comKey.Text = "";
				return;
			}
			if (!(this.chkAlt.Checked & this.chkShift.Checked))
			{
				this.comKey.Items.Clear();
				short num2 = 1;
				do
				{
					this.comKey.Items.Add("F" + TextUtil.ToStr((int)num2));
					num2 += 1;
				}
				while (num2 <= 12);
				this.comKey.Text = "";
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000029F4 File Offset: 0x00000BF4
		private void comLayouts_MouseClick(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000F078 File Offset: 0x0000D278
		private void comLayouts_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.comKey.Enabled = (this.comLayouts.Text.Length > 1);
			this.chkAlt.Enabled = (this.comLayouts.Text.Length > 1);
			this.chkCtrl.Enabled = (this.comLayouts.Text.Length > 1);
			this.chkShift.Enabled = (this.comLayouts.Text.Length > 1);
			this.WorkLay = modKeyboard.Layout[this.comLayouts.SelectedIndex];
			this.chkAlt.Checked = this.WorkLay.Shortcut.Alt;
			this.chkCtrl.Checked = this.WorkLay.Shortcut.Ctrl;
			this.chkShift.Checked = this.WorkLay.Shortcut.Shift;
			if (this.chkAlt.Checked | this.chkCtrl.Checked)
			{
				this.comKey.Text = TextUtil.ToStr(TextUtil.Chr(this.WorkLay.Shortcut.KeyCode));
				return;
			}
			this.comKey.Text = "F" + TextUtil.ToStr(checked(this.WorkLay.Shortcut.KeyCode - 111));
		}

		// Token: 0x040000D5 RID: 213
		private clsLayouts WorkLay;

		// Token: 0x040000D6 RID: 214
		private Timer _appliedTimer;

		private Button _OK_Button;

		// Token: 0x040000D7 RID: 215
		private Button _Cancel_Button;

		// Token: 0x040000D9 RID: 217
		private CheckBox _chkCtrl;

		// Token: 0x040000DA RID: 218
		private CheckBox _chkAlt;

		// Token: 0x040000DB RID: 219
		private ComboBox _comLayouts;
	}
}
