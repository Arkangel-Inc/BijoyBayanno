using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using BijoyBayanno.My;

namespace BijoyBayanno
{
	// Token: 0x0200000D RID: 13
	internal sealed class modKeyboard
	{
		// Token: 0x060001F1 RID: 497
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int UnhookWindowsHookEx(int hHook);

		// Token: 0x060001F2 RID: 498
		[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "SetWindowsHookExA", ExactSpelling = true, SetLastError = true)]
		public static extern int SetWindowsHookEx(int idHook, modKeyboard.KeyboardHookDelegate lpfn, int hmod, int dwThreadId);

		// Token: 0x060001F3 RID: 499
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int GetAsyncKeyState(int vKey);

		// Token: 0x060001F4 RID: 500
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int CallNextHookEx(int hHook, int nCode, int wParam, modKeyboard.KBDLLHOOKSTRUCT lParam);

		// Token: 0x060001F5 RID: 501
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int GetForegroundWindow();

		// Token: 0x060001F6 RID: 502
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

		// Token: 0x060001F7 RID: 503
		[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, UIntPtr wParam, IntPtr lParam);

		// Token: 0x060001F8 RID: 504 RVA: 0x00010A10 File Offset: 0x0000EC10
		private static void DoKeyBoard(NativeMethods.KEYEVENTF flags, int key)
		{
			NativeMethods.INPUT input = default(NativeMethods.INPUT);
			NativeMethods.KEYBDINPUT ki = default(NativeMethods.KEYBDINPUT);
			input.dwType = NativeMethods.InputType.Keyboard;
			input.ki = ki;
			input.ki.wVk = 0;
			input.ki.wScan = checked((short)key);
			input.ki.time = 0;
			input.ki.dwFlags = flags;
			input.ki.dwExtraInfo = IntPtr.Zero;
			int cbSize = Marshal.SizeOf(typeof(NativeMethods.INPUT));
			NativeMethods.SendInput(1, ref input, cbSize);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		private static void DoKeyBoardA(NativeMethods.KEYEVENTF flags, short key)
		{
			NativeMethods.INPUT input = default(NativeMethods.INPUT);
			NativeMethods.KEYBDINPUT ki = default(NativeMethods.KEYBDINPUT);
			input.dwType = NativeMethods.InputType.Keyboard;
			input.ki = ki;
			input.ki.wVk = Convert.ToInt16(key);
			input.ki.wScan = 0;
			input.ki.time = 0;
			input.ki.dwFlags = flags;
			input.ki.dwExtraInfo = IntPtr.Zero;
			int cbSize = Marshal.SizeOf(typeof(NativeMethods.INPUT));
			NativeMethods.SendInput(1, ref input, cbSize);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00003030 File Offset: 0x00001230
		private static bool ReQue(string Text)
		{
			modKeyboard.Que += Text;
			modKeyboard.Que = TextUtil.Right(modKeyboard.Que, 20);
			return true;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00010B34 File Offset: 0x0000ED34
		public static bool IsHooked(ref modKeyboard.KBDLLHOOKSTRUCT Hookstruct)
		{
			bool result = false;
			if ((modKeyboard.GetAsyncKeyState(91) & 32768) != 0 || (modKeyboard.GetAsyncKeyState(92) & 32768) != 0)
			{
				return false;
			}
			checked
			{
				try
				{
					int num = modKeyboard.LayoutNumber - 2;
					for (int i = 0; i <= num; i++)
					{
						if (ModState.Ctrl == modKeyboard.Layout[i].Shortcut.Ctrl & ModState.Alt == modKeyboard.Layout[i].Shortcut.Alt & ModState.Shift == modKeyboard.Layout[i].Shortcut.Shift & Hookstruct.vkCode == modKeyboard.Layout[i].Shortcut.KeyCode)
						{
							if (Program.MainForm.comLayouts.SelectedIndex == modKeyboard.Layout[i].ID + 1)
							{
								modKeyboard.defLayout = Program.MainForm.comLayouts.SelectedIndex;
								mHandleInfo.RemoveInfo(unchecked((long)modKeyboard.GetForegroundWindow()));
								Program.MainForm.comLayouts.SelectedIndex = 0;
							}
							else
							{
								Program.MainForm.comLayouts.SelectedIndex = modKeyboard.Layout[i].ID + 1;
								mHandleInfo.AddInfo(unchecked((long)modKeyboard.GetForegroundWindow()), modKeyboard.Layout[i]);
								modKeyboard.Que = new string('♠', 20);
							}
							return true;
						}
					}
					modKeyboard.AvroMode = (modKeyboard.curLayout != -1 && modKeyboard.Layout[modKeyboard.curLayout].Name == "Avro Mode");
					if (modKeyboard.AvroMode && !ModState.Ctrl && !ModState.Alt)
					{
						int vkCode = Hookstruct.vkCode;
						if (vkCode == 37 || vkCode == 38 || vkCode == 39 || vkCode == 40 || vkCode == 46 || vkCode == 36 || vkCode == 35 || vkCode == 33 || vkCode == 34)
						{
							AvroEngine.ClearBuffer();
							modKeyboard.AvroPrevOutput = 0;
							modKeyboard.AvroPrevOutputStr = "";
							return false;
						}
						if (vkCode == 8)
						{
							if (AvroEngine.GetCurrentOutput().Length == 0)
							{
								return false;
							}
							string text = AvroEngine.RemoveLastChar();
							string avroPrevOutputStr = modKeyboard.AvroPrevOutputStr;
							int num2 = 0;
							int num3 = Math.Min(avroPrevOutputStr.Length, text.Length);
							while (num2 < num3 && avroPrevOutputStr[num2] == text[num2])
							{
								num2++;
							}
							int num4 = avroPrevOutputStr.Length - num2;
							modKeyboard.Pass = true;
							for (int j = 0; j < num4; j++)
							{
								modKeyboard.DoKeyBoardA((NativeMethods.KEYEVENTF)0, 8);
								modKeyboard.DoKeyBoardA(NativeMethods.KEYEVENTF.KEYUP, 8);
								Thread.Sleep(1);
							}
							modKeyboard.Pass = false;
							string text2 = text.Substring(num2);
							if (text2.Length > 0)
							{
								modKeyboard.Pass = true;
								foreach (char key in text2)
								{
									modKeyboard.DoKeyBoard(NativeMethods.KEYEVENTF.UNICODE, (int)key);
									modKeyboard.DoKeyBoard(NativeMethods.KEYEVENTF.KEYUP | NativeMethods.KEYEVENTF.UNICODE, (int)key);
								}
								modKeyboard.Pass = false;
							}
							modKeyboard.AvroPrevOutput = text.Length;
							modKeyboard.AvroPrevOutputStr = text;
							return true;
						}
						else
						{
							if (vkCode == 32 || vkCode == 13)
							{
								AvroEngine.ClearBuffer();
								modKeyboard.AvroPrevOutput = 0;
								modKeyboard.AvroPrevOutputStr = "";
								return false;
							}
							char c = modKeyboard.VkToChar(vkCode, ModState.Shift);
							bool flag = (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '.' || c == ',' || c == ':' || c == '^' || c == '$' || c == '`';
							if (!flag && c != '\0')
							{
								AvroEngine.ClearBuffer();
								modKeyboard.AvroPrevOutput = 0;
								modKeyboard.AvroPrevOutputStr = "";
								return false;
							}
							if (c > '\0' && flag)
							{
								string currentOutput = AvroEngine.GetCurrentOutput();
								string text4 = AvroEngine.AppendChar(c);
								int num5 = 0;
								int num6 = Math.Min(currentOutput.Length, text4.Length);
								while (num5 < num6 && currentOutput[num5] == text4[num5])
								{
									num5++;
								}
								int num7 = currentOutput.Length - num5;
								modKeyboard.Pass = true;
								for (int l = 0; l < num7; l++)
								{
									modKeyboard.DoKeyBoardA((NativeMethods.KEYEVENTF)0, 8);
									modKeyboard.DoKeyBoardA(NativeMethods.KEYEVENTF.KEYUP, 8);
									Thread.Sleep(1);
								}
								foreach (char key2 in text4.Substring(num5))
								{
									modKeyboard.DoKeyBoard(NativeMethods.KEYEVENTF.UNICODE, (int)key2);
									modKeyboard.DoKeyBoard(NativeMethods.KEYEVENTF.KEYUP | NativeMethods.KEYEVENTF.UNICODE, (int)key2);
								}
								modKeyboard.Pass = false;
								modKeyboard.AvroPrevOutput = text4.Length;
								modKeyboard.AvroPrevOutputStr = text4;
								return true;
							}
							return false;
						}
					}
					else
					{
						if (!ModState.Ctrl & !ModState.Alt & modKeyboard.curLayout != -1)
						{
							if (Hookstruct.vkCode == 8)
							{
								modKeyboard.UnQue();
								if (modKeyboard.delayKey)
								{
									modKeyboard.delayKey = false;
									modKeyboard.delayTimer = 0;
									modKeyboard.delayOut = "";
									modKeyboard.lastDelayOut = "";
									modKeyboard.ConjunctPending = false;
									modKeyboard.ConjunctVowel = "";
									return true;
								}
							}
							if (Hookstruct.vkCode == 13)
							{
								modKeyboard.ReQue("\r\n");
							}
							if (!modKeyboard.Layout[modKeyboard.curLayout].Key.ContainsKey(32) & Hookstruct.vkCode == 32)
							{
								modKeyboard.ReQue(" ");
							}
							bool shiftKeyDown = ModState.Shift;
							bool flag2 = false;
							if (shiftKeyDown)
							{
								if (modKeyboard.Layout[modKeyboard.curLayout].Key.ContainsKey(Hookstruct.vkCode))
								{
									string text5 = TextUtil.ToStr(modKeyboard.Layout[modKeyboard.curLayout].Key[Hookstruct.vkCode][3]);
									if (string.Equals(TextUtil.Left(text5, 5), "DeInc", System.StringComparison.Ordinal))
									{
										if (!modKeyboard.delayKey & modKeyboard.delayTimer == 4)
										{
											modKeyboard.delayKey = true;
											modKeyboard.delayTimer = (4 + int.Parse(TextUtil.Right(text5, 1), System.Globalization.CultureInfo.InvariantCulture));
											modKeyboard.Pass = true;
											modKeyboard.DoKeyBoardA((NativeMethods.KEYEVENTF)0, 8);
											modKeyboard.DoKeyBoardA(NativeMethods.KEYEVENTF.KEYUP, 8);
											modKeyboard.Pass = false;
										}
									}
									else if (string.Equals(text5, "MoveL", System.StringComparison.Ordinal))
									{
										int num8;
										string text6;
										if (modKeyboard.FindLastCluster(out num8, out text6))
										{
											modKeyboard.refQueLen = num8;
											modKeyboard.refScreenText = text6;
										}
										else
										{
											modKeyboard.refQueLen = 0;
											modKeyboard.refScreenText = "";
										}
										modKeyboard.Pass = true;
										if (modKeyboard.refQueLen > 0)
										{
											for (int m = 0; m < modKeyboard.refScreenText.Length; m++)
											{
												modKeyboard.DoKeyBoardA((NativeMethods.KEYEVENTF)0, 8);
												modKeyboard.DoKeyBoardA(NativeMethods.KEYEVENTF.KEYUP, 8);
												Thread.Sleep(1);
											}
										}
										else
										{
											modKeyboard.DoKeyBoardA((NativeMethods.KEYEVENTF)0, 8);
											modKeyboard.DoKeyBoardA(NativeMethods.KEYEVENTF.KEYUP, 8);
											if (modKeyboard.lastClusterLen > 1)
											{
												modKeyboard.DoKeyBoardA((NativeMethods.KEYEVENTF)0, 8);
												modKeyboard.DoKeyBoardA(NativeMethods.KEYEVENTF.KEYUP, 8);
											}
										}
										modKeyboard.Pass = false;
										modKeyboard.flagLeft = true;
									}
									if (string.Equals(text5, "Delay", System.StringComparison.Ordinal))
									{
										if (modKeyboard.ConjunctPending)
										{
											modKeyboard.ConjunctPending = false;
											modKeyboard.ConjunctVowel = "";
											modKeyboard.LastSentChar = "";
										}
										else if (modKeyboard.delayTimer == 5)
										{
											modKeyboard.lastDelayOut = modKeyboard.delayOut;
										}
										modKeyboard.delayKey = true;
										modKeyboard.delayTimer = 5;
										modKeyboard.delayOut = TextUtil.ToStr(modKeyboard.Layout[modKeyboard.curLayout].Key[Hookstruct.vkCode][1]);
										modKeyboard.ReQue(modKeyboard.delayOut);
										flag2 = true;
									}
									else
									{
										string text7 = TextUtil.ToStr(modKeyboard.Layout[modKeyboard.curLayout].Key[Hookstruct.vkCode][1]);
										string text8 = "";
										if (modKeyboard.flagLeft)
										{
											try
											{
												if (modKeyboard.refQueLen > 0 && modKeyboard.refQueLen <= modKeyboard.Que.Length)
												{
													string str = TextUtil.Right(modKeyboard.Que, modKeyboard.refQueLen);
													text8 = modKeyboard.refScreenText;
													modKeyboard.Que = TextUtil.Left(modKeyboard.Que, modKeyboard.Que.Length - modKeyboard.refQueLen) + text7 + str;
													modKeyboard.Que = TextUtil.Right(modKeyboard.Que, 20);
													modKeyboard.refQueLen = 0;
													modKeyboard.refScreenText = "";
													goto IL_8D2;
												}
												int length = modKeyboard.lastClusterLen;
												if (length > modKeyboard.Que.Length)
												{
													length = modKeyboard.Que.Length;
												}
												text8 = TextUtil.Right(modKeyboard.Que, length);
												modKeyboard.Que = TextUtil.Left(modKeyboard.Que, modKeyboard.Que.Length - length) + text7 + text8;
												modKeyboard.Que = TextUtil.Right(modKeyboard.Que, 20);
												goto IL_8D2;
											}
											catch (Exception)
											{
												goto IL_8D2;
											}
										}
										modKeyboard.ReQue(text7);
										IL_8D2:
										modKeyboard.SendKey(text7);
										if (modKeyboard.flagLeft && text8.Length > 0)
										{
											modKeyboard.SendKey(text8);
										}
										modKeyboard.lastClusterLen = 1;
										modKeyboard.LastSentChar = text7;
										if (modKeyboard.delayTimer > 0 && !string.Equals(text5, "MoveL", System.StringComparison.Ordinal))
										{
											modKeyboard.delayTimer--;
										}
										flag2 = true;
									}
									if (string.Equals(text5, "MoveL", System.StringComparison.Ordinal))
									{
										modKeyboard.flagLeft = false;
									}
									if (string.Equals(text5, "Xtra", System.StringComparison.Ordinal))
									{
										modKeyboard.ReQue("‌");
									}
								}
							}
							else if (modKeyboard.Layout[modKeyboard.curLayout].Key.ContainsKey(Hookstruct.vkCode))
							{
								string text9 = TextUtil.ToStr(modKeyboard.Layout[modKeyboard.curLayout].Key[Hookstruct.vkCode][2]);
								bool flag3 = false;
								if (string.Equals(TextUtil.Left(text9, 5), "DeInc", System.StringComparison.Ordinal))
								{
									if (modKeyboard.ConjunctPending)
									{
										modKeyboard.ConjunctPending = false;
										modKeyboard.ConjunctVowel = "";
										modKeyboard.delayKey = false;
										modKeyboard.delayTimer = 0;
										modKeyboard.delayOut = "";
										flag3 = true;
									}
									else if (!modKeyboard.delayKey & modKeyboard.delayTimer == 4 & (text9 == "DeInc1" || text9 == "DeInc2"))
									{
										modKeyboard.ConjunctPending = true;
										modKeyboard.ConjunctVowel = modKeyboard.delayOut;
										modKeyboard.delayOut = "";
										modKeyboard.delayKey = true;
										modKeyboard.delayTimer = (4 + int.Parse(TextUtil.Right(text9, 1), System.Globalization.CultureInfo.InvariantCulture));
									}
								}
								else if (string.Equals(text9, "MoveL", System.StringComparison.Ordinal))
								{
									modKeyboard.Pass = true;
									modKeyboard.DoKeyBoardA((NativeMethods.KEYEVENTF)0, 37);
									modKeyboard.DoKeyBoardA(NativeMethods.KEYEVENTF.KEYUP, 37);
									modKeyboard.Pass = false;
								}
								if (string.Equals(text9, "Delay", System.StringComparison.Ordinal))
								{
									if (modKeyboard.ConjunctPending)
									{
										modKeyboard.ConjunctPending = false;
										modKeyboard.ConjunctVowel = "";
										modKeyboard.LastSentChar = "";
									}
									else if (modKeyboard.delayTimer == 5)
									{
										modKeyboard.lastDelayOut = modKeyboard.delayOut;
									}
									modKeyboard.delayKey = true;
									modKeyboard.delayTimer = 5;
									modKeyboard.delayOut = TextUtil.ToStr(modKeyboard.Layout[modKeyboard.curLayout].Key[Hookstruct.vkCode][0]);
									if (string.Equals(text9, "MoveL", System.StringComparison.Ordinal))
									{
										modKeyboard.Que = TextUtil.Left(modKeyboard.Que, modKeyboard.Que.Length - 1) + modKeyboard.delayOut + TextUtil.Right(modKeyboard.Que, 1);
										modKeyboard.Que = TextUtil.Right(modKeyboard.Que, 20);
									}
									else
									{
										modKeyboard.ReQue(modKeyboard.delayOut);
									}
									flag2 = true;
								}
								else
								{
									string text10 = TextUtil.ToStr(modKeyboard.Layout[modKeyboard.curLayout].Key[Hookstruct.vkCode][0]);
									if (!flag3)
									{
										if (string.Equals(text9, "MoveL", System.StringComparison.Ordinal))
										{
											modKeyboard.Que = TextUtil.Left(modKeyboard.Que, modKeyboard.Que.Length - 1) + text10 + TextUtil.Right(modKeyboard.Que, 1);
											modKeyboard.Que = TextUtil.Right(modKeyboard.Que, 20);
											modKeyboard.SendKey(text10);
											modKeyboard.lastClusterLen = 1;
											modKeyboard.LastSentChar = text10;
										}
										else if (modKeyboard.TryPreemptJuk(text10))
										{
											modKeyboard.ConjunctPending = false;
											modKeyboard.ConjunctVowel = "";
											modKeyboard.LastSentChar = "";
											modKeyboard.delayKey = false;
											modKeyboard.delayTimer = 0;
											modKeyboard.delayOut = "";
										}
										else
										{
											modKeyboard.ReQue(text10);
											modKeyboard.SendKey(text10);
											modKeyboard.lastClusterLen = 1;
											modKeyboard.LastSentChar = text10;
										}
									}
									if (modKeyboard.delayTimer > 0)
									{
										modKeyboard.delayTimer--;
									}
									flag2 = true;
								}
								if (string.Equals(text9, "MoveL", System.StringComparison.Ordinal))
								{
									modKeyboard.Pass = true;
									modKeyboard.DoKeyBoardA((NativeMethods.KEYEVENTF)0, 39);
									modKeyboard.DoKeyBoardA(NativeMethods.KEYEVENTF.KEYUP, 39);
									modKeyboard.Pass = false;
								}
								if (string.Equals(text9, "Xtra", System.StringComparison.Ordinal))
								{
									modKeyboard.ReQue("‌");
								}
							}
							if (modKeyboard.Layout[modKeyboard.curLayout].Juk.Count > 0)
							{
								int num9 = 5;
								do
								{
									string text11 = TextUtil.Right(modKeyboard.Que, num9);
									int length2 = text11.Length;
									if (modKeyboard.Layout[modKeyboard.curLayout].Juk.ContainsKey(text11))
									{
										if (modKeyboard.delayTimer == 5)
										{
											num9--;
										}
										if (modKeyboard.delayTimer == 3)
										{
											modKeyboard.lastDelayOut = "";
											modKeyboard.delayOut = "";
										}
										if (text11.Contains("‌"))
										{
											num9--;
										}
										if (text11 == "র‌ে্য" || text11 == "র‌ৈ্য")
										{
											num9--;
										}
										Thread.Sleep(10);
										int num10 = num9;
										for (int n = 1; n <= num10; n++)
										{
											modKeyboard.Pass = true;
											modKeyboard.DoKeyBoardA(NativeMethods.KEYEVENTF.UNICODE, 8);
											modKeyboard.DoKeyBoardA(NativeMethods.KEYEVENTF.KEYUP, 8);
											modKeyboard.Pass = false;
										}
										text11 = modKeyboard.Layout[modKeyboard.curLayout].Juk[text11];
										if (!text11.EndsWith(modKeyboard.lastDelayOut))
										{
											if (modKeyboard.delayKey & modKeyboard.lastDelayOut.Length > 0 & modKeyboard.delayTimer == 5)
											{
												modKeyboard.delayKey = false;
												modKeyboard.SendKey(modKeyboard.lastDelayOut);
												modKeyboard.lastDelayOut = "";
											}
											else if (modKeyboard.delayKey & modKeyboard.delayTimer == 4)
											{
												modKeyboard.delayKey = false;
												modKeyboard.SendKey(modKeyboard.delayOut);
												modKeyboard.delayOut = "";
											}
										}
										else
										{
											modKeyboard.lastDelayOut = "";
											if (modKeyboard.delayTimer == 4 && !text11.Contains(modKeyboard.delayOut))
											{
												modKeyboard.SendKey(modKeyboard.delayOut);
												modKeyboard.delayOut = "";
											}
										}
										if (modKeyboard.delayTimer == 5 & modKeyboard.delayKey)
										{
											modKeyboard.delayKey = false;
										}
										modKeyboard.SendKey(text11);
										modKeyboard.Que = TextUtil.Left(modKeyboard.Que, modKeyboard.Que.Length - length2) + text11;
										modKeyboard.Que = TextUtil.Right(modKeyboard.Que, 20);
										modKeyboard.delayKey = false;
										modKeyboard.delayTimer = 0;
										modKeyboard.ConjunctPending = false;
										modKeyboard.ConjunctVowel = "";
										modKeyboard.LastSentChar = "";
									}
									num9 += -1;
								}
								while (num9 >= 2);
							}
							if (modKeyboard.delayKey && modKeyboard.delayTimer == 4)
							{
								if (modKeyboard.ConjunctPending && modKeyboard.LastSentChar.Length > 0)
								{
									modKeyboard.Pass = true;
									for (int num11 = 0; num11 < 3; num11++)
									{
										modKeyboard.DoKeyBoardA((NativeMethods.KEYEVENTF)0, 8);
										modKeyboard.DoKeyBoardA(NativeMethods.KEYEVENTF.KEYUP, 8);
									}
									modKeyboard.Pass = false;
									string text12 = (modKeyboard.LastSentChar.StartsWith("্") ? "" : "্") + modKeyboard.LastSentChar + modKeyboard.ConjunctVowel;
									modKeyboard.SendKey(text12);
									modKeyboard.Que = TextUtil.Left(modKeyboard.Que, modKeyboard.Que.Length - 3) + text12;
									modKeyboard.Que = TextUtil.Right(modKeyboard.Que, 20);
									modKeyboard.lastClusterLen = text12.Length;
									modKeyboard.delayKey = false;
									modKeyboard.delayTimer = 4;
									modKeyboard.delayOut = modKeyboard.ConjunctVowel;
									modKeyboard.lastDelayOut = "";
									modKeyboard.ConjunctPending = false;
									modKeyboard.ConjunctVowel = "";
									modKeyboard.LastSentChar = "";
								}
								else
								{
									modKeyboard.ReQue(modKeyboard.delayOut);
									modKeyboard.SendKey(modKeyboard.delayOut);
									modKeyboard.lastClusterLen = 2;
									modKeyboard.delayKey = false;
								}
							}
							if (flag2)
							{
								return true;
							}
						}
						result = false;
					}
				}
				catch (Exception)
				{
				}
				return result;
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00011B8C File Offset: 0x0000FD8C
		private static void SendKey(string Text)
		{
			int length = Text.Length;
			checked
			{
				for (int i = 1; i <= length; i++)
				{
					modKeyboard.SendKeyByte((short)Text[i - 1]);
				}
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00003054 File Offset: 0x00001254
		private static void SendKeyByte(short KeyCode)
		{
			modKeyboard.Pass = true;
			modKeyboard.DoKeyBoard(NativeMethods.KEYEVENTF.UNICODE, (int)KeyCode);
			modKeyboard.DoKeyBoard(NativeMethods.KEYEVENTF.KEYUP, (int)KeyCode);
			modKeyboard.Pass = false;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00003070 File Offset: 0x00001270
		public static int KeyboardCallback(int Code, int wParam, ref modKeyboard.KBDLLHOOKSTRUCT lParam)
		{
			if ((Code == 0 & (wParam == 256 | wParam == 260) & !modKeyboard.Pass) && modKeyboard.IsHooked(ref lParam))
			{
				return 1;
			}
			return modKeyboard.CallNextHookEx(modKeyboard.KeyboardHandle, Code, wParam, lParam);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00011BC0 File Offset: 0x0000FDC0
		public static bool HookKeyboard()
		{
			modKeyboard.callback = new modKeyboard.KeyboardHookDelegate(modKeyboard.KeyboardCallback);
			modKeyboard.KeyboardHandle = modKeyboard.SetWindowsHookEx(13, modKeyboard.callback, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]).ToInt32(), 0);
			modKeyboard.CheckHooked();
			return modKeyboard.KeyboardHandle != 0;
		}

		// v2: Windows silently removes a low-level hook that ever times out (no notification).
		// Called from the UI thread after resume/unlock to install a fresh hook.
		public static void RehookKeyboard()
		{
			try
			{
				if (modKeyboard.KeyboardHandle != 0)
				{
					modKeyboard.UnhookWindowsHookEx(modKeyboard.KeyboardHandle);
					modKeyboard.KeyboardHandle = 0;
				}
			}
			catch (Exception)
			{
			}
			modKeyboard.ResetTypingState();
			modKeyboard.HookKeyboard();
		}

		// v2: clears any half-finished composition so a fresh hook starts clean.
		private static void ResetTypingState()
		{
			modKeyboard.Pass = false;
			modKeyboard.delayKey = false;
			modKeyboard.delayTimer = 0;
			modKeyboard.delayOut = "";
			modKeyboard.lastDelayOut = "";
			modKeyboard.flagLeft = false;
			modKeyboard.refQueLen = 0;
			modKeyboard.refScreenText = "";
			modKeyboard.lastClusterLen = 1;
			modKeyboard.ConjunctPending = false;
			modKeyboard.ConjunctVowel = "";
			modKeyboard.LastSentChar = "";
			modKeyboard.AvroPrevOutput = 0;
			modKeyboard.AvroPrevOutputStr = "";
			modKeyboard.Que = new string('\u2660', 20);
			AvroEngine.ClearBuffer();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000030AF File Offset: 0x000012AF
		public static void CheckHooked()
		{
			TextUtil.ToBool(modKeyboard.Hooked());
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000030BC File Offset: 0x000012BC
		private static object Hooked()
		{
			return modKeyboard.KeyboardHandle != 0;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000030CB File Offset: 0x000012CB
		public static void UnhookKeyboard()
		{
			if (TextUtil.ToBool(modKeyboard.Hooked()))
			{
				modKeyboard.UnhookWindowsHookEx(modKeyboard.KeyboardHandle);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00011C10 File Offset: 0x0000FE10
		public static char VkToChar(int vk, bool shift)
		{
			if (vk >= 65 && vk <= 90)
			{
				char c = (char)vk;
				if (!shift)
				{
					return char.ToLower(c);
				}
				return c;
			}
			else if (vk >= 48 && vk <= 57)
			{
				if (!shift)
				{
					return (char)vk;
				}
				return (new char[]
				{
					')',
					'!',
					'@',
					'#',
					'$',
					'%',
					'^',
					'&',
					'*',
					'('
				})[vk - 48];
			}
			else
			{
				switch (vk)
				{
				case 186:
					if (!shift)
					{
						return ';';
					}
					return ':';
				case 187:
					if (!shift)
					{
						return '=';
					}
					return '+';
				case 188:
					if (!shift)
					{
						return ',';
					}
					return '<';
				case 189:
					if (!shift)
					{
						return '-';
					}
					return '_';
				case 190:
					if (!shift)
					{
						return '.';
					}
					return '>';
				case 191:
					if (!shift)
					{
						return '/';
					}
					return '?';
				case 192:
					if (!shift)
					{
						return '`';
					}
					return '~';
				default:
					if (vk != 222)
					{
						return '\0';
					}
					if (!shift)
					{
						return '\'';
					}
					return '"';
				}
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00011CDC File Offset: 0x0000FEDC
		static modKeyboard()
		{
			modKeyboard.lastDelayOut = "";
			modKeyboard.lastClusterLen = 1;
			modKeyboard.ConjunctVowel = "";
			modKeyboard.LastSentChar = "";
			modKeyboard.AvroMode = false;
			modKeyboard.AvroPrevOutput = 0;
			modKeyboard.AvroPrevOutputStr = "";
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00011D34 File Offset: 0x0000FF34
		private static bool TryPreemptJuk(string nextChar)
		{
			if (modKeyboard.Layout[modKeyboard.curLayout].Juk.Count == 0)
			{
				return false;
			}
			for (int i = 4; i >= 1; i--)
			{
				if (i <= modKeyboard.Que.Length)
				{
					string key = TextUtil.Right(modKeyboard.Que, i) + nextChar;
					if (modKeyboard.Layout[modKeyboard.curLayout].Juk.ContainsKey(key))
					{
						modKeyboard.Pass = true;
						for (int j = 0; j < i; j++)
						{
							modKeyboard.DoKeyBoardA((NativeMethods.KEYEVENTF)0, 8);
							modKeyboard.DoKeyBoardA(NativeMethods.KEYEVENTF.KEYUP, 8);
						}
						modKeyboard.Pass = false;
						string text = modKeyboard.Layout[modKeyboard.curLayout].Juk[key];
						modKeyboard.Que = TextUtil.Left(modKeyboard.Que, modKeyboard.Que.Length - i) + text;
						modKeyboard.Que = TextUtil.Right(modKeyboard.Que, 20);
						modKeyboard.SendKey(text);
						modKeyboard.lastClusterLen = text.Length;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000030E4 File Offset: 0x000012E4
		private static void UnQue()
		{
			if (modKeyboard.Que.Length > 0)
			{
				modKeyboard.Que = "♠" + TextUtil.Left(modKeyboard.Que, modKeyboard.Que.Length - 1);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00003118 File Offset: 0x00001318
		private static bool IsBnConsonant(char c)
		{
			return (c >= 'ক' && c <= 'হ') || (c >= 'ড়' && c <= 'য়') || c == 'ৰ' || c == 'ৱ';
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000314C File Offset: 0x0000134C
		private static bool IsBnBase(char c)
		{
			return modKeyboard.IsBnConsonant(c) || (c >= 'অ' && c <= 'ঔ') || c == 'ৎ';
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00011E2C File Offset: 0x0001002C
		private static bool IsBnSign(char c)
		{
			return (c >= 'া' && c <= 'ৄ') || c == 'ে' || c == 'ৈ' || c == 'ো' || c == 'ৌ' || c == 'ৗ' || c == '্' || c == '়' || c == 'ৢ' || c == 'ৣ' || (c >= 'ঁ' && c <= 'ঃ');
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00011EA8 File Offset: 0x000100A8
		private static bool FindLastCluster(out int queLen, out string screenText)
		{
			queLen = 0;
			screenText = "";
			string que = modKeyboard.Que;
			if (string.IsNullOrEmpty(que))
			{
				return false;
			}
			int num = que.Length;
			if (num >= 2 && que[num - 1] == '‌' && que[num - 2] == 'র')
			{
				num--;
			}
			while (num > 0 && modKeyboard.IsBnSign(que[num - 1]))
			{
				num--;
			}
			if (num == 0 || !modKeyboard.IsBnBase(que[num - 1]))
			{
				return false;
			}
			num--;
			while (num > 0 && que[num - 1] == '্')
			{
				int num2 = num - 2;
				if (num2 >= 0 && (que[num2] == '‌' || que[num2] == '‍'))
				{
					num2--;
				}
				if (num2 < 0 || !modKeyboard.IsBnConsonant(que[num2]))
				{
					break;
				}
				num = num2;
			}
			string text = que.Substring(num);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] != '‌' || i <= 0 || text[i - 1] != 'র')
				{
					stringBuilder.Append(text[i]);
				}
			}
			screenText = stringBuilder.ToString();
			queLen = text.Length;
			return screenText.Length > 0;
		}

		// Token: 0x040000E2 RID: 226
		public static clsLayouts[] Layout;

		// Token: 0x040000E3 RID: 227
		public static int LayoutNumber;

		// Token: 0x040000E4 RID: 228
		public static string Que;

		// Token: 0x040000E5 RID: 229
		public static int curLayout;

		// Token: 0x040000E6 RID: 230
		public static int defLayout;

		// Token: 0x040000E7 RID: 231
		public static int AM;

		// Token: 0x040000E8 RID: 232
		private static bool delayKey;

		// Token: 0x040000E9 RID: 233
		private static int delayTimer;

		// Token: 0x040000EA RID: 234
		private static string delayOut;

		// Token: 0x040000EB RID: 235
		private static string lastDelayOut;

		// Token: 0x040000EC RID: 236
		private static bool flagLeft;

		// Token: 0x040000ED RID: 237
		public const int KEYEVENTF_EXTENDEDKEY = 1;

		// Token: 0x040000EE RID: 238
		public const int KEYEVENTF_KEYUP = 2;

		// Token: 0x040000EF RID: 239
		public const int KEYEVENTF_UNICODE = 4;

		// Token: 0x040000F0 RID: 240
		public const int KEYEVENTF_SCANCODE = 8;

		// Token: 0x040000F1 RID: 241
		public const int WM_SETTEXT = 12;

		// Token: 0x040000F2 RID: 242
		public static bool Pass;

		// Token: 0x040000F3 RID: 243
		private const int HC_ACTION = 0;

		// Token: 0x040000F4 RID: 244
		private const int LLKHF_EXTENDED = 1;

		// Token: 0x040000F5 RID: 245
		private const int LLKHF_INJECTED = 16;

		// Token: 0x040000F6 RID: 246
		private const int LLKHF_ALTDOWN = 32;

		// Token: 0x040000F7 RID: 247
		private const int LLKHF_UP = 128;

		// Token: 0x040000F8 RID: 248
		private const int WM_KEYUP = 257;

		// Token: 0x040000F9 RID: 249
		private const int WM_KEYDOWN = 256;

		// Token: 0x040000FA RID: 250
		public const int VK_TAB = 9;

		// Token: 0x040000FB RID: 251
		public const int VK_CONTROL = 17;

		// Token: 0x040000FC RID: 252
		public const int VK_ESCAPE = 27;

		// Token: 0x040000FD RID: 253
		public const int VK_DELETE = 46;

		// Token: 0x040000FE RID: 254
		private const int WH_KEYBOARD_LL = 13;

		// Token: 0x040000FF RID: 255
		public static int KeyboardHandle;

		// Token: 0x04000100 RID: 256
		[MarshalAs(UnmanagedType.FunctionPtr)]
		private static modKeyboard.KeyboardHookDelegate callback;

		// Token: 0x04000101 RID: 257
		public static bool AvroMode;

		// Token: 0x04000102 RID: 258
		public static int AvroPrevOutput;

		// Token: 0x04000103 RID: 259
		public static string AvroPrevOutputStr;

		// Token: 0x04000104 RID: 260
		private static int lastClusterLen;

		// Token: 0x04000105 RID: 261
		public static bool ConjunctPending;

		// Token: 0x04000106 RID: 262
		public static string ConjunctVowel;

		// Token: 0x04000107 RID: 263
		public static string LastSentChar;

		// Token: 0x04000108 RID: 264
		private static int refQueLen = 0;

		// Token: 0x04000109 RID: 265
		private static string refScreenText = "";

		// Token: 0x0200000E RID: 14
		public struct KBDLLHOOKSTRUCT
		{
			// Token: 0x0400010A RID: 266
			public int vkCode;

			// Token: 0x0400010B RID: 267
			public int scanCode;

			// Token: 0x0400010C RID: 268
			public int flags;

			// Token: 0x0400010D RID: 269
			public int time;

			// Token: 0x0400010E RID: 270
			public int dwExtraInfo;
		}

		// Token: 0x0200000F RID: 15
		// (Invoke) Token: 0x0600020D RID: 525
		public delegate int KeyboardHookDelegate(int Code, int wParam, ref modKeyboard.KBDLLHOOKSTRUCT lParam);
	}
}
