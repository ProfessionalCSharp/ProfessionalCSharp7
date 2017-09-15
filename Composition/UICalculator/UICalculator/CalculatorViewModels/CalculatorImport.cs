using System;
using System.Composition;

namespace Wrox.ProCSharp.Composition
{
    public class CalculatorImport
    {
        public event EventHandler<ImportEventArgs> ImportsSatisfied;

        [Import]
        public Lazy<ICalculator> Calculator { get; set; }

        [OnImportsSatisfied]
        public void OnImportsSatisfied() =>
            ImportsSatisfied?.Invoke(this, new ImportEventArgs { StatusMessage = "ICalculator import successful" });
    }
}
