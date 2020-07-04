using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestModifyExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestModifyOptions options, Settings settings)
        {
            JObject manifestContents = OpenExistingManifest(options);

            if (!string.IsNullOrEmpty(options.Name))
                manifestContents["packageName"] = options.Name;
            if (!string.IsNullOrEmpty(options.Author))
                manifestContents["author"] = options.Author;

            SetNullableProperty(manifestContents, "version", options.Version);
            SetNullableProperty(manifestContents, "minimumHEVersion", options.HEVersion);
            SetNullableProperty(manifestContents, "licenseFile", options.License);
            SetNullableProperty(manifestContents, "releaseNotes", options.ReleaseNotes);
            SetNullableProperty(manifestContents, "documentationLink", options.DocumentationLink);
            SetNullableProperty(manifestContents, "communityLink", options.CommunityLink);

            if (!string.IsNullOrEmpty(options.DateReleased))
                manifestContents["dateReleased"] = options.DateReleased;
            else
                manifestContents["dateReleased"] = DateTime.Now.ToString("yyyy-MM-dd");

            SaveManifest(options, manifestContents);
            return 0;
        }
    }
}
