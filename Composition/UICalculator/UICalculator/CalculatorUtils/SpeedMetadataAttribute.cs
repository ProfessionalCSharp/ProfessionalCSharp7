using System;
using System.Composition;

namespace Wrox.ProCSharp.Composition
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class SpeedMetadataAttribute : Attribute
    {
        public SpeedMetadataAttribute()
        {
        }

        public Speed Speed { get; set; }
    }
}
