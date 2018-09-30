using System;

namespace SharpLynn {
	public class Utils {
		public static DateTime UnixTimeStampToDateTime(double unixTimeStamp) {
			DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dtDateTime;
		}

		public static bool LowerEquals(string a, string b) {
			return (a.ToLower() == b.ToLower());
		}
	}
}
