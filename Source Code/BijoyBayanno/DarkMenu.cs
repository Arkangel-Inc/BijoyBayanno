using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BijoyBayanno
{
	// Dark look for the system tray menu (neutral grey, like the Windows 11 dark taskbar/menus).
	internal static class DarkMenu
	{
		internal static readonly Color Back = Color.FromArgb(43, 43, 43);
		internal static readonly Color Text = Color.FromArgb(242, 242, 242);
		internal static readonly Color Hover = Color.FromArgb(62, 62, 62);
		internal static readonly Color Line = Color.FromArgb(70, 70, 70);
		internal static readonly Color Border = Color.FromArgb(70, 70, 70);

		[DllImport("dwmapi.dll")]
		private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

		internal static void Apply(ContextMenuStrip menu)
		{
			try
			{
				menu.RenderMode = ToolStripRenderMode.Professional;
				menu.Renderer = new DarkMenuRenderer();
				menu.BackColor = DarkMenu.Back;
				menu.ForeColor = DarkMenu.Text;
				menu.Font = new Font("Segoe UI", 9.5f);
				menu.HandleCreated += DarkMenu.Menu_HandleCreated;
			}
			catch (Exception)
			{
			}
		}

		// Windows 11 only: rounded corners and a matching border colour. Ignored elsewhere.
		private static void Menu_HandleCreated(object sender, EventArgs e)
		{
			try
			{
				ContextMenuStrip contextMenuStrip = (ContextMenuStrip)sender;
				int num = 2;
				DarkMenu.DwmSetWindowAttribute(contextMenuStrip.Handle, 33, ref num, 4);
				int num2 = (DarkMenu.Border.B << 16) | (DarkMenu.Border.G << 8) | DarkMenu.Border.R;
				DarkMenu.DwmSetWindowAttribute(contextMenuStrip.Handle, 34, ref num2, 4);
			}
			catch (Exception)
			{
			}
		}
	}

	internal class DarkColorTable : ProfessionalColorTable
	{
		public override Color ToolStripDropDownBackground
		{
			get { return DarkMenu.Back; }
		}

		public override Color ImageMarginGradientBegin
		{
			get { return DarkMenu.Back; }
		}

		public override Color ImageMarginGradientMiddle
		{
			get { return DarkMenu.Back; }
		}

		public override Color ImageMarginGradientEnd
		{
			get { return DarkMenu.Back; }
		}

		public override Color MenuBorder
		{
			get { return DarkMenu.Border; }
		}
	}

	internal class DarkMenuRenderer : ToolStripProfessionalRenderer
	{
		public DarkMenuRenderer() : base(new DarkColorTable())
		{
			this.RoundedEdges = false;
		}

		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			using (SolidBrush brush = new SolidBrush(DarkMenu.Back))
			{
				e.Graphics.FillRectangle(brush, e.AffectedBounds);
			}
		}

		protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
		}

		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			Rectangle rect = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
			using (Pen pen = new Pen(DarkMenu.Border))
			{
				e.Graphics.DrawRectangle(pen, rect);
			}
		}

		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			if (e.Item.Selected && e.Item.Enabled)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				Rectangle rect = new Rectangle(4, 1, e.Item.Width - 8, e.Item.Height - 2);
				using (GraphicsPath path = DarkMenuRenderer.RoundedRect(rect, 5))
				{
					using (SolidBrush brush = new SolidBrush(DarkMenu.Hover))
					{
						e.Graphics.FillPath(brush, path);
					}
				}
			}
		}

		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			int y = e.Item.Height / 2;
			using (Pen pen = new Pen(DarkMenu.Line))
			{
				e.Graphics.DrawLine(pen, 8, y, e.Item.Width - 8, y);
			}
		}

		protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
		{
			Rectangle imageRectangle = e.ImageRectangle;
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (Pen pen = new Pen(DarkMenu.Text, 2f))
			{
				pen.StartCap = LineCap.Round;
				pen.EndCap = LineCap.Round;
				pen.LineJoin = LineJoin.Round;
				float x = (float)(imageRectangle.Left + imageRectangle.Width / 2);
				float y = (float)(imageRectangle.Top + imageRectangle.Height / 2);
				e.Graphics.DrawLines(pen, new PointF[]
				{
					new PointF(x - 4f, y),
					new PointF(x - 1f, y + 3f),
					new PointF(x + 4f, y - 3f)
				});
			}
		}

		protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
		{
			e.TextColor = (e.Item.Enabled ? DarkMenu.Text : Color.FromArgb(128, 128, 128));
			base.OnRenderItemText(e);
		}

		private static GraphicsPath RoundedRect(Rectangle r, int radius)
		{
			int num = radius * 2;
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddArc(r.Left, r.Top, num, num, 180f, 90f);
			graphicsPath.AddArc(r.Right - num, r.Top, num, num, 270f, 90f);
			graphicsPath.AddArc(r.Right - num, r.Bottom - num, num, num, 0f, 90f);
			graphicsPath.AddArc(r.Left, r.Bottom - num, num, num, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}
	}
}
