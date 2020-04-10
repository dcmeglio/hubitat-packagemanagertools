using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-add-app", HelpText = "Add an app to a manifest.")]
    internal class ManifestAddAppOptions : ManifestOptionsBase
    {
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
