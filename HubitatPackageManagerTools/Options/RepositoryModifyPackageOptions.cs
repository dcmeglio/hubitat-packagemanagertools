﻿using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace HubitatPackageManagerTools.Options
{
    [Verb("repository-modify-package", HelpText = "Modify a package in a repository.")]
    internal class RepositoryModifyPackageOptions : RepositoryOptionsBase
    {
        [Option(SetName = "matcher", HelpText = "The URL of the package manifest JSON.")]
        public string Manifest { get; set; }
        [Option(SetName = "matcher", HelpText = "The ID of the package.")]
        public string Id { get; set; }
        [Option(HelpText = "The name of the package. If not specified it is read from the manifest.", Group = "modify")]
        public string Name { get; set; }
        [Option(HelpText = "The category of the package.", Group = "modify")]
        public string Category { get; set; }
        [Option(HelpText = "The description of the package.", Group = "modify")]
        public string Description { get; set; }
        [Option(HelpText = "A list of tags for the package.", Group = "modify")]
        public IEnumerable<string> Tags { get; set; }

        [Usage(ApplicationAlias = "hpm")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Modify a package's category in a repository", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new RepositoryModifyPackageOptions
                {
                    RepositoryFile = "repository.json",
                    Manifest = "https://raw.githubusercontent.com/someuser/hubitat-project/master/packageManifest.json",
                    Category = "Convenience"
                });
            }
        }
    }
}
