using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-modify-driver", HelpText = "Modify a driver in a manifest.")]
    internal class ManifestModifyDriverOptions : ManifestOptionsBase
    {
        [Option(SetName = "matcher", HelpText = "The name of the driver.")]
        public string Name { get; set; }
        [Option(SetName = "matcher", HelpText = "The id of the driver.")]
        public string Id { get; set; }
        [Option(HelpText = "The URL of the driver's Groovy file.", Group = "modify")]
        public string Location { get; set; }
        [Option(HelpText = "The version of the driver.", Group = "modify")]
        public string Version { get; set; }
        [Option(HelpText = "Whether or not the driver is required.", Group = "modify")]
        public bool? Required { get; set; }
        [Option(HelpText = "A list of alternate names for a driver in the format namespace:name", Group = "modify")]
        public IEnumerable<string> AlternateNames { get; set; }

        [Usage(ApplicationAlias = "hpm")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Release a new version by name", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestModifyDriverOptions
                {
                    ManifestFile = "packageManifest.json",
                    Version = "1.2",
                    Name = "My App"
                });
                yield return new Example("Release a new version by id", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestModifyDriverOptions
                {
                    ManifestFile = "packageManifest.json",
                    Version = "1.2",
                    Id = "13ded13f-8ab5-42e7-9b80-31159f62ecfa"
                });
            }
        }
    }
}
