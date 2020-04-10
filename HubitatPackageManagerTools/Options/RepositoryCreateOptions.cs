using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace HubitatPackageManagerTools.Options
{
    [Verb("repository-create", HelpText = "Create a new repository.")]
    internal class RepositoryCreateOptions : RepositoryOptionsBase
    {
        [Option(HelpText = "The author of the repository.", Required = true)]
        public string Author { get; set; }
        [Option(HelpText = "The GitHub URL of the repository.")]
        public string GithubUrl { get; set; }
        [Option(HelpText = "The PayPal URL of the repository.")]
        public string PaypalUrl { get; set; }

        [Usage(ApplicationAlias = "hpm")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Create new repository", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new RepositoryCreateOptions { RepositoryFile = "repository.json", Author="Your Name" });
            }
        }
    }
}
