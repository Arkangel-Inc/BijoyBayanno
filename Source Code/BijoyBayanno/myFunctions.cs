using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BijoyBayanno
{
	// Token: 0x02000014 RID: 20
	internal sealed class myFunctions
	{
		// Token: 0x0600022C RID: 556
		[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "SendMessageA", ExactSpelling = true, SetLastError = true)]
		public static extern int SendMessage(int hWnd, int wMsg, int wParam, int lParam);

		// Token: 0x0600022D RID: 557
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int ReleaseCapture();

		// Token: 0x0600022E RID: 558
		[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "FindWindowExA", ExactSpelling = true, SetLastError = true)]
		private static extern int FindWindowEx(int hWnd1, int hWnd2, string lpsz1, string lpsz2);

		// Token: 0x0600022F RID: 559
		[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "GetWindowLongA", ExactSpelling = true, SetLastError = true)]
		public static extern int GetWindowLong(int hwnd, int nIndex);

		// Token: 0x06000230 RID: 560
		[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "SetWindowLongA", ExactSpelling = true, SetLastError = true)]
		public static extern int SetWindowLong(int hwnd, int nIndex, int dwNewLong);

		// Token: 0x06000231 RID: 561 RVA: 0x000032D1 File Offset: 0x000014D1
		public static bool Drag(Form Client)
		{
			myFunctions.ReleaseCapture();
			myFunctions.SendMessage((int)Client.Handle, 161, 2, 0);
			Client.Top = 0;
			return false;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000032F9 File Offset: 0x000014F9
		public static string VerifyPath(string Path)
		{
			if (!Path.EndsWith("\\"))
			{
				Path += "\\";
			}
			return Path;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000122C8 File Offset: 0x000104C8
		public static void SetControlStyle(Control TargetControl, myFunctions.Style NewStyleType)
		{
			bool flag = TargetControl is TextBox;
			int num = 0;
			if (flag)
			{
				num = (int)TargetControl.Handle;
			}
			else if (TargetControl is ComboBox)
			{
				int hWnd = (int)TargetControl.Handle;
				int hWnd2 = 0;
				num = myFunctions.FindWindowEx(hWnd, hWnd2, null, null);
			}
			if (num == 0)
			{
				return;
			}
			int num2 = myFunctions.GetWindowLong(num, -16);
			num2 |= (int)NewStyleType;
			myFunctions.SetWindowLong(num, -16, num2);
			TargetControl.Refresh();
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0001233C File Offset: 0x0001053C
		public static int DefineKey(string Key)
		{
			if (Key != null)
			{
				int length = Key.Length;
				if (length != 2)
				{
					if (length == 3)
					{
						switch (Key[2])
						{
						case '0':
							if (Key == "F10")
							{
								return 121;
							}
							break;
						case '1':
							if (Key == "F11")
							{
								return 122;
							}
							break;
						case '2':
							if (Key == "F12")
							{
								return 123;
							}
							break;
						}
					}
				}
				else
				{
					switch (Key[1])
					{
					case '1':
						if (Key == "F1")
						{
							return 112;
						}
						break;
					case '2':
						if (Key == "F2")
						{
							return 113;
						}
						break;
					case '3':
						if (Key == "F3")
						{
							return 114;
						}
						break;
					case '4':
						if (Key == "F4")
						{
							return 115;
						}
						break;
					case '5':
						if (Key == "F5")
						{
							return 116;
						}
						break;
					case '6':
						if (Key == "F6")
						{
							return 117;
						}
						break;
					case '7':
						if (Key == "F7")
						{
							return 118;
						}
						break;
					case '8':
						if (Key == "F8")
						{
							return 119;
						}
						break;
					case '9':
						if (Key == "F9")
						{
							return 120;
						}
						break;
					}
				}
			}
			return 0;
		}

		// Activation code for a Machine ID (same result as the original release).
		public static string Encrypt1(string Number)
		{
			long num = 0L;
			long value;
			string first12 = (Number.Length > 12) ? Number.Substring(0, 12) : Number;
			if (long.TryParse(first12, out value))
			{
				num = Convert.ToInt64(decimal.Ceiling(new decimal((double)value / 2.0)));
				num += 2602L;
			}
			char[] digits = num.ToString().ToLowerInvariant().ToCharArray();
			Array.Reverse(digits);
			string last2 = (Number.Length > 2) ? Number.Substring(Number.Length - 2) : Number;
			return last2 + new string(digits);
		}

		// Token: 0x04000121 RID: 289
		public static bool TrayMode;

		// Token: 0x04000122 RID: 290
		private const int WM_NCLBUTTONDOWN = 161;

		// Token: 0x04000123 RID: 291
		private const int HTCAPTION = 2;

		// Token: 0x04000124 RID: 292
		private const int GWL_STYLE = -16;

		// Token: 0x02000015 RID: 21
		public enum Style
		{
			// Token: 0x04000126 RID: 294
			ES_UPPERCASE = 8,
			// Token: 0x04000127 RID: 295
			ES_NUMBER = 8192
		}
	}
}
