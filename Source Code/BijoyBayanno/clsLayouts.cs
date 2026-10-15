using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using BijoyBayanno.My;

namespace BijoyBayanno
{
	// Token: 0x02000003 RID: 3
	public class clsLayouts
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002095 File Offset: 0x00000295
		public clsLayouts()
		{
			this.Key = new Dictionary<int, ArrayList>();
			this.Juk = new Dictionary<string, string>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000388C File Offset: 0x00001A8C
		public void Init(string Path)
		{
			using (XmlReader xmlReader = XmlReader.Create(Path, new XmlReaderSettings
			{
				CheckCharacters = true,
				CloseInput = true
			}))
			{
				try
				{
					xmlReader.ReadStartElement("KeyLayout");
					this.Name = xmlReader.ReadElementString("Name");
					xmlReader.ReadStartElement("Shortcut");
					this.Shortcut.Alt = TextUtil.ToBool(xmlReader.ReadElementString("Alt"));
					this.Shortcut.Ctrl = TextUtil.ToBool(xmlReader.ReadElementString("Ctrl"));
					this.Shortcut.Shift = TextUtil.ToBool(xmlReader.ReadElementString("Shift"));
					this.Shortcut.KeyCode = TextUtil.ToInt(xmlReader.ReadElementString("KeyCode"));
					xmlReader.ReadEndElement();
					int num = TextUtil.ToInt(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options\\" + this.Name, "KeyCode", 0));
					if (num != 0)
					{
						this.Shortcut.Alt = TextUtil.ToBool(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options\\" + this.Name, "Alt", false));
						this.Shortcut.Ctrl = TextUtil.ToBool(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options\\" + this.Name, "Ctrl", false));
						this.Shortcut.Shift = TextUtil.ToBool(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options\\" + this.Name, "Shift", false));
						this.Shortcut.KeyCode = num;
					}
					this.IconName = xmlReader.ReadElementString("IconName");
					xmlReader.ReadStartElement("Keys");
					bool flag;
					do
					{
						ArrayList arrayList = new ArrayList();
						if (xmlReader.NodeType == XmlNodeType.Element | xmlReader.NodeType == XmlNodeType.Attribute)
						{
							int key = TextUtil.ToInt(xmlReader.GetAttribute("KeyCode"));
							arrayList.Add(xmlReader.GetAttribute("Normal_Option"));
							arrayList.Add(xmlReader.GetAttribute("Shift_Option"));
							xmlReader.Read();
							arrayList.Insert(0, xmlReader.ReadElementString("Normal"));
							arrayList.Insert(1, xmlReader.ReadElementString("Shift"));
							this.Key.Add(key, arrayList);
						}
						xmlReader.Read();
						flag = (string.Equals(xmlReader.Name, "Keys", System.StringComparison.Ordinal));
					}
					while (!flag);
					xmlReader.ReadEndElement();
					xmlReader.ReadStartElement("Juks");
					bool flag2;
					do
					{
						if (xmlReader.NodeType == XmlNodeType.Element)
						{
							xmlReader.Read();
							string key2 = xmlReader.ReadElementString("Seq");
							string value = xmlReader.ReadElementString("Out");
							this.Juk.Add(key2, value);
						}
						xmlReader.Read();
						flag2 = (string.Equals(xmlReader.Name, "Juks", System.StringComparison.Ordinal));
					}
					while (!flag2);
				}
				catch (Exception)
				{
				}
				finally
				{
					xmlReader.Close();
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00003BD8 File Offset: 0x00001DD8
		public void Update()
		{
			this.Key.Clear();
			this.Juk.Clear();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.CheckCharacters = true;
			xmlReaderSettings.CloseInput = true;
			using (XmlReader xmlReader = XmlReader.Create(this.Path, xmlReaderSettings))
			{
				try
				{
					xmlReader.ReadStartElement("KeyLayout");
					this.Name = xmlReader.ReadElementString("Name");
					xmlReader.ReadStartElement("Shortcut");
					this.Shortcut.Alt = TextUtil.ToBool(xmlReader.ReadElementString("Alt"));
					this.Shortcut.Ctrl = TextUtil.ToBool(xmlReader.ReadElementString("Ctrl"));
					this.Shortcut.Shift = TextUtil.ToBool(xmlReader.ReadElementString("Shift"));
					this.Shortcut.KeyCode = TextUtil.ToInt(xmlReader.ReadElementString("KeyCode"));
					xmlReader.ReadEndElement();
					int num = TextUtil.ToInt(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options\\" + this.Name, "KeyCode", 0));
					if (num != 0)
					{
						this.Shortcut.Alt = TextUtil.ToBool(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options\\" + this.Name, "Alt", false));
						this.Shortcut.Ctrl = TextUtil.ToBool(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options\\" + this.Name, "Ctrl", false));
						this.Shortcut.Shift = TextUtil.ToBool(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\BijoyBayanno\\Options\\" + this.Name, "Shift", false));
						this.Shortcut.KeyCode = num;
					}
					this.IconName = xmlReader.ReadElementString("IconName");
					xmlReader.ReadStartElement("Keys");
					bool flag;
					do
					{
						ArrayList arrayList = new ArrayList();
						if (xmlReader.NodeType == XmlNodeType.Element | xmlReader.NodeType == XmlNodeType.Attribute)
						{
							int key = TextUtil.ToInt(xmlReader.GetAttribute("KeyCode"));
							arrayList.Add(xmlReader.GetAttribute("Normal_Option"));
							arrayList.Add(xmlReader.GetAttribute("Shift_Option"));
							xmlReader.Read();
							arrayList.Insert(0, xmlReader.ReadElementString("Normal"));
							arrayList.Insert(1, xmlReader.ReadElementString("Shift"));
							this.Key.Add(key, arrayList);
						}
						xmlReader.Read();
						flag = (string.Equals(xmlReader.Name, "Keys", System.StringComparison.Ordinal));
					}
					while (!flag);
					xmlReader.ReadEndElement();
					xmlReader.ReadStartElement("Juks");
					bool flag2;
					do
					{
						if (xmlReader.NodeType == XmlNodeType.Element)
						{
							xmlReader.Read();
							string key2 = xmlReader.ReadElementString("Seq");
							string value = xmlReader.ReadElementString("Out");
							this.Juk.Add(key2, value);
						}
						xmlReader.Read();
						flag2 = (string.Equals(xmlReader.Name, "Juks", System.StringComparison.Ordinal));
					}
					while (!flag2);
				}
				catch (Exception)
				{
				}
				finally
				{
					xmlReader.Close();
				}
			}
		}

		// Token: 0x04000003 RID: 3
		public string Name;

		// Token: 0x04000004 RID: 4
		public string IconName;

		// v2: icons loaded once at startup and kept for the life of the process (never disposed).
		public Icon Icon16;

		public Bitmap Bitmap24;

		public void LoadIcons(string iconsDir)
		{
			try
			{
				string file = iconsDir + this.IconName + ".ico";
				Icon small = new Icon(file, 16, 16);
				Icon large = new Icon(file, 24, 24);
				this.Icon16 = small;
				this.Bitmap24 = large.ToBitmap();
			}
			catch (Exception)
			{
				this.Icon16 = null;
				this.Bitmap24 = null;
			}
		}

		// Token: 0x04000005 RID: 5
		public int ID;

		// Token: 0x04000006 RID: 6
		public Dictionary<int, ArrayList> Key;

		// Token: 0x04000007 RID: 7
		public Dictionary<string, string> Juk;

		// Token: 0x04000008 RID: 8
		public clsLayouts.LayoutShortcut Shortcut;

		// Token: 0x04000009 RID: 9
		public string Path;

		// Token: 0x02000004 RID: 4
		public struct LayoutShortcut
		{
			// Token: 0x0400000A RID: 10
			public bool Alt;

			// Token: 0x0400000B RID: 11
			public bool Ctrl;

			// Token: 0x0400000C RID: 12
			public bool Shift;

			// Token: 0x0400000D RID: 13
			public int KeyCode;
		}
	}
}
