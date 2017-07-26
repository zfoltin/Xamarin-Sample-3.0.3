using System;
using JudoPayDotNet.Models;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{
	public partial class WebVerificationPage : ContentPage, WebViewListener
	{
		private PaymentRequiresThreeDSecureModel model;
		private EventHandler<CardVerificationResult> handler;
		private string loadingTextLabel;

		public WebVerificationPage(PaymentRequiresThreeDSecureModel model, EventHandler<CardVerificationResult> handler) : this(model, handler, "Verifying card") { }

		public WebVerificationPage(PaymentRequiresThreeDSecureModel model, EventHandler<CardVerificationResult> handler, string loadingTextLabel)
		{
			this.model = model;
			this.handler = handler;
			this.loadingTextLabel = loadingTextLabel;

			InitializeComponent();
			LoadWebPage();
		}

		protected override bool OnBackButtonPressed()
		{
			return true;
		}

		void LoadWebPage()
		{
			webView.ResultCallback += handler;
			webView.Listener = this;
			webView.Request3dSecure = new CardVerificationRequest {
				acsUrl = model.AcsUrl,
				md = model.Md,
				paReq = model.PaReq,
				redirectUrl = "https://pay.judopay.com/Android/Parse3DS"
			};
		}

		public void OnPageStarted()
		{
			webView.IsVisible = false;
			loadingText.Text = loadingTextLabel;
			loadingOverlay.IsVisible = true;
		}

		public void OnPageLoaded()
		{
			loadingOverlay.IsVisible = false;
		}
	}
}