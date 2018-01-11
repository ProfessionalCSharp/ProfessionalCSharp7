using Windows.Foundation;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml.Controls;

namespace ControlsSamples.Utilities
{
    public class ColorSelection : BindableBase
    {
        public ColorSelection(InkCanvas inkCanvas)
        {
            _inkCanvas = inkCanvas;
            Red = false;
            Green = false;
            Blue = false;
        }
        private InkCanvas _inkCanvas;

        private bool? _red;
        public bool? Red
        {
            get => _red;
            set => SetColor(ref _red, value);
        }
        private bool? _green;
        public bool? Green
        {
            get => _green; 
            set => SetColor(ref _green, value);
        }
        private bool? _blue;
        public bool? Blue
        {
            get => _blue;
            set => SetColor(ref _blue, value);
        }

        public void SetColor(ref bool? item, bool? value)
        {
            SetProperty(ref item, value);

            InkDrawingAttributes defaultAttributes = _inkCanvas.InkPresenter.CopyDefaultDrawingAttributes();
            defaultAttributes.PenTip = PenTipShape.Rectangle;
            defaultAttributes.Size = new Size(3, 3);

            defaultAttributes.Color = new Windows.UI.Color()
            {
                A = 255,
                R = Red == true ? (byte)0xff : (byte)0,
                G = Green == true ? (byte)0xff : (byte)0,
                B = Blue == true ? (byte)0xff : (byte)0
            };
            _inkCanvas.InkPresenter.UpdateDefaultDrawingAttributes(defaultAttributes);
        }
    }
}
