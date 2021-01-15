using System;

namespace GldKiddiesWorld.Models
{
    internal class SqlDefaultValueAttribute : Attribute
    {
        public string DefaultValue { get; set; }
    }
}