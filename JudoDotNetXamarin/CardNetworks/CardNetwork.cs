using System;
using System.Reflection;

namespace JudoDotNetXamarin
{
	public enum CardNetwork
	{
		[DisplayName("Unknown")]
		UNKNOWN = 0,
		[DisplayName("Visa")]
		VISA = 1,
		[DisplayName("Mastercard")]
		MASTERCARD = 2,
		[DisplayName("Visa Electron")]
		VISA_ELECTRON = 3,
		[DisplayName("Switch")]
		SWITCH = 4,
		[DisplayName("Solo")]
		SOLO = 5,
		[DisplayName("Laser")]
		LASER = 6,
		[DisplayName("China Union Pay")]
		CHINA_UNION_PAY = 7,
		[DisplayName("AmEx")]
		AMEX = 8,
		[DisplayName("JCB")]
		JCB = 9,
		[DisplayName("Maestro")]
		MAESTRO = 10,
		[DisplayName("Visa Debit")]
		VISA_DEBIT = 11,
	}

	public static class EnumExtensions
	{
		/// <summary>
		/// Retrieve the description on the enum, e.g.
		/// [Description("Bright Pink")]
		/// BrightPink = 2,
		/// Then when you pass in the enum, it will retrieve the description
		/// </summary>
		/// <param name="en">The Enumeration</param>
		/// <returns>A string representing the friendly name</returns>
		public static TAttribute GetAttribute<TAttribute>(this Enum en) where TAttribute : Attribute
		{
			return en.GetType()
					 .GetRuntimeField(en.ToString())
					 .GetCustomAttribute<TAttribute>();
		}
	}
}
