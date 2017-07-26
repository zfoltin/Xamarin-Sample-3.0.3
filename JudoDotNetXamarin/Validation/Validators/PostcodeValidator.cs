using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JudoDotNetXamarin
{
	public class PostcodeValidator : IValidator
	{
		private readonly ICountry _country;

		public PostcodeValidator(ICountry country)
		{
			_country = country;
		}

		public ValidationResponse Validate(string entry, ICardNetwork cardNetwork, List<CardNetwork> acceptableCardNetworksForTransaction)
		{
			var strippedEntry = (entry ?? string.Empty).Replace(" ", string.Empty);

			var regex = new Regex(_country.GetPostcodeRegex(), RegexOptions.IgnoreCase);
			var match = regex.Match(strippedEntry);

			var errorMessage = string.Empty;

			if (!match.Success && strippedEntry.Length >= _country.GetPostcodeLength())
			{
				errorMessage = string.Format("Check {0}", _country.GetPostcodeTitle());
			}

			return new ValidationResponse(match.Success, errorMessage, !string.IsNullOrWhiteSpace(errorMessage));
		}
	}
	
}
