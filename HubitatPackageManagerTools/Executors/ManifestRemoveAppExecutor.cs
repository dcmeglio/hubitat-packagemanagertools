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
    internal static class ManifestRemoveAppExecutor
    {
        public static int Execute(ManifestRemoveAppOptions options)
        {
            JObject manifestContents = null;
            using (var file = File.OpenText(options.ManifestFile))
            {
                manifestContents = (JObject)JToken.ReadFrom(new JsonTextReader(file));
                JArray apps = new JArray();
                if (manifestContents["apps"] == null)
                    return 0;

                apps = manifestContents["apps"] as JArray;

                JToken app = null;
                if (!string.IsNullOrEmpty(options.Name))
                    app = apps.FirstOrDefault(p => p["name"]?.ToString() == options.Name);
                else
                    app = apps.FirstOrDefault(p => p["id"]?.ToString() == options.Id);

                if (app != null)
                    apps.Remove(app);
            }
            File.WriteAllText(options.ManifestFile, manifestContents.ToString());
            return 0;
        }
    }
}
