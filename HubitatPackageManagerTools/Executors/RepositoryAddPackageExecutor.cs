using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryAddPackageExecutor : RepositoryExecutorBase
    {
        public int Execute(RepositoryAddPackageOptions options)
        {
            JObject repositoryContents = OpenExistingRepository(options);
            JArray packages = EnsureArrayExists(repositoryContents, "packages");

            if (packages == null)
                throw new ApplicationException("Repository is missing a packages element.");

            string name = options.Name;
            var manifestContents = DownloadJsonFile(options.Manifest);
            if (manifestContents == null)
                throw new ApplicationException($"Manifest file {options.Manifest} either does not exist or is not valid.");
            
            name ??= manifestContents["packageName"]?.ToString();

            if (name == null)
                throw new ApplicationException("Unable to determine package name from the manifest.");

            packages.Add(JObject.FromObject(new
            {
                id = Guid.NewGuid().ToString(),
                name = name,
                category = options.Category,
                location = options.Manifest,
                description = options.Description,
                zwave = options.ZWave ?? false,
                zigbee = options.Zigbee ?? false,
                lan = options.LAN ?? false,
                cloud = options.Cloud ?? false
            }));

            SaveRepository(options, repositoryContents);
            return 0;
        }
    }
}
