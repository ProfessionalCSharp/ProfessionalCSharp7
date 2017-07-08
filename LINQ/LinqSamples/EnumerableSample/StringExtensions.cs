namespace EnumerableSample
{
    public static class StringExtensions
    {
        public static string FirstName(this string name) =>
            name.Substring(0, name.LastIndexOf(' '));

        public static string LastName(this string name) =>
            name.Substring(name.LastIndexOf(' ') + 1);
    }
}
