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

	private readonly List<List<int>> _rows = new ();

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
	}

	public void AddRow(params int[] terms)
	{
		var tableHasEnoughRows = _rows.Count == _numberOfCombinations;

		if (Gates.Not(tableHasEnoughRows))
			throw new TruthyException($"There should be only {_numberOfCombinations} rows.");

		var rightNumberOfTerms = (terms.Length >= MinLimit).
			And(terms.Length == _numberOfTerms);

		if (Gates.Not(rightNumberOfTerms))
			throw new TruthyException($"There should be only {_numberOfTerms} terms.");

		var row = terms.ToList();

		if (Gates.Not(RowIsValid(row)))
			throw new TruthyException($"The row {row} has been added already.");

		_rows.Add(row);
	}

	private bool RowIsValid(List<int> row)
	{
		foreach (var r in _rows)
		{
			var isEqual = false;

			for (var i = 0; i < r.Count; i++)
			{

			}
		}

		return true;
	}

}
