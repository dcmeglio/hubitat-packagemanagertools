using HubitatPackageManagerTools.Extensions;
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

            JArray drivers = new JArray();
            if (manifestContents["drivers"] == null)
                return 0;

            drivers = manifestContents["drivers"] as JArray;

            JObject driver = null;
            if (!string.IsNullOrEmpty(options.Name))
                driver = drivers.FirstOrDefault(p => p["name"]?.ToString() == options.Name) as JObject;
            else
                driver = drivers.FirstOrDefault(p => p["id"]?.ToString() == options.Id) as JObject;

            if (driver != null)
            {
                if (options.Version.IsSpecified())
                    driver["version"] = options.Version;
                else if (options.Version.IsNullValue())
                    driver.Remove("version");

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
