using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationSample
{
    public class SomeData : IDataErrorInfo
    {
        private int _value1;
        public int Value1
        {
            get { return _value1; }
            set
            {
                if (value < 5 || value > 12)
                {
                    throw new ArgumentException(
                        "value must not be less than 5 or greater than 12");
                }
                _value1 = value;
            }
        }

        public int Value2 { get; set; }

        string IDataErrorInfo.Error => null;


        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (columnName == "Value2")
                {
                    if (this.Value2 < 0 || this.Value2 > 80)
                        return "age must not be less than 0 or greater than 80";

                }
                return null;
            }
        }

    }

}
