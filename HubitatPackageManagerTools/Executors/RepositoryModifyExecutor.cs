using HubitatPackageManagerTools.Extensions;
using HubitatPackageManagerTools.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace HubitatPackageManagerTools.Executors
{
    internal static class RepositoryModifyExecutor
    {
        public static int Execute(RepositoryModifyOptions options)
        {
            JObject repositoryContents = null;
            using (var file = File.OpenText(options.RepositoryFile))
            {
                repositoryContents = (JObject)JToken.ReadFrom(new JsonTextReader(file));
                repositoryContents["author"] = options.Author;
                if (options.GithubUrl.IsSpecified())
                    repositoryContents["gitHubUrl"] = options.GithubUrl;
                 else if (options.GithubUrl.IsNullValue())
                    repositoryContents.Remove("gitHubUrl");
                if (options.PaypalUrl.IsSpecified())
                    repositoryContents["payPalUrl"] = options.PaypalUrl;
                else if (options.PaypalUrl.IsNullValue())
                   repositoryContents.Remove("payPalUrl");

            }
            File.WriteAllText(options.RepositoryFile, repositoryContents.ToString());
            return 0;
        }
    }
}
