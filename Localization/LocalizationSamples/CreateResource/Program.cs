using System;
using System.Collections;
using System.IO;
using System.Resources;

namespace CreateResource
{
    class Program
    {
        static void Main()
        {
            CreateResource();
            ReadResource();
        }

        private const string ResourceFile = "Demo.resources";

        public static void ReadResource()
        {
            FileStream stream = File.OpenRead(ResourceFile);
            using (var reader = new ResourceReader(stream))
            {
                foreach (DictionaryEntry resource in reader)
                {
                    Console.WriteLine($"{resource.Key} {resource.Value}");
                }
            }
        }

        private static void CreateResource()
        {
            FileStream stream = File.OpenWrite(ResourceFile);

            using (var writer = new ResourceWriter(stream))
            {
                writer.AddResource("Title", "Professional C#");
                writer.AddResource("Author", "Christian Nagel");
                writer.AddResource("Publisher", "Wrox Press");

                writer.Generate();
            }
        }
    }
}
