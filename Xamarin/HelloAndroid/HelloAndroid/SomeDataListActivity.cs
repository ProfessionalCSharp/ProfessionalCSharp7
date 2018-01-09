using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using HelloAndroid.Models;
using System.Collections.Generic;
using System.Linq;

namespace HelloAndroid
{
    [Activity(Label = "SomeDataListActivity")]
    public class SomeDataListActivity : ListActivity
    {
        private IList<SomeData> _items;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            _items = Enumerable.Range(0, 100).Select(i => new SomeData { Number = i, Text = $"sample {i}" }).ToList();
            ListAdapter = new SomeDataListAdapter(this, _items);
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
        }
    }
}