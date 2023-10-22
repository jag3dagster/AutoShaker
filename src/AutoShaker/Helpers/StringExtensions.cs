namespace AutoShaker.Helpers
{
	public static class StringExtensions
	{
		public static bool IsNullOrEmpty(this string str)
		{
			return string.IsNullOrEmpty(str);
		}

		public static bool IEquals(this string str, string str2)
		{
			return string.Equals(str, str2, System.StringComparison.OrdinalIgnoreCase);
		}
	}
}
