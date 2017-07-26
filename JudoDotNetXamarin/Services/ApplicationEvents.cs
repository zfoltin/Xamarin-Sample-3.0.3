using System;
using System.Collections.Generic;

namespace JudoDotNetXamarin
{
	public class ApplicationEvents
	{
		List<DateTime> _resumed = new List<DateTime>();

		public void OnResume()
		{
			_resumed.Add(DateTime.UtcNow);
		}

		public Dictionary<string, object> Export()
		{
			var dict = new Dictionary<string, object>();
			dict.Add("appResumed", _resumed);

			return dict;
		}
	}
}