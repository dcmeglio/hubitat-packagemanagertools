using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-convert", HelpText = "Convert a SmartThings Community Installer manifest to a Hubitat Package Manager manifest.")]
    internal class ManifestConvertOptions : ManifestOptionsBase
    {
        [Value(0, HelpText = "The local path to the SmartThings installer manifest JSON.", MetaName = "stInstallerFile", Required = true)]
        public string SmartThingsFile { get; set; }
    }
}
