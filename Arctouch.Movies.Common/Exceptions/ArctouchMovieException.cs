using System;

namespace Arctouch.Movies.Common.Exceptions
{
	public class ArctouchMovieException : Exception
	{
		public ArctouchMovieException()
			: base() { }

		public ArctouchMovieException(string message)
			: base(message)
		{ }

		public ArctouchMovieException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}