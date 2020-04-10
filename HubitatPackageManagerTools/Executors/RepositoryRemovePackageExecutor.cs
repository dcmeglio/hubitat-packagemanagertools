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

            JArray packages = new JArray();
            if (repositoryContents["packages"] == null)
                return 0;

            packages = repositoryContents["packages"] as JArray;

            var package = packages.FirstOrDefault(p => p["location"]?.ToString() == options.Manifest);

            if (package != null)
                packages.Remove(package);

            SaveRepository(options, repositoryContents);
            return 0;
        }
    }
}
