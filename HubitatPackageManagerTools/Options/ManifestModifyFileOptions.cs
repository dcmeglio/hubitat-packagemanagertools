using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-modify-file", HelpText = "Modify a file manager file in a manifest.")]
    internal class ManifestModifyFileOptions : ManifestOptionsBase
    {
        [Option(SetName = "matcher", HelpText = "The name of the file.")]
        public string Name { get; set; }
        [Option(SetName = "matcher", HelpText = "The id of the file.")]
        public string Id { get; set; }
        [Option(HelpText = "The URL of the file.", Required = true)]
        public string Location { get; set; }
        

        [Usage(ApplicationAlias = "hpm")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Release a new version by name", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestModifyFileOptions
                {
                    ManifestFile = "packageManifest.json",
                    Name="myscript.js",
                    Location = "https://raw.githubusercontent.com/someuser/hubitat-app/file.js"
                });
                yield return new Example("Release a new version by id", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestModifyFileOptions
                {
                    ManifestFile = "packageManifest.json",
                    Id = "13ded13f-8ab5-42e7-9b80-31159f62ecfa",
                    Location = "https://raw.githubusercontent.com/someuser/hubitat-app/file.js"
                });
            }
        }
    }
}
