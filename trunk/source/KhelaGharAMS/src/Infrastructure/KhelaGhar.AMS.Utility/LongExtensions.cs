using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Utility
{
	public static class LongExtensions
	{
		public static string LongToStringWithLeftPad (this long number, int totalWidth)
		{
			return number.ToString().PadLeft(totalWidth, '0');
		}
	}
}
