using SampleLib;
using System.Collections.ObjectModel;

namespace DefaultInterfaceMembersSample
{
    public class CustomCollection<T> : Collection<T>, ICustomEnumerable<T>
    {
    }
}
