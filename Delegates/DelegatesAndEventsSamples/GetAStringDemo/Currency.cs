namespace GetAStringDemo
{
    struct Currency
    {
        public uint Dollars;
        public ushort Cents;

        public Currency(uint dollars, ushort cents)
        {
            Dollars = dollars;
            Cents = cents;
        }

        //public override string ToString()
        //{
        //  return string.Format("${0}.{1,2:00}", Dollars, Cents);
        //}

        public override string ToString() => $"${Dollars}.{Cents,2:00}";


        public static string GetCurrencyUnit() => "Dollar";

        public static explicit operator Currency(float value)
        {
            checked
            {
                uint dollars = (uint)value;
                ushort cents = (ushort)((value - dollars) * 100);
                return new Currency(dollars, cents);
            }
        }

        public static implicit operator float(Currency value) =>
          value.Dollars + (value.Cents / 100.0f);


        public static implicit operator Currency(uint value) =>
          new Currency(value, 0);


        public static implicit operator uint(Currency value) =>
          value.Dollars;
    }
}