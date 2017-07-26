using System;
using JudoPayDotNet.Models;

namespace JudoDotNetXamarin
{
	public class TransactionPresenter
	{
		private TransactionView view;

		public bool Loading { get; private set;}

		public TransactionPresenter(TransactionView view)
		{
			this.view = view;
		}

		public void HandleResult(IResult<ITransactionResult> result)
		{
			var resultText = result.HasError ? "" : result.Response.Result;

			if(result.HasError || "Success".Equals(resultText))
			{
				Loading = false;
				view.OnResult(result);
			}
			else if("Declined".Equals(resultText))
			{
				Loading = false;
				view.HideLoading();
				view.OnDeclined();
			}
			else if(result.Response.GetType() == typeof(PaymentRequiresThreeDSecureModel))
			{
				view.OnDisplay3dSecure(result.Response as PaymentRequiresThreeDSecureModel);				
			}
		}
	}
}