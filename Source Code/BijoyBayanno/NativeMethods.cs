using System;
using System.Runtime.InteropServices;

namespace BijoyBayanno
{
	// Token: 0x0200001A RID: 26
	public class NativeMethods
	{
		// Token: 0x06000248 RID: 584
		[DllImport("user32.dll")]
		internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000249 RID: 585
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern int SendInput(int cInputs, ref NativeMethods.INPUT pInputs, int cbSize);

		// Token: 0x0200001B RID: 27
		[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 28)]
		internal struct INPUT
		{
			// Token: 0x04000136 RID: 310
			[FieldOffset(0)]
			public NativeMethods.InputType dwType;

			// Token: 0x04000137 RID: 311
			[FieldOffset(4)]
			public NativeMethods.MOUSEINPUT mi;

			// Token: 0x04000138 RID: 312
			[FieldOffset(4)]
			public NativeMethods.KEYBDINPUT ki;

			// Token: 0x04000139 RID: 313
			[FieldOffset(4)]
			public NativeMethods.HARDWAREINPUT hi;
		}

		// Token: 0x0200001C RID: 28
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct MOUSEINPUT
		{
			// Token: 0x0400013A RID: 314
			public int dx;

			// Token: 0x0400013B RID: 315
			public int dy;

			// Token: 0x0400013C RID: 316
			public int mouseData;

			// Token: 0x0400013D RID: 317
			public NativeMethods.MOUSEEVENTF dwFlags;

			// Token: 0x0400013E RID: 318
			public int time;

			// Token: 0x0400013F RID: 319
			public IntPtr dwExtraInfo;
		}

		// Token: 0x0200001D RID: 29
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct KEYBDINPUT
		{
			// Token: 0x04000140 RID: 320
			public short wVk;

			// Token: 0x04000141 RID: 321
			public short wScan;

			// Token: 0x04000142 RID: 322
			public NativeMethods.KEYEVENTF dwFlags;

			// Token: 0x04000143 RID: 323
			public int time;

			// Token: 0x04000144 RID: 324
			public IntPtr dwExtraInfo;
		}

		// Token: 0x0200001E RID: 30
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct HARDWAREINPUT
		{
			// Token: 0x04000145 RID: 325
			public int uMsg;

			// Token: 0x04000146 RID: 326
			public short wParamL;

			// Token: 0x04000147 RID: 327
			public short wParamH;
		}

		// Token: 0x0200001F RID: 31
		internal enum InputType
		{
			// Token: 0x04000149 RID: 329
			Mouse,
			// Token: 0x0400014A RID: 330
			Keyboard,
			// Token: 0x0400014B RID: 331
			Hardware
		}

		// Token: 0x02000020 RID: 32
		[Flags]
		internal enum MOUSEEVENTF
		{
			// Token: 0x0400014D RID: 333
			MOVE = 1,
			// Token: 0x0400014E RID: 334
			LEFTDOWN = 2,
			// Token: 0x0400014F RID: 335
			LEFTUP = 4,
			// Token: 0x04000150 RID: 336
			RIGHTDOWN = 8,
			// Token: 0x04000151 RID: 337
			RIGHTUP = 16,
			// Token: 0x04000152 RID: 338
			MIDDLEDOWN = 32,
			// Token: 0x04000153 RID: 339
			MIDDLEUP = 64,
			// Token: 0x04000154 RID: 340
			XDOWN = 128,
			// Token: 0x04000155 RID: 341
			XUP = 256,
			// Token: 0x04000156 RID: 342
			VIRTUALDESK = 1024,
			// Token: 0x04000157 RID: 343
			WHEEL = 2048,
			// Token: 0x04000158 RID: 344
			ABSOLUTE = 32768
		}

		// Token: 0x02000021 RID: 33
		[Flags]
		internal enum KEYEVENTF
		{
			// Token: 0x0400015A RID: 346
			EXTENDEDKEY = 1,
			// Token: 0x0400015B RID: 347
			KEYUP = 2,
			// Token: 0x0400015C RID: 348
			UNICODE = 4,
			// Token: 0x0400015D RID: 349
			SCANCODE = 8
		}
	}
}
