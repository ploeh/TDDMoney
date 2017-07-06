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
            m_bank.AddRate("CHF", "USD", 2);
        }

        [Fact]
        public void Reduce()
        {
            IExpression sum = new Sum(Money.Dollar(3), Money.Dollar(4));
            Money result = m_bank.Reduce(sum, "USD");
            Assert.Equal(Money.Dollar(7), result);
        }

        [Fact]
        public void PlusMoney()
        {
            IExpression sum = new Sum(m_fiveBucks, m_tenFrancs).Plus(m_fiveBucks);
            Money result = m_bank.Reduce(sum, "USD");

            Assert.Equal(Money.Dollar(15), result);
        }

        [Fact]
        public void Times()
        {
            IExpression sum = new Sum(m_fiveBucks, m_tenFrancs).Times(2);
            Money result = m_bank.Reduce(sum, "USD");

            Assert.Equal(Money.Dollar(20), result);
        }

        private Bank m_bank = new Bank();
        private IExpression m_fiveBucks = Money.Dollar(5);
        private IExpression m_tenFrancs = Money.Franc(10);
    }
}
