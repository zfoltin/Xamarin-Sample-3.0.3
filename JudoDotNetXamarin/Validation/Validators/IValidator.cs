using System.Collections.Generic;

namespace JudoDotNetXamarin
{
	public interface IValidator
	{
		ValidationResponse Validate(string entry, ICardNetwork cardNetwork, List<CardNetwork> acceptableCardNetworksForTransaction);
	}        
}

