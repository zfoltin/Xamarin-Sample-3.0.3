namespace JudoDotNetXamarin
{
	public interface IApplePayService
	{
		bool IsApplePayAvailable(Judo judo);

		void Payment(Judo judo, ApplePayModel model, JudoSuccessCallback success, JudoFailureCallback failure);

		void PreAuth(Judo judo, ApplePayModel model, JudoSuccessCallback success, JudoFailureCallback failure);
	}
}