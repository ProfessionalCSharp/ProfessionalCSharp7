namespace SampleLib
{
    public class Demo
    {
#if NETSTANDARD2_0
        private static string s_info = ".NET Standard 2.0";
#elif DOTNET47
        private static string s_info = ".NET 4.7";
#else
        private static string s_info = "Unknown";
#endif

        public static string Show() => s_info;
    }
}
