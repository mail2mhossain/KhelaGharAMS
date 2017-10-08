using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Utility
{
	public static class DateTimeExtensions
	{
		public static DateTime GetFirstDateOfMonth (this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, day: 1);
		}

		public static DateTime GetFirstMomentOfHour (this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, minute: 0, second: 0);
		}

		public static DateTime GetFirstMomentOfPreviousHour (this DateTime dateTime)
		{
			return dateTime.GetFirstMomentOfHour() - new TimeSpan(hours: 1, minutes: 0, seconds: 0);
		}

		public static DateTime GetPreviousDate (this DateTime dateTime)
		{
			return dateTime.Date - new TimeSpan(days: 1, hours: 0, minutes: 0, seconds: 0);
		}

		public static DateTime GetFirstDateOfPreviousMonth (this DateTime dateTime)
		{
			return dateTime.Month == 1
				? new DateTime(dateTime.Year - 1, month: 12, day: 1)
				: new DateTime(dateTime.Year, dateTime.Month - 1, day: 1);
		}

		public static int MonthDifference (this DateTime lValue, DateTime rValue)
		{
			return Math.Abs((lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year));
		}

		public static double DayDifference (this DateTime lvalue, DateTime rvalue)
		{
			return Math.Abs((lvalue - rvalue).TotalDays);
		}
	}
}
