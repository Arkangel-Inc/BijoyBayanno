using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace BijoyBayanno
{
	// Small, explicit helpers (plain .NET) used instead of Microsoft.VisualBasic runtime helpers.
	internal static class TextUtil
	{
		private const string AppTitle = "BijoyBayanno";

		// First <length> characters; same rules as the classic Left(): negative throws, too long returns the whole string.
		public static string Left(string s, int length)
		{
			if (length < 0)
			{
				throw new ArgumentException("Argument 'Length' must be greater than or equal to zero.", "length");
			}
			if (length == 0 || s == null)
			{
				return "";
			}
			if (length >= s.Length)
			{
				return s;
			}
			return s.Substring(0, length);
		}

		// Last <length> characters; same rules as the classic Right().
		public static string Right(string s, int length)
		{
			if (length < 0)
			{
				throw new ArgumentException("Argument 'Length' must be greater than or equal to zero.", "length");
			}
			if (length == 0 || s == null)
			{
				return "";
			}
			if (length >= s.Length)
			{
				return s;
			}
			return s.Substring(s.Length - length, length);
		}

		// Character code of the first character.
		public static int AscW(string s)
		{
			if (s == null || s.Length == 0)
			{
				throw new ArgumentException("Argument 'String' cannot be empty.", "s");
			}
			return (int)s[0];
		}

		// Character for a code: 0-127 is ASCII, 128-255 uses the system ANSI code page.
		public static char Chr(int code)
		{
			if (code < 0 || code > 255)
			{
				throw new ArgumentException("Argument 'CharCode' must be in the range 0 to 255.", "code");
			}
			if (code < 128)
			{
				return (char)code;
			}
			return Encoding.Default.GetString(new byte[] { (byte)code })[0];
		}

		public static string ToStr(object value)
		{
			if (value == null)
			{
				return "";
			}
			string text = value as string;
			if (text != null)
			{
				return text;
			}
			return Convert.ToString(value, CultureInfo.CurrentCulture);
		}

		public static int ToInt(object value)
		{
			if (value == null)
			{
				return 0;
			}
			if (value is int)
			{
				return (int)value;
			}
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				int result;
				if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
				{
					return result;
				}
				double number;
				if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out number))
				{
					return (int)Math.Round(number);
				}
				throw new InvalidCastException("Conversion to integer is not valid.");
			}
			return Convert.ToInt32(value, CultureInfo.InvariantCulture);
		}

		public static bool ToBool(object value)
		{
			if (value == null)
			{
				return false;
			}
			if (value is bool)
			{
				return (bool)value;
			}
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				bool flag;
				if (bool.TryParse(text, out flag))
				{
					return flag;
				}
				double number;
				if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out number))
				{
					return number != 0.0;
				}
				throw new InvalidCastException("Conversion to Boolean is not valid.");
			}
			return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
		}

		public static void ShowError(string message)
		{
			MessageBox.Show(message, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}

	// Modifier key state (same source the classic keyboard helper used: Control.ModifierKeys).
	internal static class ModState
	{
		public static bool Ctrl
		{
			get { return (Control.ModifierKeys & Keys.Control) == Keys.Control; }
		}

		public static bool Alt
		{
			get { return (Control.ModifierKeys & Keys.Alt) == Keys.Alt; }
		}

		public static bool Shift
		{
			get { return (Control.ModifierKeys & Keys.Shift) == Keys.Shift; }
		}
	}
}
