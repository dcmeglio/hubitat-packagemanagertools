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

            JArray drivers = new JArray();
            if (manifestContents["drivers"] == null)
                manifestContents.Add("drivers", drivers);
            else
                drivers = manifestContents["drivers"] as JArray;

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
