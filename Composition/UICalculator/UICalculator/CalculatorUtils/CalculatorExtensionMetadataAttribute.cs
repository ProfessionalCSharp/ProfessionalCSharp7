using System;
using System.Composition;

namespace Wrox.ProCSharp.Composition
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class CalculatorExtensionMetadataAttribute : Attribute
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }
    }
}
