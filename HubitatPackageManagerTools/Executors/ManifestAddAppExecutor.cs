using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestAddAppExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestAddAppOptions options)
        {
            JObject manifestContents = OpenExistingManifest(options);

            JArray apps = EnsureArrayExists(manifestContents, "apps");

            string name = null;
            string @namespace = null;

            var groovyFile = DownloadGroovyFile(options.Location);
            if (groovyFile != null)
                (name, @namespace) = GetNameAndNamespace(groovyFile);

            var app = JObject.FromObject(new
            {
                id = Guid.NewGuid().ToString(),
                name = @name,
                @namespace = @namespace,
                location = options.Location,
                required = options.Required,
                oauth = options.Oauth
            });
            SetNonNullPropertyIfSpecified(app, "version", options.Version);

            apps.Add(app);

            SaveManifest(options, manifestContents);
            return 0;
        }
    }
}
