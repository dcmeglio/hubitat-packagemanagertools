using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace HubitatPackageManagerTools.Options
{
    [Verb("repository-add-package", HelpText = "Add a package to a repository repository.")]
    internal class RepositoryAddPackageOptions : RepositoryOptionsBase
    {
        [Option(HelpText = "The URL of the package manifest JSON.", Required = true)]
        public string Manifest { get; set; }
        [Option(HelpText = "The name of the package. If not specified it will be read from the manifest.")]
        public string Name { get; set; }
        [Option(HelpText = "The category of the package.", Required = true)]
        public string Category { get; set; }
        [Option(HelpText = "The description of the package.", Required = true)]
        public string Description { get; set; }
        [Option(HelpText = "Whether or not this package supports Z-Wave devices.")]
        public bool? ZWave { get; set; }
        [Option(HelpText = "Whether or not this package supports Zigbee devices.")]
        public bool? Zigbee { get; set; }
        [Option(HelpText = "Whether or not this package requires LAN connectivity.")]
        public bool? LAN { get; set; }
        [Option(HelpText = "Whether or not this package requires cloud/internet connectivity.")]
        public bool? Cloud { get; set; }
        [Usage(ApplicationAlias = "hpm")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Add a package to the repository", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new RepositoryAddPackageOptions
                {
                    RepositoryFile = "repository.json",
                    Manifest = "https://raw.githubusercontent.com/someuser/hubitat-project/master/packageManifest.json",
                    Category = "Integrations",
                    Description = "A new integration",
                    Name = "My App"
                });
            }
        }
    }
}
