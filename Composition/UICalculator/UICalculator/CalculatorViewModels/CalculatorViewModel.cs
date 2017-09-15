using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Wrox.ProCSharp.Composition
{
    public class CalculatorViewModel : Observable
    {
        public CalculatorViewModel()
        {
            _calculatorManager = new CalculatorManager();
            _calculatorManager.ImportsSatisfied += (sender, e) =>
            {
                Status += $"{e.StatusMessage}\n";
            };

            CalculateCommand = new RelayCommand(OnCalculate);
        }

        public void Init(params Type[] parts)
        {
            _calculatorManager.InitializeContainer(parts);
            var operators = _calculatorManager.GetOperators();
            CalcAddInOperators.Clear();
            foreach (var op in operators)
            {
                CalcAddInOperators.Add(op);
            }
        }

        private CalculatorManager _calculatorManager;

        public ICommand CalculateCommand { get; set; }

        public void OnClearLog()
        {
            Status = string.Empty;
        }

        public void OnCalculate()
        {
            if (_currentOperands.Length == 2)
            {
                string[] input = Input.Split(' ');
                _currentOperands[1] = double.Parse(input[2]);
                Result = _calculatorManager.InvokeCalculator(_currentOperation, _currentOperands);
            }
            Input = string.Empty;
        }

        private string _status;

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        private string _input;
        public string Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }

        private double _result;
        public double Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        private string _fullInputText;

        public string FullInputText
        {
            get => _fullInputText;
            set => _fullInputText = value;
        }

        private IOperation _currentOperation;

        public IOperation CurrentOperation
        {
            get => _currentOperation;
            set => SetCurrentOperation(value);
        }

        private double[] _currentOperands;

        private void SetCurrentOperation(IOperation op)
        {
            try
            {
                _currentOperands = new double[op.NumberOperands];
                _currentOperands[0] = double.Parse(Input);
                Input += $" {op.Name} ";

                SetProperty(ref _currentOperation, op, nameof(CurrentOperation));
            }
            catch (FormatException ex)
            {
                Status = ex.Message;
            }
        }
        public ObservableCollection<IOperation> CalcAddInOperators { get; } = new ObservableCollection<IOperation>();
    }
}
