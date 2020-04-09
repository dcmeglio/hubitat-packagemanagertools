using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("repository-add-package", HelpText = "Add a package to a repository repository.")]
    internal class RepositoryAddPackageOptions
    {
        [Value(0, HelpText = "The local path to the repository JSON.", MetaName = "repositoryFile")]
        public string RepositoryFile { get; set; }

        [Option(HelpText = "The URL of the package manifest JSON.", Required = true)]
        public string Manifest { get; set; }
        [Option(HelpText = "The category of the package.", Required = true)]
        public string Category { get; set; }
        [Option(HelpText = "The description of the package.", Required = true)]
        public string Description { get; set; }
    }
}
