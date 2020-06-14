using System;

namespace Core
{
	public static class DateTimeExtensions
	{
		private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


		public static long GetMillisecondsSinceUnixEpoch (this DateTime date) 
		{
			DateTime utc = date.ToUniversalTime();
			return (long) (utc - UnixEpoch).TotalMilliseconds;
		}

        public static TimeSpan ToUnixEpoch(this DateTime date)
        {
            return date.ToUniversalTime().Subtract(UnixEpoch);
        }
    }
}

