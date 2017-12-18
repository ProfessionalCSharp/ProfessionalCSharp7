namespace Models
{
    public interface ICountry
    {
        string ImagePath { get; set; }
        string Name { get; set; }

        string ToString();
    }
}