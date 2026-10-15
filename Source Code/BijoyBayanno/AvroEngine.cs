using System;
using System.Text;

namespace BijoyBayanno
{
	// Token: 0x02000016 RID: 22
	public static class AvroEngine
	{
		// Token: 0x06000236 RID: 566 RVA: 0x000124B4 File Offset: 0x000106B4
		private static AvroEngine.Pattern P(string find, string replace)
		{
			return new AvroEngine.Pattern
			{
				Find = find,
				Replace = replace,
				Rules = null
			};
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000124E4 File Offset: 0x000106E4
		private static AvroEngine.Pattern PR(string find, string replace, AvroEngine.Rule[] rules)
		{
			return new AvroEngine.Pattern
			{
				Find = find,
				Replace = replace,
				Rules = rules
			};
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00012514 File Offset: 0x00010714
		private static AvroEngine.Rule R(string rep, params AvroEngine.Match[] matches)
		{
			return new AvroEngine.Rule
			{
				Replace = rep,
				Matches = matches
			};
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0001253C File Offset: 0x0001073C
		private static AvroEngine.Match M(string type, string scope, string value)
		{
			bool flag = scope.StartsWith("!");
			string scope2 = flag ? scope.Substring(1) : scope;
			return new AvroEngine.Match
			{
				Type = type,
				Scope = scope2,
				Negative = flag,
				Value = value
			};
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00003319 File Offset: 0x00001519
		private static AvroEngine.Match M(string type, string scope)
		{
			return AvroEngine.M(type, scope, "");
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00003327 File Offset: 0x00001527
		public static string AppendChar(char c)
		{
			AvroEngine._buffer.Append(c);
			return AvroEngine.Parse(AvroEngine._buffer.ToString());
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0001258C File Offset: 0x0001078C
		public static string RemoveLastChar()
		{
			if (AvroEngine._buffer.Length == 0)
			{
				return "";
			}
			AvroEngine._buffer.Remove(AvroEngine._buffer.Length - 1, 1);
			if (AvroEngine._buffer.Length == 0)
			{
				return "";
			}
			return AvroEngine.Parse(AvroEngine._buffer.ToString());
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00003344 File Offset: 0x00001544
		public static string GetCurrentOutput()
		{
			if (AvroEngine._buffer.Length == 0)
			{
				return "";
			}
			return AvroEngine.Parse(AvroEngine._buffer.ToString());
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00003367 File Offset: 0x00001567
		public static void ClearBuffer()
		{
			AvroEngine._buffer.Length = 0;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000125E4 File Offset: 0x000107E4
		public static string Parse(string input)
		{
			input = input.Replace("TH", "ৎ");
			string text = AvroEngine.FixString(input);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < text.Length; i++)
			{
				int num = i;
				bool flag = false;
				AvroEngine.Pattern[] patterns = AvroEngine.Patterns;
				int j = 0;
				while (j < patterns.Length)
				{
					AvroEngine.Pattern pattern = patterns[j];
					int num2 = i + pattern.Find.Length;
					if (num2 <= text.Length && string.CompareOrdinal(text, num, pattern.Find, 0, pattern.Find.Length) == 0)
					{
						int prev = num - 1;
						if (pattern.Rules != null)
						{
							AvroEngine.Rule[] rules = pattern.Rules;
							for (int k = 0; k < rules.Length; k++)
							{
								string text2 = AvroEngine.EvalRule(rules[k], text, num, num2, prev);
								if (text2 != null)
								{
									stringBuilder.Append(text2);
									i = num2 - 1;
									flag = true;
									break;
								}
							}
						}
						if (!flag)
						{
							stringBuilder.Append(pattern.Replace);
							i = num2 - 1;
							flag = true;
							break;
						}
						break;
					}
					else
					{
						j++;
					}
				}
				if (!flag)
				{
					stringBuilder.Append(text[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0001271C File Offset: 0x0001091C
		private static string EvalRule(AvroEngine.Rule rule, string text, int start, int end, int prev)
		{
			foreach (AvroEngine.Match match in rule.Matches)
			{
				int num = (match.Type == "suffix") ? end : prev;
				string scope = match.Scope;
				if (!(scope == "punctuation"))
				{
					if (!(scope == "vowel"))
					{
						if (!(scope == "consonant"))
						{
							if (scope == "exact")
							{
								int start2;
								int end2;
								if (match.Type == "suffix")
								{
									start2 = end;
									end2 = end + match.Value.Length;
								}
								else
								{
									start2 = start - match.Value.Length;
									end2 = start;
								}
								if (!AvroEngine.IsExact(match.Value, text, start2, end2, match.Negative))
								{
									return null;
								}
							}
						}
						else if (!((match.Type == "prefix") ? (num >= 0) : (num < text.Length)))
						{
							if (!match.Negative)
							{
								return null;
							}
						}
						else if (AvroEngine.IsConsonant(text[num]) == match.Negative)
						{
							return null;
						}
					}
					else if (!((match.Type == "prefix") ? (num >= 0) : (num < text.Length)))
					{
						if (!match.Negative)
						{
							return null;
						}
					}
					else if (AvroEngine.IsVowel(text[num]) == match.Negative)
					{
						return null;
					}
				}
				else if (((match.Type == "prefix" && num < 0) || (match.Type == "suffix" && num >= text.Length) || AvroEngine.IsPunctuation(text[num])) == match.Negative)
				{
					return null;
				}
			}
			return rule.Replace;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000128E4 File Offset: 0x00010AE4
		private static string FixString(string input)
		{
			StringBuilder stringBuilder = new StringBuilder(input.Length);
			foreach (char c in input)
			{
				if (AvroEngine.IsCaseSensitive(c))
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append(char.ToLowerInvariant(c));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00003374 File Offset: 0x00001574
		private static bool IsVowel(char c)
		{
			return "aeiou".IndexOf(char.ToLowerInvariant(c)) >= 0;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000338C File Offset: 0x0000158C
		private static bool IsConsonant(char c)
		{
			return "bcdfghjklmnpqrstvwxyz".IndexOf(char.ToLowerInvariant(c)) >= 0;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000033A4 File Offset: 0x000015A4
		private static bool IsPunctuation(char c)
		{
			return !AvroEngine.IsVowel(c) && !AvroEngine.IsConsonant(c);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000033B9 File Offset: 0x000015B9
		private static bool IsCaseSensitive(char c)
		{
			return "oiudgjnrstyz".IndexOf(char.ToLowerInvariant(c)) >= 0;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000033D1 File Offset: 0x000015D1
		private static bool IsExact(string needle, string text, int start, int end, bool not)
		{
			return (start >= 0 && end <= text.Length && string.CompareOrdinal(text, start, needle, 0, needle.Length) == 0) ^ not;
		}

		// Token: 0x04000128 RID: 296
		private const string VOWELS = "aeiou";

		// Token: 0x04000129 RID: 297
		private const string CONSONANTS = "bcdfghjklmnpqrstvwxyz";

		// Token: 0x0400012A RID: 298
		private const string CASESENSITIVE = "oiudgjnrstyz";

		// Token: 0x0400012B RID: 299
		private static readonly StringBuilder _buffer = new StringBuilder(64);

		// Token: 0x0400012C RID: 300
		private static readonly AvroEngine.Pattern[] Patterns = new AvroEngine.Pattern[]
		{
			AvroEngine.P("bhl", "ভ্ল"),
			AvroEngine.P("bdh", "ব্ধ"),
			AvroEngine.P("bj", "ব্জ"),
			AvroEngine.P("bd", "ব্দ"),
			AvroEngine.P("bb", "ব্ব"),
			AvroEngine.P("bl", "ব্ল"),
			AvroEngine.P("bh", "ভ"),
			AvroEngine.P("vl", "ভ্ল"),
			AvroEngine.P("b", "ব"),
			AvroEngine.P("v", "ভ"),
			AvroEngine.P("cNG", "চ্ঞ"),
			AvroEngine.P("cch", "চ্ছ"),
			AvroEngine.P("cc", "চ্চ"),
			AvroEngine.P("ch", "ছ"),
			AvroEngine.P("c", "চ"),
			AvroEngine.P("dhn", "ধ্ন"),
			AvroEngine.P("dhm", "ধ্ম"),
			AvroEngine.P("dgh", "দ্ঘ"),
			AvroEngine.P("ddh", "দ্ধ"),
			AvroEngine.P("dbh", "দ্ভ"),
			AvroEngine.P("dv", "দ্ভ"),
			AvroEngine.P("dm", "দ্ম"),
			AvroEngine.P("DD", "ড্ড"),
			AvroEngine.P("Dh", "ঢ"),
			AvroEngine.P("dh", "ধ"),
			AvroEngine.P("dg", "দ্গ"),
			AvroEngine.P("dd", "দ্দ"),
			AvroEngine.P("D", "ড"),
			AvroEngine.P("d", "দ"),
			AvroEngine.P("...", "..."),
			AvroEngine.P(".`", "."),
			AvroEngine.P("..", "।।"),
			AvroEngine.P(".", "।"),
			AvroEngine.P("ghn", "ঘ্ন"),
			AvroEngine.P("Ghn", "ঘ্ন"),
			AvroEngine.P("gdh", "গ্ধ"),
			AvroEngine.P("Gdh", "গ্ধ"),
			AvroEngine.P("gN", "গ্ণ"),
			AvroEngine.P("GN", "গ্ণ"),
			AvroEngine.P("gn", "গ্ন"),
			AvroEngine.P("Gn", "গ্ন"),
			AvroEngine.P("gm", "গ্ম"),
			AvroEngine.P("Gm", "গ্ম"),
			AvroEngine.P("gl", "গ্ল"),
			AvroEngine.P("Gl", "গ্ল"),
			AvroEngine.P("gg", "জ্ঞ"),
			AvroEngine.P("GG", "জ্ঞ"),
			AvroEngine.P("Gg", "জ্ঞ"),
			AvroEngine.P("gG", "জ্ঞ"),
			AvroEngine.P("gh", "ঘ"),
			AvroEngine.P("Gh", "ঘ"),
			AvroEngine.P("g", "গ"),
			AvroEngine.P("G", "গ"),
			AvroEngine.P("hN", "হ্ণ"),
			AvroEngine.P("hn", "হ্ন"),
			AvroEngine.P("hm", "হ্ম"),
			AvroEngine.P("hl", "হ্ল"),
			AvroEngine.P("h", "হ"),
			AvroEngine.P("jjh", "জ্ঝ"),
			AvroEngine.P("jNG", "জ্ঞ"),
			AvroEngine.P("jj", "জ্জ"),
			AvroEngine.P("jh", "ঝ"),
			AvroEngine.P("j", "জ"),
			AvroEngine.P("J", "জ"),
			AvroEngine.P("kkhN", "ক্ষ্ণ"),
			AvroEngine.P("kShN", "ক্ষ্ণ"),
			AvroEngine.P("kkhm", "ক্ষ্ম"),
			AvroEngine.P("kShm", "ক্ষ্ম"),
			AvroEngine.P("kxN", "ক্ষ্ণ"),
			AvroEngine.P("kxm", "ক্ষ্ম"),
			AvroEngine.P("kkh", "ক্ষ"),
			AvroEngine.P("kSh", "ক্ষ"),
			AvroEngine.P("ksh", "কশ"),
			AvroEngine.P("kx", "ক্ষ"),
			AvroEngine.P("kk", "ক্ক"),
			AvroEngine.P("kT", "ক্ট"),
			AvroEngine.P("kt", "ক্ত"),
			AvroEngine.P("kl", "ক্ল"),
			AvroEngine.P("km", "ক্ম"),
			AvroEngine.P("ks", "ক্স"),
			AvroEngine.P("kh", "খ"),
			AvroEngine.P("k", "ক"),
			AvroEngine.P("lbh", "ল্ভ"),
			AvroEngine.P("ldh", "ল্ধ"),
			AvroEngine.P("lkh", "ল্খ"),
			AvroEngine.P("lgh", "ল্ঘ"),
			AvroEngine.P("lph", "ল্ফ"),
			AvroEngine.P("lk", "ল্ক"),
			AvroEngine.P("lg", "ল্গ"),
			AvroEngine.P("lT", "ল্ট"),
			AvroEngine.P("lD", "ল্ড"),
			AvroEngine.P("lp", "ল্প"),
			AvroEngine.P("lv", "ল্ভ"),
			AvroEngine.P("lm", "ল্ম"),
			AvroEngine.P("ll", "ল্ল"),
			AvroEngine.P("lb", "ল্ব"),
			AvroEngine.P("l", "ল"),
			AvroEngine.P("mth", "ম্থ"),
			AvroEngine.P("mph", "ম্ফ"),
			AvroEngine.P("mbh", "ম্ভ"),
			AvroEngine.P("mpl", "ম্প্ল"),
			AvroEngine.P("mn", "ম্ন"),
			AvroEngine.P("mp", "ম্প"),
			AvroEngine.P("mv", "ম্ভ"),
			AvroEngine.P("mm", "ম্ম"),
			AvroEngine.P("ml", "ম্ল"),
			AvroEngine.P("mb", "ম্ব"),
			AvroEngine.P("mf", "ম্ফ"),
			AvroEngine.P("m", "ম"),
			AvroEngine.P("0", "০"),
			AvroEngine.P("1", "১"),
			AvroEngine.P("2", "২"),
			AvroEngine.P("3", "৩"),
			AvroEngine.P("4", "৪"),
			AvroEngine.P("5", "৫"),
			AvroEngine.P("6", "৬"),
			AvroEngine.P("7", "৭"),
			AvroEngine.P("8", "৮"),
			AvroEngine.P("9", "৯"),
			AvroEngine.P("NgkSh", "ঙ্ক্ষ"),
			AvroEngine.P("Ngkkh", "ঙ্ক্ষ"),
			AvroEngine.P("NGch", "ঞ্ছ"),
			AvroEngine.P("Nggh", "ঙ্ঘ"),
			AvroEngine.P("Ngkh", "ঙ্খ"),
			AvroEngine.P("NGjh", "ঞ্ঝ"),
			AvroEngine.P("ngOU", "ঙ্গৌ"),
			AvroEngine.P("ngOI", "ঙ্গৈ"),
			AvroEngine.P("Ngkx", "ঙ্ক্ষ"),
			AvroEngine.P("NGc", "ঞ্চ"),
			AvroEngine.P("nch", "ঞ্ছ"),
			AvroEngine.P("njh", "ঞ্ঝ"),
			AvroEngine.P("ngh", "ঙ্ঘ"),
			AvroEngine.P("Ngk", "ঙ্ক"),
			AvroEngine.P("Ngx", "ঙ্ষ"),
			AvroEngine.P("Ngg", "ঙ্গ"),
			AvroEngine.P("Ngm", "ঙ্ম"),
			AvroEngine.P("NGj", "ঞ্জ"),
			AvroEngine.P("ndh", "ন্ধ"),
			AvroEngine.P("nTh", "ন্ঠ"),
			AvroEngine.P("NTh", "ণ্ঠ"),
			AvroEngine.P("nth", "ন্থ"),
			AvroEngine.P("nkh", "ঙ্খ"),
			AvroEngine.P("ngo", "ঙ্গ"),
			AvroEngine.P("nga", "ঙ্গা"),
			AvroEngine.P("ngi", "ঙ্গি"),
			AvroEngine.P("ngI", "ঙ্গী"),
			AvroEngine.P("ngu", "ঙ্গু"),
			AvroEngine.P("ngU", "ঙ্গূ"),
			AvroEngine.P("nge", "ঙ্গে"),
			AvroEngine.P("ngO", "ঙ্গো"),
			AvroEngine.P("NDh", "ণ্ঢ"),
			AvroEngine.P("nsh", "ন্শ"),
			AvroEngine.P("Ngr", "ঙর"),
			AvroEngine.P("NGr", "ঞর"),
			AvroEngine.P("ngr", "ংর"),
			AvroEngine.P("nj", "ঞ্জ"),
			AvroEngine.P("Ng", "ঙ"),
			AvroEngine.P("NG", "ঞ"),
			AvroEngine.P("nk", "ঙ্ক"),
			AvroEngine.P("ng", "ং"),
			AvroEngine.P("nn", "ন্ন"),
			AvroEngine.P("NN", "ণ্ণ"),
			AvroEngine.P("Nn", "ণ্ন"),
			AvroEngine.P("nm", "ন্ম"),
			AvroEngine.P("Nm", "ণ্ম"),
			AvroEngine.P("nd", "ন্দ"),
			AvroEngine.P("nT", "ন্ট"),
			AvroEngine.P("NT", "ণ্ট"),
			AvroEngine.P("nD", "ন্ড"),
			AvroEngine.P("ND", "ণ্ড"),
			AvroEngine.P("nt", "ন্ত"),
			AvroEngine.P("ns", "ন্স"),
			AvroEngine.P("nc", "ঞ্চ"),
			AvroEngine.P("n", "ন"),
			AvroEngine.P("N", "ণ"),
			AvroEngine.PR("OI`", "ৈ", null),
			AvroEngine.PR("OU`", "ৌ", null),
			AvroEngine.PR("O`", "ো", null),
			AvroEngine.PR("OI", "ৈ", new AvroEngine.Rule[]
			{
				AvroEngine.R("ঐ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "!consonant")
				}),
				AvroEngine.R("ঐ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation")
				})
			}),
			AvroEngine.PR("OU", "ৌ", new AvroEngine.Rule[]
			{
				AvroEngine.R("ঔ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "!consonant")
				}),
				AvroEngine.R("ঔ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation")
				})
			}),
			AvroEngine.PR("O", "ো", new AvroEngine.Rule[]
			{
				AvroEngine.R("ও", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "!consonant")
				}),
				AvroEngine.R("ও", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation")
				})
			}),
			AvroEngine.P("phl", "ফ্ল"),
			AvroEngine.P("pT", "প্ট"),
			AvroEngine.P("pt", "প্ত"),
			AvroEngine.P("pn", "প্ন"),
			AvroEngine.P("pp", "প্প"),
			AvroEngine.P("pl", "প্ল"),
			AvroEngine.P("ps", "প্স"),
			AvroEngine.P("ph", "ফ"),
			AvroEngine.P("fl", "ফ্ল"),
			AvroEngine.P("f", "ফ"),
			AvroEngine.P("p", "প"),
			AvroEngine.PR("rri`", "ৃ", null),
			AvroEngine.PR("rri", "ৃ", new AvroEngine.Rule[]
			{
				AvroEngine.R("ঋ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "!consonant")
				}),
				AvroEngine.R("ঋ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation")
				})
			}),
			AvroEngine.P("rrZ", "রর‍্য"),
			AvroEngine.P("rry", "রর‍্য"),
			AvroEngine.PR("rZ", "র‍্য", new AvroEngine.Rule[]
			{
				AvroEngine.R("্র্য", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "consonant"),
					AvroEngine.M("prefix", "!exact", "r"),
					AvroEngine.M("prefix", "!exact", "y"),
					AvroEngine.M("prefix", "!exact", "w"),
					AvroEngine.M("prefix", "!exact", "x")
				})
			}),
			AvroEngine.PR("ry", "র‍্য", new AvroEngine.Rule[]
			{
				AvroEngine.R("্র্য", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "consonant"),
					AvroEngine.M("prefix", "!exact", "r"),
					AvroEngine.M("prefix", "!exact", "y"),
					AvroEngine.M("prefix", "!exact", "w"),
					AvroEngine.M("prefix", "!exact", "x")
				})
			}),
			AvroEngine.PR("rr", "রর", new AvroEngine.Rule[]
			{
				AvroEngine.R("র্", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "!consonant"),
					AvroEngine.M("suffix", "!vowel"),
					AvroEngine.M("suffix", "!exact", "r"),
					AvroEngine.M("suffix", "!punctuation")
				}),
				AvroEngine.R("্রর", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "consonant"),
					AvroEngine.M("prefix", "!exact", "r")
				})
			}),
			AvroEngine.P("Rg", "ড়্গ"),
			AvroEngine.P("Rh", "ঢ়"),
			AvroEngine.P("R", "ড়"),
			AvroEngine.PR("r", "র", new AvroEngine.Rule[]
			{
				AvroEngine.R("্র", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "consonant"),
					AvroEngine.M("prefix", "!exact", "r"),
					AvroEngine.M("prefix", "!exact", "y"),
					AvroEngine.M("prefix", "!exact", "w"),
					AvroEngine.M("prefix", "!exact", "x"),
					AvroEngine.M("prefix", "!exact", "Z")
				})
			}),
			AvroEngine.P("shch", "শ্ছ"),
			AvroEngine.P("ShTh", "ষ্ঠ"),
			AvroEngine.P("Shph", "ষ্ফ"),
			AvroEngine.P("Sch", "শ্ছ"),
			AvroEngine.P("skl", "স্ক্ল"),
			AvroEngine.P("skh", "স্খ"),
			AvroEngine.P("sth", "স্থ"),
			AvroEngine.P("sph", "স্ফ"),
			AvroEngine.P("shc", "শ্চ"),
			AvroEngine.P("sht", "শ্ত"),
			AvroEngine.P("shn", "শ্ন"),
			AvroEngine.P("shm", "শ্ম"),
			AvroEngine.P("shl", "শ্ল"),
			AvroEngine.P("Shk", "ষ্ক"),
			AvroEngine.P("ShT", "ষ্ট"),
			AvroEngine.P("ShN", "ষ্ণ"),
			AvroEngine.P("Shp", "ষ্প"),
			AvroEngine.P("Shf", "ষ্ফ"),
			AvroEngine.P("Shm", "ষ্ম"),
			AvroEngine.P("spl", "স্প্ল"),
			AvroEngine.P("sk", "স্ক"),
			AvroEngine.P("Sc", "শ্চ"),
			AvroEngine.P("sT", "স্ট"),
			AvroEngine.P("st", "স্ত"),
			AvroEngine.P("sn", "স্ন"),
			AvroEngine.P("sp", "স্প"),
			AvroEngine.P("sf", "স্ফ"),
			AvroEngine.P("sm", "স্ম"),
			AvroEngine.P("sl", "স্ল"),
			AvroEngine.P("sh", "শ"),
			AvroEngine.P("St", "শ্ত"),
			AvroEngine.P("Sn", "শ্ন"),
			AvroEngine.P("Sm", "শ্ম"),
			AvroEngine.P("Sl", "শ্ল"),
			AvroEngine.P("Sh", "ষ"),
			AvroEngine.P("s", "স"),
			AvroEngine.P("S", "শ"),
			AvroEngine.PR("oo`", "ু", null),
			AvroEngine.PR("oo", "ু", new AvroEngine.Rule[]
			{
				AvroEngine.R("উ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "!consonant"),
					AvroEngine.M("suffix", "!exact", "`")
				}),
				AvroEngine.R("উ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation"),
					AvroEngine.M("suffix", "!exact", "`")
				})
			}),
			AvroEngine.PR("o`", "", null),
			AvroEngine.P("oZ", "অ্য"),
			AvroEngine.PR("o", "", new AvroEngine.Rule[]
			{
				AvroEngine.R("ও", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "vowel"),
					AvroEngine.M("prefix", "!exact", "o")
				}),
				AvroEngine.R("অ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "vowel"),
					AvroEngine.M("prefix", "exact", "o")
				}),
				AvroEngine.R("অ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation")
				})
			}),
			AvroEngine.P("tth", "ত্থ"),
			AvroEngine.P("t``", "ৎ"),
			AvroEngine.P("TT", "ট্ট"),
			AvroEngine.P("Tm", "ট্ম"),
			AvroEngine.P("Th", "ঠ"),
			AvroEngine.P("tn", "ত্ন"),
			AvroEngine.P("tm", "ত্ম"),
			AvroEngine.P("th", "থ"),
			AvroEngine.P("tt", "ত্ত"),
			AvroEngine.P("T", "ট"),
			AvroEngine.P("t", "ত"),
			AvroEngine.P("aZ", "অ্যা"),
			AvroEngine.P("AZ", "অ্যা"),
			AvroEngine.PR("a`", "া", null),
			AvroEngine.PR("A`", "া", null),
			AvroEngine.PR("a", "া", new AvroEngine.Rule[]
			{
				AvroEngine.R("আ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation"),
					AvroEngine.M("suffix", "!exact", "`")
				}),
				AvroEngine.R("য়া", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "!consonant"),
					AvroEngine.M("prefix", "!exact", "a"),
					AvroEngine.M("suffix", "!exact", "`")
				}),
				AvroEngine.R("আ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "exact", "a"),
					AvroEngine.M("suffix", "!exact", "`")
				})
			}),
			AvroEngine.PR("i`", "ি", null),
			AvroEngine.PR("i", "ি", new AvroEngine.Rule[]
			{
				AvroEngine.R("ই", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "!consonant"),
					AvroEngine.M("suffix", "!exact", "`")
				}),
				AvroEngine.R("ই", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation"),
					AvroEngine.M("suffix", "!exact", "`")
				})
			}),
			AvroEngine.PR("I`", "ী", null),
			AvroEngine.PR("I", "ী", new AvroEngine.Rule[]
			{
				AvroEngine.R("ঈ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "!consonant"),
					AvroEngine.M("suffix", "!exact", "`")
				}),
				AvroEngine.R("ঈ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation"),
					AvroEngine.M("suffix", "!exact", "`")
				})
			}),
			AvroEngine.PR("u`", "ু", null),
			AvroEngine.PR("u", "ু", new AvroEngine.Rule[]
			{
				AvroEngine.R("উ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "!consonant"),
					AvroEngine.M("suffix", "!exact", "`")
				}),
				AvroEngine.R("উ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation"),
					AvroEngine.M("suffix", "!exact", "`")
				})
			}),
			AvroEngine.PR("U`", "ূ", null),
			AvroEngine.PR("U", "ূ", new AvroEngine.Rule[]
			{
				AvroEngine.R("ঊ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "!consonant"),
					AvroEngine.M("suffix", "!exact", "`")
				}),
				AvroEngine.R("ঊ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation"),
					AvroEngine.M("suffix", "!exact", "`")
				})
			}),
			AvroEngine.PR("ee`", "ী", null),
			AvroEngine.PR("ee", "ী", new AvroEngine.Rule[]
			{
				AvroEngine.R("ঈ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "!consonant"),
					AvroEngine.M("suffix", "!exact", "`")
				}),
				AvroEngine.R("ঈ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation"),
					AvroEngine.M("suffix", "!exact", "`")
				})
			}),
			AvroEngine.PR("e`", "ে", null),
			AvroEngine.PR("e", "ে", new AvroEngine.Rule[]
			{
				AvroEngine.R("এ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "!consonant"),
					AvroEngine.M("suffix", "!exact", "`")
				}),
				AvroEngine.R("এ", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation"),
					AvroEngine.M("suffix", "!exact", "`")
				})
			}),
			AvroEngine.P("z", "য"),
			AvroEngine.P("Z", "্য"),
			AvroEngine.PR("y", "্য", new AvroEngine.Rule[]
			{
				AvroEngine.R("য়", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "!consonant"),
					AvroEngine.M("prefix", "!punctuation")
				}),
				AvroEngine.R("ইয়", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation")
				})
			}),
			AvroEngine.P("Y", "য়"),
			AvroEngine.P("q", "ক"),
			AvroEngine.PR("w", "ও", new AvroEngine.Rule[]
			{
				AvroEngine.R("ওয়", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation"),
					AvroEngine.M("suffix", "vowel")
				}),
				AvroEngine.R("্ব", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "consonant")
				})
			}),
			AvroEngine.PR("x", "ক্স", new AvroEngine.Rule[]
			{
				AvroEngine.R("এক্স", new AvroEngine.Match[]
				{
					AvroEngine.M("prefix", "punctuation")
				})
			}),
			AvroEngine.PR(":`", ":", null),
			AvroEngine.P("::", ":"),
			AvroEngine.P(":", "ঃ"),
			AvroEngine.PR("^`", "^", null),
			AvroEngine.P("^", "ঁ"),
			AvroEngine.P(",.", "্‌"),
			AvroEngine.P(",,", "্"),
			AvroEngine.P(",", ","),
			AvroEngine.P("$", "৳"),
			AvroEngine.PR("`", "", null)
		};

		// Token: 0x02000017 RID: 23
		private struct Match
		{
			// Token: 0x0400012D RID: 301
			public string Type;

			// Token: 0x0400012E RID: 302
			public string Scope;

			// Token: 0x0400012F RID: 303
			public bool Negative;

			// Token: 0x04000130 RID: 304
			public string Value;
		}

		// Token: 0x02000018 RID: 24
		private struct Rule
		{
			// Token: 0x04000131 RID: 305
			public AvroEngine.Match[] Matches;

			// Token: 0x04000132 RID: 306
			public string Replace;
		}

		// Token: 0x02000019 RID: 25
		private struct Pattern
		{
			// Token: 0x04000133 RID: 307
			public string Find;

			// Token: 0x04000134 RID: 308
			public string Replace;

			// Token: 0x04000135 RID: 309
			public AvroEngine.Rule[] Rules;
		}
	}
}
