using HubitatPackageManagerTools.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryAddPackageExecutor : RepositoryExecutorBase
    {
        public int Execute(RepositoryAddPackageOptions options)
        {
            JObject repositoryContents = OpenExistingRepository(options);

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


            SaveRepository(options, repositoryContents);
            return 0;
        }
    }
}
