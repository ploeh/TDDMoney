using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TDDMoney
{
    public class SumTests
    {
        public SumTests()
        {
            bank.AddRate("CHF", "USD", 2);
        }

        [Fact]
        public void Reduce()
        {
            IExpression sum = new Sum(Money.Dollar(3), Money.Dollar(4));
            Money result = bank.Reduce(sum, "USD");
            Assert.Equal(Money.Dollar(7), result);
        }

        [Fact]
        public void PlusMoney()
        {
            IExpression sum = new Sum(fiveBucks, tenFrancs).Plus(fiveBucks);
            Money result = bank.Reduce(sum, "USD");

            Assert.Equal(Money.Dollar(15), result);
        }

        [Fact]
        public void Times()
        {
            IExpression sum = new Sum(fiveBucks, tenFrancs).Times(2);
            Money result = bank.Reduce(sum, "USD");

            Assert.Equal(Money.Dollar(20), result);
        }

        private readonly Bank bank = new Bank();
        private readonly IExpression fiveBucks = Money.Dollar(5);
        private readonly IExpression tenFrancs = Money.Franc(10);
    }
}
