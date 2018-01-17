using AutoSuggestSample.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace AutoSuggestSample
{
    public sealed partial class MainPage : Page
    {
        public MainPage() => InitializeComponent();

        private const string RacersUri = "http://www.cninnovation.com/downloads/Racers.xml";
        private IEnumerable<Racer> _racers;

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            XElement xmlRacers = null;
            using (var client = new HttpClient())
            using (Stream stream = await client.GetStreamAsync(RacersUri))
            {
                xmlRacers = XElement.Load(stream);
            }

            _racers = xmlRacers.Elements("Racer").Select(r => new Racer
            {
                FirstName = r.Element("Firstname").Value,
                LastName = r.Element("Lastname").Value,
                Country = r.Element("Country").Value
            }).ToList();
        }

        private void OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput && sender.Text.Length >= 2)
            {
                string input = sender.Text;
                var racers = _racers.Where(r => r.FirstName.StartsWith(input, StringComparison.CurrentCultureIgnoreCase)).OrderBy(r => r.FirstName).ThenBy(r => r.LastName).ThenBy(r => r.Country).ToArray();
                if (racers.Length == 0)
                {
                    racers = _racers.Where(r => r.LastName.StartsWith(input, StringComparison.CurrentCultureIgnoreCase)).OrderBy(r => r.LastName).ThenBy(r => r.FirstName).ThenBy(r => r.Country).ToArray();
                    if (racers.Length == 0)
                    {
                        racers = _racers.Where(r => r.Country.StartsWith(input, StringComparison.CurrentCultureIgnoreCase)).OrderBy(r => r.Country).ThenBy(r => r.LastName).ThenBy(r => r.FirstName).ToArray();
                    }
                }
                sender.ItemsSource = racers;
            }
        }

        private async void OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var dlg = new MessageDialog($"suggestion: {args.SelectedItem}");
            await dlg.ShowAsync();
        }

        private async void OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            string message = $"query: {args.QueryText}";
            if (args.ChosenSuggestion != null)
            {
                message += $" suggestion: {args.ChosenSuggestion}";
            }
            var dlg = new MessageDialog(message);
            await dlg.ShowAsync();
        }
    }
}
