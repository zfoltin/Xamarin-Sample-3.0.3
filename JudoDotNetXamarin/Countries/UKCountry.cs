using System;
namespace JudoDotNetXamarin
{
	public class UKCountry : ICountry
	{
		public string GetPostcodeTitle()
		{
			return "Billing postcode";
		}

		public string GetPostcodeRegex()
		{
			return @"^(GIR ?0AA|[A-PR-UWYZ]([0-9]{1,2}|([A-HK-Y][0-9]([0-9ABEHMNPRV-Y])?)|[0-9][A-HJKPS-UW]) ?[0-9][ABD-HJLNP-UW-Z]{2})$";
		}

		public ICurrency GetCurrency()
		{
			return new BritishPoundCurrency();
		}

		public int? GetISOCode()
		{
			return 826;
		}

		public int GetPostcodeLength()
		{
			return 7;
		}

		public bool IsPostcodeNumeric()
		{
			return false;
		}

		public Country GetName()
		{
			return Country.UK;
		}

		public bool IsPostcodeRequired()
		{
			return true;
		}
	}	
}
