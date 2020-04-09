using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("repository-remove-package", HelpText = "Remove a package in a repository.")]
    internal class RepositoryRemovePackageOptions
    {
        [Value(0, HelpText = "The local path to the repository JSON.", MetaName = "repositoryFile")]
        public string RepositoryFile { get; set; }
        [Option(HelpText = "The URL of the package manifest JSON.", Required = true)]
        public string Manifest { get; set; }
    }
}
