namespace Wrox.ProCSharp.Generics
{
    // covariant
    public interface IIndex<out T>
    {
        T this[int index] { get; }
        int Count { get; }
    }
}
