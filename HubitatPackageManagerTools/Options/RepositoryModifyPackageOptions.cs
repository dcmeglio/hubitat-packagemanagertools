using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    [Verb("repository-modify-package", HelpText = "Modify a package in a repository.")]
    internal class RepositoryModifyPackageOptions
    {
        [Value(0, HelpText = "The local path to the repository JSON.", MetaName = "repositoryFile")]
        public string RepositoryFile { get; set; }
        [Option(HelpText = "The URL of the package manifest JSON.", Required = true)]
        public string Manifest { get; set; }
        [Option(HelpText = "The name of the package. If not specified it is read from the manifest.", Group = "modify")]
        public string Name { get; set; }
        [Option(HelpText = "The category of the package.", Group = "modify")]
        public string Category { get; set; }
        [Option(HelpText = "The description of the package.", Group = "modify")]
        public string Description { get; set; }
    }
}
