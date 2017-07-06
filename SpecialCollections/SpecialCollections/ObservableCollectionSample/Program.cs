using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ObservableCollectionSample
{
    class Program
    {
        static void Main()
        {
            var data = new ObservableCollection<string>();
            
            data.CollectionChanged += Data_CollectionChanged;
            data.Add("One");
            data.Add("Two");
            data.Insert(1, "Three");
            data.Remove("One");

            data.CollectionChanged -= Data_CollectionChanged;

            Console.ReadLine();
        }

        public static void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine($"action: {e.Action.ToString()}");

            if (e.OldItems != null)
            {
                Console.WriteLine($"starting index for old item(s): {e.OldStartingIndex}");
                Console.WriteLine("old item(s):");
                foreach (var item in e.OldItems)
                {
                    Console.WriteLine(item);
                }
            }
            if (e.NewItems != null)
            {
                Console.WriteLine($"starting index for new item(s): {e.NewStartingIndex}");
                Console.WriteLine("new item(s): ");
                foreach (var item in e.NewItems)
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine();
        }
    }
}