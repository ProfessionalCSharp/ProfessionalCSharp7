using Microsoft.AspNetCore.DataProtection;
using static System.Console;

namespace DataProtectionSample
{
    public class MySafe
    {
        private IDataProtector _protector;
        public MySafe(IDataProtectionProvider provider) =>
            _protector = provider.CreateProtector("MySafe.MyProtection.v2");            

        public string Encrypt(string input) => _protector.Protect(input);

        public string Decrypt(string encrypted) => _protector.Unprotect(encrypted);
    }
}
