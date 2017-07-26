using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoDotNetXamarin
{
	public interface TransactionView
	{
		Task OnDeclined();

		void OnResult(IResult<ITransactionResult> result);

		void OnDisplay3dSecure(PaymentRequiresThreeDSecureModel result);

		Task OnDisplayConnectionError();

		void HideLoading();

		void ShowLoading();
	}
}