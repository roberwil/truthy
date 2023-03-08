namespace Truthy.Tests;

[TestClass]
public class TruthTableTests
{
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
	[DataRow(true, true, true)]
	public void CheckingTheRightNumberOfArguments(bool a, bool b, bool c)
	{
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
