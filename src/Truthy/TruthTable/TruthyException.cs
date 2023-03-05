using System;

namespace Truthy;

public class TruthyException : Exception
{
	public TruthyException() : base() { }
	public TruthyException(string message) : base(message) { }
	public TruthyException(string message, Exception innerException) : base(message, innerException) { }
}
