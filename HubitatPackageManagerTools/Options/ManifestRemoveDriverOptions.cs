using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-remove-driver", HelpText = "Remove a driver in a manifest.")]
    internal class ManifestRemoveDriverOptions : ManifestOptionsBase
    {
        [Option(SetName = "matcher", HelpText = "The name of the driver.")]
        public string Name { get; set; }
        [Option(SetName = "matcher", HelpText = "The id of the driver.")]
        public string Id { get; set; }
    }
}
