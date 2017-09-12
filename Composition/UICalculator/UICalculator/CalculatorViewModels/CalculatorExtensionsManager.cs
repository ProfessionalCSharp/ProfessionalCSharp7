using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Linq;

namespace Wrox.ProCSharp.Composition
{
    public sealed class CalculatorExtensionsManager
    {
        private CalculatorExtensionsImport _calcExtensionImport;
        public event EventHandler<ImportEventArgs> ImportsSatisfied;

        public CalculatorExtensionsManager()
        {
            _calcExtensionImport = new CalculatorExtensionsImport();
            _calcExtensionImport.ImportsSatisfied += (sender, e) =>
            {
                ImportsSatisfied?.Invoke(this, e);
            };
        }


        public void InitializeContainer(params Type[] parts)
        {
            var configuration = new ContainerConfiguration().WithParts(parts);
            using (CompositionHost host = configuration.CreateContainer())
            {
                host.SatisfyImports(_calcExtensionImport);
            }
        }

        public IEnumerable<Lazy<ICalculatorExtension, CalculatorExtensionMetadataAttribute>> GetExtensionInformation() => 
            _calcExtensionImport.CalculatorExtensions.ToArray();
    }
}
