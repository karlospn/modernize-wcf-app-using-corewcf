using System;
using System.Runtime.InteropServices;

namespace BookingMgmt.Domain.Properties
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ComVisible(true)]
    public sealed class NugetPackageGenerationAttribute : Attribute
    {
        public NugetPackageGenerationAttribute(bool Description)
        {
            this.Description = Description;
        }

        public bool Description { get; }
    }
}
