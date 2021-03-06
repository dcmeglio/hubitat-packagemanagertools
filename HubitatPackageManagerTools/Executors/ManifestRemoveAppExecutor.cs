﻿using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestRemoveAppExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestRemoveAppOptions options, Settings settings)
        {
            JObject manifestContents = OpenExistingManifest(options);

            JArray apps = manifestContents["apps"] as JArray;
            if (apps == null)
                throw new ApplicationException("Package is missing a apps element.");

            JToken app = null;
            if (!string.IsNullOrEmpty(options.Name))
                app = apps.FirstOrDefault(p => p["name"]?.ToString() == options.Name);
            else
                app = apps.FirstOrDefault(p => p["id"]?.ToString() == options.Id);

            if (app != null)
                apps.Remove(app);

            SaveManifest(options, manifestContents);
            return 0;
        }
    }
}
