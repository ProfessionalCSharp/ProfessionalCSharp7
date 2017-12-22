using System.Collections.Generic;

namespace Models
{
  public sealed class CountryRepository
  {
    private static IEnumerable<Country> s_countries;

    public IEnumerable<Country> GetCountries() => s_countries ?? (s_countries = new List<Country>
    {
        new Country { Name="Austria", ImagePath = "Images/Austria.bmp" },
        new Country { Name="Germany", ImagePath = "Images/Germany.bmp" },
        new Country { Name="Norway", ImagePath = "Images/Norway.bmp" },
        new Country { Name="USA", ImagePath = "Images/USA.bmp" }
    });
  }
}
