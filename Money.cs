namespace TDDMoney
{
    public class Money : IExpression
    {
        public Money(int amount, string currency)
        {
            this.amount = amount;
            this.currency = currency;
        }

        public override string ToString()
        {
            return currency + " " + amount;
        }

        public override bool Equals(object o)
        {
            Money money = (Money)o;

            return amount == money.amount &&
              currency == money.currency;
        }

        public override int GetHashCode()
        {
            return
                amount.GetHashCode() ^
                currency.GetHashCode();
        }

        public IExpression Times(int multiplier)
        {
            return new Money(amount * multiplier, currency);
        }

        public int Amount
        {
            get
            {
                return amount;
            }
        }

        public string Currency
        {
            get
            {
                return currency;
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
            int rate = bank.Rate(currency, to);
            return new Money(amount / rate, to);
        }

        protected int amount;
        protected string currency;
    }
}
