using System;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{
	public class CardVerificationWebView : View
	{
		public static readonly BindableProperty ListenerProperty =
			BindableProperty.Create(nameof(Listener), typeof(WebViewListener), typeof(CardVerificationWebView), default(WebViewListener));

		public static readonly BindableProperty PostProperty =
			BindableProperty.Create(nameof(Request3dSecure), typeof(CardVerificationRequest), typeof(CardVerificationWebView), default(CardVerificationRequest));

		public static readonly BindableProperty ResultCallbackProperty =
			BindableProperty.Create(nameof(ResultCallback), typeof(EventHandler<CardVerificationResult>), typeof(CardVerificationWebView), default(EventHandler<CardVerificationResult>));
		
		public WebViewListener Listener
		{
			get { return (WebViewListener)GetValue(ListenerProperty); }
			set { SetValue(ListenerProperty, value); }		
		}

		public CardVerificationRequest Request3dSecure
		{
			get { return (CardVerificationRequest)GetValue(PostProperty); }
			set { SetValue(PostProperty, value); }
		}

		public EventHandler<CardVerificationResult> ResultCallback
		{
			get { return (EventHandler<CardVerificationResult>)GetValue(ResultCallbackProperty); }
			set { SetValue(ResultCallbackProperty, value); }
		}
	}
}
