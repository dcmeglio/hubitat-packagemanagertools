using HubitatPackageManagerTools.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    public class RepositoryRemovePackageExecutor
    {
        internal static int Execute(RepositoryRemovePackageOptions options)
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
                    packages.Remove(package);
            }
            File.WriteAllText(options.RepositoryFile, repositoryContents.ToString());
            return 0;
        }
    }
}
