using System.Linq;

namespace Truthy
{
	public abstract class Gates
	{
		public static bool Or(params bool[] terms) => terms.
			Aggregate(false, (current, term) => current || term);

		public static bool And(params bool[] terms) => terms.
			Aggregate(true, (current, term) => current && term);

		public static bool Not(bool term) =>
			!term;

		public static bool Xor(params bool[] terms)
		{
			var firstTerm = terms.First();

			for (var i = 1; i < terms.Length; i++)
				if (terms[i] != firstTerm)
					return true;

			return false;
		}

		public static bool Nor(params bool[] terms) =>
			Not(Or(terms));

		public static bool Nand(params bool[] terms) =>
			Not(And(terms));

		public static bool Xnor(params bool[] terms) =>
			Not(Xor(terms));
	}
}
