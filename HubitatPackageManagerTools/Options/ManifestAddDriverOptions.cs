using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-add-driver", HelpText = "Add a driver to a manifest.")]
    internal class ManifestAddDriverOptions : ManifestOptionsBase
    {
        [Option(HelpText = "The URL of the driver's Groovy file.", Required = true)]
        public string Location { get; set; }
        [Option(HelpText = "The version of the driver.")]
        public string Version { get; set; }
        [Option(HelpText = "Whether or not the driver is required.")]
        public bool? Required { get; set; }

        [Usage(ApplicationAlias = "hpm")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Add a driver", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestAddDriverOptions
                {
                    ManifestFile = "packageManifest.json",
                    Location = "https://raw.githubusercontent.com/someuser/hubitat-app/master/driver.groovy"
                });
                yield return new Example("Add a driver with version", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestAddDriverOptions
                {
                    ManifestFile = "packageManifest.json",
                    Location = "https://raw.githubusercontent.com/someuser/hubitat-app/master/driver.groovy",
                    Version = "1.0"
                });
            }
        }
    }
}
