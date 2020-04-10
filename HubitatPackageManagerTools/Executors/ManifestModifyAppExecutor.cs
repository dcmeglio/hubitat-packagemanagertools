using HubitatPackageManagerTools.Extensions;
using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestModifyAppExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestModifyAppOptions options)
        {
            JObject manifestContents = OpenExistingManifest(options);

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

                SetNonNullPropertyIfSpecified(app, "name", options.Name);
                SetNonNullPropertyIfSpecified(app, "namespace", options.Namespace);
                SetNonNullPropertyIfSpecified(app, "location", options.Location);
            }

            SaveManifest(options, manifestContents);
            return 0;
        }
    }
}
