using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace BijoyBayanno
{
	// Token: 0x0200000B RID: 11
	internal sealed class mHandleInfo
	{
		// Token: 0x060001E9 RID: 489
		[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int IsWindow(int hWnd);

		// Token: 0x060001EA RID: 490 RVA: 0x0001068C File Offset: 0x0000E88C
		public static bool AddInfo(long Handle, clsLayouts Status)
		{
			try
			{
				for (int i = mHandleInfo.layInfo.Count - 1; i >= 0; i--)
				{
					if ((long)((clsHandleInfo)mHandleInfo.layInfo[i]).Handle == Handle)
					{
						mHandleInfo.layInfo.RemoveAt(i);
					}
				}
			}
			catch (Exception)
			{
			}
			clsHandleInfo clsHandleInfo = new clsHandleInfo();
			clsHandleInfo.Handle = checked((int)Handle);
			clsHandleInfo.Status = Status;
			mHandleInfo.layInfo.Add(clsHandleInfo);
			for (int j = mHandleInfo.layInfo.Count - 1; j >= 0; j--)
			{
				if (mHandleInfo.IsWindow(((clsHandleInfo)mHandleInfo.layInfo[j]).Handle) == 0)
				{
					mHandleInfo.layInfo.RemoveAt(j);
				}
			}
			return true;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0001074C File Offset: 0x0000E94C
		public static clsLayouts FindInfo(long Handle)
		{
			try
			{
				foreach (object obj in mHandleInfo.layInfo)
				{
					clsHandleInfo clsHandleInfo = (clsHandleInfo)obj;
					if ((long)clsHandleInfo.Handle == Handle)
					{
						return clsHandleInfo.Status;
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
			return null;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000107D8 File Offset: 0x0000E9D8
		public static bool RemoveInfo(long Handle)
		{
			try
			{
				for (int i = mHandleInfo.layInfo.Count - 1; i >= 0; i--)
				{
					if ((long)((clsHandleInfo)mHandleInfo.layInfo[i]).Handle == Handle)
					{
						mHandleInfo.layInfo.RemoveAt(i);
					}
				}
			}
			catch (Exception)
			{
			}
			return true;
		}

		// Token: 0x040000E0 RID: 224
		public static ArrayList layInfo = new ArrayList();

		// Token: 0x040000E1 RID: 225
		public static int lamTargetHandle = 0;
	}
}
