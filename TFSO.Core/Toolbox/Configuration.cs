using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;

// ReSharper disable JoinDeclarationAndInitializer

[assembly: InternalsVisibleTo("TFSO.Tests")]
namespace TFSO.Core.Toolbox
{
    public sealed class Configuration
    {
        private static readonly Lazy<Configuration> Lazy = new Lazy<Configuration>(() => new Configuration());
        internal IConfiguration Settings { get; set; }

        public static Configuration Instance => Lazy.Value;

        private Configuration()
        {
            string configFileName;
#if LOCAL
            configFileName = "config.local.json";
#else
            configFileName = "config.json";
#endif
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configFileName);

            Settings = builder.Build();
        }
    }
}
