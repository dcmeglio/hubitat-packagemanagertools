﻿using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestModifyDriverExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestModifyDriverOptions options, Settings settings)
        {
            JObject manifestContents = OpenExistingManifest(options);

            JArray drivers = manifestContents["drivers"] as JArray;
            if (drivers == null)
                throw new ApplicationException("Package is missing a drivers element.");

            JObject driver = null;
            if (!string.IsNullOrEmpty(options.Name))
                driver = drivers.FirstOrDefault(p => p["name"]?.ToString() == options.Name) as JObject;
            else
                driver = drivers.FirstOrDefault(p => p["id"]?.ToString() == options.Id) as JObject;

            if (driver != null)
            {
                SetNullableProperty(driver, "version", options.Version);
                SetNullableProperty(driver, "betaVersion", options.BetaVersion);
                SetNullableProperty(driver, "betaLocation", options.BetaLocation);

                if (options.Required == true)
                    driver["required"] = true;
                else if (options.Required == false)
                    driver["required"] = false;

                if (!string.IsNullOrEmpty(options.Location))
                {
                    var groovyFile = DownloadGroovyFile(options.Location);

                    string name;
                    string @namespace;
                    if (groovyFile != null)
                        (name, @namespace) = GetNameAndNamespace(groovyFile);
                    else
                        throw new ApplicationException($"The driver Groovy file {options.Location} either was not found or is not valid.");

                    if (name == null || @namespace == null)
                        throw new ApplicationException($"The driver Groovy file {options.Location} could not be parsed to determine the name and namespace. Please report this as a bug.");

                    SetNonNullPropertyIfSpecified(driver, "name", name);
                    SetNonNullPropertyIfSpecified(driver, "namespace", @namespace);
                    SetNonNullPropertyIfSpecified(driver, "location", options.Location);
                }
                var alternateNames = BuildAlternateNames(options.AlternateNames);
                if (alternateNames != null)
                    driver["alternateNames"] = alternateNames;
            }
            else
                throw new ApplicationException($"The driver was not found in the manifest.");

            SaveManifest(options, manifestContents);
            return 0;
        }
    }
}
