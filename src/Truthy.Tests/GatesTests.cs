namespace Truthy.Tests;

[TestClass]
public class GatesTests
{
	[TestMethod]
	[DataRow(1, 1, 2, 4, true)]
	[DataRow(7, 1, 2, 4, false)]
	[DataRow("hello", "xpto", "xxy", "again", false)]
	[DataRow("xxy", "xpto", "xxy", "again", true)]
	[DataRow('A', 'B', 'C', 'D', false)]
	[DataRow('B', 'B', 'C', 'D', true)]
	[DataRow(100.2, 19.2, 12.1, 11.33, false)]
	[DataRow(11.33, 19.2, 12.1, 11.33, true)]
	[DataRow(true, false, false, false, false)]
	[DataRow(true, true, false, false, true)]
	public void SpecialOrIsValid(object a, object b, object c, object d, bool expected)
	{
		var actual = a.Sor(b, c, d);
		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[DataRow(true, true, true)]
	[DataRow(true, false, true)]
	[DataRow(false, true, true)]
	[DataRow(false, false, false)]
	public void OrIsValid(bool a, bool b, bool expected)
	{
		var actual = Gates.Or(a, b);
		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[DataRow(1, 1, 1, 1)]
	[DataRow(1, 1, 0, 1)]
	[DataRow(1, 0, 1, 1)]
	[DataRow(1, 0, 0, 1)]
	[DataRow(0, 1, 1, 1)]
	[DataRow(0, 1, 0, 1)]
	[DataRow(0, 0, 1, 1)]
	[DataRow(0, 0, 0, 0)]
	public void OrIsValidWith3Params(int a, int b, int c, int expected)
	{
		var actual = Gates.Or(a, b, c);
		Assert.AreEqual(expected , actual);
	}

	[TestMethod]
	[DataRow(true, true, true)]
	[DataRow(true, false, false)]
	[DataRow(false, true, false)]
	[DataRow(false, false, false)]
	public void AndIsValid(bool a, bool b, bool expected)
	{
		var actual = Gates.And(a, b);
		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[DataRow(1, 1, 1, 1)]
	[DataRow(1, 1, 0, 0)]
	[DataRow(1, 0, 1, 0)]
	[DataRow(1, 0, 0, 0)]
	[DataRow(0, 1, 1, 0)]
	[DataRow(0, 1, 0, 0)]
	[DataRow(0, 0, 1, 0)]
	[DataRow(0, 0, 0, 0)]
	public void AndIsValidWith3Params(int a, int b, int c, int expected)
	{
		var actual = Gates.And(a, b, c);
		Assert.AreEqual(expected , actual);
	}

	[TestMethod]
	[DataRow(true, false)]
	[DataRow(false, true)]
	public void NotIsValid(bool a, bool expected)
	{
		var actual = Gates.Not(a);
		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[DataRow(true, true, false)]
	[DataRow(true, false, true)]
	[DataRow(false, true, true)]
	[DataRow(false, false, false)]
	public void XorIsValid(bool a, bool b, bool expected)
	{
		var actual = Gates.Xor(a, b);
		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[DataRow(1, 1, 1, 1)]
	[DataRow(1, 1, 0, 0)]
	[DataRow(1, 0, 1, 0)]
	[DataRow(1, 0, 0, 1)]
	[DataRow(0, 1, 1, 0)]
	[DataRow(0, 1, 0, 1)]
	[DataRow(0, 0, 1, 1)]
	[DataRow(0, 0, 0, 0)]
	public void XorIsValidWith3Params(int a, int b, int c, int expected)
	{
		var actual = Gates.Xor(a, b, c);
		Assert.AreEqual(expected , actual);
	}

	[TestMethod]
	[DataRow(true, true, false)]
	[DataRow(true, false, false)]
	[DataRow(false, true, false)]
	[DataRow(false, false, true)]
	public void NorIsValid(bool a, bool b, bool expected)
	{
		var actual = Gates.Nor(a, b);
		Assert.AreEqual(expected, actual);
	}


	[TestMethod]
	[DataRow(1, 1, 1, 0)]
	[DataRow(1, 1, 0, 0)]
	[DataRow(1, 0, 1, 0)]
	[DataRow(1, 0, 0, 0)]
	[DataRow(0, 1, 1, 0)]
	[DataRow(0, 1, 0, 0)]
	[DataRow(0, 0, 1, 0)]
	[DataRow(0, 0, 0, 1)]
	public void NorIsValidWith3Params(int a, int b, int c, int expected)
	{
		var actual = Gates.Nor(a, b, c);
		Assert.AreEqual(expected , actual);
	}

	[TestMethod]
	[DataRow(true, true, false)]
	[DataRow(true, false, true)]
	[DataRow(false, true, true)]
	[DataRow(false, false, true)]
	public void NandIsValid(bool a, bool b, bool expected)
	{
		var actual = Gates.Nand(a, b);
		Assert.AreEqual(expected, actual);
	}


	[TestMethod]
	[DataRow(1, 1, 1, 0)]
	[DataRow(1, 1, 0, 1)]
	[DataRow(1, 0, 1, 1)]
	[DataRow(1, 0, 0, 1)]
	[DataRow(0, 1, 1, 1)]
	[DataRow(0, 1, 0, 1)]
	[DataRow(0, 0, 1, 1)]
	[DataRow(0, 0, 0, 1)]
	public void NandIsValidWith3Params(int a, int b, int c, int expected)
	{
		var actual = Gates.Nand(a, b, c);
		Assert.AreEqual(expected , actual);
	}

	[TestMethod]
	[DataRow(true, true, true)]
	[DataRow(true, false, false)]
	[DataRow(false, true, false)]
	[DataRow(false, false, true)]
	public void XnorIsValid(bool a, bool b, bool expected)
	{
		var actual = Gates.Xnor(a, b);
		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[DataRow(1, 1, 1, 0)]
	[DataRow(1, 1, 0, 1)]
	[DataRow(1, 0, 1, 1)]
	[DataRow(1, 0, 0, 0)]
	[DataRow(0, 1, 1, 1)]
	[DataRow(0, 1, 0, 0)]
	[DataRow(0, 0, 1, 0)]
	[DataRow(0, 0, 0, 1)]
	public void XnorIsValidWith3Params(int a, int b, int c, int expected)
	{
		var actual = Gates.Xnor(a, b, c);
		Assert.AreEqual(expected , actual);
	}
}
