using System;
using System.Collections.Generic;

namespace ListSamples
{
    public enum CompareType
    {
        FirstName,
        LastName,
        Country,
        Wins
    }

    public class RacerComparer : IComparer<Racer>
    {
        private CompareType _compareType;
        public RacerComparer(CompareType compareType) =>
          _compareType = compareType;

        public int Compare(Racer x, Racer y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            int result;
            switch (_compareType)
            {
                case CompareType.FirstName:
                    result = string.Compare(x.FirstName, y.FirstName);
                    break;
                case CompareType.LastName:
                    result = string.Compare(x.LastName, y.LastName);
                    break;
                case CompareType.Country:
                    result = string.Compare(x.Country, y.Country);
                    if (result == 0)
                    {
                        result = string.Compare(x.LastName, y.LastName);
                    }
                    break;
                case CompareType.Wins:
                    result = x.Wins.CompareTo(y.Wins);
                    break;
                default:
                    throw new ArgumentException("Invalid Compare Type");
            }
            return result;
        }
    }
}