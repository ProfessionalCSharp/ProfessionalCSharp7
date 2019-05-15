using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityBindingSample
{
    public class Data
    {
        public string ProcessSomeData
        {
            get
            {
                Task.Delay(8000).Wait(); // blocking call
                return "the final result is here";
            }
        }
    }

}
