using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryAddPackageExecutor : RepositoryExecutorBase
    {
        public int Execute(RepositoryAddPackageOptions options, Settings settings)
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

            if (!settings.ValidateCategory(options.Category))
                throw new ApplicationException($"Invalid category specified, {options.Category}");

            var newPkg = JObject.FromObject(new
            {
                id = Guid.NewGuid().ToString(),
                name = name,
                category = options.Category,
                location = options.Manifest,
                description = options.Description,
            });
            if (options.Tags?.Any() == true)
            {
                if (!settings.ValidateTags(options.Tags))
                    throw new ApplicationException("An invalid tag was specified.");
                newPkg["tags"] = new JArray(options.Tags);
            }
            packages.Add(newPkg);
            SaveRepository(options, repositoryContents);
            return 0;
        }
    }
}
