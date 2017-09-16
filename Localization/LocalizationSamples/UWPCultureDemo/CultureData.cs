using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPCultureDemo
{
    public class CultureData
    {
        public CultureInfo CultureInfo { get; set; }
        public IList<CultureData> SubCultures { get; set; }
        double numberSample = 9876543.21;
        public string NumberSample => numberSample.ToString("N", CultureInfo);
        public string DateSample => DateTime.Today.ToString("D", CultureInfo);
        public string TimeSample => DateTime.Now.ToString("T", CultureInfo);
        public RegionInfo RegionInfo
        {
            get
            {
                RegionInfo ri;
                try
                {
                    ri = new RegionInfo(CultureInfo.Name);
                }
                catch (ArgumentException)
                {
                    // with some neutral cultures regions are not available
                    return null;
                }
                return ri;
            }
        }
    }

}
