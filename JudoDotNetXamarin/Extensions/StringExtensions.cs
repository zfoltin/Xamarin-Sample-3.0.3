using System;
using System.Text;

namespace JudoDotNetXamarin
{
	public static class StringExtensions
	{
		public static string FormatToJudoString(this string text, string format)
		{
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(format))
			{
				return text;
			}

			var result = new StringBuilder();

			foreach (char character in text)
			{
				if (char.IsDigit(character) || character == '*')
				{
					result.Append(character);
				}
			}

			for (var i = 0; i < format.Length; i++)
			{
				if (!char.IsDigit(format[i]) && i < result.Length + 1)
				{
					result.Insert(i, format[i]);
				}
			}

			return result.ToString();
		}
	}
}
