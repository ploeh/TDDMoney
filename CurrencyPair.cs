namespace TDDMoney
{
    public class CurrencyPair
    {
        public CurrencyPair(string from, string to)
        {
            this.from = from;
            this.to = to;
        }

        public override bool Equals(object obj)
        {
            CurrencyPair pair = (CurrencyPair)obj;
            return from == pair.from && to == pair.to;
        }

        public override int GetHashCode()
        {
            return (from + to).GetHashCode();
        }

        private readonly string from;
        private readonly string to;
    }
}
