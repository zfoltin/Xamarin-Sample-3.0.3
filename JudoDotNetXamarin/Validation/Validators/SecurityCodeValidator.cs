using System.Collections.Generic;

namespace JudoDotNetXamarin
{
	public class SecurityCodeValidator : IValidator
	{
		public ValidationResponse Validate(string entry, ICardNetwork cardNetwork, List<CardNetwork> acceptableCardNetworksForTransaction)
		{
			int i;
			entry = entry ?? string.Empty;
			var valid = int.TryParse(entry, out i) && entry.Length == cardNetwork.GetSecurityCodeLength();
			var errorMessage = string.Empty;

			if (!valid && entry.Length >= cardNetwork.GetSecurityCodeLength())
			{
				errorMessage = "Check security code";
			}

			return new ValidationResponse(valid, errorMessage, !string.IsNullOrWhiteSpace(errorMessage));
		}
	}
}
