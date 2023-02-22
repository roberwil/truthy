
public static class GatesExtensions
{
	public static bool Or(this bool a, bool b, params bool[] terms) =>
		Gates.Or(a, b, terms);

	public static bool And(this bool a, bool b, params bool[] terms) =>
		Gates.And(a, b, terms);

	public static bool Not(this bool term) =>
		Gates.Not(term);

	public static bool Xor(this bool a, bool b, params bool[] terms) =>
		Gates.Xor(a, b, terms);


}
