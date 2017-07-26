using System.Collections.Generic;

namespace JudoDotNetXamarin
{
	public class IssueNumberValidator : IValidator
	{
		public ValidationResponse Validate(string entry, ICardNetwork cardNetwork, List<CardNetwork> acceptableCardNetworksForTransaction)
		{
			int i;
			entry = entry ?? string.Empty;
			var valid = int.TryParse(entry, out i) && entry.Length > 0 && entry.Length <= cardNetwork.GetIssueNumberLength();
			var errorMessage = !valid ? "Check issue number" : string.Empty;
			return new ValidationResponse(valid, errorMessage, !string.IsNullOrWhiteSpace(errorMessage) && entry.Length > 0);
		}
	}
	
}
