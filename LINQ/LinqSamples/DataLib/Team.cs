using System.Collections.Generic;

namespace DataLib
{
    public class Team
    {
        public Team(string name, params int[] years)
        {
            Name = name;
            Years = years != null ? new List<int>(years) : new List<int>();
        }
        public string Name { get; }
        public IEnumerable<int> Years { get; }
    }
}