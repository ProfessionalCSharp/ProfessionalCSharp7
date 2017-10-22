using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace WindowsPrincipalSample
{
    class Program
    {
        static void Main()
        {
            WindowsIdentity identity = ShowIdentityInformation();

            WindowsPrincipal principal = ShowPrincipal(identity);

            ShowClaims(principal.Claims);
        }

        public static WindowsIdentity ShowIdentityInformation()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity == null)
            {
                Console.WriteLine("not a Windows Identity");
                return null;
            }

            identity.AddClaim(new Claim("Age", "25"));


            Console.WriteLine($"IdentityType: {identity}");
            Console.WriteLine($"Name: {identity.Name}");
            Console.WriteLine($"Authenticated: {identity.IsAuthenticated}");
            Console.WriteLine($"Authentication Type: {identity.AuthenticationType}");
            Console.WriteLine($"Anonymous? {identity.IsAnonymous}");
            Console.WriteLine($"Access Token: {identity.AccessToken.DangerousGetHandle()}");
            Console.WriteLine();
            return identity;
        }

        public static WindowsPrincipal ShowPrincipal(WindowsIdentity identity)
        {
            Console.WriteLine("Show principal information");
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            if (principal == null)
            {
                Console.WriteLine("not a Windows Principal");
                return null;
            }
            Console.WriteLine($"Users? {principal.IsInRole(WindowsBuiltInRole.User)}");
            Console.WriteLine($"Administrators? {principal.IsInRole(WindowsBuiltInRole.Administrator)}");
            Console.WriteLine();
            return principal;
        }

        public static void ShowClaims(IEnumerable<Claim> claims)
        {
            Console.WriteLine("Claims");
            foreach (var claim in claims)
            {
                Console.WriteLine($"Subject: {claim.Subject}");
                Console.WriteLine($"Issuer: {claim.Issuer}");
                Console.WriteLine($"Type: {claim.Type}");
                Console.WriteLine($"Value type: {claim.ValueType}");
                Console.WriteLine($"Value: {claim.Value}");
                foreach (var prop in claim.Properties)
                {
                    Console.WriteLine($"\tProperty: {prop.Key} {prop.Value}");
                }
                Console.WriteLine();
            }
        }
    }
}
