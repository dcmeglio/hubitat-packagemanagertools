using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("repository-add-package", HelpText = "Add a package to a repository repository.")]
    internal class RepositoryAddPackageOptions : RepositoryOptionsBase
    {
        [Option(HelpText = "The URL of the package manifest JSON.", Required = true)]
        public string Manifest { get; set; }
        [Option(HelpText = "The name of the package. If not specified it will be read from the manifest.")]
        public string Name { get; set; }
        [Option(HelpText = "The category of the package.", Required = true)]
        public string Category { get; set; }
        [Option(HelpText = "The description of the package.", Required = true)]
        public string Description { get; set; }
    }
}
