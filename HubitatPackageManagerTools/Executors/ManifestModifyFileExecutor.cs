using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestModifyFileExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestModifyFileOptions options, Settings settings)
        {
            JObject manifestContents = OpenExistingManifest(options);

            JArray files = manifestContents["files"] as JArray;
            if (files == null)
                throw new ApplicationException("Package is missing a files element.");

            JObject file = null;
            if (!string.IsNullOrEmpty(options.Name))
                file = files.FirstOrDefault(p => p["name"]?.ToString() == options.Name) as JObject;
            else
                file = files.FirstOrDefault(p => p["id"]?.ToString() == options.Id) as JObject;

            if (file != null)
            {

                    var fileContents = DownloadFile(options.Location);

                    if (fileContents != null)
                    {
                        if (!IsFilePlaintext(fileContents))
                            throw new ApplicationException($"The file manager file {options.Location} is not a plaintext file.");
                }
                else
                    throw new ApplicationException($"The file manager file {options.Location} either was not found or is not valid.");
            }
            else
                throw new ApplicationException($"The file was not found in the manifest.");

            SaveManifest(options, manifestContents);
            return 0;
        }
    }
}
