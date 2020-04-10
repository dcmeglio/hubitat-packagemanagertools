using HubitatPackageManagerTools.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace HubitatPackageManagerTools.Executors
{
    internal static class ManifestAddAppExecutor
    {
        static Regex matchName = new Regex("definition\\s*?\\(.*?name:\\s*\"([^\"]*)\"", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.Singleline);
        static Regex matchNamespace = new Regex("definition\\s*?\\(.*?namespace:\\s*\"([^\"]*)\"", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.Singleline);
        public static int Execute(ManifestAddAppOptions options)
        {
            JObject manifestContents = null;
            using (var file = File.OpenText(options.ManifestFile))
            {
                manifestContents = (JObject)JToken.ReadFrom(new JsonTextReader(file));
                JArray apps = new JArray();
                if (manifestContents["apps"] == null)
                    manifestContents.Add("apps", apps);
                else
                    apps = manifestContents["apps"] as JArray;

                string name = options.Name;
                string @namespace = options.Namespace;

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(@namespace))
                {
                    WebClient wc = new WebClient();

                    var groovyFile = wc.DownloadString(options.Location).Replace("\r","").Replace("\n","");
                    var nameMatches = matchName.Match(groovyFile);
                    var namespaceMatches = matchNamespace.Match(groovyFile);
                    if (nameMatches?.Groups.Count > 1)
                        name = nameMatches.Groups[1].Value;
                    if (namespaceMatches?.Groups.Count > 1)
                        @namespace = namespaceMatches.Groups[1].Value;
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
                if (!string.IsNullOrEmpty(options.Version))
                    app["version"] = options.Version;
                apps.Add(app);

            }
            File.WriteAllText(options.ManifestFile, manifestContents.ToString());
            return 0;
        }
    }
}
