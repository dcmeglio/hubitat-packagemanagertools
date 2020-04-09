using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HubitatPackageManagerTools.Executors
{
    internal static class RepositoryCreateExecutor
    {
        public static int Execute(RepositoryCreateOptions options)
        {
            var newRepositoryContents = new JObject(new JProperty("author", options.Author));
            if (options.GithubUrl != null)
                newRepositoryContents.Add("gitHubUrl", options.GithubUrl);
            if (options.PaypalUrl != null)
                newRepositoryContents.Add("payPalUrl", options.PaypalUrl);
            File.WriteAllText(options.RepositoryFile, newRepositoryContents.ToString());
            return 0;
        }
    }
}
