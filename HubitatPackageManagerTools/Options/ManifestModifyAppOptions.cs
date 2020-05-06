using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

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
        [Option(HelpText = "The URL of the app's beta Groovy file.")]
        public string BetaLocation { get; set; }
        [Option(HelpText = "The version of the app.", Group = "modify")]
        public string Version { get; set; }
        [Option(HelpText = "The beta version of the app.")]
        public string BetaVersion { get; set; }
        [Option(HelpText = "Whether or not the app is required.", Group = "modify", Default = null)]
        public bool? Required { get; set; }
        [Option(HelpText = "Whether or not the app uses OAuth.", Group = "modify", Default = null)]
        public bool? Oauth { get; set; }
        [Option(HelpText = "A list of alternate names for an app in the format namespace:name", Group = "modify" )]
        public IEnumerable<string> AlternateNames { get; set; }

        [Usage(ApplicationAlias = "hpm")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Release a new version by name", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestModifyAppOptions
                {
                    ManifestFile = "packageManifest.json",
                    Version = "1.2",
                    Name="My App"
                });
                yield return new Example("Release a new version by id", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestModifyAppOptions
                {
                    ManifestFile = "packageManifest.json",
                    Version = "1.2",
                    Id = "13ded13f-8ab5-42e7-9b80-31159f62ecfa"
                });
            }
        }
    }
}
