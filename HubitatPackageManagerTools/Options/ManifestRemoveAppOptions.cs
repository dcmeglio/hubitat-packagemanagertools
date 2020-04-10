using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-remove-app", HelpText = "Remove an app in a manifest.")]
    internal class ManifestRemoveAppOptions : ManifestOptionsBase
    {
        [Option(SetName = "matcher", HelpText = "The name of the app.")]
        public string Name { get; set; }
        [Option(SetName = "matcher", HelpText = "The id of the app.")]
        public string Id { get; set; }

        [Usage(ApplicationAlias = "hpm")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Remove an app by name", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestRemoveDriverOptions
                {
                    ManifestFile = "packageManifest.json",
                    Name = "My App"
                });
                yield return new Example("Remove an app by id", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestRemoveDriverOptions
                {
                    ManifestFile = "packageManifest.json",
                    Id = "da254635-819c-4a9e-949c-2b1812d2c310"
                });
            }
        }
    }
}
