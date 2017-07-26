using System;
namespace JudoDotNetXamarin
{
	public class OtherCountry : ICountry
	{
		public string GetPostcodeTitle()
		{
			return "Billing postcode";
		}

		public string GetPostcodeRegex()
		{
			return @"^[a-zA-Z0-9]{0,8}$";
		}

		public ICurrency GetCurrency()
		{
			return new UnknownCurrency();
		}

		public int? GetISOCode()
		{
			return null;
		}

		public int GetPostcodeLength()
		{
			return 8;
		}

		public bool IsPostcodeNumeric()
		{
			return false;
		}

		public Country GetName()
		{
			return Country.Other;
		}

		public bool IsPostcodeRequired()
		{
			return false;
		}
	}
}
