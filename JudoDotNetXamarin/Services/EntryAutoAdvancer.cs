using System.Collections.Generic;
using System.Linq;

namespace JudoDotNetXamarin
{
	public class EntryAutoAdvancer : IEntryAutoAdvancer
	{
		private Dictionary<int, JudoEntry> _entries = new Dictionary<int, JudoEntry>();

		public void RegisterNext(JudoEntry entry)
		{
			_entries.Add(_entries.Count + 1, entry);
		}

		public JudoEntry First()
		{
			var next = _entries.FirstOrDefault(x => IsMatch(x.Value));
			return next.Value ?? null;
		}

		public JudoEntry Next()
		{
			JudoEntry entry = null;

			var focused = _entries.SingleOrDefault(x => x.Value.IsNativeFocused);

			if (focused.Value != null)
			{
				var next = _entries.FirstOrDefault(x => x.Key > focused.Key && IsMatch(x.Value));
				entry = next.Value ?? null;
			}
			else
			{
				entry = _entries.First().Value;
			}

			return entry;
		}

		public void RemoveFocus()
		{
			foreach (JudoEntry entry in _entries.Values)
			{
				entry.Blur();
			}
		}

		public bool CanAdvance(JudoEntry entry, bool isValid)
		{
			return entry.IsNativeFocused && isValid && (entry.Text ?? string.Empty).Length == entry.MaxLength;
		}

		private bool IsMatch(JudoEntry entry)
		{
			return string.IsNullOrWhiteSpace(entry.Text) && entry.IsEnabled && entry.IsVisible;
		}
	}
}