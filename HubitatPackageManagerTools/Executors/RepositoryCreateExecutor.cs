using HubitatPackageManagerTools.Extensions;
using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System.IO;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryCreateExecutor : RepositoryExecutorBase
    {
        public int Execute(RepositoryCreateOptions options)
        {
            var newRepositoryContents = new JObject
            {
                ["author"] = options.Author
            };
            if (!string.IsNullOrEmpty(options.GithubUrl))
                newRepositoryContents["gitHubUrl"] = options.GithubUrl;
            if (!string.IsNullOrEmpty(options.PaypalUrl))
                newRepositoryContents["payPalUrl"] = options.PaypalUrl;

            SaveRepository(options, newRepositoryContents);
            
            return 0;
        }
    }
}
