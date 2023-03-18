using System;
using System.Collections.Generic;
using System.Linq;
// Custom type
using Row = System.Collections.Generic.List<int>;

namespace Truthy;

public class TruthTable
{
	// Pre-calculated number of combinations for n terms
	private static readonly Dictionary<int, int> Combinations = new()
	{
		{ 2, 4 },
		{ 3, 8 },
		{ 4, 16 },
		{ 5, 32 },
		{ 6, 64 },
		{ 7, 128 },
		{ 8, 256 },
		{ 9, 512 },
		{ 10, 1024 }
	};

	// Cache
	private readonly Dictionary<string, bool> _cache = new();

	// Letters to parse formula
	private static readonly List<char> Letters = new()
	{
		'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J'
	};

	// The amount of possible combinations  with n terms: 2^n combinations
	private readonly int _numberOfCombinations;

	// The number of terms of a truth table
	private readonly int _numberOfTerms;

	// Limits for the amount of terms in a truth table
	private const int MaxLimit = 6;
	private const int MinLimit = 2;

	// Constants to build formula
	private const char Self = 'S';
	private const char Complement = 'C';
	private readonly List<char> _formula = new();

	// Flags to control which algorithm to use
	private bool _usingSumOfProducts;
	private bool _usingProductOfSums;
	private int NumberOfZeros { get; set; }
	private int NumberOfOnes { get; set; }

	// The rows of a truth table with n + 1 elements, where n is the number of terms and +1 is the result of the row
	private readonly List<Row> _rows = new();

	public TruthTable(int numberOfTerms)
	{
		switch (numberOfTerms)
		{
			case > MaxLimit:
				throw new TruthyException($"The maximum number of terms is {MaxLimit}.");
			case < MinLimit:
				throw new TruthyException($"The minimum number of terms is {MinLimit}.");
			default:
				_numberOfTerms = numberOfTerms;
				_numberOfCombinations = Combinations[numberOfTerms];
				break;
		}

		// Per default, the engine uses Sum of products
		UseSumOfProducts();
	}

	/// <summary>
	/// Add a row to the truth table. If the truth table is of n terms, then, the row must have n + 1 elements,
	/// where +1 is the result of the row.
	/// </summary>
	/// <param name="terms">The terms of the row and their result</param>
	/// <exception cref="TruthyException"> Check the related message to debug the issue.</exception>
	public void AddRow(params int[] terms)
	{
		var tableHasEnoughRows = _rows.Count == _numberOfCombinations;

		if (tableHasEnoughRows)
			throw new TruthyException($"There should be only {_numberOfCombinations} rows.");

		var rightNumberOfTerms = terms.Length == _numberOfTerms + 1;

		if (Gates.Not(rightNumberOfTerms))
			throw new TruthyException($"There should be only {_numberOfTerms + 1} terms.");

		_cache.Clear();

		var row = terms.ToList();

		if (Gates.Not(RowIsValid(row)))
			throw new TruthyException($"The combination [{row}] has been used already.");

		var rowOutput = row[^1];
		var changeAlgorithm = (_rows.Count == 0).And(rowOutput == 0);

		if (changeAlgorithm)
			UseProductsOfSums();

		_rows.Add(row);

		if (rowOutput == 1)
			NumberOfOnes++;
		else
			NumberOfZeros++;

		Compute(row);
	}

