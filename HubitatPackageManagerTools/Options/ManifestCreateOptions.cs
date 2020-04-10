using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-create", HelpText = "Create a new manifest.")]
    internal class ManifestCreateOptions : ManifestOptionsBase
    {
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
        [Option(HelpText = "The release date of the package in YYYY-MM-DD format. If not specified today's date is used.")]
        public string DateReleased { get; set; }
    }
}