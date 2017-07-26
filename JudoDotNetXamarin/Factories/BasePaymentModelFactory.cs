namespace JudoDotNetXamarin
{
	public abstract class BasePaymentModelFactory
	{
		protected string ConsumerReference(BasePaymentViewModel paymentViewModel, Judo judo)
		{
			if (string.IsNullOrWhiteSpace(paymentViewModel.ConsumerReference))
			{
				return "Consumer:" + judo.JudoId;
			}
			else
			{
				return paymentViewModel.ConsumerReference;
			}
		}
	}
}