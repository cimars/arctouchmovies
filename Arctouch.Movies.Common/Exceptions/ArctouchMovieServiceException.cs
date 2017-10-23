using System;

namespace Arctouch.Movies.Common.Exceptions
{
	public class ArctouchMovieServiceException : ArctouchMovieException
	{
		public ArctouchMovieServiceException()
			: base() { }

		public ArctouchMovieServiceException(string message)
			: base(message)
		{ }

		public ArctouchMovieServiceException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}