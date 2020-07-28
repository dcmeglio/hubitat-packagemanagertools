using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestAddFileExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestAddFileOptions options, Settings settings)
        {
            JObject manifestContents = OpenExistingManifest(options);

            JArray files = EnsureArrayExists(manifestContents, "files");
            var fileContents = DownloadFile(options.Location);


            if (fileContents != null)
            {
                if (!IsFilePlaintext(fileContents))
                    throw new ApplicationException($"The file manager file {options.Location} is not a plaintext file.");
            }
            else
                throw new ApplicationException($"The file manager file {options.Location} either was not found or is not valid.");

            
            var app = JObject.FromObject(new
            {
                id = Guid.NewGuid().ToString(),
                name = options.Name,
                location = options.Location
            });

            files.Add(app);

            SaveManifest(options, manifestContents);
            return 0;
        }
    }
}
