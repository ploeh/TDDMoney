using NUnit;
using NUnit.Framework;

namespace TDDMoney {
  [TestFixture]
  class With_Money {
    [Test]
    public void Currency_name() {
      Assert.AreEqual("USD", Money.Dollar(1).Currency);
      Assert.AreEqual("CHF", Money.Franc(1).Currency);
    }

    [Test]
    public void Multiplication() {
      Money dollarFive = Money.Dollar(5);
      
      Assert.AreEqual(Money.Dollar(10), dollarFive.Times(2));
      Assert.AreEqual(Money.Dollar(15), dollarFive.Times(3));
    }

    [Test]
    public void Equality() {
      Assert.IsTrue(Money.Dollar(5).Equals(Money.Dollar(5)));
      Assert.IsFalse(Money.Dollar(5).Equals(Money.Dollar(6)));
      Assert.IsFalse(Money.Franc(5).Equals(Money.Dollar(5)));
    }

    [Test]
    public void Simple_addition() {
      Bank bank = new Bank();
      Money five = Money.Dollar(5);
      IExpression sum = five.Plus(five);
      Money reduced = bank.Reduce(sum, "USD");

      Assert.AreEqual(Money.Dollar(10), reduced);
    }

    [Test]
    public void Plus_should_return_Sum() {
      Money five = Money.Dollar(5);
      IExpression result = five.Plus(five);
      Sum sum = (Sum)result;
      Assert.AreEqual(five, sum.Augend);
      Assert.AreEqual(five, sum.Addend);
    }

    [Test]
    public void Reduce() {
      Bank bank = new Bank();
      Money result = bank.Reduce(Money.Dollar(1), "USD");
      Assert.AreEqual(Money.Dollar(1), result);
    }

    [Test]
    public void Reduce_different_currencies() {
      Bank bank = new Bank();
      bank.AddRate("CHF", "USD", 2);

      Money result = bank.Reduce(Money.Franc(2), "USD");
      Assert.AreEqual(Money.Dollar(1), result);
    }

    [Test]
    public void Mixed_addition() {
      IExpression fiveBucks = Money.Dollar(5);
      IExpression tenFrancs = Money.Franc(10);
      Bank bank = new Bank();
      bank.AddRate("CHF", "USD", 2);
      Money result = bank.Reduce(fiveBucks.Plus(tenFrancs), "USD");

      Assert.AreEqual(Money.Dollar(10), result);
    }
  }

  [TestFixture]
  class With_Sum {
    [SetUp]
    public void Setup() {
      m_bank.AddRate("CHF", "USD", 2);
    }

    [Test]
    public void Reduce() {
      IExpression sum = new Sum(Money.Dollar(3), Money.Dollar(4));
      Money result = m_bank.Reduce(sum, "USD");
      Assert.AreEqual(Money.Dollar(7), result);
    }

    [Test]
    public void Plus_Money() {
      IExpression sum = new Sum(m_fiveBucks, m_tenFrancs).Plus(m_fiveBucks);
      Money result = m_bank.Reduce(sum, "USD");

      Assert.AreEqual(Money.Dollar(15), result);
    }

    [Test]
    public void Times() {
      IExpression sum = new Sum(m_fiveBucks, m_tenFrancs).Times(2);
      Money result = m_bank.Reduce(sum, "USD");

      Assert.AreEqual(Money.Dollar(20), result);
    }

    private Bank m_bank = new Bank();
    private IExpression m_fiveBucks = Money.Dollar(5);
    private IExpression m_tenFrancs = Money.Franc(10);
  }

  [TestFixture]
  class With_Bank {
    [Test]
    public void Identity_rate_should_be_1() {
      Assert.AreEqual(1, new Bank().Rate("USD", "USD"));
    }
  }
}
