using System.Linq;
namespace Truthy;

public static partial class Gates
{
	public static bool Or(bool a, bool b, params bool[] terms) =>
		terms.Aggregate(a || b, (current, term) => current || term);

	public static bool And(bool a, bool b, params bool[] terms) =>
		terms.Aggregate(a && b, (current, term) => current && term);

	public static bool Not(bool term) =>
		!term;

	public static bool Xor(bool a, bool b, params bool[] terms) =>
		a != b || terms.Any(t => t != a);

	public static bool Nor(bool a, bool b, params bool[] terms) =>
		Not(Or(a, b, terms));

	public static bool Nand(bool a, bool b, params bool[] terms) =>
		Not(And(a, b, terms));

	public static bool Xnor(bool a, bool b, params bool[] terms) =>
		Not(Xor(a, b, terms));

}
