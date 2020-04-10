﻿using HubitatPackageManagerTools.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Net;

namespace HubitatPackageManagerTools.Executors
{
    public static class RepositoryModifyPackageExecutor
    {
        internal static int Execute(RepositoryModifyPackageOptions options)
        {
            JObject repositoryContents = null;
            using (var file = File.OpenText(options.RepositoryFile))
            {
                repositoryContents = (JObject)JToken.ReadFrom(new JsonTextReader(file));
                JArray packages = new JArray();
                if (repositoryContents["packages"] == null)
                    return 0;

                packages = repositoryContents["packages"] as JArray;

                var package = packages.FirstOrDefault(p => p["location"]?.ToString() == options.Manifest);

                if (package != null)
                {
                    if (!string.IsNullOrEmpty(options.Category))
                        package["category"] = options.Category;
                    if (!string.IsNullOrEmpty(options.Name))
                        package["name"] = options.Name;
                    else
                    {
                        using (WebClient wc = new WebClient())
                        {
                            StringReader sr = new StringReader(wc.DownloadString(options.Manifest));
                            var manifestContents = (JObject)JToken.ReadFrom(new JsonTextReader(sr));

                            package["name"] = manifestContents["packageName"].ToString();
                        }
                    }
                    if (!string.IsNullOrEmpty(options.Description))
                        package["description"] = options.Description;
                }
            }
            File.WriteAllText(options.RepositoryFile, repositoryContents.ToString());
            return 0;
        }
    }
}