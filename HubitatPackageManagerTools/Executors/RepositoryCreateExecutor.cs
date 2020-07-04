using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryCreateExecutor : RepositoryExecutorBase
    {
        public int Execute(RepositoryCreateOptions options, Settings settings)
        {
            var newRepositoryContents = new JObject
            {
                ["author"] = options.Author
            };
            SetNonNullPropertyIfSpecified(newRepositoryContents, "gitHubUrl", options.GithubUrl);
            SetNonNullPropertyIfSpecified(newRepositoryContents, "payPalUrl", options.PaypalUrl);

            SaveRepository(options, newRepositoryContents);
            
            return 0;
        }
    }
}
