using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestAddDriverExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestAddDriverOptions options)
        {
            JObject manifestContents = OpenExistingManifest(options);
            JArray drivers = EnsureArrayExists(manifestContents, "drivers");

            string name = null;
            string @namespace = null;
            
            var groovyFile = DownloadGroovyFile(options.Location);
            if (groovyFile != null)
                (name, @namespace) = GetNameAndNamespace(groovyFile);

            var driver = JObject.FromObject(new
            {
                id = Guid.NewGuid().ToString(),
                name = @name,
                @namespace = @namespace,
                location = options.Location,
                required = options.Required
            });
            SetNonNullPropertyIfSpecified(driver, "version", options.Version);
 
            drivers.Add(driver);

            SaveManifest(options, manifestContents);
            return 0;
        }
    }
}
