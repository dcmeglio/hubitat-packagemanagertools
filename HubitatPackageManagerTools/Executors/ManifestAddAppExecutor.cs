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

            JArray apps = new JArray();
            if (manifestContents["apps"] == null)
                manifestContents.Add("apps", apps);
            else
                apps = manifestContents["apps"] as JArray;

            string name = options.Name;
            string @namespace = options.Namespace;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(@namespace))
            {
                var groovyFile = DownloadGroovyFile(options.Location);
                if (groovyFile != null)
                {
                    var nameMatches = nameMatcher.Match(groovyFile);
                    var namespaceMatches = namespaceMatcher.Match(groovyFile);
                    if (nameMatches?.Groups.Count > 1)
                        name = nameMatches.Groups[1].Value;
                    if (namespaceMatches?.Groups.Count > 1)
                        @namespace = namespaceMatches.Groups[1].Value;
                }
            }

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
