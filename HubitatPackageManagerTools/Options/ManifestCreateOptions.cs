using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-create", HelpText = "Create a new manifest.")]
    internal class ManifestCreateOptions : ManifestOptionsBase
    {
        [Option(HelpText ="The name of the package.", Required = true)]
        public string Name { get; set; }
        [Option(HelpText = "The author of the package.", Required = true)]
        public string Author { get; set; }
        [Option(HelpText = "The version of the package.")]
        public string Version { get; set; }
        [Option(HelpText = "The minimum Hubitat firmware supported by the package.")]
        public string HEVersion { get; set; }
        [Option(HelpText = "The URL of the package's license.")]
        public string License { get; set; }
        [Option(HelpText = "The release date of the package in YYYY-MM-DD format. If not specified today's date is used.")]
        public string DateReleased { get; set; }

        [Usage(ApplicationAlias = "hpm")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Create a package with version", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestCreateOptions
                {
                    ManifestFile = "packageManifest.json",
                    Name = "My Package",
                    Author = "My Name",
                    Version = "1.0"
                });
                yield return new Example("Create a package with no version", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestCreateOptions
                {
                    ManifestFile = "packageManifest.json",
                    Name = "My Package",
                    Author = "My Name"
                });
                yield return new Example("Create a package with license", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestCreateOptions
                {
                    ManifestFile = "packageManifest.json",
                    Name = "My Package",
                    Author = "My Name",
                    License = "https://opensource.org/licenses/LGPL-2.0"
                });
            }
        }
    }
}