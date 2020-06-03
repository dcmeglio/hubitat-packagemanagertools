using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryModifyPackageExecutor : RepositoryExecutorBase
    {
        internal int Execute(RepositoryModifyPackageOptions options)
        {
            JObject repositoryContents = OpenExistingRepository(options);

            JArray packages = repositoryContents["packages"] as JArray;
            if (packages == null)
                throw new ApplicationException("Repository is missing a packages element.");

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
                    else
                        throw new ApplicationException($"Manifest file {options.Manifest} either does not exist or is not valid.");
                }
                SetNonNullPropertyIfSpecified(package, "description", options.Description);

                if (options.ZWave == true)
                    package["zwave"] = true;
                else if (options.ZWave == false)
                    package["zwave"] = false;

                if (options.Zigbee == true)
                    package["zigbee"] = true;
                else if (options.Zigbee == false)
                    package["zigbee"] = false;

                if (options.LAN == true)
                    package["lan"] = true;
                else if (options.LAN == false)
                    package["lan"] = false;

                if (options.Cloud == true)
                    package["cloud"] = true;
                else if (options.Cloud == false)
                    package["cloud"] = false;
            }
            else
                throw new ApplicationException($"The package {options.Manifest} was not found in the repository.");

            SaveRepository(options, repositoryContents);
            return 0;
        }
    }
}
