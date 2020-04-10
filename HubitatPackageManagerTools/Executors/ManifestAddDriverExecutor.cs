using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestAddDriverExecutor : ManifestExecutorBase
    {
        static Regex matchName = new Regex("definition\\s*?\\(.*?name:\\s*\"([^\"]*)\"", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.Singleline);
        static Regex matchNamespace = new Regex("definition\\s*?\\(.*?namespace:\\s*\"([^\"]*)\"", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.Singleline);
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
                WebClient wc = new WebClient();

                var groovyFile = wc.DownloadString(options.Location).Replace("\r", "").Replace("\n", "");
                var nameMatches = matchName.Match(groovyFile);
                var namespaceMatches = matchNamespace.Match(groovyFile);
                if (nameMatches?.Groups.Count > 1)
                    name = nameMatches.Groups[1].Value;
                if (namespaceMatches?.Groups.Count > 1)
                    @namespace = namespaceMatches.Groups[1].Value;
            }


            var driver = JObject.FromObject(new
            {
                id = Guid.NewGuid().ToString(),
                name = @name,
                @namespace = @namespace,
                location = options.Location,
                required = options.Required
            });
            if (!string.IsNullOrEmpty(options.Version))
                driver["version"] = options.Version;
            drivers.Add(driver);


            SaveManifest(options, manifestContents);
            return 0;
        }
    }
}
