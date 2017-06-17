namespace Wrox.ProCSharp.Generics
{
    // contra-variant
    public interface IDisplay<in T>
    {
        void Show(T item);
    }
}
