using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestRemoveDriverExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestRemoveDriverOptions options)
        {
            JObject manifestContents = OpenExistingManifest(options);

            JArray drivers = new JArray();
            if (manifestContents["drivers"] == null)
                return 0;

            drivers = manifestContents["drivers"] as JArray;

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
