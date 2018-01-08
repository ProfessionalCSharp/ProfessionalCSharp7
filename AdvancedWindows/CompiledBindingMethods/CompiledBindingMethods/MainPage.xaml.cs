using CompiledBindingMethods.Models;
using System;
using Windows.UI.Xaml.Controls;

namespace CompiledBindingMethods
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            Person1 = new Person { GivenName = "Katharina", Surname = "Nagel" };
            Person2 = new Person { GivenName = "Stephanie", Surname = "Nagel" };
            this.InitializeComponent();
        }

        public Person Person1 { get; }
        public Person Person2 { get; set; }

        public string ToName(Person p, bool firstLast)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));

            if (firstLast)
            {
                return $"{p.GivenName} {p.Surname}";
            }
            else
            {
                return $"{p.Surname}, {p.GivenName}";
            }
        }

        public void ToPerson2(string name)
        {
            string[] names = name.Split(' ');
            if (names.Length != 2) return; // don't do anything with wrong inputs
            Person2.GivenName = names[0];
            Person2.Surname = names[1];
            Bindings.Update();
        }
    }
}
