using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{
	public class JudoEntry : Entry
	{
		public JudoEntry()
		{
			EntrySessions = new EntrySessionState();
			PasteEvents = new List<DateTime>();
		}

		public static readonly BindableProperty FormatProperty =
			BindableProperty.Create(nameof(Format), typeof(string), typeof(Entry), default(string));

		public static readonly BindableProperty ErrorProperty = 
			BindableProperty.Create(nameof(Error), typeof(string), typeof(Entry), default(string));

		public static readonly BindableProperty DigitsProperty = 
			BindableProperty.Create(nameof(Digits), typeof(string), typeof(Entry), default(string));

		public static readonly BindableProperty MaxLengthProperty = 
			BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(Entry), default(int));

		public static readonly BindableProperty IsNativeFocusedProperty =
			BindableProperty.Create(nameof(IsNativeFocused), typeof(bool), typeof(Entry), default(bool));

		public static readonly BindableProperty PasteEventsProperty =
			BindableProperty.Create(nameof(PasteEvents), typeof(List<DateTime>), typeof(Entry), default(List<DateTime>));

		public static readonly BindableProperty IsValidProperty =
			BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(Entry), default(bool));
		
		public static readonly BindableProperty LabelColorProperty =
			BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(Entry), default(Color));

		public static readonly BindableProperty HintColorProperty =
			BindableProperty.Create(nameof(HintColor), typeof(Color), typeof(Entry), default(Color));

		public static readonly BindableProperty IsUpperCaseProperty =
			BindableProperty.Create(nameof(IsUpperCase), typeof(bool), typeof(Entry), default(bool));

		public static readonly BindableProperty ShouldImageOpacityBeAppliedProperty =
			BindableProperty.Create(nameof(ShouldImageOpacityBeApplied), typeof(bool), typeof(Entry), default(bool));

		public static readonly BindableProperty EntrySessionsProperty =
			BindableProperty.Create(nameof(EntrySessions), typeof(EntrySessionState), typeof(Entry), default(EntrySessionState));

		public static readonly BindableProperty KeystrokesProperty =
			BindableProperty.Create(nameof(Keystrokes), typeof(int), typeof(Entry), default(int));

		public EventHandler BlurHandler;
		public EventHandler<bool> NativeFocused { get; set;}

		public bool IsNativeFocused
		{
			get { return (bool)GetValue(IsNativeFocusedProperty); }
			set { SetValue(IsNativeFocusedProperty, value); }
		}

		public bool ShouldImageOpacityBeApplied
		{
			get { return (bool)GetValue(ShouldImageOpacityBeAppliedProperty); }
			set { SetValue(ShouldImageOpacityBeAppliedProperty, value); }
		}

		public bool IsUpperCase
		{
			get { return (bool)GetValue(IsUpperCaseProperty); }
			set { SetValue(IsUpperCaseProperty, value); }
		}

		public List<DateTime> PasteEvents
		{
			get { return (List<DateTime>)GetValue(PasteEventsProperty); }
			set { SetValue(PasteEventsProperty, value); }
		}

		public bool IsValid
		{
			get { return (bool)GetValue(IsValidProperty); }
			set { SetValue(IsValidProperty, value); }
		}

		public Color LabelColor
		{
			get { return (Color)GetValue(LabelColorProperty); }
			set { SetValue(LabelColorProperty, value); }
		}

		public Color HintColor
		{
			get { return (Color)GetValue(HintColorProperty); }
			set { SetValue(HintColorProperty, value); }
		}

		public string Format
		{
			get { return (string)GetValue(FormatProperty); }
			set { SetValue(FormatProperty, value); }
		}

		public string Error
		{
			get { return (string)GetValue(ErrorProperty); }
			set { SetValue(ErrorProperty, value); }
		}

		public string Digits
		{
			get { return (string)GetValue(DigitsProperty); }
			set { SetValue(DigitsProperty, value); }
		}

		public int MaxLength
		{
			get { return (int)GetValue(MaxLengthProperty); }
			set { SetValue(MaxLengthProperty, value); }
		}

		public EntrySessionState EntrySessions
		{
			get { return (EntrySessionState)GetValue(EntrySessionsProperty); }
			set { SetValue(EntrySessionsProperty, value); }
		}

		public int Keystrokes
		{
			get { return (int)GetValue(KeystrokesProperty); }
			set { SetValue(KeystrokesProperty, value); }
		}

		internal void Blur() => BlurHandler?.Invoke(this, new EventArgs());
	}
}