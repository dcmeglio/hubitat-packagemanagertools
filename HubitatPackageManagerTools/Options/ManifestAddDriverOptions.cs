using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-add-driver", HelpText = "Add a driver to a manifest.")]
    internal class ManifestAddDriverOptions
    {
        [Value(0, HelpText = "The local path to the package manifest JSON.", MetaName = "manifestFile")]
        public string ManifestFile { get; set; }

        [Option(HelpText = "The name of the driver.", Required = true)]
        public string Name { get; set; }
        [Option(HelpText = "The namespace of the driver.", Required = true)]
        public string Namespace { get; set; }
        [Option(HelpText = "The URL of the driver's Groovy file.", Required = true)]
        public string Location { get; set; }
        [Option(HelpText = "The version of the driver.")]
        public string Version { get; set; }
        [Option(HelpText = "Whether or not the driver is required.")]
        public bool Required { get; set; }
    }
}
