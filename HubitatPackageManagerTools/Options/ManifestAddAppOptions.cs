using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-add-app", HelpText = "Add an app to a manifest.")]
    internal class ManifestAddAppOptions
    {
        [Value(0, HelpText = "The local path to the package manifest JSON.", MetaName = "manifestFile")]
        public string ManifestFile { get; set; }
        [Option(HelpText = "The name of the app.")]
        public string Name { get; set; }
        [Option(HelpText = "The namespace of the app.")]
        public string Namespace { get; set; }
        [Option(HelpText = "The URL of the app's Groovy file.", Required = true)]
        public string Location { get; set; }
        [Option(HelpText = "The version of the driver.")]
        public string Version { get; set; }
        [Option(HelpText = "Whether or not the app is required.")]
        public bool Required { get; set; }
        [Option(HelpText = "Whether or not the app uses OAuth.")]
        public bool Oauth { get; set; }
    }
}
