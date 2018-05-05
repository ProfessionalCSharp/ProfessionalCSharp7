using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UWPCultureDemo
{
    public class CulturesViewModel : INotifyPropertyChanged
    {
        public CulturesViewModel() => SetupCultures();

        public event PropertyChangedEventHandler PropertyChanged;

        private void SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(item, value))
            {
                item = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
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
                if (cd.CultureInfo.Parent.LCID == 0x7f)  // check for invariant culture
                {
                    rootCultures.Add(cd);
                }
                else // add to parent culture
                {
                    if (cultureDataDict.TryGetValue(cd.CultureInfo.Parent.Name, out CultureData parentCultureData))
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

        private CultureData _selectedCulture;
        public CultureData SelectedCulture
        {
            get => _selectedCulture;
            set => SetProperty(ref _selectedCulture, value);
        }
    }
}
