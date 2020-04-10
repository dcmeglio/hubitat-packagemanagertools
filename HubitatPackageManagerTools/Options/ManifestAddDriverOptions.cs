using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-add-driver", HelpText = "Add a driver to a manifest.")]
    internal class ManifestAddDriverOptions : ManifestOptionsBase
    {
        [Option(HelpText = "The URL of the driver's Groovy file.", Required = true)]
        public string Location { get; set; }
        [Option(HelpText = "The version of the driver.")]
        public string Version { get; set; }
        [Option(HelpText = "Whether or not the driver is required.")]
        public bool Required { get; set; }
    }
}
