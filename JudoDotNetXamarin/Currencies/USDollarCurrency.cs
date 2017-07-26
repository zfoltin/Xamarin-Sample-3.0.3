using System;
namespace JudoDotNetXamarin
{
	public class USDollarCurrency : ICurrency
	{
		public string GetAbbreviation()
		{
			return "USD";
		}

		public string GetSymbol()
		{
			return "$";
		}
	}
}
