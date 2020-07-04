using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace HubitatPackageManagerTools.Options
{
    [Verb("repository-remove-package", HelpText = "Remove a package in a repository.")]
    internal class RepositoryRemovePackageOptions : RepositoryOptionsBase
    {
        [Option(SetName = "matcher", HelpText = "The URL of the package manifest JSON.")]
        public string Manifest { get; set; }
        [Option(SetName = "matcher", HelpText = "The ID of the package.")]
        public string Id { get; set; }

        [Usage(ApplicationAlias = "hpm")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Remove a package from the repository", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new RepositoryRemovePackageOptions
                {
                    RepositoryFile = "repository.json",
                    Manifest = "https://raw.githubusercontent.com/someuser/hubitat-project/master/packageManifest.json"
                });
            }
        }
    }
}
