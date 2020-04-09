using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-create", HelpText = "Create a new manifest.")]
    internal class ManifestCreateOptions
    {
        [Value(0, HelpText = "The local path to the package manifest JSON.", MetaName = "manifestFile")]
        public string ManifestFile { get; set; }

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
    }
}