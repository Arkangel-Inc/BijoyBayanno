using System;

namespace BijoyBayanno
{
	// Token: 0x02000002 RID: 2
	public class clsHandleInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002064 File Offset: 0x00000264
		// (set) Token: 0x06000002 RID: 2 RVA: 0x0000206C File Offset: 0x0000026C
		public int Handle
		{
			get
			{
				return this.lnghWnd;
			}
			set
			{
				this.lnghWnd = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002075 File Offset: 0x00000275
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000207D File Offset: 0x0000027D
		public clsLayouts Status
		{
			get
			{
				return this.layStatus;
			}
			set
			{
				this.layStatus = value;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002086 File Offset: 0x00000286
		public clsHandleInfo()
		{
			this.lnghWnd = -1;
		}

		// Token: 0x04000001 RID: 1
		private int lnghWnd;

		// Token: 0x04000002 RID: 2
		private clsLayouts layStatus;
	}
}
