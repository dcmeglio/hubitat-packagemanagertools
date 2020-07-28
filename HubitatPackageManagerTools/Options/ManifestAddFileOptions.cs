using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-add-file", HelpText = "Add a file manager file to a manifest.")]
    internal class ManifestAddFileOptions : ManifestOptionsBase
    {
        [Option(HelpText = "The URL of the file.", Required = true)]
        public string Location { get; set; }

        
        [Option(HelpText="The name of the file to be installed in file manager.")]
        public string Name { get; set; }

        [Usage(ApplicationAlias = "hpm")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Add a file", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestAddFileOptions
                {
                    ManifestFile = "packageManifest.json",
                    Location = "https://raw.githubusercontent.com/someuser/hubitat-app/file.js",
                    Name = "myfile.js"
                });
            }
        }
    }
}
