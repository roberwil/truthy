using System.Linq;

namespace Truthy;

public static partial class Gates
{
	/// <summary>
	/// Special 'Or' evaluates if a base term, a, is equal to any other terms.
	/// if you plan to use use it with yor objects, write your "Equals" as it will
	/// serve as basis for comparison.
	/// </summary>
	/// <param name="a">Base terms to evaluate with others</param>
	/// <param name="b">First term of comparison</param>
	/// <param name="terms">Other terms of comparison</param>
	/// <returns>True if 'a' is equal to any other terms; False, if not.</returns>
	public static bool Sor(object a, object b, params object[] terms) =>
		a.Equals(b) || terms.Contains(a);


	/// <summary>
	/// Perform 'Or' logical operation, i.e., operation is true if one of the terms is true
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static bool Or(bool a, bool b, params bool[] terms) =>
		terms.Aggregate(a || b, (current, term) => current || term);

	/// <summary>
	/// Perform 'And' logical operation, i.e., operation is true if all of the terms are true
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static bool And(bool a, bool b, params bool[] terms) =>
		terms.Aggregate(a && b, (current, term) => current && term);

	/// <summary>
	/// Perform the "Not" logical operation, the complement.
	/// </summary>
	/// <param name="term">term to be evaluated</param>
	/// <returns>True if term is false; False, if term is true</returns>
	public static bool Not(bool term) =>
		!term;

	private static bool BaseXor(bool a, bool b) =>
		a != b;

	/// <summary>
	/// Perform 'Xor' logical operation, i.e., operation is true if two terms are different.
	/// Xor is evaluated for every two terms.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static bool Xor(bool a, bool b, params bool[] terms) =>
		terms.Aggregate(BaseXor(a, b), BaseXor);

	/// <summary>
	/// Perform 'Nor' logical operation, i.e., the inverse of 'Or' operation.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static bool Nor(bool a, bool b, params bool[] terms) =>
		Not(Or(a, b, terms));

	/// <summary>
	/// Perform 'Nand' logical operation, i.e., the inverse of 'And' operation.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static bool Nand(bool a, bool b, params bool[] terms) =>
		Not(And(a, b, terms));

	/// <summary>
	/// Perform 'Xnor' logical operation, i.e., the inverse of 'Xor' operation.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static bool Xnor(bool a, bool b, params bool[] terms) =>
		Not(Xor(a, b, terms));
}
