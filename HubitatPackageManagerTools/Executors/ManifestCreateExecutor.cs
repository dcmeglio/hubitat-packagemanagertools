using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestCreateExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestCreateOptions options)
        {
            var newManifestContents = new JObject
            {
                ["name"] = options.Name,
                ["author"] = options.Author
            };
            if (!string.IsNullOrEmpty(options.Version))
                newManifestContents["version"] = options.Version;
            if (!string.IsNullOrEmpty(options.HEVersion))
                newManifestContents["minimumHEVersion"] = options.HEVersion;
            if (!string.IsNullOrEmpty(options.License))
                newManifestContents["licenseFile"] = options.License;
            if (!string.IsNullOrEmpty(options.DateReleased))
                newManifestContents["dateReleased"] = options.DateReleased;
            else
                newManifestContents["dateReleased"] = DateTime.Now.ToString("yyyy-MM-dd");

            newManifestContents["packageId"] = Guid.NewGuid().ToString();

            SaveManifest(options, newManifestContents);
            return 0;
        }
    }
}
