using HubitatPackageManagerTools.Extensions;
using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System.IO;

namespace HubitatPackageManagerTools.Executors
{
    internal static class RepositoryCreateExecutor
    {
        public static int Execute(RepositoryCreateOptions options)
        {
            var newRepositoryContents = new JObject
            {
                ["author"] = options.Author
            };
            if (options.GithubUrl.IsSpecified())
                newRepositoryContents["gitHubUrl"] = options.GithubUrl;
            if (options.PaypalUrl.IsSpecified())
                newRepositoryContents["payPalUrl"] = options.PaypalUrl;
            
            File.WriteAllText(options.RepositoryFile, newRepositoryContents.ToString());
            return 0;
        }
    }
}
