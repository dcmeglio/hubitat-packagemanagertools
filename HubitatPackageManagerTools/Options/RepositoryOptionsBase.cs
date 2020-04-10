using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    internal class RepositoryOptionsBase
    {
        [Value(0, HelpText = "The local path to the repository JSON.", MetaName = "repositoryFile", Required = true)]
        public string RepositoryFile { get; set; }
    }
}
