using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace FileAccessControl
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = null;
            if (args.Length == 0) return;

            filename = args[0];

            using (FileStream stream = File.Open(filename, FileMode.Open))
            {
                FileSecurity securityDescriptor = stream.GetAccessControl();
                AuthorizationRuleCollection rules =
                      securityDescriptor.GetAccessRules(true, true, typeof(NTAccount));

                foreach (AuthorizationRule rule in rules)
                {
                    var fileRule = rule as FileSystemAccessRule;
                    Console.WriteLine($"Access type: {fileRule.AccessControlType}");
                    Console.WriteLine($"Rights: {fileRule.FileSystemRights}");
                    Console.WriteLine($"Identity: {fileRule.IdentityReference.Value}");
                    Console.WriteLine();
                }
            }
        }
    }
}
