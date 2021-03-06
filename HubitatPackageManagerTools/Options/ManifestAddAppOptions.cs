﻿using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace HubitatPackageManagerTools.Options
{
    [Verb("manifest-add-app", HelpText = "Add an app to a manifest.")]
    internal class ManifestAddAppOptions : ManifestOptionsBase
    {
        [Option(HelpText = "The URL of the app's Groovy file.", Required = true)]
        public string Location { get; set; }
        [Option(HelpText = "The URL of the app's beta Groovy file.")]
        public string BetaLocation { get; set; }
        [Option(HelpText = "The version of the app.")]
        public string Version { get; set; }
        [Option(HelpText = "The beta version of the app.")]
        public string BetaVersion { get; set; }
        [Option(HelpText = "Whether or not the app is required.")]
        public bool? Required { get; set; }
        [Option(HelpText = "Whether or not the app uses OAuth.")]
        public bool? Oauth { get; set; }

        [Option(HelpText = "A list of alternate names for an app in the format namespace:name.")]
        public IEnumerable<string> AlternateNames { get; set; }
        [Option(HelpText="Whether this is the primary app that should be automatically launched after the package is installed.")]
        public bool? Primary { get; set; }

        [Usage(ApplicationAlias = "hpm")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Add an app", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestAddAppOptions
                {
                    ManifestFile = "packageManifest.json",
                    Location = "https://raw.githubusercontent.com/someuser/hubitat-app/master/app.groovy"
                });
                yield return new Example("Add an app with version", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new ManifestAddAppOptions
                {
                    ManifestFile = "packageManifest.json",
                    Location = "https://raw.githubusercontent.com/someuser/hubitat-app/master/app.groovy",
                    Version = "1.0"
                });
            }
        }
    }
}
