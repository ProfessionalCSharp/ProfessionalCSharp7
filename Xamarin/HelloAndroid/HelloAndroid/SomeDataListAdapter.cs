using Android.App;
using Android.Views;
using Android.Widget;
using HelloAndroid.Models;
using System.Collections.Generic;

namespace HelloAndroid
{
    public class SomeDataListAdapter : BaseAdapter
    {
        private readonly Activity _activity;
        private readonly IList<SomeData> _items;

        public SomeDataListAdapter(Activity activity, IList<SomeData> items)
        {
            _activity = activity;
            _items = items;
        }

        public override Java.Lang.Object GetItem(int position) => position;

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
            {
                view = _activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = $"{_items[position].Number}: {_items[position].Text}";
            return view;
        }

        public override int Count => _items.Count;
    }
}