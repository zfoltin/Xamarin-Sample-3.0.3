using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JudoPayDotNet.Models;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{

	public static class ThemeExtensions
	{ 
		public static void SetThemeProperty<TView>(this TView view, Expression<Func<TView, Color>> viewColor, Color themeColor) where TView : View
		{
			var memberSelectorExpression = viewColor.Body as MemberExpression;
			if (memberSelectorExpression != null)
			{
				var property = memberSelectorExpression.Member as PropertyInfo;
				if (property != null)
				{
					if (themeColor != Color.Default)
					{
						property.SetValue(view, themeColor, null);
					}
				}
			}
		}
	}
}
