using HubitatPackageManagerTools.Extensions;
using HubitatPackageManagerTools.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HubitatPackageManagerTools.Executors
{
    internal static class ManifestModifyExecutor
    {
        public static int Execute(ManifestModifyOptions options)
        {
            JObject manifestContents = null;
            using (var file = File.OpenText(options.ManifestFile))
            {
                manifestContents = (JObject)JToken.ReadFrom(new JsonTextReader(file));
                if (!string.IsNullOrEmpty(options.Name))
                    manifestContents["name"] = options.Name;
                if (!string.IsNullOrEmpty(options.Author))
                    manifestContents["author"] = options.Author;

                if (options.Version.IsSpecified())
                    manifestContents["version"] = options.Version;
                else if (options.Version.IsNullValue())
                    manifestContents.Remove("version");
                if (options.HEVersion.IsSpecified())
                    manifestContents["minimumHEVersion"] = options.HEVersion;
                else if (options.HEVersion.IsNullValue())
                    manifestContents.Remove("minimumHEVersion");
                if (options.License.IsSpecified())
                    manifestContents["licenseFile"] = options.License;
                else if (options.License.IsNullValue())
                    manifestContents.Remove("licenseFile");
                if (!string.IsNullOrEmpty(options.DateReleased))
                    manifestContents["dateReleased"] = options.DateReleased;
                else
                    manifestContents["dateReleased"] = DateTime.Now.ToString("yyyy-MM-dd");
                if (options.ReleaseNotes.IsSpecified())
                    manifestContents["releaseNotes"] = options.ReleaseNotes;
                else if (options.ReleaseNotes.IsNullValue())
                    manifestContents.Remove("releaseNotes");
            }
            File.WriteAllText(options.ManifestFile, manifestContents.ToString());
            return 0;
        }
    }
}
