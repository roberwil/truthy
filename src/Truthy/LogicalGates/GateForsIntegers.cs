using System.Linq;
namespace Truthy;

public static partial class Gates
{
	/// <summary>
	/// Perform 'Or' logical operation, i.e., operation is true if one of the terms is true
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static int Or(int a, int b, params int[] terms) =>
		terms.Aggregate(a | b, (current, term) => current | term);

	/// <summary>
	/// Perform 'And' logical operation, i.e., operation is true if all of the terms are true
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static int And(int a, int b, params int[] terms) =>
		terms.Aggregate(a & b, (current, term) => current & term);

	/// <summary>
	/// Perform the "Not" logical operation, the complement.
	/// </summary>
	/// <param name="term">term to be evaluated</param>
	/// <returns>True if term is false; False, if term is true</returns>
	public static int Not(int term) =>
		term == 1 ? 0 : 1;

	private static int BaseXor(int a, int b) =>
		a != b ? 1 : 0;

	/// <summary>
	/// Perform 'Xor' logical operation, i.e., operation is true if two terms are different.
	/// Xor is evaluated for every two terms.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static int Xor(int a, int b, params int[] terms) =>
		terms.Aggregate(BaseXor(a, b), BaseXor);

	/// <summary>
	/// Perform 'Nor' logical operation, i.e., the inverse of 'Or' operation.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static int Nor(int a, int b, params int[] terms) =>
		Not(Or(a, b, terms));

	/// <summary>
	/// Perform 'Nor' logical operation, i.e., the inverse of 'Or' operation.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static int Nand(int a, int b, params int[] terms) =>
		Not(And(a, b, terms));

	/// <summary>
	/// Perform 'Nand' logical operation, i.e., the inverse of 'And' operation.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static int Xnor(int a, int b, params int[] terms) =>
		Not(Xor(a, b, terms));

}
