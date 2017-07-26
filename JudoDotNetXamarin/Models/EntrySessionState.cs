using System;
using System.Collections.Generic;

namespace JudoDotNetXamarin
{
	public class EntrySessionState
	{
		List<EntrySession> _sessions;
		EntrySession _currentSession;

		public EntrySessionState()
		{
			_sessions = new List<EntrySession>();
		}

		public List<EntrySession> Sessions
		{
			get
			{
				var sessions = new List<EntrySession>(_sessions);

				if (_currentSession != null)
				{
					sessions.Add(_currentSession);
				}

				return sessions;
			}
		}

		internal void SetValid(bool valid)
		{
			if (_currentSession != null)
			{
				_currentSession.Valid = valid;
			}
		}

		internal void StartSession()
		{
			if (_currentSession != null && _currentSession.TimeEnded != null)
			{
				_sessions.Add(_currentSession);
			}
			_currentSession = new EntrySession { TimeStarted = DateTime.UtcNow};
		}

		internal void EndSession()
		{
			if (_currentSession != null && _currentSession.TimeEnded == null)
			{
				_currentSession.TimeEnded = DateTime.UtcNow;
				_sessions.Add(_currentSession);

				_currentSession = null;
			}
		}

		internal void StartEditing()
		{
			if (_currentSession != null && _currentSession.TimeEdited == null)
			{
				_currentSession.TimeEdited = DateTime.UtcNow;
			}
		}

		internal bool IsSessionStarted()
		{
			return _currentSession?.TimeStarted != null;
		}
	}
}