using System;
using System.Collections.Generic;
using System.Globalization;

namespace UWPCultureDemo
{
    public class CultureData
    {
        public CultureInfo CultureInfo { get; set; }
        public IList<CultureData> SubCultures { get; set; }
        private double _numberSample = 9876543.21;
        public string NumberSample => _numberSample.ToString("N", CultureInfo);
        public string DateSample => DateTime.Today.ToString("D", CultureInfo);
        public string TimeSample => DateTime.Now.ToString("T", CultureInfo);
        public RegionInfo RegionInfo
        {
            get
            {
                try
                {
                    return new RegionInfo(CultureInfo.Name);
                }
                catch (ArgumentException)
                {
                    // with some neutral cultures regions are not available
                    return null;
                }
            }
        }
    }

}
