using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-modify-driver", HelpText = "Modify a driver in a manifest.")]
    internal class ManifestModifyDriverOptions : ManifestOptionsBase
    {
        [Option(SetName = "matcher", HelpText = "The name of the driver.")]
        public string Name { get; set; }
        [Option(SetName = "matcher", HelpText = "The id of the driver.")]
        public string Id { get; set; }
        [Option(HelpText = "The URL of the driver's Groovy file.", Group = "modify")]
        public string Location { get; set; }
        [Option(HelpText = "The version of the driver.", Group = "modify")]
        public string Version { get; set; }
        [Option(HelpText = "Whether or not the driver is required.", Group = "modify")]
        public bool Required { get; set; }
    }
}
