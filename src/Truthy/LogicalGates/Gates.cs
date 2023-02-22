using System.Linq;

public static class Gates
{
	public static bool Or(bool a, bool b, params bool[] terms) =>
		terms.Aggregate(a || b, (current, term) => current || term);

	public static bool Or(int a, int b, params int[] terms) =>
		Or(a.Bool(), b.Bool(), terms.Select(t => t.Bool()).ToArray());

	public static bool And(bool a, bool b, params bool[] terms) =>
		terms.Aggregate(a && b, (current, term) => current && term);

	public static bool And(int a, int b, params int[] terms) =>
		And(a.Bool(), b.Bool(), terms.Select(t => t.Bool()).ToArray());

	public static bool Not(bool term) =>
		!term;

	public static bool Not(int term) =>
		Not(term.Bool());

	public static bool Xor(bool a, bool b, params bool[] terms) =>
		 a != b || terms.Any(t => t != a);

	public static bool Xor(int a, int b, params int[] terms) =>
		Xor(a.Bool(), b.Bool(), terms.Select(t => t.Bool()).ToArray());

	public static bool Nor(bool a, bool b, params bool[] terms) =>
		Not(Or(a, b, terms));

	public static bool Nor(int a, int b, params int[] terms) =>
		Nor(a.Bool(), b.Bool(), terms.Select(t => t.Bool()).ToArray());

	public static bool Nand(bool a, bool b, params bool[] terms) =>
		Not(And(a, b, terms));

	public static bool Nand(int a, int b, params int[] terms) =>
		Nand(a.Bool(), b.Bool(), terms.Select(t => t.Bool()).ToArray());

	public static bool Xnor(bool a, bool b, params bool[] terms) =>
		Not(Xor(a, b, terms));

	public static bool Xnor(int a, int b, params int[] terms) =>
		Xnor(a.Bool(), b.Bool(), terms.Select(t => t.Bool()).ToArray());

	public static bool Bool(this int n) =>
		n >= 1;
}
