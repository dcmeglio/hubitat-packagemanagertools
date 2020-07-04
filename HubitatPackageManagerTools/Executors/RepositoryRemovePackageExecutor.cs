using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryRemovePackageExecutor : RepositoryExecutorBase
    {
        internal int Execute(RepositoryRemovePackageOptions options, Settings settings)
        {
            JObject repositoryContents = OpenExistingRepository(options);

            JArray packages = repositoryContents["packages"] as JArray;
            if (packages == null)
                throw new ApplicationException("Repository is missing a packages element.");

            JToken package;
            if (!string.IsNullOrWhiteSpace(options.Manifest))
                package = packages.FirstOrDefault(p => p["location"]?.ToString() == options.Manifest);
            else
                package = packages.FirstOrDefault(p => p["id"]?.ToString() == options.Id);

            if (package != null)
                packages.Remove(package);
            else
                throw new ApplicationException($"The package {options.Manifest} was not found in the repository.");

            SaveRepository(options, repositoryContents);
            return 0;
        }
    }
}
