namespace TDDMoney
{
    class CurrencyPair
    {
        public CurrencyPair(string from, string to)
        {
            m_from = from;
            m_to = to;
        }

        public override bool Equals(object obj)
        {
            CurrencyPair pair = (CurrencyPair)obj;
            return m_from == pair.m_from && m_to == pair.m_to;
        }

        public override int GetHashCode()
        {
            return (m_from + m_to).GetHashCode();
        }

        private string m_from;
        private string m_to;
    }
}
