# Truthy

### What is truthy anyway?

Well, think of truth table you just invented, you put your 1s and 0s and **Truthy** figures out which formula works
for the table.

| A   | B   | C   |
|-----|-----|-----|
| 0   | 0   | 1   |
| 1   | 0   | 1   |
| 1   | 1   | 0   |
| 0   | 1   | 0   |

If you create the above table with **Truthy**, it will know what to do so that your table makes sense.
Also, **Truthy** has implementations for the following logical gates: `and`, `or`, `not`,
`xor`, `nand`, `nor`, `xnor` and a special `or`.

### Where can I use it?

- Access matrices are a good example;
- Other examples are just day-to-day comparisons you do with booleans in your code.

Let your imagination make good use of Truthy.

### Usage

Every functionality is under `Truthy` namespace. Some methods throw an exception, the `TruthyException`.

#### Truth Table

To create a truth table, you do as follows:

```csharp
var t = new TruthTable(2);
```

2 is the number of terms in the truth table. The **minimum** number of terms is **2** and the **maximum** is **6**.
If you violate any of the limits, `TruthyException` will be raised.

To add a new row, you do as follows:

```csharp
var t = new TruthTable(2);
t.AddRow(1, 1, 0);
```

For a truth table of **n** terms, the rows take **n+1** terms, where **+1** is the result of the operation for such row.
`TruthyException` will be raised if:

- There are already enough rows for the defined truth table;
- You try to add equal rows;
- You try to add a row with **m** terms, where **m > n + 1**.

After you have defined your truth table and added your rows, you can check boolean values against it as follows:

```csharp
var t = new TruthTable(2);
t.AddRow(1, 1, 0);

t.Check(2+2==4, 3+3==6) // > False
t.Check(2+2==1, 3+3==6) // > True
```
For a truth table of **n** terms, you check **n** terms.

Beware of the behaviour of Truthy, see the following example to understand.

```csharp
var t = new TruthTable(2);
t.AddRow(1, 1, 0); // the other combinations will evaluate to True
t.Check(true, false) // > True

var u = new TruthTable(2);
u.AddRow(1, 1, 0);
u.AddRow(1, 0, 1);

u.Check(true, true) // > False
u.Check(true, false) // > True

// the other combinations will evaluate to True
u.Check(false, false) // > True
u.Check(false, true) // > True


var w = new TruthTable(2);
w.AddRow(1, 1, 0);
w.AddRow(1, 0, 0);
w.AddRow(0, 1, 1);

w.Check(true, true) // > False
w.Check(true, false) // > False
w.Check(false, true) // > True

// the other combinations will evaluate to False
u.Check(false, false) // > False
```

The tricks to master it are:
- The first row you add is determinant, take **table t**, if the first row was equal to 1, the the rest would be False;
- if there are more 0s than 1s explicitly (like **table w**), every combination that equals 0 and others not specified will evaluate to 0, which means, it could be simplified;
- if there are more 1s than 0s explicitly, every combination that equals 1 and others not specified will evaluate to 1.

At last, if you wish to know the formula, just call `.ToString()`

```csharp
var t = new TruthTable(2);
t.AddRow(1, 1, 0); // the other combinations will evaluate to True
t.Check(true, false) // > True

t.ToString() // > (~A+~B)
```

#### Logical Gates

The logical gates are all found in the `Gates` class, but there are also extension methods for booleans.
All operations support multiple terms and accept a minimum of 2 terms, except `.Not()` which accepts only 1 term.

`.And` is equivalent to `&&`. Operation is `True` if every term is `True`.

```csharp
var a = (2+2==4)
var b = (2+1==3)
var c = (2+1==5)

a.And(b) // > True
a.And(b, c) // > False
```

`.Or` is equivalent to `||`. Operation is `True` if any term is `True`.

```csharp
var a = (2+2==4)
var b = (2+1==3)
var c = (2+1==5)

a.Or(b) // > True
a.Or(b, c) // > True
Gates.Or(c, c) // > False
```

`.Not` inverts the value.

```csharp
var a = (2+2==4)
var b = (2+1==3)
var c = (2+1==5)

a.Not() // > False
Gates.Not(b) // > False
Gates.Not(c) // > True
```

`.Xor` is the exclusive OR, the operation is true if the terms are different. XOR is a binary operation,
which means that if you test multiple terms, XOR will be applied for every two terms.
Example: 3 terms (a, b and c), first, we do a XOR b, and then (a XOR b) XOR c.

```csharp
var a = (2+2==4)
var b = (2+1==3)
var c = (2+1==5)

a.Xor(b) // > False
a.Xor(c) // > True
```

`.Nand` is the inverse of AND operation.

```csharp
var a = (2+2==4)
var b = (2+1==3)
var c = (2+1==5)

a.And(b) // > False
a.And(b, c) // > True
```

`.Nor` is the inverse of OR operation.

```csharp
var a = (2+2==4)
var b = (2+1==3)
var c = (2+1==5)

a.Or(b) // > False
a.Or(b, c) // > False
Gates.Or(c, c) // > True
```

`.Xnor`is the inverse of XOR operation.

```csharp
var a = (2+2==4)
var b = (2+1==3)
var c = (2+1==5)

a.Xor(b) // > True
a.Xor(c) // > False
```

`.Sor` is the Special OR. Suppose you have a base object you want to compare with other objects, so that to know
if one of those objects is equal to the base object, that's where you use SOR.

```csharp
int a = 1, b = 1, c = 2, d = 3;

a.Sor(b, c, d) // > True
Gates.Sor(a, c, d) // > False
```

**Note:** If you use it with your custom objects, write their `.Equals()` accordingly, since
it is what SOR uses for comparison.
