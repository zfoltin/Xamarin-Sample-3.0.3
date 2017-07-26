using System;
namespace JudoDotNetXamarin
{
	public class ValidationResponse
	{ 
		public bool IsValid { get; private set; }
		public string ErrorMessage { get; private set;  }
		public bool ShouldDisplayErrorMessage { get; private set; }

		public ValidationResponse(bool isValid, string errorMessage, bool shouldDisplayErrorMessage)
		{
			IsValid = isValid;
			ErrorMessage = errorMessage;
			ShouldDisplayErrorMessage = shouldDisplayErrorMessage;
		}
	}
}
