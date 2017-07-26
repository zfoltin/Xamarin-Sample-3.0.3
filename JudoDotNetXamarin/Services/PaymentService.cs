using System;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Models;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{
	public class PaymentService : IPaymentService
	{
		Judo _judo;
		JudoPayApi _judoAPI;
		IClientService _clientService;

		CardPaymentModel _sessionPaymentModel { get; set; }
		TokenPaymentModel _sessionTokenPaymentModel { get; set; }
		PKPaymentModel _sessionPKPaymentModel { get; set; }
		AndroidPaymentModel _sessionAndroidPaymentModel { get; set; }

		public PaymentService(Judo judo)
		{
			_clientService = DependencyService.Get<IClientService>();
			_judoAPI = JudoPaymentsFactory.Create(judo.Environment, judo.Token, judo.Secret, _clientService.GetUserAgent());
			_judo = judo;
			CycleSession();
		}

		public void CycleSession()
		{
			_sessionPaymentModel = new CardPaymentModel();
			_sessionTokenPaymentModel = new TokenPaymentModel();
			_sessionPKPaymentModel = new PKPaymentModel();
			_sessionAndroidPaymentModel = new AndroidPaymentModel();
		}

		public async Task<IResult<ITransactionResult>> Payment(PaymentViewModel paymentViewModel)
		{
			await PopulatePaymentModel(paymentViewModel);
			var task = _judoAPI.Payments.Create(_sessionPaymentModel);

			return await task;
		}

		public async Task<IResult<ITransactionResult>> PreAuth(PaymentViewModel authorisation)
		{
			await PopulatePaymentModel(authorisation);

			var task = _judoAPI.PreAuths.Create(_sessionPaymentModel);
			return await task;
		}

		public async Task<IResult<ITransactionResult>> TokenPayment(TokenPaymentViewModel tokenPayment)
		{
			await PopulateTokenPaymentModel(tokenPayment);

			var task = _judoAPI.Payments.Create(_sessionTokenPaymentModel);
			return await task;
		}

		public async Task<IResult<ITransactionResult>> TokenPreAuth(TokenPaymentViewModel tokenPayment)
		{
			await PopulateTokenPaymentModel(tokenPayment);

			var task = _judoAPI.PreAuths.Create(_sessionTokenPaymentModel);
			return await task;
		}

		public async Task<IResult<ITransactionResult>> RegisterCard(PaymentViewModel payment)
		{
			await PopulatePaymentModel(payment);
			var task = _judoAPI.RegisterCards.Create(_sessionPaymentModel);
			return await task;
		}

		public async Task<IResult<ITransactionResult>> Collection(CollectionModel collectionModel)
		{
			var task = _judoAPI.Collections.Create(collectionModel);
			return await task;
		}

		public async Task<IResult<ITransactionResult>> Refund(RefundModel refundModel)
		{
			var task = _judoAPI.Refunds.Create(refundModel);
			return await task;
		}

		public async Task<IResult<ITransactionResult>> ApplePayPayment(PKPaymentViewModel payment)
		{
			await PopulatePKPaymentModel(payment);
			return await _judoAPI.Payments.Create(_sessionPKPaymentModel);
		}

		public async Task<IResult<ITransactionResult>> ApplePayPreAuth(PKPaymentViewModel payment)
		{
			await PopulatePKPaymentModel(payment);
			return await _judoAPI.PreAuths.Create(_sessionPKPaymentModel);
		}

		public async Task<IResult<ITransactionResult>> AndroidPayPayment(AndroidPaymentModel payment)
		{
			await PopulateAndroidPayModel(payment);
			return await _judoAPI.Payments.Create(_sessionAndroidPaymentModel);
		}

		public async Task<IResult<ITransactionResult>> AndroidPayPreAuth(AndroidPaymentModel payment)
		{
			await PopulateAndroidPayModel(payment);
			return await _judoAPI.PreAuths.Create(_sessionAndroidPaymentModel);
		}

		public async Task<IResult<ITransactionResult>> Complete3DSecure(long receiptID, string paRes, string md)
		{
			try
			{
				var model = new ThreeDResultModel();
				model.PaRes = paRes;

				var task = _judoAPI.ThreeDs.Complete3DSecure(receiptID, model);
				return await task;
			}
			catch (Exception e)
			{
				var error = new JudoError()
				{
					Exception = e,
					ApiError = new JudoPayDotNet.Errors.ModelError
					{
						Message = e.InnerException.ToString()
					}
				};
				throw error;
			}
		}

		async Task PopulatePaymentModel(PaymentViewModel paymentViewModel)
		{
			_judo.Validate();

			var factory = new CardPaymentModelFactory();
			var clientDetails = await _clientService.IdentifyDevice(_judo, paymentViewModel.ClientDetails);

			_sessionPaymentModel = factory.Create(paymentViewModel, _judo, clientDetails);
		}

		async Task PopulateTokenPaymentModel(TokenPaymentViewModel tokenPayment)
		{
			_judo.Validate();

			var factory = new TokenPaymentModelFactory();
			var clientDetails = await _clientService.IdentifyDevice(_judo, tokenPayment.ClientDetails);

			_sessionTokenPaymentModel = factory.Create(tokenPayment, _judo, clientDetails);
		}

		async Task PopulatePKPaymentModel(PKPaymentViewModel paymentViewModel)
		{
			_judo.Validate();

			var clientDetails = await _clientService.IdentifyDevice(_judo, paymentViewModel.ClientDetails);
			var factory = new PKPaymentModelFactory();

			_sessionPKPaymentModel = factory.Create(paymentViewModel, _judo, clientDetails);
		}

		async Task PopulateAndroidPayModel(AndroidPaymentModel paymentModel)
		{
			_judo.Validate();

			_sessionAndroidPaymentModel.JudoId = (string.IsNullOrWhiteSpace(paymentModel.JudoId) ? _judo.JudoId : paymentModel.JudoId);
			_sessionAndroidPaymentModel.YourConsumerReference = (string.IsNullOrWhiteSpace(paymentModel.YourConsumerReference) ? ("Consumer:" + _judo.JudoId) : paymentModel.YourConsumerReference);
			_sessionAndroidPaymentModel.Amount = paymentModel.Amount;

			var clientDetails = await _clientService.IdentifyDevice(_judo, null);

			_sessionAndroidPaymentModel.ClientDetails = clientDetails;
			_sessionAndroidPaymentModel.Wallet = paymentModel.Wallet;
		}
	}
}