using System;
namespace JudoDotNetXamarin
{
	public class CanadianDollar : ICurrency
	{
		public string GetAbbreviation()
		{
			return "CAD";
		}

		public string GetSymbol()
		{
			return "$";
		}
	}
}
