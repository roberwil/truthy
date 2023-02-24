using System.Linq;
namespace Truthy;

public static partial class Gates
{
	public static int Or(int a, int b, params int[] terms) =>
		terms.Aggregate(a | b, (current, term) => current | term);

	public static int And(int a, int b, params int[] terms) =>
		terms.Aggregate(a & b, (current, term) => current & term);

	public static int Not(int term) =>
		term == 1 ? 0 : 1;

	public static int Xor(int a, int b, params int[] terms) =>
		(a != b) | terms.Any(t => t != a)? 1 : 0;

	public static int Nor(int a, int b, params int[] terms) =>
		Not(Or(a, b, terms));

	public static int Nand(int a, int b, params int[] terms) =>
		Not(And(a, b, terms));

	public static int Xnor(int a, int b, params int[] terms) =>
		Not(Xor(a, b, terms));

}
