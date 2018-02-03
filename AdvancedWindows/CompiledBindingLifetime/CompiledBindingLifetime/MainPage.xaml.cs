using CompiledBindingLifetime.Models;
using Windows.UI.Xaml.Controls;

namespace CompiledBindingLifetime
{
    public sealed partial class MainPage : Page
    {
        public MainPage() => InitializeComponent();

        public Book Book { get; } = new Book { Title = "Professional C# 7", Publisher = "Wrox Press" };

        private int csharpversion = 7;
        public void OnChangeBook() =>
            Book.Title = $"Professional C# {++csharpversion}";

        private void OnUpdateBinding() =>
            Bindings.Update();

        private void OnStopTracking() =>
            Bindings.StopTracking();
    }
}
