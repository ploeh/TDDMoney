using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TDDMoney
{
    public class MoneyTests
    {
        [Fact]
        public void CurrencyName()
        {
            Assert.Equal("USD", Money.Dollar(1).Currency);
            Assert.Equal("CHF", Money.Franc(1).Currency);
        }

        [Fact]
        public void Multiplication()
        {
            Money dollarFive = Money.Dollar(5);

            Assert.Equal(Money.Dollar(10), dollarFive.Times(2), Compare.UsingBank);
            Assert.Equal(Money.Dollar(15), dollarFive.Times(3), Compare.UsingBank);
        }

        [Fact]
        public void Equality()
        {
            Assert.True(Money.Dollar(5).Equals(Money.Dollar(5)));
            Assert.False(Money.Dollar(5).Equals(Money.Dollar(6)));
            Assert.False(Money.Franc(5).Equals(Money.Dollar(5)));
        }

        [Fact]
        public void SimpleAddition()
        {
            Bank bank = new Bank();
            Money five = Money.Dollar(5);
            IExpression sum = five.Plus(five);
            Money reduced = bank.Reduce(sum, "USD");

            Assert.Equal(Money.Dollar(10), reduced);
        }

        [Fact]
        public void PlusShouldReturnSum()
        {
            Money five = Money.Dollar(5);
            IExpression result = five.Plus(five);
            Sum sum = (Sum)result;
            Assert.Equal(five, sum.Augend);
            Assert.Equal(five, sum.Addend);
        }

        [Fact]
        public void Reduce()
        {
            Bank bank = new Bank();
            Money result = bank.Reduce(Money.Dollar(1), "USD");
            Assert.Equal(Money.Dollar(1), result);
        }

        [Fact]
        public void ReduceDifferentCurrencies()
        {
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);

            Money result = bank.Reduce(Money.Franc(2), "USD");
            Assert.Equal(Money.Dollar(1), result);
        }

        [Fact]
        public void MixedAddition()
        {
            IExpression fiveBucks = Money.Dollar(5);
            IExpression tenFrancs = Money.Franc(10);
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            Money result = bank.Reduce(fiveBucks.Plus(tenFrancs), "USD");

            Assert.Equal(Money.Dollar(10), result);
        }
    }
}
