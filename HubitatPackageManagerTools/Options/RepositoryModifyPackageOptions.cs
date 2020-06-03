using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace HubitatPackageManagerTools.Options
{
    [Verb("repository-modify-package", HelpText = "Modify a package in a repository.")]
    internal class RepositoryModifyPackageOptions : RepositoryOptionsBase
    {
        [Option(HelpText = "The URL of the package manifest JSON.", Required = true)]
        public string Manifest { get; set; }
        [Option(HelpText = "The name of the package. If not specified it is read from the manifest.", Group = "modify")]
        public string Name { get; set; }
        [Option(HelpText = "The category of the package.", Group = "modify")]
        public string Category { get; set; }
        [Option(HelpText = "The description of the package.", Group = "modify")]
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
                yield return new Example("Modify a package's category in a repository", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new RepositoryModifyPackageOptions
                {
                    RepositoryFile = "repository.json",
                    Manifest = "https://raw.githubusercontent.com/someuser/hubitat-project/master/packageManifest.json",
                    Category = "Convenience"
                });
            }
        }
    }
}
