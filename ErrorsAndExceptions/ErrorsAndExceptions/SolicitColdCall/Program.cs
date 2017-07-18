using System;
using System.IO;

namespace SolicitColdCall
{
    class Program
    {
        static void Main()
        {
            Console.Write("Please type in the name of the file " +
                "containing the names of the people to be cold called > ");
            string fileName = Console.ReadLine();
            ColdCallFileReaderLoop1(fileName);
            Console.WriteLine();
            ColdCallFileReaderLoop2(fileName);
            Console.WriteLine();

            Console.ReadLine();
        }

        private static void ColdCallFileReaderLoop2(string fileName)
        {
            using (var peopleToRing = new ColdCallFileReader())
            {

                try
                {
                    peopleToRing.Open(fileName);
                    for (int i = 0; i < peopleToRing.NPeopleToRing; i++)
                    {
                        peopleToRing.ProcessNextPerson();
                    }
                    Console.WriteLine("All callers processed correctly");
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"The file {fileName} does not exist");
                }
                catch (ColdCallFileFormatException ex)
                {
                    Console.WriteLine($"The file {fileName} appears to have been corrupted");
                    Console.WriteLine($"Details of problem are: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner exception was: {ex.InnerException.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred:\n{ex.Message}");
                }
            }
        }

        public static void ColdCallFileReaderLoop1(string fileName)
        {
            var peopleToRing = new ColdCallFileReader();

            try
            {
                peopleToRing.Open(fileName);
                for (int i = 0; i < peopleToRing.NPeopleToRing; i++)
                {
                    peopleToRing.ProcessNextPerson();
                }
                Console.WriteLine("All callers processed correctly");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"The file {fileName} does not exist");
            }
            catch (ColdCallFileFormatException ex)
            {
                Console.WriteLine($"The file {fileName} appears to have been corrupted");
                Console.WriteLine($"Details of problem are: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception was: {ex.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred:\n{ex.Message}");
            }
            finally
            {
                peopleToRing.Dispose();
            }
        }
    }
}
