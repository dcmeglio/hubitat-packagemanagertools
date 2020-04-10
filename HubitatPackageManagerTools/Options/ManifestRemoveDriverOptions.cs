using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-remove-driver", HelpText = "Remove a driver in a manifest.")]
    internal class ManifestRemoveDriverOptions : ManifestOptionsBase
    {
        [Option(SetName = "matcher", HelpText = "The name of the driver.")]
        public string Name { get; set; }
        [Option(SetName = "matcher", HelpText = "The id of the driver.")]
        public string Id { get; set; }

        [Usage(ApplicationAlias = "hpm")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Remove a driver by name", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestRemoveDriverOptions
                {
                    ManifestFile = "packageManifest.json",
                    Name="My App"
                });
                yield return new Example("Remove a driver by id", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestRemoveDriverOptions
                {
                    ManifestFile = "packageManifest.json",
                    Id= "da254635-819c-4a9e-949c-2b1812d2c310"
                });
            }
        }
    }
}
