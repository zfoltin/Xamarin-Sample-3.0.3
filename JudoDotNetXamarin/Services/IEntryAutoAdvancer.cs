namespace JudoDotNetXamarin
{
	public interface IEntryAutoAdvancer
	{
		void RegisterNext(JudoEntry entry);

		JudoEntry First();

		JudoEntry Next();

		void RemoveFocus();

		bool CanAdvance(JudoEntry entry, bool isValid);
	}
}