using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-modify", HelpText = "Modify an existing manifest.")]
    internal class ManifestModifyOptions : ManifestOptionsBase
    {
        [Option(HelpText = "The name of the package.", Group = "modify")]
        public string Name { get; set; }
        [Option(HelpText = "The author of the package.", Group = "modify")]
        public string Author { get; set; }
        [Option(HelpText = "The version of the package.", Group = "modify")]
        public string Version { get; set; }
        [Option(HelpText = "The minimum Hubitat firmware supported by the package.", Group = "modify")]
        public string HEVersion { get; set; }
        [Option(HelpText = "The URL of the package's license.", Group = "modify")]
        public string License { get; set; }
        [Option(HelpText = "The release notes of the package version.", Group = "modify")]
        public string ReleaseNotes { get; set; }
        [Option(HelpText = "The release date of the package in YYYY-MM-DD format. If not specified today's date is used.", Group = "modify")]
        public string DateReleased { get; set; }
    }
}
