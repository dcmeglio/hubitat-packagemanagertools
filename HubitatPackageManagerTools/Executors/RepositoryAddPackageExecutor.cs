using HubitatPackageManagerTools.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryAddPackageExecutor
    {
        public static int Execute(RepositoryAddPackageOptions options)
        {
            JObject repositoryContents = null;
            using (var file = File.OpenText(options.RepositoryFile))
            {
                repositoryContents = (JObject)JToken.ReadFrom(new JsonTextReader(file));
                JArray packages = new JArray();
                if (repositoryContents["packages"] == null)
                    repositoryContents.Add("packages", packages);
                else
                    packages = repositoryContents["packages"] as JArray;

                string name = options.Name;

                if (name == null)
                {
                    using (WebClient wc = new WebClient())
                    {
                        StringReader sr = new StringReader(wc.DownloadString(options.Manifest));
                        var manifestContents = (JObject)JToken.ReadFrom(new JsonTextReader(sr));

                        name = manifestContents["packageName"].ToString();
                    }
                }
                packages.Add(JObject.FromObject(new
                {
                    name = name,
                    category = options.Category,
                    location = options.Manifest,
                    description = options.Description
                }));

            }
            File.WriteAllText(options.RepositoryFile, repositoryContents.ToString());
            return 0;
        }
    }
}
