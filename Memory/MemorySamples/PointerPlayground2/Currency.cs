namespace PointerPlayground2
{
    internal struct CurrencyStruct
    {
        public long Dollars;
        public byte Cents;

        public override string ToString() => $"$ {Dollars}.{Cents}";
    }

    internal class CurrencyClass
    {
        public long Dollars = 0;
        public byte Cents = 0;

        public override string ToString() => $"$ {Dollars}.{Cents}";
    }
}
