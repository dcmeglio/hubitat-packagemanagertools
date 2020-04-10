using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace HubitatPackageManagerTools.Options
{
    [Verb("repository-modify", HelpText = "Modify a repository.")]
    internal class RepositoryModifyOptions : RepositoryOptionsBase
    {
        [Option(HelpText = "The author of the repository.", Group = "modify")]
        public string Author { get; set; }
        [Option(HelpText = "The GitHub URL of the repository.", Group = "modify")]
        public string GithubUrl { get; set; }
        [Option(HelpText = "The PayPal URL of the repository.", Group = "modify")]
        public string PaypalUrl { get; set; }

        [Usage(ApplicationAlias = "hpm")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Modify the repository's author", new[] { UnParserSettings.WithUseEqualTokenOnly() }, new RepositoryModifyOptions
                {
                    RepositoryFile = "repository.json",
                    Author = "New Author"
                });
            }
        }
    }
}
