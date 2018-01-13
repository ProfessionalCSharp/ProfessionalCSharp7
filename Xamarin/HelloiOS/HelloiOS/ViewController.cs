using System;

using UIKit;

namespace HelloiOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void OnButtonClick(UIButton sender)
        {
            var alert = new UIAlertView
            {
                Title = "Hello",
                Message = "Hello iOS!",
            };
            alert.AddButton("Close");
            alert.Clicked += (sender1, e) =>
            {
                // dialog closed
            };
            alert.Show();
        }
    }
}