	/// <summary>
	/// Check if booleans match the truth table definition. It accepts n terms, where n is the number of
	/// terms of the truth table.
	/// </summary>
	/// <param name="terms"></param>
	/// <returns></returns>
	/// <exception cref="TruthyException"></exception>
	public bool Check(params bool[] terms)
	{
		var hasRightNumberOfTerms = terms.Length != _numberOfTerms;

		if (hasRightNumberOfTerms)
			throw new TruthyException($"There should be only {_numberOfTerms} terms.");

		ChangeAlgorithmIfNeeded();

		// Try to get result from Cache
		var cacheCode = GetRowCacheCode(terms);

		if (_cache.TryGetValue(cacheCode, out var cacheResult))
			return cacheResult;

		// No cached result, so, tet us find out and then cache it
		var termsCursor = 0;
		var partialResult = true;
		var result = false;

		if (_usingProductOfSums)
		{
			partialResult = false;
			result = true;
		}

		// Evaluate every term with the formula, depending on the algorithm being used
		foreach (var t in _formula)
		{
			if (_usingSumOfProducts)
				partialResult = t == Self
					? partialResult.And(terms[termsCursor])
					: partialResult.And(!terms[termsCursor]);
			else
				partialResult = t == Self
					? partialResult.Or(terms[termsCursor])
					: partialResult.Or(!terms[termsCursor]);

			termsCursor++;

			if (termsCursor != _numberOfTerms)
				continue;

			termsCursor = 0;

			if (_usingSumOfProducts)
			{
				// Since it is "OR" gate, if one is true, then the rest is
				if (partialResult) return true;
				// ... search continues
				result = result.Or(partialResult);
				partialResult = true;
			}
			else
			{
				// Since it is "AND" gate, if one is false, the whole opeartion is
				if (!partialResult) return false;
				// ... search continues
				result = result.And(partialResult);
				partialResult = false;
			}
		}

		// Cache the result
		_cache.Add(cacheCode, result);
		return result;
	}

	private static string GetRowCacheCode(params bool[] terms) =>
		 terms.Aggregate(string.Empty, (current, term) => current + (term ? "T" : "F"));

	/// <summary>
	/// Computes the formula equivalent in string.
	/// </summary>
	/// <returns>The formula you would use to compute on your own, raw!</returns>
	public override string ToString()
	{
		var termsCursor = 0;
		var partialResult = string.Empty;
		var result = string.Empty;


		// Evaluate every term with the formula, depending on the algorithm being used
		foreach (var t in _formula)
		{
			if (_usingSumOfProducts)
				partialResult += t == Self
					? $"{Letters[termsCursor]}."
					: $"~{Letters[termsCursor]}.";
			else
				partialResult += t == Self
					? $"{Letters[termsCursor]}+"
					: $"~{Letters[termsCursor]}+";

			termsCursor++;

			if (termsCursor != _numberOfTerms)
				continue;

			termsCursor = 0;

			result += _usingSumOfProducts
				? $"({partialResult[..^1]})+"
				: $"({partialResult[..^1]}).";

			partialResult = string.Empty;
		}

		return result[..^1];
	}

	/// <summary>
	/// A row is valid if no ther row equal to itself was added already
	/// </summary>
	/// <param name="row">The rwo to be checked</param>
	/// <returns>True if row is valid; False toherwise</returns>
	private bool RowIsValid(IReadOnlyList<int> row)
	{
		foreach (var existingRow in _rows)
		{
			var isEqual = true;

			for (var i = 0; i < existingRow.Count - 1; i++)
				isEqual = isEqual.And(row[i] == existingRow[i]);

			if (isEqual)
				return false;
		}

		return true;
	}

	/// <summary>
	/// Evaluates a row to extract part of the formula depending on the algorithm
	/// </summary>
	/// <param name="row">The row to be evaluated</param>
	private void Compute(List<int> row)
	{
		var rowOutput = row[^1];

		if (_usingSumOfProducts.And(rowOutput != 1))
			return;

		if (_usingProductOfSums.And(rowOutput != 0))
			return;

		for (var i = 0; i < row.Count - 1; i++)
		{
			if (_usingSumOfProducts)
				_formula.Add(row[i] == 1 ? Self : Complement);
			else
				_formula.Add(row[i] == 1 ? Complement : Self);
		}
	}

	private void ChangeAlgorithmIfNeeded()
	{
		if (_usingSumOfProducts.And(NumberOfZeros == 0))
			return;

		if (_usingProductOfSums.And(NumberOfOnes == 0))
			return;

		var moreZeros = NumberOfZeros >= NumberOfOnes;

		if (_usingSumOfProducts.And(moreZeros))
			return;

		if (_usingProductOfSums.And(!moreZeros))
			return;

		if (moreZeros)
			UseSumOfProducts();
		else
			UseProductsOfSums();

		_formula.Clear();
		_cache.Clear();

		foreach (var row in _rows)
			Compute(row);
	}

	private void UseSumOfProducts()
	{
		_usingSumOfProducts = true;
		_usingProductOfSums = false;
	}

	private void UseProductsOfSums()
	{
		_usingSumOfProducts = false;
		_usingProductOfSums = true;
	}
}
