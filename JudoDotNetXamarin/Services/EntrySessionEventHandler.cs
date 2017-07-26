namespace JudoDotNetXamarin
{
	public class EntrySessionEventHandler
	{
		public void OnFocusEvent(EntrySessionState state, bool focused)
		{
			if (focused && !state.IsSessionStarted())
			{
				state.StartSession();
			}
			else if (!focused && state.IsSessionStarted())
			{
				state.EndSession();
			}
		}

		public void OnValidEvent(EntrySessionState state, bool valid)
		{
			if (state.IsSessionStarted())
			{
				state.SetValid(valid);
			}
		}

		public void OnTextChangeEvent(EntrySessionState state)
		{
			if (state.IsSessionStarted())
			{
				state.StartEditing();
			}
		}
	}
}