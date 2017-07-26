using System;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{
	public class JudoPicker : Picker 
	{  
		public static readonly BindableProperty LabelColorProperty =
			BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(Entry), default(Color));

		public static readonly BindableProperty HintColorProperty =
			BindableProperty.Create(nameof(HintColor), typeof(Color), typeof(Entry), default(Color));

		public static readonly BindableProperty LabelProperty =
			BindableProperty.Create(nameof(Label), typeof(string), typeof(Entry), default(string));

		public Color LabelColor
		{
			get { return (Color)GetValue(LabelColorProperty); }
			set { SetValue(LabelColorProperty, value); }
		}

		public string Label
		{
			get { return (string)GetValue(LabelProperty); }
			set { SetValue(LabelProperty, value); }
		}

		public Color HintColor
		{
			get { return (Color)GetValue(HintColorProperty); }
			set { SetValue(HintColorProperty, value); }
		}
	}
}