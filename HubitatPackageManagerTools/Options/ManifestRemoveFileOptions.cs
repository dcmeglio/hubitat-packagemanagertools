using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-remove-file", HelpText = "Remove a file manager file in a manifest.")]
    internal class ManifestRemoveFileOptions : ManifestOptionsBase
    {
        [Option(SetName = "matcher", HelpText = "The name of the file.")]
        public string Name { get; set; }
        [Option(SetName = "matcher", HelpText = "The id of the file.")]
        public string Id { get; set; }

        [Usage(ApplicationAlias = "hpm")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Remove a file by name", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestRemoveFileOptions
                {
                    ManifestFile = "packageManifest.json",
                    Name = "myscript.js"
                });
                yield return new Example("Remove a file by id", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestRemoveFileOptions
                {
                    ManifestFile = "packageManifest.json",
                    Id = "da254635-819c-4a9e-949c-2b1812d2c310"
                });
            }
        }
    }
}
