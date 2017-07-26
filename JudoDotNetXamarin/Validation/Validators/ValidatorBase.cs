using System;
using System.Globalization;

namespace JudoDotNetXamarin
{
	public abstract class ValidatorBase
	{
		private const int maxDateLength = 4;

		protected ValidationResponse ValidateDate(string entry, DateTime dateToMeasureFrom, Func<DateTime, DateTime, bool> compareFunc, string errorMessageIfFail)
		{
			var strippedEntry = (entry ?? string.Empty).Replace(" ", "").Replace("/", "");

			var valid = false;
			var errorMessage = string.Empty;

			if (strippedEntry.Length == maxDateLength)
			{
				int dayPart = 0, monthPart = 0, yearPart = 0;
				var yearPrefix = "20";

				try
				{
					monthPart = int.Parse(strippedEntry.Substring(0, 2));
					yearPart = int.Parse(strippedEntry.Substring(2, 2));
					dayPart = DateTime.DaysInMonth(yearPart, monthPart);
				}
				//Happy to capture the expection and move on.
				catch { }


				if (yearPart < 10) yearPrefix += "0";
				DateTime d;
				string[] formats = { "MM/dd/yyyy", "M/dd/yyyy" };
				valid = DateTime.TryParseExact(string.Format("{0}/{1}/{2}{3}", monthPart, dayPart, yearPrefix, yearPart), formats, new CultureInfo("en-US"), DateTimeStyles.None, out d) && compareFunc(d, dateToMeasureFrom);

				if (!valid)
				{
					errorMessage = errorMessageIfFail;
				}
			}

			return new ValidationResponse(valid, errorMessage, !string.IsNullOrWhiteSpace(errorMessage));
		}
	}
}
