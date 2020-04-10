using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-modify-app", HelpText = "Modify an app in a manifest.")]
    internal class ManifestModifyAppOptions : ManifestOptionsBase
    {
        [Option(SetName = "matcher", HelpText = "The name of the app.")]
        public string Name { get; set; }
        [Option(SetName = "matcher", HelpText = "The id of the app.")]
        public string Id { get; set; }
        [Option(HelpText = "The URL of the app's Groovy file.", Group = "modify")]
        public string Location { get; set; }
        [Option(HelpText = "The version of the driver.", Group = "modify")]
        public string Version { get; set; }
        [Option(HelpText = "Whether or not the app is required.", Group = "modify", Default = null)]
        public bool? Required { get; set; }
        [Option(HelpText = "Whether or not the app uses OAuth.", Group = "modify", Default = null)]
        public bool? Oauth { get; set; }

    }
}
