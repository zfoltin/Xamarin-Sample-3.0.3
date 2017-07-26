using System;

namespace JudoDotNetXamarin
{
	public class USACountry : ICountry
	{
		public string GetPostcodeTitle()
		{
			return "Billing zip code";
		}

		public string GetPostcodeRegex()
		{
			return @"(^\d{5}$)";
		}

		public ICurrency GetCurrency()
		{
			return new USDollarCurrency();
		}

		public int? GetISOCode()
		{
			return 840;
		}

		public int GetPostcodeLength()
		{
			return 5;
		}

		public bool IsPostcodeNumeric()
		{
			return true;
		}

		public Country GetName()
		{
			return Country.USA;
		}

		public bool IsPostcodeRequired()
		{
			return true;
		}
	}
}
