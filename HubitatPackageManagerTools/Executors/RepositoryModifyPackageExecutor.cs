using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryModifyPackageExecutor : RepositoryExecutorBase
    {
        internal int Execute(RepositoryModifyPackageOptions options)
        {
            JObject repositoryContents = OpenExistingRepository(options);

            JArray packages = new JArray();
            if (repositoryContents["packages"] == null)
                return -1;

            packages = repositoryContents["packages"] as JArray;

            var package = packages.FirstOrDefault(p => p["location"]?.ToString() == options.Manifest);

            if (package != null)
            {
                SetNonNullPropertyIfSpecified(package, "category", options.Category);
                
                if (!string.IsNullOrEmpty(options.Name))
                    package["name"] = options.Name;
                else
                {
                    var manifestContents = DownloadJsonFile(options.Manifest);
                    if (manifestContents != null)
                        package["name"] = manifestContents["packageName"].ToString();
                }
                SetNonNullPropertyIfSpecified(package, "description", options.Description);
            }

            SaveRepository(options, repositoryContents);
            return 0;
        }
    }
}
