using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestAddDriverExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestAddDriverOptions options, Settings settings)
        {
            JObject manifestContents = OpenExistingManifest(options);
            JArray drivers = EnsureArrayExists(manifestContents, "drivers");
            string groovyFile = DownloadGroovyFile(options.Location);
            string name;
            string @namespace = null;
            if (groovyFile != null)
                (name, @namespace) = GetNameAndNamespace(groovyFile);
            else
                throw new ApplicationException($"The driver Groovy file {options.Location} either was not found or is not valid.");

            if (name == null || @namespace == null)
                throw new ApplicationException($"The driver Groovy file {options.Location} could not be parsed to determine the name and namespace. Please report this as a bug.");

            var driver = JObject.FromObject(new
            {
                id = Guid.NewGuid().ToString(),
                name = @name,
                @namespace = @namespace,
                location = options.Location,
                required = options.Required ?? false
            });
            SetNonNullPropertyIfSpecified(driver, "version", options.Version);
            SetNonNullPropertyIfSpecified(driver, "betaVersion", options.BetaVersion);
            SetNonNullPropertyIfSpecified(driver, "betaLocation", options.BetaLocation);
            var alternateNames = BuildAlternateNames(options.AlternateNames);
            if (alternateNames != null)
                driver.Add("alternateNames", alternateNames);

            drivers.Add(driver);

            SaveManifest(options, manifestContents);
            return 0;
        }
    }
}
