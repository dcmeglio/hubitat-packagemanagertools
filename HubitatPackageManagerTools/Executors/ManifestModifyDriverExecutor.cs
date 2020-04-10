using HubitatPackageManagerTools.Extensions;
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
    internal static class ManifestModifyDriverExecutor
    {
        public static int Execute(ManifestModifyDriverOptions options)
        {
            JObject manifestContents = null;
            using (var file = File.OpenText(options.ManifestFile))
            {
                manifestContents = (JObject)JToken.ReadFrom(new JsonTextReader(file));
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

                    if (!string.IsNullOrEmpty(options.Name))
                        driver["name"] = options.Name;

                    if (!string.IsNullOrEmpty(options.Namespace))
                        driver["namespace"] = options.Namespace;

                    if (!string.IsNullOrEmpty(options.Location))
                        driver["location"] = options.Location;
                }
            }
            File.WriteAllText(options.ManifestFile, manifestContents.ToString());
            return 0;
        }
    }
}
