using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestModifyDriverExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestModifyDriverOptions options)
        {
            JObject manifestContents = OpenExistingManifest(options);

            JArray drivers = manifestContents["drivers"] as JArray;
            if (drivers == null)
                return -1;

            JObject driver = null;
            if (!string.IsNullOrEmpty(options.Name))
                driver = drivers.FirstOrDefault(p => p["name"]?.ToString() == options.Name) as JObject;
            else
                driver = drivers.FirstOrDefault(p => p["id"]?.ToString() == options.Id) as JObject;

            if (driver != null)
            {
                SetNullableProperty(driver, "version", options.Version);

                if (options.Required == true)
                    driver["required"] = true;
                else if (options.Required == false)
                    driver["required"] = false;

                SetNonNullPropertyIfSpecified(driver, "name", options.Name);
                SetNonNullPropertyIfSpecified(driver, "namespace", options.Namespace);
                SetNonNullPropertyIfSpecified(driver, "location", options.Location);
            }

            SaveManifest(options, manifestContents);
            return 0;
        }
    }
}
