
using Xunit;

namespace TDDMoney {
  public class With_Money {
    [Fact]
    public void Currency_name() {
      Assert.Equal("USD", Money.Dollar(1).Currency);
      Assert.Equal("CHF", Money.Franc(1).Currency);
    }

    [Fact]
    public void Multiplication() {
      Money dollarFive = Money.Dollar(5);
      
      Assert.Equal(Money.Dollar(10), dollarFive.Times(2));
      Assert.Equal(Money.Dollar(15), dollarFive.Times(3));
    }

    [Fact]
    public void Equality() {
      Assert.True(Money.Dollar(5).Equals(Money.Dollar(5)));
      Assert.False(Money.Dollar(5).Equals(Money.Dollar(6)));
      Assert.False(Money.Franc(5).Equals(Money.Dollar(5)));
    }

    [Fact]
    public void Simple_addition() {
      Bank bank = new Bank();
      Money five = Money.Dollar(5);
      IExpression sum = five.Plus(five);
      Money reduced = bank.Reduce(sum, "USD");

      Assert.Equal(Money.Dollar(10), reduced);
    }

    [Fact]
    public void Plus_should_return_Sum() {
      Money five = Money.Dollar(5);
      IExpression result = five.Plus(five);
      Sum sum = (Sum)result;
      Assert.Equal(five, sum.Augend);
      Assert.Equal(five, sum.Addend);
    }

    [Fact]
    public void Reduce() {
      Bank bank = new Bank();
      Money result = bank.Reduce(Money.Dollar(1), "USD");
      Assert.Equal(Money.Dollar(1), result);
    }

    [Fact]
    public void Reduce_different_currencies() {
      Bank bank = new Bank();
      bank.AddRate("CHF", "USD", 2);

      Money result = bank.Reduce(Money.Franc(2), "USD");
      Assert.Equal(Money.Dollar(1), result);
    }

    [Fact]
    public void Mixed_addition() {
      IExpression fiveBucks = Money.Dollar(5);
      IExpression tenFrancs = Money.Franc(10);
      Bank bank = new Bank();
      bank.AddRate("CHF", "USD", 2);
      Money result = bank.Reduce(fiveBucks.Plus(tenFrancs), "USD");

      Assert.Equal(Money.Dollar(10), result);
    }
  }

  public class With_Sum {
    public With_Sum()
    {
      m_bank.AddRate("CHF", "USD", 2);
    }

    [Fact]
    public void Reduce() {
      IExpression sum = new Sum(Money.Dollar(3), Money.Dollar(4));
      Money result = m_bank.Reduce(sum, "USD");
      Assert.Equal(Money.Dollar(7), result);
    }

    [Fact]
    public void Plus_Money() {
      IExpression sum = new Sum(m_fiveBucks, m_tenFrancs).Plus(m_fiveBucks);
      Money result = m_bank.Reduce(sum, "USD");

      Assert.Equal(Money.Dollar(15), result);
    }

    [Fact]
    public void Times() {
      IExpression sum = new Sum(m_fiveBucks, m_tenFrancs).Times(2);
      Money result = m_bank.Reduce(sum, "USD");

      Assert.Equal(Money.Dollar(20), result);
    }

    private Bank m_bank = new Bank();
    private IExpression m_fiveBucks = Money.Dollar(5);
    private IExpression m_tenFrancs = Money.Franc(10);
  }

  public class With_Bank {
    [Fact]
    public void Identity_rate_should_be_1() {
      Assert.Equal(1, new Bank().Rate("USD", "USD"));
    }
  }
}
