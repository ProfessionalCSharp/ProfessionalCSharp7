using System.Collections.Generic;

namespace Wrox.ProCSharp.Composition
{
    public class FuelEconomyViewModel : Observable
    {
        public FuelEconomyViewModel()
        {
            InitializeFuelEcoTypes();
            CalculateCommand = new RelayCommand(OnCalculate);
        }

        public RelayCommand CalculateCommand { get; }

        public void OnCalculate()
        {
            double fuel = double.Parse(Fuel);
            double distance = double.Parse(Distance);
            FuelEconomyType ecoType = SelectedFuelEcoType;
            double result = 0;
            switch (ecoType.Id)
            {
                case "lpk":
                    result = fuel / (distance / 100);
                    break;
                case "mpg":
                    result = distance / fuel;
                    break;
                default:
                    break;
            }
            Result = result.ToString();
        }

        public List<FuelEconomyType> FuelEcoTypes { get; } = new List<FuelEconomyType>();

        private void InitializeFuelEcoTypes()
        {
            var t1 = new FuelEconomyType
            {
                Id = "lpk",
                Text = "L/100 km",
                DistanceText = "Distance (kilometers)",
                FuelText = "Fuel used (liters)"
            };
            var t2 = new FuelEconomyType
            {
                Id = "mpg",
                Text = "Miles per gallon",
                DistanceText = "Distance (miles)",
                FuelText = "Fuel used (gallons)"
            };
            FuelEcoTypes.AddRange(new FuelEconomyType[] { t1, t2 });
        }

        private FuelEconomyType _selectedFuelEcoType;

        public FuelEconomyType SelectedFuelEcoType
        {
            get => _selectedFuelEcoType;
            set => SetProperty(ref _selectedFuelEcoType, value);
        }

        private string _fuel;
        public string Fuel
        {
            get => _fuel;
            set => SetProperty(ref _fuel, value);
        }

        private string _distance;
        public string Distance
        {
            get => _distance;
            set => SetProperty(ref _distance, value);
        }

        private string _result;
        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }
    }
}
