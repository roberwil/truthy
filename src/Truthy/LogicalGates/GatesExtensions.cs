namespace Truthy;

public static class GatesExtensions
{
	public static bool Or(this bool a, bool b, params bool[] terms) =>
		Gates.Or(a, b, terms);

	public static bool Or(this int a, int b, params int[] terms) =>
		Gates.Or(a, b, terms);

	public static bool And(this bool a, bool b, params bool[] terms) =>
		Gates.And(a, b, terms);

	public static bool And(this int a, int b, params int[] terms) =>
		Gates.And(a, b, terms);

	public static bool Not(this bool term) =>
		Gates.Not(term);

	public static bool Not(this int term) =>
		Gates.Not(term);

	public static bool Xor(this bool a, bool b, params bool[] terms) =>
		Gates.Xor(a, b, terms);

	public static bool Xor(this int a, int b, params int[] terms) =>
		Gates.Xor(a, b, terms);

	public static bool Nor(this bool a, bool b, params bool[] terms) =>
		Gates.Nor(a, b, terms);

	public static bool Nor(this int a, int b, params int[] terms) =>
		Gates.Nor(a, b, terms);

	public static bool Nand(this bool a, bool b, params bool[] terms) =>
		Gates.Nand(a, b, terms);

	public static bool Nand(this int a, int b, params int[] terms) =>
		Gates.Nand(a, b, terms);


	public static bool Xnor(this bool a, bool b, params bool[] terms) =>
		Gates.Xnor(a, b, terms);

	public static bool Xnor(this int a, int b, params int[] terms) =>
		Gates.Xnor(a, b, terms);

}
