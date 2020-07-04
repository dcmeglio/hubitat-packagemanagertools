using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestCreateExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestCreateOptions options, Settings settings)
        {
            var newManifestContents = new JObject
            {
                ["packageName"] = options.Name,
                ["author"] = options.Author
            };
            SetNonNullPropertyIfSpecified(newManifestContents, "version", options.Version);
            SetNonNullPropertyIfSpecified(newManifestContents, "minimumHEVersion", options.HEVersion ?? "0.0");
            if (!string.IsNullOrEmpty(options.License))
            {
                if (DownloadFile(options.License) == null)
                    throw new ApplicationException($"Unable to access license file {options.License}");
            }
            SetNonNullPropertyIfSpecified(newManifestContents, "licenseFile", options.License);
            SetNonNullPropertyIfSpecified(newManifestContents, "version", options.Version);

            SetNonNullPropertyIfSpecified(newManifestContents, "documentationLink", options.DocumentationLink);
            SetNonNullPropertyIfSpecified(newManifestContents, "communityLink", options.CommunityLink);

            if (!string.IsNullOrEmpty(options.DateReleased))
                newManifestContents["dateReleased"] = options.DateReleased;
            else
                newManifestContents["dateReleased"] = DateTime.Now.ToString("yyyy-MM-dd");

            SaveManifest(options, newManifestContents);
            return 0;
        }
    }
}
