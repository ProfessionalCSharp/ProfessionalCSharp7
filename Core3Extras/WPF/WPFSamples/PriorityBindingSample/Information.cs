using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityBindingSample
{
    public class Information
    {
        public string Info1 => "please wait...";

        public string Info2
        {
            get
            {
                Task.Delay(5000).Wait();
                return "please wait a little more";
            }
        }
    }

}
