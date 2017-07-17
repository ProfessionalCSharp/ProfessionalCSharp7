using DataLib;

namespace Tuples
{
    public static class RacerExtensions
    {
        public static void Deconstruct(this Racer r, out string firstName, out string lastName, out int starts, out int wins)
        {
            firstName = r.FirstName;
            lastName = r.LastName;
            starts = r.Starts;
            wins = r.Wins;
        }
    }
}
