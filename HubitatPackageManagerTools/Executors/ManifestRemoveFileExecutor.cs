using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestRemoveFileExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestRemoveFileOptions options, Settings settings)
        {
            JObject manifestContents = OpenExistingManifest(options);

            JArray files = manifestContents["files"] as JArray;
            if (files == null)
                throw new ApplicationException("Package is missing a files element.");

            JToken file = null;
            if (!string.IsNullOrEmpty(options.Name))
                file = files.FirstOrDefault(p => p["name"]?.ToString() == options.Name);
            else
                file = files.FirstOrDefault(p => p["id"]?.ToString() == options.Id);

            if (file != null)
                files.Remove(file);

            SaveManifest(options, manifestContents);
            return 0;
        }
    }
}
