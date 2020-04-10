using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryRemovePackageExecutor : RepositoryExecutorBase
    {
        internal int Execute(RepositoryRemovePackageOptions options)
        {
            JObject repositoryContents = OpenExistingRepository(options);

            JArray packages = repositoryContents["packages"] as JArray;
            if (packages == null)
                return -1;

            var package = packages.FirstOrDefault(p => p["location"]?.ToString() == options.Manifest);

            if (package != null)
                packages.Remove(package);

            SaveRepository(options, repositoryContents);
            return 0;
        }
    }
}
