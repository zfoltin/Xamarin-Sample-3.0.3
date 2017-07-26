using System;
namespace JudoDotNetXamarin
{
	public class CanadaCountry : ICountry
	{
		public string GetPostcodeTitle()
		{
			return "Billing postal code";
		}

		public string GetPostcodeRegex()
		{
			return @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";
		}

		public ICurrency GetCurrency()
		{
			return new CanadianDollar();
		}

		public int? GetISOCode()
		{
			return 124;
		}

		public int GetPostcodeLength()
		{
			return 6;
		}

		public bool IsPostcodeNumeric()
		{
			return false;
		}

		public Country GetName()
		{
			return Country.Canada;
		}

		public bool IsPostcodeRequired()
		{
			return true;
		}
	}
}
