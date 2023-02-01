namespace DoMe.Domain.Exceptions;

internal sealed class UnsupportedColourException : Exception
{
	public UnsupportedColourException(string code) : 
		base($"Colour \"{code}\" is unsupported.") { }
}