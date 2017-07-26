using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JudoPayDotNet.Models;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{
	public abstract partial class CardEntryPage : KeyboardAwarePage, TransactionView
	{
		ICardNetwork _currentDiscoveredNetwork = new UnknownCardNetwork();
		ICountry _currentDiscoveredCountry = new UKCountry();

		readonly CardNetworkDiscoverer _cardNetworkDiscoverer = new CardNetworkDiscoverer();
		readonly CountryDiscoverer _countryDiscoverer = new CountryDiscoverer();
		readonly IApplicationEventTracker _appEventTracker = DependencyService.Get<IApplicationEventTracker>();

		Dictionary<CardPart, bool> _validParts = new Dictionary<CardPart, bool>();
		List<CardNetwork> _acceptedCardNetworks = new List<CardNetwork>();

		IEntryAutoAdvancer _advancer = new EntryAutoAdvancer();

		public EventHandler<IResult<ITransactionResult>> resultHandler { get; set; }

		protected TransactionPresenter Presenter;
		protected readonly IPaymentService PaymentService;
		protected readonly Judo Judo;

		bool _isTokenPayment;

		public CardEntryPage(Judo judo, PaymentDefaultsViewModel defaults) : this(judo)
		{
			SetUpDefaults(defaults);
		}

		public CardEntryPage(Judo judo, TokenPaymentDefaultsViewModel defaults) : this(judo)
		{
			_isTokenPayment = true;
			SetUpDefaults(defaults);
		}

		public CardEntryPage(Judo judo)
		{
			PaymentService = new PaymentService(judo);
			Judo = judo;
			_acceptedCardNetworks = _cardNetworkDiscoverer.GetAvailableCardNetworks();
			Presenter = new TransactionPresenter(this);
			InitializeComponent();
			InitializeView();
		}

		protected abstract Task OnSubmit(CardViewModel card, Dictionary<string, object> signals);

		public void InitializeView()
		{
			_advancer.RegisterNext(cardNumberEntry);
			_advancer.RegisterNext(startDateEntry);
			_advancer.RegisterNext(issueNumberEntry);
			_advancer.RegisterNext(expiryDateEntry);
			_advancer.RegisterNext(cvvEntry);
			_advancer.RegisterNext(postcodeEntry);

			InitializeTheme();

			_currentDiscoveredNetwork.SetAvsEnabled(Judo.AvsEnabled);

			foreach (var countryName in _countryDiscoverer.GetCountryNames())
			{
				countryPicker.Items.Add(countryName);
			}

			SetBillingCountry();

			cardNumberEntry.TextChanged += (sender, e) => { Validate(sender); };
			expiryDateEntry.TextChanged += (sender, e) => { Validate(sender); };
			cvvEntry.TextChanged += (sender, e) => { Validate(sender); };
			startDateEntry.TextChanged += (sender, e) => { Validate(sender); };
			issueNumberEntry.TextChanged += (sender, e) => { Validate(sender); };
			postcodeEntry.TextChanged += (sender, e) => { Validate(sender); };
			countryPicker.SelectedIndexChanged += (sender, e) => SetBillingCountry();

			DisplayPaymentButtonIfValidPartsMet();

			if (Device.OS == TargetPlatform.Android)
			{
				countryPicker.Margin = new Thickness(5, 0, 0, 0);
			}

			payButton.Clicked += (sender, e) =>
			{
				_advancer.RemoveFocus();

				var card = new CardViewModel
				{
					CardNumber = cardNumberEntry.Text,
					ExpiryDate = expiryDateEntry.Text,
					SecurityCode = cvvEntry.Text,
					StartDate = startDateEntry.Text,
					IssueNumber = issueNumberEntry.Text,
					Postcode = postcodeEntry.Text,
					Country = _currentDiscoveredCountry
				};

				var entryExporter = new EntryExporter();

				entryExporter.Add("cardNumber", cardNumberEntry);
				entryExporter.Add("expiryDate", expiryDateEntry);
				entryExporter.Add("securityCode", cvvEntry);
				entryExporter.Add("startDate", startDateEntry);
				entryExporter.Add("issueNumber", issueNumberEntry);
				entryExporter.Add("postcode", postcodeEntry);

				var appEvents = _appEventTracker.Export();
				var entryEvents = entryExporter.Export();

				var events = appEvents.Union(entryEvents).GroupBy(d => d.Key)
									  .ToDictionary((arg) => arg.Key, arg => arg.First().Value);
				OnSubmit(card, events);
			};
		}

		void InitializeTheme()
		{
			if (Judo.Theme.BackgroundColor != Color.Default)
			{
				BackgroundColor = Judo.Theme.BackgroundColor;
			}

			securityLabel.IsVisible = Judo.Theme.ShowSecurityMessage;

			List<JudoEntry> entries = new List<JudoEntry>
			{
				cardNumberEntry,
				expiryDateEntry,
				cvvEntry,
				startDateEntry,
				issueNumberEntry,
				postcodeEntry
			};

			InitializeEntryFromTheme(entries);
			payButton.SetThemeProperty(x => x.BackgroundColor, Judo.Theme.ButtonBackgroundColor);
			payButton.SetThemeProperty(x => x.TextColor, Judo.Theme.ButtonTextColor);
			securityLabel.SetThemeProperty(x => x.TextColor, Judo.Theme.SecondaryTextColor);
			loadingOverlay.SetThemeProperty(x => x.BackgroundColor, Judo.Theme.OverlayColor);

			payButton.Text = GetButtonLabel();
			Title = GetTitle();
			loadingOverlayTitleLabel.Text = GetLoadingOverlayTitleLabel();
		}

		void InitializeEntryFromTheme(List<JudoEntry> entries)
		{
			foreach (JudoEntry entry in entries)
			{
				entry.SetThemeProperty(x => x.TextColor, Judo.Theme.PrimaryColor);
				entry.SetThemeProperty(x => x.LabelColor, Judo.Theme.LabelTextColor);
				entry.SetThemeProperty(x => x.HintColor, Judo.Theme.HintTextColor);
				entry.SetThemeProperty(x => x.PlaceholderColor, Judo.Theme.EntryLabelPLaceholderColor);
			}

			countryPicker.SetThemeProperty(x => x.TextColor, Judo.Theme.PrimaryColor);
			countryPicker.SetThemeProperty(x => x.LabelColor, Judo.Theme.LabelTextColor);
			countryPicker.SetThemeProperty(x => x.HintColor, Judo.Theme.HintTextColor);
		}

		protected void SetEnabledForAllViews(bool isEnabled)
		{
			cardNumberEntry.IsEnabled = isEnabled;
			expiryDateEntry.IsEnabled = isEnabled;
			cvvEntry.IsEnabled = isEnabled;
			startDateEntry.IsEnabled = isEnabled;
			issueNumberEntry.IsEnabled = isEnabled;
			postcodeEntry.IsEnabled = isEnabled;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			_advancer.First()?.Focus();
			_appEventTracker.OnAppResumed();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			_appEventTracker.OnAppPaused();
		}

		public void OnDisplay3dSecure(PaymentRequiresThreeDSecureModel result)
		{
			WebVerificationPage webVerificationPage = new WebVerificationPage(result, async (sender, webResult) =>
			{
				Presenter.HandleResult(await PaymentService.Complete3DSecure(result.ReceiptId, webResult.paRes, webResult.md));
				await Navigation.PopModalAsync();
			});

			Navigation.PushModalAsync(webVerificationPage);
		}

		void SetUpDefaults(PaymentDefaultsViewModel defaults)
		{
			//Set defaults.
			cardNumberEntry.Text = defaults.CardNumber;
			cardNumberEntry.ShouldImageOpacityBeApplied = !string.IsNullOrWhiteSpace(defaults.CardNumber);
			expiryDateEntry.Text = defaults.ExpiryDate;
			startDateEntry.Text = defaults.StartDate;
			issueNumberEntry.Text = defaults.IssueNumber;
			UpdateCardIcons();
		}

		void SetUpDefaults(TokenPaymentDefaultsViewModel defaults)
		{
			cardNumberEntry.Format = "";
			cardNumberEntry.Text = defaults.MaskedCardNumber;
			cardImage.Opacity = !string.IsNullOrWhiteSpace(defaults.MaskedCardNumber) ? 1 : 0.5;
			expiryDateEntry.Text = defaults.ExpiryDate;
			expiryDateEntry.Format = "";
			cardNumberEntry.IsEnabled = false;
			expiryDateEntry.IsEnabled = false;
			startDateEntry.IsEnabled = false;
			issueNumberEntry.IsEnabled = false;
			_currentDiscoveredNetwork = defaults.CardNetork;
			UpdateCardIcons();
			UpdateCvvMaxLength();
			_currentDiscoveredNetwork.SetAvsEnabled(Judo.AvsEnabled);
		}

		public void Validate(object sender)
		{
			_validParts.Clear();

			if (!_isTokenPayment)
			{
				var network = _cardNetworkDiscoverer.DiscoverCardNetwork(cardNumberEntry.Text ?? string.Empty);

				if (network.GetCardNetworkType() != _currentDiscoveredNetwork.GetCardNetworkType())
				{
					_currentDiscoveredNetwork = network;
					UpdateCardIcons();
					UpdateCvvMaxLength();
					UpdateCardNumberFormat();
					UpdateCardNumberMaxLength();

					_currentDiscoveredNetwork.SetAvsEnabled(Judo.AvsEnabled);
					startDateEntry.IsVisible = _currentDiscoveredNetwork.ShouldDisplayStartDate();
					issueNumberEntry.IsVisible = _currentDiscoveredNetwork.ShouldDisplayIssueNumber();
				}

				ValidatePart(cardNumberEntry, new CardNumberValidator(), CardPart.CardNumber);
				ValidatePart(expiryDateEntry, new ExpiryDateValidator(DateTime.Now), CardPart.ExpiryDate);
				ValidatePart(startDateEntry, new StartDateValidator(DateTime.Now), CardPart.StartDate);
				ValidatePart(issueNumberEntry, new IssueNumberValidator(), CardPart.IssueNumber);
			}

			ValidatePart(cvvEntry, new SecurityCodeValidator(), CardPart.SecurityCode);
			ValidatePart(postcodeEntry, new PostcodeValidator(_currentDiscoveredCountry), CardPart.Postcode);

			DisplayAvsFieldIfAvsEnabledAndPrimaryPartsAreMet();
			DisplayPaymentButtonIfValidPartsMet();

			var entry = (JudoEntry)sender;

			Advance(entry, string.IsNullOrWhiteSpace(entry.Error));
		}

		public async Task OnDeclined()
		{
			PaymentService.CycleSession();
			await DisplayAlert("Payment declined", "Please check your details and try again", "OK");
		}

		public void OnResult(IResult<ITransactionResult> result)
		{
			if (resultHandler != null)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					resultHandler.Invoke(this, result);
				});
			}
		}

		public void SetAmexAsUnaccepted()
		{
			_acceptedCardNetworks.Remove(CardNetwork.AMEX);
		}

		public void SetMaestroAsUnaccepted()
		{
			_acceptedCardNetworks.Remove(CardNetwork.MAESTRO);
		}

		void SetBillingCountry()
		{
			countryPicker.SelectedIndex = countryPicker.SelectedIndex > -1 ? countryPicker.SelectedIndex : 0;
			var selectedValue = countryPicker.Items[countryPicker.SelectedIndex];
			_currentDiscoveredCountry = _countryDiscoverer.DiscoverCountry(selectedValue);
			postcodeEntry.Placeholder = _currentDiscoveredCountry.GetPostcodeTitle();
			postcodeEntry.Text = string.Empty;

			if (_currentDiscoveredCountry.IsPostcodeNumeric())
			{
				postcodeEntry.Keyboard = Keyboard.Numeric;
				postcodeEntry.Digits = "0123456789";
			}
			else
			{
				postcodeEntry.Keyboard = Keyboard.Text;
				postcodeEntry.Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			}

			postcodeEntry.MaxLength = _currentDiscoveredCountry.GetPostcodeLength();
			postcodeEntry.IsEnabled = _currentDiscoveredCountry.IsPostcodeRequired();
			Validate(postcodeEntry);
		}

		void ValidatePart(JudoEntry entry, IValidator validator, CardPart cardPart)
		{
			var validationResponse = validator.Validate(entry.Text, _currentDiscoveredNetwork, _acceptedCardNetworks);
			ChangeValidParts(cardPart, validationResponse.IsValid);
			entry.Error = !validationResponse.IsValid && validationResponse.ShouldDisplayErrorMessage ? validationResponse.ErrorMessage : null;
			entry.IsValid = validationResponse.IsValid;
		}

		void Advance(JudoEntry entry, bool isValid)
		{
			if (_advancer.CanAdvance(entry, isValid))
			{
				var nextEntry = _advancer.Next();

				if (nextEntry != null)
				{
					nextEntry.Focus();
				}
			}
		}

		void ChangeValidParts(CardPart cardPart, bool isValid)
		{
			if (!_validParts.ContainsKey(cardPart))
			{
				_validParts.Add(cardPart, isValid);
			}
			else
			{
				_validParts[cardPart] = isValid;
			}
		}

		void DisplayPaymentButtonIfValidPartsMet()
		{
			var parts = _currentDiscoveredNetwork.GetPartsNeededToBeValid(_isTokenPayment);
			var anyFail = AreAllPartsValid(parts);
			payButton.IsVisible = !anyFail;
		}

		void DisplayAvsFieldIfAvsEnabledAndPrimaryPartsAreMet()
		{
			if (Judo.AvsEnabled)
			{
				var parts = _currentDiscoveredNetwork.GetPartsNeededToBeValid(_isTokenPayment).Where(x => x != CardPart.Postcode).ToList();
				var anyFail = AreAllPartsValid(parts);
				countryPicker.IsVisible = !anyFail;
				postcodeEntry.IsVisible = !anyFail;
			}
		}

		string GetButtonLabel()
		{
			if (!string.IsNullOrWhiteSpace(Judo.Theme.ButtonLabel))
			{
				return Judo.Theme.ButtonLabel;
			}
			return GetDefaultButtonLabel();
		}

		protected virtual string GetDefaultButtonLabel()
		{
			return "Pay";
		}

		string GetTitle()
		{
			if (!string.IsNullOrWhiteSpace(Judo.Theme.PageTitle))
			{
				return Judo.Theme.PageTitle;
			}
			return GetDefaultTitle();
		}

		protected virtual string GetDefaultTitle()
		{
			return "Payment";
		}

		protected virtual string GetLoadingOverlayTitleLabel()
		{
			return "Processing payment";
		}

		bool AreAllPartsValid(List<CardPart> parts)
		{
			return parts.Any(x => !_validParts.ContainsKey(x) || !_validParts[x]);
		}

		void UpdateCardIcons()
		{
			cardImage.Source = _currentDiscoveredNetwork.GetCardImageSource();
			cvvImage.Source = _currentDiscoveredNetwork.GetSecurityCodeImageSource();
			cvvEntry.Placeholder = _currentDiscoveredNetwork.GetSecurityCodeLabel();
		}

		void UpdateCvvMaxLength()
		{
			cvvEntry.MaxLength = _currentDiscoveredNetwork.GetSecurityCodeLength();
		}

		void UpdateCardNumberMaxLength()
		{
			cardNumberEntry.MaxLength = _currentDiscoveredNetwork.GetFormattedCardNumberLength();
		}

		void UpdateCardNumberFormat()
		{
			cardNumberEntry.Format = _currentDiscoveredNetwork.CardNumberFormat();
		}

		public void HideLoading()
		{
			SetEnabledForAllViews(true);
			loadingOverlay.IsVisible = false;
		}

		public void ShowLoading()
		{
			SetEnabledForAllViews(false);
			loadingOverlay.IsVisible = true;
		}

		protected override bool OnBackButtonPressed()
		{
			return Presenter.Loading;
		}

		public async Task OnDisplayConnectionError()
		{
			await DisplayAlert("Can't connect", "Please check your internet connection", "OK");
		}
	}
}