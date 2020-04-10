using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-remove-app", HelpText = "Remove an app in a manifest.")]
    internal class ManifestRemoveAppOptions : ManifestOptionsBase
    {
        [Option(SetName = "matcher", HelpText = "The name of the app.")]
        public string Name { get; set; }
        [Option(SetName = "matcher", HelpText = "The id of the app.")]
        public string Id { get; set; }
    }
}
