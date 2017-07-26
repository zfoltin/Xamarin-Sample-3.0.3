using System;
namespace JudoDotNetXamarin
{

	public class EntryValidationResponse
	{
		public JudoEntry Entry { get; private set; }
		public CardPart CardPart { get; private set; }
		public ValidationResponse ValidationResponse { get; private set; }

		public EntryValidationResponse(JudoEntry entry, CardPart cardPart, ValidationResponse validationResponse)
		{
			Entry = entry;
			CardPart = cardPart;
			ValidationResponse = validationResponse;
		}
	}
}
