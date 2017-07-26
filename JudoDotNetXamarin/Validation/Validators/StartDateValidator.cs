using System;
using System.Collections.Generic;

namespace JudoDotNetXamarin
{
	public class StartDateValidator : ValidatorBase, IValidator
	{
		private readonly DateTime _dateToMessaureFrom;

		public StartDateValidator(DateTime dateToMessaureFrom)
		{
			_dateToMessaureFrom = dateToMessaureFrom;
		}

		public ValidationResponse Validate(string entry, ICardNetwork cardNetwork, List<CardNetwork> acceptableCardNetworksForTransaction)
		{
			return ValidateDate(entry, _dateToMessaureFrom, (entryDate, dateToMeasureFrom) => 
			{
				return (entryDate <= dateToMeasureFrom || (entryDate.Month == dateToMeasureFrom.Month && entryDate.Year == dateToMeasureFrom.Year)) && entryDate >= dateToMeasureFrom.AddYears(-10); 
			}, "Check start date");
		}
	}
}
