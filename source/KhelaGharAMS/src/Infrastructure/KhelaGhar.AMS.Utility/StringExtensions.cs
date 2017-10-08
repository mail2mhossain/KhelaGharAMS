using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Utility
{
	public static class StringExtensions
	{
		#region Methods

		public static string Left (this string str, int length)
		{
			str = (str ?? string.Empty);
			return str.Substring(0, Math.Min(length, str.Length));
		}

		public static string Right (this string str, int length)
		{
			str = (str ?? string.Empty);
			return (str.Length >= length)
				? str.Substring(str.Length - length, length)
				: str;
		}

		#endregion
	}
}
