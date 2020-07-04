using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestRemoveDriverExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestRemoveDriverOptions options, Settings settings)
        {
            JObject manifestContents = OpenExistingManifest(options);

            JArray drivers = manifestContents["drivers"] as JArray;
            if (drivers == null)
                throw new ApplicationException("Package is missing a drivers element.");

            JToken driver = null;
            if (!string.IsNullOrEmpty(options.Name))
                driver = drivers.FirstOrDefault(p => p["name"]?.ToString() == options.Name);
            else
                driver = drivers.FirstOrDefault(p => p["id"]?.ToString() == options.Id);

            if (driver != null)
                drivers.Remove(driver);

            SaveManifest(options, manifestContents);
            return 0;
        }
    }
}
