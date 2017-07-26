using System;
using System.Collections.Generic;

namespace JudoDotNetXamarin
{
	public class ExpiryDateValidator : ValidatorBase, IValidator
	{
		private readonly DateTime _dateToMessaureFrom;

		public ExpiryDateValidator(DateTime dateToMessaureFrom)
		{
			_dateToMessaureFrom = dateToMessaureFrom;
		}

		public ValidationResponse Validate(string entry, ICardNetwork cardNetwork, List<CardNetwork> acceptableCardNetworksForTransaction)
		{
			return ValidateDate(entry, _dateToMessaureFrom, (entryDate, dateToMeasureFrom) => { return entryDate >= dateToMeasureFrom && entryDate <= dateToMeasureFrom.AddYears(10); }, "Check expiry date");
		}
	}
}
