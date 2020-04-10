using HubitatPackageManagerTools.Extensions;
using HubitatPackageManagerTools.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace HubitatPackageManagerTools.Executors
{
    internal static class ManifestModifyAppExecutor
    {
        public static int Execute(ManifestModifyAppOptions options)
        {
            JObject manifestContents = null;
            using (var file = File.OpenText(options.ManifestFile))
            {
                manifestContents = (JObject)JToken.ReadFrom(new JsonTextReader(file));
                JArray apps = new JArray();
                if (manifestContents["apps"] == null)
                    return 0;

                apps = manifestContents["apps"] as JArray;

                JObject app = null;
                if (!string.IsNullOrEmpty(options.Name))
                    app = apps.FirstOrDefault(p => p["name"]?.ToString() == options.Name) as JObject;
                else
                    app = apps.FirstOrDefault(p => p["id"]?.ToString() == options.Id) as JObject;

                if (app != null)
                {
                    if (options.Version.IsSpecified())
                        app["version"] = options.Version;
                    else if (options.Version.IsNullValue())
                        app.Remove("version");

                    if (options.Required == true)
                        app["required"] = true;
                    else if (options.Required == false)
                        app["required"] = false;

                    if (options.Oauth == true)
                        app["oauth"] = true;
                    else if (options.Oauth == false)
                        app["oauth"] = false;

                    if (!string.IsNullOrEmpty(options.Name))
                        app["name"] = options.Name;

                    if (!string.IsNullOrEmpty(options.Namespace))
                        app["namespace"] = options.Namespace;

                    if (!string.IsNullOrEmpty(options.Location))
                        app["location"] = options.Location;

                }
            }
            File.WriteAllText(options.ManifestFile, manifestContents.ToString());
            return 0;
        }
    }
}
