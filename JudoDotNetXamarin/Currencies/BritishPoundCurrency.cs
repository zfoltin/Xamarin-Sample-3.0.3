using System;
namespace JudoDotNetXamarin
{
	public class BritishPoundCurrency : ICurrency
	{
		public string GetAbbreviation()
		{
			return "GBP";
		}

		public string GetSymbol()
		{
			return "£";
		}
	}
}
