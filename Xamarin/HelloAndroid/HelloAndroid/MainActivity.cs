using Android.App;
using Android.Widget;
using Android.OS;

namespace HelloAndroid
{
    [Activity(Label = "HelloAndroid", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += (sender, e) =>
            {
                Toast.MakeText(ApplicationContext, Resource.String.hello, ToastLength.Long).Show();
            };

            Button showlistButton = FindViewById<Button>(Resource.Id.showlistbutton);
            showlistButton.Click += (sender, e) =>
                StartActivity(typeof(SomeDataListActivity));
        }

    }
}

