using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;

namespace BijoyBayanno.My.Resources
{
	// Token: 0x0200002A RID: 42
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal sealed class Resources
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000036EF File Offset: 0x000018EF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("BijoyBayanno.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000371B File Offset: 0x0000191B
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00003722 File Offset: 0x00001922
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000372A File Offset: 0x0000192A
		internal static Icon BanN
		{
			get
			{
				using (Stream manifestResourceStream = typeof(Resources).Assembly.GetManifestResourceStream("BijoyBayanno.EnglishIcon.ico"))
				{
					int num = Resources.TrayIconPixels();
					return new Icon(manifestResourceStream, num, num);
				}
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000378A File Offset: 0x0000198A
		internal static Bitmap Close2
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Close2", Resources.resourceCulture);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000280 RID: 640 RVA: 0x000037CA File Offset: 0x000019CA
		internal static Bitmap Main
		{
			get
			{
				using (Stream manifestResourceStream = typeof(Resources).Assembly.GetManifestResourceStream("BijoyBayanno.EnglishIcon24.png"))
				{
					using (Bitmap source = new Bitmap(manifestResourceStream))
					{
						return new Bitmap(source);
					}
				}
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000281 RID: 641 RVA: 0x000037EA File Offset: 0x000019EA
		internal static Bitmap Main16
		{
			get
			{
				using (Stream manifestResourceStream = typeof(Resources).Assembly.GetManifestResourceStream("BijoyBayanno.EnglishIcon16.png"))
				{
					using (Bitmap source = new Bitmap(manifestResourceStream))
					{
						return new Bitmap(source);
					}
				}
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000380A File Offset: 0x00001A0A
		internal static Bitmap show
		{
			get
			{
				Bitmap result;
				using (Stream manifestResourceStream = typeof(Resources).Assembly.GetManifestResourceStream("BijoyBayanno.ShowTopbar16.png"))
				{
					using (Bitmap source = new Bitmap(manifestResourceStream))
					{
						result = new Bitmap(source);
					}
				}
				return result;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000382A File Offset: 0x00001A2A
		internal static Bitmap SysTray
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("SysTray", Resources.resourceCulture);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00015228 File Offset: 0x00013428
		internal static Bitmap options_gear_icon
		{
			get
			{
				Bitmap result;
				using (Stream manifestResourceStream = typeof(Resources).Assembly.GetManifestResourceStream("BijoyBayanno.options_gear_icon.png"))
				{
					result = new Bitmap(manifestResourceStream);
				}
				return result;
			}
		}

		internal static Bitmap options_gear_icon16
		{
			get
			{
				Bitmap result;
				using (Stream manifestResourceStream = typeof(Resources).Assembly.GetManifestResourceStream("BijoyBayanno.options_gear_icon16.png"))
				{
					using (Bitmap source = new Bitmap(manifestResourceStream))
					{
						result = new Bitmap(source);
					}
				}
				return result;
			}
		}

		// Tray icon size that matches the real display scaling (16 / 20 / 24 / 32 pixels).
		private static int TrayIconPixels()
		{
			int num = 96;
			try
			{
				object value = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop\\WindowMetrics", "AppliedDPI", null);
				if (value is int)
				{
					num = (int)value;
				}
			}
			catch (Exception)
			{
			}
			int num2 = (int)Math.Round(16.0 * (double)num / 96.0);
			if (num2 <= 16)
			{
				return 16;
			}
			if (num2 <= 20)
			{
				return 20;
			}
			if (num2 <= 24)
			{
				return 24;
			}
			return 32;
		}

		internal static Bitmap Bijoy52
		{
			get
			{
				Bitmap result;
				using (Stream manifestResourceStream = typeof(Resources).Assembly.GetManifestResourceStream("BijoyBayanno.Bijoy52.png"))
				{
					using (Bitmap source = new Bitmap(manifestResourceStream))
					{
						result = new Bitmap(source);
					}
				}
				return result;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00015274 File Offset: 0x00013474
		internal static Bitmap TopBar
		{
			get
			{
				Bitmap result;
				using (Stream manifestResourceStream = typeof(Resources).Assembly.GetManifestResourceStream("BijoyBayanno.TopBar.png"))
				{
					result = new Bitmap(manifestResourceStream);
				}
				return result;
			}
		}

		// Token: 0x0400016E RID: 366
		private static ResourceManager resourceMan;

		// Token: 0x0400016F RID: 367
		private static CultureInfo resourceCulture;
	}
}
