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

	private readonly bool _usingSumOfProducts;
	private readonly bool _usingProductOfSums;

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

		_usingSumOfProducts = true;
		_usingProductOfSums = false;
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

		_rows.Add(row);

		if (row[^1] == 1)
			NumberOfOnes++;
		else
			NumberOfZeros++;

		Compute(row);
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
		var result = row[^1];

		if (_usingSumOfProducts.And(result != 1))
			return;

		if (_usingProductOfSums.And(result != 0))
			return;

		for (var i = 0; i < row.Count - 1; i++)
			_formula.Add(row[i] == 1 ? Self : Complement);
	}
}
