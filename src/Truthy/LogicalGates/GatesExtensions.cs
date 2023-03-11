namespace Truthy;

public static class GatesExtensions
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
	public static object Sor(this object a, object b, params object[] terms) =>
		Gates.Sor(a, b, terms);

	/// <summary>
	/// Perform 'Or' logical operation, i.e., operation is true if one of the terms is true
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static bool Or(this bool a, bool b, params bool[] terms) =>
		Gates.Or(a, b, terms);

	/// <summary>
	/// Perform 'Or' logical operation, i.e., operation is true if one of the terms is true
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static int Or(this int a, int b, params int[] terms) =>
		Gates.Or(a, b, terms);

	/// <summary>
	/// Perform 'And' logical operation, i.e., operation is true if all of the terms are true
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static bool And(this bool a, bool b, params bool[] terms) =>
		Gates.And(a, b, terms);

	/// <summary>
	/// Perform 'And' logical operation, i.e., operation is true if all of the terms are true
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static int And(this int a, int b, params int[] terms) =>
		Gates.And(a, b, terms);

	/// <summary>
	/// Perform the "Not" logical operation, the complement.
	/// </summary>
	/// <param name="term">term to be evaluated</param>
	/// <returns>True if term is false; False, if term is true</returns>
	public static bool Not(this bool term) =>
		Gates.Not(term);

	/// <summary>
	/// Perform the "Not" logical operation, the complement.
	/// </summary>
	/// <param name="term">term to be evaluated</param>
	/// <returns>True if term is false; False, if term is true</returns>
	public static int Not(this int term) =>
		Gates.Not(term);

	/// <summary>
	/// Perform 'Xor' logical operation, i.e., operation is true if two terms are different.
	/// Xor is evaluated for every two terms.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static bool Xor(this bool a, bool b, params bool[] terms) =>
		Gates.Xor(a, b, terms);

	/// <summary>
	/// Perform 'Xor' logical operation, i.e., operation is true if two terms are different.
	/// Xor is evaluated for every two terms.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static int Xor(this int a, int b, params int[] terms) =>
		Gates.Xor(a, b, terms);

	/// <summary>
	/// Perform 'Nor' logical operation, i.e., the inverse of 'Or' operation.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static bool Nor(this bool a, bool b, params bool[] terms) =>
		Gates.Nor(a, b, terms);

	/// <summary>
	/// Perform 'Nor' logical operation, i.e., the inverse of 'Or' operation.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static int Nor(this int a, int b, params int[] terms) =>
		Gates.Nor(a, b, terms);

	/// <summary>
	/// Perform 'Nand' logical operation, i.e., the inverse of 'And' operation.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static bool Nand(this bool a, bool b, params bool[] terms) =>
		Gates.Nand(a, b, terms);

	/// <summary>
	/// Perform 'Nand' logical operation, i.e., the inverse of 'And' operation.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static int Nand(this int a, int b, params int[] terms) =>
		Gates.Nand(a, b, terms);

	/// <summary>
	/// Perform 'Xnor' logical operation, i.e., the inverse of 'Xor' operation.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static bool Xnor(this bool a, bool b, params bool[] terms) =>
		Gates.Xnor(a, b, terms);

	/// <summary>
	/// Perform 'Xnor' logical operation, i.e., the inverse of 'Xor' operation.
	/// </summary>
	/// <param name="a">first term</param>
	/// <param name="b">second term</param>
	/// <param name="terms">other terms</param>
	/// <returns>Boolean value of the operation</returns>
	public static int Xnor(this int a, int b, params int[] terms) =>
		Gates.Xnor(a, b, terms);

}
