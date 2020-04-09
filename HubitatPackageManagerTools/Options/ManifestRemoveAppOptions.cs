using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-remove-app", HelpText = "Remove an app in a manifest.")]
    internal class ManifestRemoveAppOptions
    {
        [Value(0, HelpText = "The local path to the package manifest JSON.", MetaName = "manifestFile")]
        public string ManifestFile { get; set; }
        [Option(SetName = "matcher", HelpText = "The name of the app.")]
        public string Name { get; set; }
        [Option(SetName = "matcher", HelpText = "The id of the app.")]
        public string Id { get; set; }
    }
}
