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
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage($"clicked {_items[position]}")
                .SetTitle(Resource.String.somedata_clickeditem_title);

            builder.SetNeutralButton(Android.Resource.String.Ok, (sender, e) =>
            {
                // user clicked the ok button
            });
            AlertDialog dialog = builder.Create();

            dialog.Show();
        }
    }
}