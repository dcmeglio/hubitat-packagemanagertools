using HubitatPackageManagerTools.Extensions;
using HubitatPackageManagerTools.Options;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryModifyExecutor : RepositoryExecutorBase
    {
        public int Execute(RepositoryModifyOptions options)
        {
            var repositoryContents = OpenExistingRepository(options);

            SetNonNullPropertyIfSpecified(repositoryContents, "author", options.Author);
            if (options.GithubUrl.IsSpecified())
                repositoryContents["gitHubUrl"] = options.GithubUrl;
            else if (options.GithubUrl.IsNullValue())
                repositoryContents.Remove("gitHubUrl");
            if (options.PaypalUrl.IsSpecified())
                repositoryContents["payPalUrl"] = options.PaypalUrl;
            else if (options.PaypalUrl.IsNullValue())
                repositoryContents.Remove("payPalUrl");

            SaveRepository(options, repositoryContents);
            return 0;
        }
    }
}
