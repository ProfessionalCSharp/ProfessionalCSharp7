using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiledBindingLifetime.Models
{
    public class Book : BindableBase
    {
        public int BookId { get; set; }
        private string _title;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        public string Publisher { get; set; }
        public override string ToString() => Title;
    }

}
