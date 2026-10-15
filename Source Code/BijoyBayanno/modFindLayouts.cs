using System;
using System.IO;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using BijoyBayanno.My;

namespace BijoyBayanno
{
	// Token: 0x0200000C RID: 12
	internal sealed class modFindLayouts
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00010838 File Offset: 0x0000EA38
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public static bool Find()
		{
			string text = myFunctions.VerifyPath(Program.AppDirectory);
			text += "Layouts";
			if (!Directory.Exists(text))
			{
				TextUtil.ShowError("'Layouts' folder not found");
				System.Environment.Exit(0);
			}
			string[] files = Directory.GetFiles(text, "*.xml", SearchOption.AllDirectories);
			if (files.Length == 0)
			{
				TextUtil.ShowError("No file found under 'Layouts' folder");
				System.Environment.Exit(0);
			}
			checked
			{
				modKeyboard.LayoutNumber = files.Length + 1;
				modKeyboard.Layout = new clsLayouts[modKeyboard.LayoutNumber - 2 + 1];
				int num = modKeyboard.LayoutNumber - 2;
				for (int i = 0; i <= num; i++)
				{
					modKeyboard.Layout[i] = new clsLayouts();
					modKeyboard.Layout[i].Init(files[i]);
					modKeyboard.Layout[i].Path = files[i];
					Program.MainForm.comLayouts.Items.Add(modKeyboard.Layout[i].Name);
					ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
					toolStripMenuItem.Text = modKeyboard.Layout[i].Name;
					modKeyboard.Layout[i].LoadIcons(myFunctions.VerifyPath(Program.AppDirectory) + "Icons\\");
					try
					{
						if (modKeyboard.Layout[i].Icon16 != null)
						{
							toolStripMenuItem.Image = modKeyboard.Layout[i].Icon16.ToBitmap();
						}
					}
					catch (Exception)
					{
					}
					modKeyboard.Layout[i].ID = i;
					toolStripMenuItem.Tag = i + 2;
					Program.MainForm.NotifyMenu.Items.Insert(0, toolStripMenuItem);
				}
				return false;
			}
		}
	}
}
