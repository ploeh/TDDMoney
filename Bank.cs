using System.Collections.Generic;

namespace TDDMoney
{
    class Bank
    {
        public Money Reduce(IExpression source, string to)
        {
            return source.Reduce(this, to);
        }

        public void AddRate(string from, string to, int rate)
        {
            m_rates[new CurrencyPair(from, to)] = rate;
        }

        public int Rate(string from, string to)
        {
            if (from == to) return 1;
            return m_rates[new CurrencyPair(from, to)];
        }

        private Dictionary<CurrencyPair, int> m_rates = new Dictionary<CurrencyPair, int>();
    }
}
