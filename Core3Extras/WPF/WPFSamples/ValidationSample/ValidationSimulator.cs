using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ValidationSample
{
    public static class ValidationSimulator
    {
        public static Task<string> Validate(int val,
            [CallerMemberName] string propertyName = null)
        {
            return Task.Run(async () =>
            {
                await Task.Delay(3000);
                if (val > 50) return "bad value";
                else return null;
            });
        }
    }

}
