using System;
using System.Runtime.InteropServices;

namespace BijoyBayanno
{
	// Token: 0x02000010 RID: 16
	internal sealed class modMouse
	{
		// Token: 0x06000210 RID: 528
		[DllImport("user32", SetLastError = true)]
		private static extern int SetWindowsHookEx(int idHook, modMouse.MouseHookDelegate lpfn, IntPtr hmod, int dwThreadId);

		// Token: 0x06000211 RID: 529
		[DllImport("user32", SetLastError = true)]
		private static extern int UnhookWindowsHookEx(int hHook);

		// Token: 0x06000212 RID: 530
		[DllImport("user32", SetLastError = true)]
		private static extern int CallNextHookEx(int hHook, int nCode, int wParam, ref modMouse.MSLLHOOKSTRUCT lParam);

		// Token: 0x06000213 RID: 531 RVA: 0x00003170 File Offset: 0x00001370
		private static int MouseCallback(int nCode, int wParam, ref modMouse.MSLLHOOKSTRUCT lParam)
		{
			if (nCode == 0 && (wParam == 513 || wParam == 516))
			{
				AvroEngine.ClearBuffer();
				modKeyboard.AvroPrevOutput = 0;
				modKeyboard.AvroPrevOutputStr = "";
			}
			return modMouse.CallNextHookEx(modMouse._mouseHookHandle, nCode, wParam, ref lParam);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000031A7 File Offset: 0x000013A7
		public static void HookMouse()
		{
			modMouse._mouseCallback = new modMouse.MouseHookDelegate(modMouse.MouseCallback);
			modMouse._mouseHookHandle = modMouse.SetWindowsHookEx(14, modMouse._mouseCallback, IntPtr.Zero, 0);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000031D1 File Offset: 0x000013D1
		public static void RehookMouse()
		{
			modMouse.UnhookMouse();
			modMouse.HookMouse();
		}

		public static void UnhookMouse()
		{
			if (modMouse._mouseHookHandle != 0)
			{
				modMouse.UnhookWindowsHookEx(modMouse._mouseHookHandle);
				modMouse._mouseHookHandle = 0;
			}
		}

		// Token: 0x0400010F RID: 271
		private const int WH_MOUSE_LL = 14;

		// Token: 0x04000110 RID: 272
		private const int WM_LBUTTONDOWN = 513;

		// Token: 0x04000111 RID: 273
		private static int _mouseHookHandle;

		// Token: 0x04000112 RID: 274
		private static modMouse.MouseHookDelegate _mouseCallback;

		// Token: 0x04000113 RID: 275
		private const int WM_RBUTTONDOWN = 516;

		// Token: 0x02000011 RID: 17
		// (Invoke) Token: 0x06000218 RID: 536
		public delegate int MouseHookDelegate(int nCode, int wParam, ref modMouse.MSLLHOOKSTRUCT lParam);

		// Token: 0x02000012 RID: 18
		public struct MSLLHOOKSTRUCT
		{
			// Token: 0x04000114 RID: 276
			public int x;

			// Token: 0x04000115 RID: 277
			public int y;

			// Token: 0x04000116 RID: 278
			public int mouseData;

			// Token: 0x04000117 RID: 279
			public int flags;

			// Token: 0x04000118 RID: 280
			public int time;

			// Token: 0x04000119 RID: 281
			public IntPtr dwExtraInfo;
		}
	}
}
