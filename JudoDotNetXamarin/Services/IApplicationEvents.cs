using System.Collections.Generic;

namespace JudoDotNetXamarin
{
	public interface IApplicationEventTracker
	{
		void OnAppPaused();

		void OnAppResumed();

		Dictionary<string, object> Export();
	}
}