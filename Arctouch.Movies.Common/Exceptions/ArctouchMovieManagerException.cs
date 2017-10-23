using System;

namespace Arctouch.Movies.Common.Exceptions
{
	public class ArctouchMovieManagerException : ArctouchMovieException
	{
		public ArctouchMovieManagerException()
			: base() { }

		public ArctouchMovieManagerException(string message)
			: base(message)
		{ }

		public ArctouchMovieManagerException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}