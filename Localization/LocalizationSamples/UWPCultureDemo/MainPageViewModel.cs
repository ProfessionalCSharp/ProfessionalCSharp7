using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPCultureDemo
{
    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            SetupCultures();
        }

        private void SetupCultures()
        {
            var cultureDataDict = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .OrderBy(c => c.Name)
                .Select(c => new CultureData
                {
                    CultureInfo = c,
                    SubCultures = new List<CultureData>()
                })
                .ToDictionary(c => c.CultureInfo.Name);

            var rootCultures = new List<CultureData>();
            foreach (var cd in cultureDataDict.Values)
            {
                if (cd.CultureInfo.Parent.LCID == 127)
                {
                    rootCultures.Add(cd);
                }
                else
                {
                    CultureData parentCultureData;
                    if (cultureDataDict.TryGetValue(cd.CultureInfo.Parent.Name,
                    out parentCultureData))
                    {
                        parentCultureData.SubCultures.Add(cd);
                    }
                    else
                    {
                        throw new InvalidOperationException("parent culture not found");
                    }
                }
            }

            foreach (var rootCulture in rootCultures.OrderBy(cd => cd.CultureInfo.EnglishName))
            {
                RootCultures.Add(rootCulture);
            }
        }

        public IList<CultureData> RootCultures { get; } = new List<CultureData>();
    }
}
