using System;
using System.Collections.Generic;

namespace JudoDotNetXamarin
{
	public class EntrySession
	{
		public bool Valid { get; set;}
		public DateTime? TimeStarted { get; set; }
		public DateTime? TimeEdited { get; set; }
		public DateTime? TimeEnded { get; set; }

		public Dictionary<string, object> Export()
		{
			return new Dictionary<string, object>
			{
				{ "valid", Valid },
				{ "timeStarted", TimeStarted },
				{ "timeEdited", TimeEdited },
				{ "timeEnded", TimeEnded },
			};
		}
	}
}