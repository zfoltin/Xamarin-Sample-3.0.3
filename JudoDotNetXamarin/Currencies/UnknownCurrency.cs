using System;
namespace JudoDotNetXamarin
{

	public class UnknownCurrency : ICurrency
	{
		public string GetAbbreviation()
		{
			return string.Empty;
		}

		public string GetSymbol()
		{
			return string.Empty;
		}
	}
}
