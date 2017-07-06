namespace TDDMoney
{
    class Money : IExpression
    {
        public Money(int amount, string currency)
        {
            m_amount = amount;
            m_currency = currency;
        }

        public override string ToString()
        {
            return m_currency + " " + m_amount;
        }

        public override bool Equals(object o)
        {
            Money money = (Money)o;

            return m_amount == money.m_amount &&
              m_currency == money.m_currency;
        }

        public IExpression Times(int multiplier)
        {
            return new Money(m_amount * multiplier, m_currency);
        }

        public int Amount
        {
            get
            {
                return m_amount;
            }
        }

        public string Currency
        {
            get
            {
                return m_currency;
            }
        }

        public static Money Dollar(int amount)
        {
            return new Money(amount, "USD");
        }

        public static Money Franc(int amount)
        {
            return new Money(amount, "CHF");
        }

        public IExpression Plus(IExpression addend)
        {
            return new Sum(this, addend);
        }

        public Money Reduce(Bank bank, string to)
        {
            int rate = bank.Rate(m_currency, to);
            return new Money(m_amount / rate, to);
        }

        protected int m_amount;
        protected string m_currency;
    }
}
