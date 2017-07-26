using System;
using System.Collections.Generic;
using System.Linq;

namespace JudoDotNetXamarin
{
	public class EntryExporter
	{
		Dictionary<string, JudoEntry> entries = new Dictionary<string, JudoEntry>();

		public void Add(string name, JudoEntry entry)
		{
			entries.Add(name, entry);
		}

		internal Dictionary<string, object> Export()
		{
			var export = new Dictionary<string, object>();
			var fieldMetaData = new List<Dictionary<string, object>>();

			foreach (var item in entries)
			{
				var field = new Dictionary<string, object>();

				field.Add("field", item.Key);
				field.Add("sessions", item.Value.EntrySessions.Sessions.Select(x => x.Export()));
				field.Add("pasted", item.Value.PasteEvents);

				fieldMetaData.Add(field);
			}

			export.Add("fieldMetaData", fieldMetaData);
			var totalKeystrokes = entries.Sum((KeyValuePair<string, JudoEntry> pair) => pair.Value.Keystrokes);
			export.Add("totalKeystrokes", totalKeystrokes);

			return export;
		}
	}
}