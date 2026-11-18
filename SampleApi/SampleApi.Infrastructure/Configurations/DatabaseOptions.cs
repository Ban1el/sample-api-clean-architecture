using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApi.Infrastructure.Configurations
{
    public class DatabaseOptions
    {
        public const string SectionName = "Database";
        public string DefaultConnection { get; set; } = string.Empty;
    }
}
