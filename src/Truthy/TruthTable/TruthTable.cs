using System.Collections.Generic;
using System.Linq;

namespace Truthy;

public class TruthTable
{
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

	private readonly int _numberOfCombinations;
	private readonly int _numberOfTerms;

	private const int MaxLimit = 10;
	private const int MinLimit = 2;

	private const char Self = 'S';
	private const char Complement = 'C';
	private readonly List<char> _formula = new();

	private bool _usingSumOfProducts;
	private bool _usingProductOfSums;

	private int NumberOfZeros { get; set; }
	private int NumberOfOnes { get; set; }

	private readonly List<List<int>> _rows = new();

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

		UseSumOfProducts();
	}

	public void AddRow(params int[] terms)
	{
		var tableHasEnoughRows = _rows.Count == _numberOfCombinations;

		if (tableHasEnoughRows)
			throw new TruthyException($"There should be only {_numberOfCombinations} rows.");

		var rightNumberOfTerms = terms.Length == _numberOfTerms + 1;

		if (Gates.Not(rightNumberOfTerms))
			throw new TruthyException($"There should be only {_numberOfTerms + 1} terms.");

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

	public bool Check(params bool[] terms)
	{
		if (terms.Length != _numberOfTerms)
			throw new TruthyException($"There should be only {_numberOfTerms} terms.");

		ChangeAlgorithmIfNeeded();

		var termsCursor = 0;
		var partialResult = true;
		var result = false;

		if (_usingProductOfSums)
		{
			partialResult = false;
			result = true;
		}

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
				result = result.Or(partialResult);
				partialResult = true;
			}
			else
			{
				result = result.And(partialResult);
				partialResult = false;
			}
		}

		return result;
	}

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
