using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("repository-create", HelpText = "Create a new repository.")]
    internal class RepositoryCreateOptions
    {
        [Value(0, HelpText = "The local path to the repository JSON.", MetaName = "repositoryFile")]
        public string RepositoryFile { get; set; }
        [Option(HelpText = "The author of the repository.", Required = true)]
        public string Author { get; set; }
        [Option(HelpText = "The GitHub URL of the repository.")]
        public string GithubUrl { get; set; }
        [Option(HelpText = "The PayPal URL of the repository.")]
        public string PaypalUrl { get; set; }
    }
}
