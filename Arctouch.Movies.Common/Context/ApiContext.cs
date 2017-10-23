namespace Arctouch.Movies.Common.Context
{
	/// <summary>
	/// Holds global configuration for API
	/// </summary>
	public static class ApiContext
	{
		#region Constants
		private const string BASE_API_URL = "https://api.themoviedb.org";

		private const string API_KEY = "1f54bd990f1cdfb230adb312546d765d";

		private const string DEFAULT_LANGUAGE = "en-US";
		#endregion

		#region Properties
		public static string BaseApiUrl
		{
			get
			{
				return BASE_API_URL;
			}
		}

		public static string ApiKey
		{
			get
			{
				return API_KEY;
			}
		}

		public static string Language
		{
			get
			{
				return DEFAULT_LANGUAGE;
			}
		}
		#endregion
	}
}