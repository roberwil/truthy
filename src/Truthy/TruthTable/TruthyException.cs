using System;

namespace Truthy;

public class TruthyException : Exception
{
	public TruthyException(string message) : base(message) { }
}
