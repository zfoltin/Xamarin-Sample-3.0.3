using System;
namespace JudoDotNetXamarin
{

	public class ValidationRequest
	{
		public IValidator Validator { get; }
		public JudoEntry Entry { get; }
		public CardPart CardPart { get; }

		public ValidationRequest(IValidator validator, JudoEntry entry, CardPart cardPart)
		{
			Validator = validator;
			Entry = entry;
			CardPart = cardPart;
		}
	}
}
