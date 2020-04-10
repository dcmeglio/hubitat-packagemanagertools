using HubitatPackageManagerTools.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestExecutorBase
    {
        protected JObject OpenExistingManifest(ManifestOptionsBase options)
        {
            using var file = File.OpenText(options.ManifestFile);
            return (JObject)JToken.ReadFrom(new JsonTextReader(file));
        }

        protected void SaveManifest(ManifestOptionsBase options, JToken contents)
        {
            File.WriteAllText(options.ManifestFile, contents.ToString());
        }
    }
}
