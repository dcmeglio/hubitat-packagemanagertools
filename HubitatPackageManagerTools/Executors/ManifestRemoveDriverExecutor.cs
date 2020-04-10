using HubitatPackageManagerTools.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HubitatPackageManagerTools.Executors
{
    internal static class ManifestRemoveDriverExecutor
    {
        public static int Execute(ManifestRemoveDriverOptions options)
        {
            JObject manifestContents = null;
            using (var file = File.OpenText(options.ManifestFile))
            {
                manifestContents = (JObject)JToken.ReadFrom(new JsonTextReader(file));
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
            }
            File.WriteAllText(options.ManifestFile, manifestContents.ToString());
            return 0;
        }
    }
}
