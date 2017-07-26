using System.Threading.Tasks;
using JudoPayDotNet.Models;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("JudoDotNetXamariniOSSDK")]
[assembly: InternalsVisibleTo("JudoDotNetXamarinAndroidSDK")]
[assembly: InternalsVisibleTo("JudoDotNetXamariniOS.Tests")]
namespace JudoDotNetXamarin
{
	public interface IPaymentService
	{
		Task<IResult<ITransactionResult>> Payment(PaymentViewModel payment);

		Task<IResult<ITransactionResult>> PreAuth(PaymentViewModel payment);

		Task<IResult<ITransactionResult>> TokenPayment(TokenPaymentViewModel payment);

		Task<IResult<ITransactionResult>> TokenPreAuth(TokenPaymentViewModel payment);

		Task<IResult<ITransactionResult>> RegisterCard(PaymentViewModel payment);

		Task<IResult<ITransactionResult>> Complete3DSecure(long receiptId, string paRes, string md);

		Task<IResult<ITransactionResult>> Collection(CollectionModel collectionModel);

		Task<IResult<ITransactionResult>> Refund(RefundModel refundModel);

		Task<IResult<ITransactionResult>> ApplePayPayment(PKPaymentViewModel payment);

		Task<IResult<ITransactionResult>> ApplePayPreAuth(PKPaymentViewModel payment);

		Task<IResult<ITransactionResult>> AndroidPayPayment(AndroidPaymentModel payment);

		Task<IResult<ITransactionResult>> AndroidPayPreAuth(AndroidPaymentModel payment);

		void CycleSession();
	}
}