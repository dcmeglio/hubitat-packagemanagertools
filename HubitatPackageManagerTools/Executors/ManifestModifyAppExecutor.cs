using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestModifyAppExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestModifyAppOptions options)
        {
            JObject manifestContents = OpenExistingManifest(options);

            JArray apps = manifestContents["apps"] as JArray;
            if (apps == null)
                throw new ApplicationException("Package is missing a apps element.");

            JObject app = null;
            if (!string.IsNullOrEmpty(options.Name))
                app = apps.FirstOrDefault(p => p["name"]?.ToString() == options.Name) as JObject;
            else
                app = apps.FirstOrDefault(p => p["id"]?.ToString() == options.Id) as JObject;

            if (app != null)
            {
                SetNullableProperty(app, "version", options.Version);
                SetNullableProperty(app, "betaVersion", options.BetaVersion);
                SetNullableProperty(app, "betaLocation", options.BetaLocation);

                if (options.Required == true)
                    app["required"] = true;
                else if (options.Required == false)
                    app["required"] = false;

                if (options.Oauth == true)
                    app["oauth"] = true;
                else if (options.Oauth == false)
                    app["oauth"] = false;

                if (!string.IsNullOrEmpty(options.Location))
                {
                    var groovyFile = DownloadGroovyFile(options.Location);

                    string name;
                    string @namespace;
                    if (groovyFile != null)
                        (name, @namespace) = GetNameAndNamespace(groovyFile);
                    else
                        throw new ApplicationException($"The app Groovy file {options.Location} either was not found or is not valid.");

                    if (name == null || @namespace == null)
                        throw new ApplicationException($"The app Groovy file {options.Location} could not be parsed to determine the name and namespace. Please report this as a bug.");

                    SetNonNullPropertyIfSpecified(app, "name", name);
                    SetNonNullPropertyIfSpecified(app, "namespace", @namespace);
                    SetNonNullPropertyIfSpecified(app, "location", options.Location);
                }
                var alternateNames = BuildAlternateNames(options.AlternateNames);
                if (alternateNames != null)
                    app["alternateNames"] = alternateNames;
            }
            else
                throw new ApplicationException($"The driver was not found in the manifest.");

            SaveManifest(options, manifestContents);
            return 0;
        }
    }
}
