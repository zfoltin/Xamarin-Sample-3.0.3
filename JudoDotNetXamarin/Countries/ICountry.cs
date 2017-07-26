using System;
namespace JudoDotNetXamarin
{
	public interface ICountry
	{
		Country GetName();

		int? GetISOCode();

		string GetPostcodeTitle();

		string GetPostcodeRegex();

		bool IsPostcodeNumeric();

		int GetPostcodeLength();

		ICurrency GetCurrency();

		bool IsPostcodeRequired();
	}
}