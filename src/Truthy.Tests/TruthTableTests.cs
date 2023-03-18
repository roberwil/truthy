namespace Truthy.Tests;

[TestClass]
public class TruthTableTests
{
	/*[TestMethod]
	[DataRow(true, true, "TT")]
	[DataRow(true, false, "TF")]
	[DataRow(false, false, "FF")]
	[DataRow(false, true, "FT")]
	public void RowCacheCodeIsValid(bool a, bool b, string cacheCode)
	{
		var actualCacheCode = TruthTable.GetRowCacheCode(a, b);
		Assert.AreEqual(cacheCode, actualCacheCode);
	}*/

	[TestMethod]
	public void TruthTableWith1RowHasRightFormula()
	{
		var t = new TruthTable(2);
		t.AddRow(1, 1, 0);

		const string expected = "(~A+~B)";
		var actual = t.ToString();

		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	public void TruthTableWith2RowsHasRightFormula()
	{
		var t = new TruthTable(2);
		t.AddRow(1, 1, 1);
		t.AddRow(1, 0, 1);

		const string expected = "(A.B)+(A.~B)";
		var actual = t.ToString();

		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[DataRow(true, true, false)]
	[DataRow(true, false, true)]
	[DataRow(false, true, true)]
	[DataRow(false, false, true)]
	public void TruthTableOfTwoIsValid(bool a, bool b, bool expected)
	{
		var t = new TruthTable(2);
		t.AddRow(1, 1, 0);

		var actual = t.Check(a, b);

		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[DataRow(true, true, false)]
	[DataRow(true, false, false)]
	[DataRow(false, true, true)]
	[DataRow(false, false, true)]
	public void TruthTableOfTwoWith4RowsIsValid(bool a, bool b, bool expected)
	{
		var t = new TruthTable(2);
		t.AddRow(1, 1, 0);
		t.AddRow(1, 0, 0);
		t.AddRow(0, 1, 1);
		t.AddRow(0, 0, 1);

		var actual = t.Check(a, b);

		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[DataRow(true, true, false)]
	[DataRow(true, false, false)]
	[DataRow(false, true, true)]
	[DataRow(false, false, true)]
	public void TruthTableOfTwoWith4RowsIsValidWithCache(bool a, bool b, bool expected)
	{
		var t = new TruthTable(2);
		t.AddRow(1, 1, 0);
		t.AddRow(1, 0, 0);
		t.AddRow(0, 1, 1);
		t.AddRow(0, 0, 1);

		var actual = t.Check(a, b);
		actual = t.Check(a, b); //gets result from cache
		actual = t.Check(a, b); //gets result from cache
		actual = t.Check(a, b); //gets result from cache
		actual = t.Check(a, b); //gets result from cache

		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[DataRow(true, true, false, true)]
	[DataRow(true, false, true, false)]
	[DataRow(false, true, true, false)]
	[DataRow(false, false, true, false)]
	public void TruthTableOfThreeIsValid(bool a, bool b, bool c, bool expected)
	{
		var t = new TruthTable(3);
		t.AddRow(1, 1, 0, 1);

		var actual = t.Check(a, b, c);

		Assert.AreEqual(expected, actual);
	}


	// Things that throw exceptions
	[TestMethod]
	[ExpectedException(typeof(TruthyException))]
	public void EqualRowsShouldNotBeAdded()
	{
		var t = new TruthTable(2);
		t.AddRow(1, 1, 0);
		t.AddRow(1, 1, 0);
	}

	[TestMethod]
	[ExpectedException(typeof(TruthyException))]
	public void InvalidRowsShouldNotBeAdded()
	{
		var t = new TruthTable(2);
		t.AddRow(1, 1, 0, 1);
	}

	[TestMethod]
	[ExpectedException(typeof(TruthyException))]
	public void IfEnoughRowsDontAddMoreRows()
	{
		var t = new TruthTable(2);
		t.AddRow(1, 1, 0);
		t.AddRow(1, 0, 0);
		t.AddRow(0, 1, 0);
		t.AddRow(0, 0, 1);
		t.AddRow(1, 1, 0);
	}


	[TestMethod]
	[ExpectedException(typeof(TruthyException))]
	public void CheckingTheRightNumberOfArguments()
	{
		const bool a = true;
		const bool b = true;
		const bool c = true;

		var t = new TruthTable(2);

		t.AddRow(1, 1, 0);
		t.Check(a, b, c);
	}

	[TestMethod]
	[ExpectedException(typeof(TruthyException))]
	[DataRow(1)]
	[DataRow(11)]
	[DataRow(12)]
	public void TruthTableIsInvalid(int numberOfTerms)
	{
		var t = new TruthTable(numberOfTerms);
	}
}
