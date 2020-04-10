using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("repository-remove-package", HelpText = "Remove a package in a repository.")]
    internal class RepositoryRemovePackageOptions : RepositoryOptionsBase
    {
        [Option(HelpText = "The URL of the package manifest JSON.", Required = true)]
        public string Manifest { get; set; }
    }
}
