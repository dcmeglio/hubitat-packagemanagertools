using HubitatPackageManagerTools.Options;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryModifyExecutor : RepositoryExecutorBase
    {
        public int Execute(RepositoryModifyOptions options)
        {
            var repositoryContents = OpenExistingRepository(options);

            SetNonNullPropertyIfSpecified(repositoryContents, "author", options.Author);
            SetNullableProperty(repositoryContents, "gitHubUrl", options.GithubUrl);
            SetNullableProperty(repositoryContents, "payPalUrl", options.PaypalUrl);

            SaveRepository(options, repositoryContents);
            return 0;
        }
    }
}
