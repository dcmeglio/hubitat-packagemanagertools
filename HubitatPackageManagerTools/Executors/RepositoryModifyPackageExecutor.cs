using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryModifyPackageExecutor : RepositoryExecutorBase
    {
        internal int Execute(RepositoryModifyPackageOptions options, Settings settings)
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
            {
                if (!string.IsNullOrEmpty(options.Category) && !settings.ValidateCategory(options.Category))
                    throw new ApplicationException($"Invalid category specified, {options.Category}");
                SetNonNullPropertyIfSpecified(package, "category", options.Category);
                
                if (!string.IsNullOrEmpty(options.Name))
                    package["name"] = options.Name;
                else if (!string.IsNullOrEmpty(options.Manifest))
                {
                    var manifestContents = DownloadJsonFile(options.Manifest);
                    if (manifestContents != null)
                        package["name"] = manifestContents["packageName"].ToString();
                    else
                        throw new ApplicationException($"Manifest file {options.Manifest} either does not exist or is not valid.");
                }
                SetNonNullPropertyIfSpecified(package, "description", options.Description);

                if (options.Tags?.Any() == true)
                {
                    if (!settings.ValidateTags(options.Tags))
                        throw new ApplicationException("An invalid tag was specified.");
                    package["tags"] = new JArray(options.Tags);
                }
            }
            else
                throw new ApplicationException($"The package {options.Manifest} was not found in the repository.");

            SaveRepository(options, repositoryContents);
            return 0;
        }
    }
}
