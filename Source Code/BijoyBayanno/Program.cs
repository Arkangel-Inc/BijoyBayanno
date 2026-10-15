using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace BijoyBayanno
{
	// Plain WinForms entry point (single instance via a named mutex).
	internal static class Program
	{
		private static frmMain _mainForm;
		private static frmOptions _optionsForm;

		internal static frmMain MainForm
		{
			get { return Program._mainForm; }
		}

		internal static frmOptions OptionsForm
		{
			get
			{
				if (Program._optionsForm == null || Program._optionsForm.IsDisposed)
				{
					Program._optionsForm = new frmOptions();
				}
				return Program._optionsForm;
			}
		}

		internal static string AppDirectory
		{
			get { return Path.GetDirectoryName(Application.ExecutablePath); }
		}

		internal static string AssemblyName
		{
			get { return Assembly.GetExecutingAssembly().GetName().Name; }
		}

		[STAThread]
		private static void Main()
		{
			bool createdNew;
			using (Mutex mutex = new Mutex(true, "Local\\BijoyBayanno.SingleInstance", out createdNew))
			{
				if (!createdNew)
				{
					return;
				}
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Program._mainForm = new frmMain();
				Application.Run(Program._mainForm);
			}
		}
	}
}
