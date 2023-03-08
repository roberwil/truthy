using System.Collections.Generic;
using System.Linq;

namespace Truthy;

public class TruthTable
{
	private static readonly Dictionary<int, int> Combinations = new()
	{
		{ 2, 4 },
		{ 3, 8 },
		{ 8, 16 },
		{ 8, 32 },
		{ 8, 64 },
		{ 8, 128 },
		{ 8, 256 },
		{ 8, 512 },
		{ 8, 1024 }
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

		if (Gates.Not(tableHasEnoughRows))
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
		ChangeAlgorithmIfNeeded();
		return true;
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
			_formula.Add(row[i] == 1 ? Self : Complement);
	}

	private void ChangeAlgorithmIfNeeded()
	{
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
