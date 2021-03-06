

namespace Org.Apache.Hadoop.FS
{
	/// <summary>Tests for glob patterns</summary>
	public class TestGlobPattern
	{
		private void AssertMatch(bool yes, string glob, params string[] input)
		{
			GlobPattern pattern = new GlobPattern(glob);
			foreach (string s in input)
			{
				bool result = pattern.Matches(s);
				Assert.True(glob + " should" + (yes ? string.Empty : " not") + 
					" match " + s, yes ? result : !result);
			}
		}

		private void ShouldThrow(params string[] globs)
		{
			foreach (string glob in globs)
			{
				try
				{
					GlobPattern.Compile(glob);
				}
				catch (PatternSyntaxException e)
				{
					Runtime.PrintStackTrace(e);
					continue;
				}
				Assert.True("glob " + glob + " should throw", false);
			}
		}

		[Fact]
		public virtual void TestValidPatterns()
		{
			AssertMatch(true, "*", "^$", "foo", "bar");
			AssertMatch(true, "?", "?", "^", "[", "]", "$");
			AssertMatch(true, "foo*", "foo", "food", "fool");
			AssertMatch(true, "f*d", "fud", "food");
			AssertMatch(true, "*d", "good", "bad");
			AssertMatch(true, "\\*\\?\\[\\{\\\\", "*?[{\\");
			AssertMatch(true, "[]^-]", "]", "-", "^");
			AssertMatch(true, "]", "]");
			AssertMatch(true, "^.$()|+", "^.$()|+");
			AssertMatch(true, "[^^]", ".", "$", "[", "]");
			AssertMatch(false, "[^^]", "^");
			AssertMatch(true, "[!!-]", "^", "?");
			AssertMatch(false, "[!!-]", "!", "-");
			AssertMatch(true, "{[12]*,[45]*,[78]*}", "1", "2!", "4", "42", "7", "7$");
			AssertMatch(false, "{[12]*,[45]*,[78]*}", "3", "6", "9ß");
			AssertMatch(true, "}", "}");
		}

		[Fact]
		public virtual void TestInvalidPatterns()
		{
			ShouldThrow("[", "[[]]", "[][]", "{", "\\");
		}
	}
}
